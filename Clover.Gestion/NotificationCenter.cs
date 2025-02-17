using Clover.DbLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class NotificationCenter : Form
    {
        private List<int> Highlighted = new List<int>();

        public NotificationCenter()
        {
            InitializeComponent();
            dgvRegistry.AutoGenerateColumns = false;
        }

        public void UpdateGrid(List<Record> data)
        {
            dgvRegistry.DataSource = data;
            HighlightRecords();
        }
        public void AddHighlightedRecords(IEnumerable<int> highlighted)
        {
            Highlighted.AddRange(highlighted);
            HighlightRecords();
        }
        private void HighlightRecords()
        {
            foreach (DataGridViewRow row in dgvRegistry.Rows)
            {
                var record = (Record)row.DataBoundItem;
                if (Highlighted.Contains(record.RecordID))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 237, 153);
                }
            }
        }

        private void dgvRegistry_SelectionChanged(object sender, EventArgs e)
        {
            dgvRegistry.ClearSelection();
        }
        private void dgvRegistry_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            // Verifica que no haya otras ventanas abiertas.
            if (Application.OpenForms.Count > 2)
            {
                MessageBox.Show("Primero debe cerrar todas las ventanas secundarias.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Notifica a la interfaz principal.
            var record = (Record)dgvRegistry.Rows[e.RowIndex].DataBoundItem;
            var parent = (Main)this.Owner;
            this.WindowState = FormWindowState.Minimized;
            parent.OpenRecordElement(record);
        }
    }
}
