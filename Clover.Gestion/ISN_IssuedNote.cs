using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ISN_IssuedNote : Form
    {
        private int? NoteID = null;
        private IssuedNote CurrentNote = null;
        private bool SafeExit = false;

        public ISN_IssuedNote(int? NoteID = null)
        {
            this.NoteID = NoteID;
            InitializeComponent();
        }

        private async void ISN_IssuedNote_Load(object sender, EventArgs e)
        {
            if (NoteID.HasValue)
            {
                try
                {
                    CurrentNote = await Task.Run(() => IssuedNote.GetNoteById(NoteID.Value));
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboNoteType.DataSource = new string[] { "A", "B", "C" };
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint ISN201
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ISN201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = "Visualizando nota: " + CurrentNote.NoteID.ToString("D8");
                txtNoteID.Text = CurrentNote.NoteID.ToString("D8");
                cboBusiness.SelectedValue = CurrentNote.BusinessID;
                dtpDate.Value = CurrentNote.Date;
                cboCustomer.SelectedValue = CurrentNote.CustomerID;
                rbnIsDebit.Checked = CurrentNote.IsDebit;
                rbnIsCredit.Checked = !CurrentNote.IsDebit;
                cboNoteType.SelectedItem = CurrentNote.NoteType;
                txtNoteNumber.Text = CurrentNote.NoteNumber;
                nudTotalAmount.Value = CurrentNote.TotalAmount;
                cboCurrency.SelectedValue = CurrentNote.CurrencyID;
                txtReason.Text = CurrentNote.Reason;
            }
            else
            {
                try
                {
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    if (((List<Customer>)cboCustomer.DataSource).Count == 0)
                    {
                        MessageBox.Show("No hay clientes registrados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                    cboNoteType.DataSource = new string[] { "A", "B", "C" };
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint ISN202
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ISN202 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
            }
        }
        private void ISN_IssuedNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentNote == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNoteNumber.Text))
            {
                MessageBox.Show("El número de la nota está incompleto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudTotalAmount.Value == 0)
            {
                MessageBox.Show("El importe debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Comprueba duplicados (si es nota nueva)
            if (CurrentNote == null)
            {
                string noteNumber = txtNoteNumber.Text.Trim();
                bool hasDuplicates;
                try
                {
                    hasDuplicates = await Task.Run(() => IssuedNote.CheckNoteNumberDuplicates(noteNumber));
                }
                catch (Exception dbException)
                {
                    // Waypoint ISN203
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ISN203 (Flag: MySQL). Message: " + dbException.Message);
                    return;
                }
                if (hasDuplicates)
                {
                    var prompt = MessageBox.Show("Ya existe otra nota registrada con el mismo número. ¿Desea continuar?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (prompt != DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            var note = new IssuedNote()
            {
                NoteID = (CurrentNote == null) ? 0 : CurrentNote.NoteID,
                BusinessID = (int)cboBusiness.SelectedValue,
                Date = dtpDate.Value.Date,
                CustomerID = (int)cboCustomer.SelectedValue,
                IsDebit = rbnIsDebit.Checked,
                NoteType = (string)cboNoteType.SelectedItem,
                NoteNumber = txtNoteNumber.Text,
                TotalAmount = nudTotalAmount.Value,
                CurrencyID = (int)cboCurrency.SelectedValue,
                Reason = txtReason.Text
            };
            if (CurrentNote == null)
            {
                try
                {
                    await Task.Run(() => note.Insert());
                    MessageBox.Show("Nota registrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint ISN204
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ISN204 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => note.Update());
                    MessageBox.Show("Nota actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint ISN205
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ISN205 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }

        private void cboCustomer_Validating(object sender, CancelEventArgs e)
        {
            // Borra el texto ingresado si no es un elemento de la lista.
            if (cboCustomer.SelectedItem == null)
            {
                cboCustomer.Text = string.Empty;
            }
        }
    }
}
