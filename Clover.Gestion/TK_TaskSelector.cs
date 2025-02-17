using Clover.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class TK_TaskSelector : Form
    {
        public List<ScheduledTask> SelectedTasks = null;

        public TK_TaskSelector(List<ScheduledTask> tasks)
        {
            InitializeComponent();
            clbxTasks.DataSource = tasks;
            for (int i = 0; i < clbxTasks.Items.Count; i++)
            {
                clbxTasks.SetItemChecked(i, true);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (clbxTasks.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una tarea para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedTasks = clbxTasks.CheckedItems.Cast<ScheduledTask>().ToList();
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clbxTasks_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((ScheduledTask)e.ListItem).Description;
        }
    }
}
