using Clover.DbLayer;
using Clover.Shared;
using iText.StyledXmlParser.Jsoup.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CU_CustomerBalance : Form
    {
        private int CustomerID;
        private Customer CurrentCustomer;

        public CU_CustomerBalance(int CustomerID)
        {
            this.CustomerID = CustomerID;

            InitializeComponent();
        }

        private async void CU_CustomerBalance_Load(object sender, EventArgs e)
        {
            // Carga cliente actual y lista de monedas
            try
            {
                CurrentCustomer = await Task.Run(() => Customer.GetCustomerById(CustomerID));
                cboCurrency.DisplayMember = "CurrencySymbol";
                cboCurrency.ValueMember = "CurrencyID";
                cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
            }
            catch (Exception dbException)
            {
                // Waypoint CU901
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU901 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }

            // Coloca nombre del cliente en formulario.
            lblCustomerName.Text = $"Cuenta corriente: {CurrentCustomer.CustomerName}";
            
            // Valores predeterminados de los controles de fecha (1 mes hacia atras).
            dtpDateFrom.Value = DateTime.Today.AddMonths(-1);
            dtpDateTo.Value = DateTime.Today;
        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            // Verifica que las fechas sean coherentes.
            if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
            {
                MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de cierre.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Borra la lista actual.
            dgvBalance.Rows.Clear();

            #region Consulta de datos
            // Obtiene información de facturas, notas de débito/crédito y cobros en las fechas y moneda solicitadas.
            var prevDate = dtpDateFrom.Value.Date.AddDays(-1);
            var startDate = dtpDateFrom.Value.Date;
            var endDate = dtpDateTo.Value.Date;
            int currencyId = (int)cboCurrency.SelectedValue;

            decimal prevBalance;
            List<SaleInvoice> invoices;
            List<IssuedNote> notes;
            List<CustomerPayment> payments;

            try
            {
                // Obtiene débitos y créditos hasta día anterior (incluido) al intervalo solicitado.
                decimal debit = await Task.Run(() => SaleInvoice.GetTotalByCustomerId(CustomerID, currencyId, prevDate))
                              + await Task.Run(() => IssuedNote.GetTotalDebitByCustomerId(CustomerID, currencyId, prevDate));

                decimal credit = await Task.Run(() => CustomerPayment.GetTotalByCustomerId(CustomerID, currencyId, prevDate))
                               + await Task.Run(() => IssuedNote.GetTotalCreditByCustomerId(CustomerID, currencyId, prevDate));

                prevBalance = debit - credit;

                // Obtiene facturas, notas de débito/crédito y cobros en el intervalo solicitado.
                invoices = await Task.Run(() => SaleInvoice.GetInvoicesByCustomerId(CustomerID, currencyId, startDate, endDate));
                notes = await Task.Run(() => IssuedNote.GetIssuedNotesByCustomerID(CustomerID, currencyId, startDate, endDate));
                payments = await Task.Run(() => CustomerPayment.GetPaymentsByCustomerId(CustomerID, currencyId, startDate, endDate));
            }
            catch (Exception dbException)
            {
                // Waypoint CU902
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU902 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            #endregion

            #region Post-procesado
            // Construye una lista combinada de facturas, notas y cobros.
            var balance = invoices.Cast<DbEntity>()
                .Concat(notes.Cast<DbEntity>())
                .Concat(payments.Cast<DbEntity>())
                .OrderBy((item) =>
                {
                    switch (item)
                    {
                        case SaleInvoice invoice:
                            return invoice.InvoiceDate;
                        case IssuedNote note:
                            return note.Date;
                        case CustomerPayment payment:
                            return payment.Date;
                        default:
                            return DateTime.Today;
                    }
                });
            #endregion

            #region Resultados
            // Agrega primera línea a la tabla (saldo previo).
            dgvBalance.Rows.Add(prevDate, "Saldo previo", null, null, prevBalance);

            // Color de texto rojo para saldos negativos.
            if (prevBalance < 0)
            {
                dgvBalance["colBalance", 0].Style.ForeColor = Color.Red;
            }

            // Agrega facturas, notas y cobros a la tabla.
            decimal currBalance = prevBalance;
            int rowCount = 1;

            foreach (var item in balance)
            {
                if (item is SaleInvoice)
                {
                    var invoice = (SaleInvoice)item;

                    // Agrega factura a la tabla.
                    currBalance += invoice.TotalAmount;
                    dgvBalance.Rows.Add(invoice.InvoiceDate, $"Factura {invoice.InvoiceType} {invoice.InvoiceNumber}", invoice.TotalAmount, null, currBalance);

                    // Cambia color de fondo de la fila.
                    dgvBalance.Rows[rowCount].DefaultCellStyle.BackColor = Color.LightGray;

                    // Asocia objeto de datos a la fila (habilita función de botón derecho).
                    dgvBalance.Rows[rowCount].Tag = invoice;
                }
                else if (item is IssuedNote)
                {
                    var note = (IssuedNote)item;

                    if (note.IsDebit)
                    {
                        // Agrega nota de débito a la tabla.
                        currBalance += note.TotalAmount;
                        dgvBalance.Rows.Add(note.Date, $"Nota de débito {note.NoteType} {note.NoteNumber}", note.TotalAmount, null, currBalance);

                        // Cambia color de fondo de la fila.
                        dgvBalance.Rows[rowCount].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    else
                    {
                        // Agrega nota de crédito a la tabla.
                        currBalance -= note.TotalAmount;
                        dgvBalance.Rows.Add(note.Date, $"Nota de crédito {note.NoteType} {note.NoteNumber}", null, note.TotalAmount, currBalance);
                    }

                    // Asocia objeto de datos a la fila (habilita función de botón derecho).
                    dgvBalance.Rows[rowCount].Tag = note;
                }
                else
                {
                    var payment = (CustomerPayment)item;

                    // Agrega cobro a la tabla.
                    currBalance -= payment.TotalAmount;
                    dgvBalance.Rows.Add(payment.Date, "Cobro", null, payment.TotalAmount, currBalance);

                    // Asocia objeto de datos a la fila (habilita función de botón derecho).
                    dgvBalance.Rows[rowCount].Tag = payment;
                }

                // Color de texto rojo para saldos negativos.
                if (currBalance < 0)
                {
                    dgvBalance["colBalance", rowCount].Style.ForeColor = Color.Red;
                }

                // Incrementa contador de filas.
                rowCount++;
            }

            // Agrega última línea a la tabla (saldo final).
            dgvBalance.Rows.Add(endDate, "Saldo a fecha final", null, null, currBalance);

            // Color de texto rojo para saldos negativos.
            if (currBalance < 0)
            {
                dgvBalance["colBalance", rowCount].Style.ForeColor = Color.Red;
            }
            #endregion
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Cierra el formulario.
            this.Close();
        }

        private void cmsItemOpenDetail_Click(object sender, EventArgs e)
        {
            // Verifica que haya una fila seleccionada.
            if (dgvBalance.SelectedRows.Count == 0)
            {
                return;
            }

            // Identifica el tipo de objeto seleccionado y abre el formulario correspondiente.
            var selectedRowTag = dgvBalance.SelectedRows[0].Tag;

            if (selectedRowTag is SaleInvoice)
            {
                int saleInvoiceId = ((SaleInvoice)selectedRowTag).SaleInvoiceID;

                using (var form = new SI_SaleInvoice(saleInvoiceId, SIParameterType.SaleInvoiceID))
                {
                    form.ShowDialog();
                }
            }
            else if (selectedRowTag is IssuedNote)
            {
                int noteId = ((IssuedNote)selectedRowTag).NoteID;

                using (var form = new ISN_IssuedNote(noteId))
                {
                    form.ShowDialog();
                }
            }
            else if (selectedRowTag is CustomerPayment)
            {
                int paymentId = ((CustomerPayment)selectedRowTag).CustomerPaymentID;

                using (var form = new CP_CustomerPayment(paymentId, CPParameterType.CustomerPaymentID))
                {
                    form.ShowDialog();
                }
            }
            else
            {
                // No hay objeto de datos asociado a la fila (entonces, estamos en la primera o última fila).
            }
        }

        private void dgvBalance_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona automáticamente la fila cuando se hace click con el botón derecho.
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvBalance.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    dgvBalance.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            // Pregunta al usuario ruta de destino y formato.
            string filePath;
            int filterIndex;
            using (var saveDialog = new SaveFileDialog()
            {
                AddExtension = true,
                FileName = "informe.xlsx",
                Filter = "Libro de Excel|*.xlsx|Documento PDF|*.pdf"
            })
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveDialog.FileName;
                    filterIndex = saveDialog.FilterIndex;
                }
                else
                {
                    return;
                }
            }

            // Construye la tabla de datos a exportar.
            var table = new DataTable();

            // Genera las columnas a partir del DataGridView.
            var column1 = new DataColumn
            {
                ColumnName = "column1",
                Caption = "Fecha",
                DataType = typeof(DateTime)
            };
            var column2 = new DataColumn
            {
                ColumnName = "column2",
                Caption = "Movimiento",
                DataType = typeof(string)
            };
            var column3 = new DataColumn
            {
                ColumnName = "column3",
                Caption = "Debe",
                DataType = typeof(decimal)
            };
            var column4 = new DataColumn
            {
                ColumnName = "column4",
                Caption = "Haber",
                DataType = typeof(decimal)
            };
            var column5 = new DataColumn
            {
                ColumnName = "column5",
                Caption = "Saldo",
                DataType = typeof(decimal)
            };


            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);

            // Genera las filas a partir del DataGridView.
            for (int i = 0; i < dgvBalance.RowCount; i++)
            {
                var values = dgvBalance.Rows[i].Cells.Cast<DataGridViewCell>().Select(x => x.Value).ToArray();
                table.Rows.Add(values);
            }

            // Exporta información.
            try
            {
                switch (filterIndex)
                {
                    case 1:
                    default: // Formato Excel.
                        {
                            await Task.Run(() => ExcelGeneration.ExportExcelDataTable(filePath, table));
                            break;
                        }
                    case 2: // Formato PDF.
                        {
                            float[] columnWidths = GetColumnsWidthPercent(dgvBalance);
                            await Task.Run(() => PdfGeneration.ExportPdfDataTable($"Cuenta Corriente: {CurrentCustomer.CustomerName}", columnWidths, table, filePath, true, true));
                            break;
                        }
                }
            }
            catch (Exception exportException)
            {
                // Waypoint CU903
                MessageBox.Show("Error exportar la información."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU903. Message: " + exportException.Message);
                return;
            }
            // Abre el archivo.
            try
            {
                using (var fileOpenProcess = new Process())
                {
                    fileOpenProcess.StartInfo.FileName = filePath;
                    fileOpenProcess.Start();
                }
            }
            catch (Exception fileOpenException)
            {
                // Waypoint CU904
                MessageBox.Show("Error al abrir archivo."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + fileOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU904. Message: " + fileOpenException.Message);
            }
        }
        private float[] GetColumnsWidthPercent(DataGridView dataGridView)
        {
            float[] columnWidths = dataGridView.Columns.Cast<DataGridViewColumn>().Select(x => (float)x.Width).ToArray();
            float sum = columnWidths.Sum();
            return columnWidths.Select(x => x / sum).ToArray();
        }
    }
}
