using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class TK_Task : Form
    {
        private int? TaskID = null;
        private ScheduledTask CurrentTask = null;
        
        public TK_Task(int? TaskID = null)
        {
            this.TaskID = TaskID;
            InitializeComponent();
        }

        private async void TK_Task_Load(object sender, EventArgs e)
        {
            if (TaskID.HasValue)
            {
                try
                {
                    CurrentTask = await Task.Run(() => ScheduledTask.GetTaskById(TaskID.Value));
                    cboPriority.DataSource = new string[] { "Alta", "Normal", "Baja" };
                    this.Text = "Visualizando tarea";
                    dtpTaskDate.Value = CurrentTask.Date;
                    cboPriority.SelectedItem = CurrentTask.Priority;
                    txtDescription.Text = CurrentTask.Description;
                    rbnPendent.Checked = !CurrentTask.Completed;
                    rbnCompleted.Checked = CurrentTask.Completed;
                    btnCancel.Text = "Cerrar";
                    btnAccept.Text = "Guardar";
                    rbnCompleted.Enabled = true;
                    rbnPendent.Enabled = true;
                }
                catch (Exception dbException)
                {
                    // Waypoint TK301
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint TK301 (Flag: MySql). Message: " + dbException.Message);
                    this.Close();
                }
            }
            else
            {
                cboPriority.DataSource = new string[] { "Alta", "Normal", "Baja" };
                cboPriority.SelectedItem = "Normal";
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Por favor, complete la descripción de la tarea.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Construye objeto
            var task = new ScheduledTask
            {
                TaskID = (CurrentTask == null) ? 0 : CurrentTask.TaskID,
                Date = dtpTaskDate.Value.Date,
                Description = txtDescription.Text.Replace(Environment.NewLine, " "),
                Priority = (string)cboPriority.SelectedItem,
                Completed = rbnCompleted.Checked
            };
            // Registra o guarda tarea.
            if (CurrentTask == null)
            {
                try
                {
                    await Task.Run(() => task.Insert());
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint TK302
                    MessageBox.Show("Error al guardar tarea."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint TK302. Message: " + dbException.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => task.Update());
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint TK303
                    MessageBox.Show("Error al registrar tarea."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint TK303. Message: " + dbException.Message);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsItemInsertProviderInformation_Click(object sender, EventArgs e)
        {
            using (var form = new TK_InsertProviderInformation())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtDescription.Text += form.Output;
                }
            }
        }
        private void cmsItemInsertCustomerInformation_Click(object sender, EventArgs e)
        {
            using (var form = new TK_InsertCustomerInformation())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtDescription.Text += form.Output;
                }
            }
        }
    }
}
