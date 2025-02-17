using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SHA_ViewSettings : Form
    {
        private ViewSettings CurrentViewSettings;

        public SHA_ViewSettings(ViewSettings CurrentViewSettings)
        {
            this.CurrentViewSettings = CurrentViewSettings;
            InitializeComponent();
            (dgvFilters.Columns["fieldColumn"] as DataGridViewComboBoxColumn).DisplayMember = "Caption";
            (dgvFilters.Columns["fieldColumn"] as DataGridViewComboBoxColumn).ValueMember = "Name";
            (dgvFilters.Columns["fieldColumn"] as DataGridViewComboBoxColumn).DataSource = CurrentViewSettings.Fields;
            (dgvFilters.Columns["conditionColumn"] as DataGridViewComboBoxColumn).DisplayMember = "Value";
            (dgvFilters.Columns["conditionColumn"] as DataGridViewComboBoxColumn).ValueMember = "Key";
            (dgvFilters.Columns["conditionColumn"] as DataGridViewComboBoxColumn).DataSource = new KeyValuePair<int, string>[]
                {
                    new KeyValuePair<int, string>(1,"ES"),
                    new KeyValuePair<int, string>(2,"NO ES"),
                    new KeyValuePair<int, string>(3,"EMPIEZA CON"),
                    new KeyValuePair<int, string>(4,"TERMINA CON"),
                    new KeyValuePair<int, string>(5,"CONTIENE"),
                    new KeyValuePair<int, string>(6,"NO CONTIENE"),
                    new KeyValuePair<int, string>(7,"ES MAYOR A"),
                    new KeyValuePair<int, string>(8,"ES MAYOR O IGUAL A"),
                    new KeyValuePair<int, string>(9,"ES MENOR A"),
                    new KeyValuePair<int, string>(10,"ES MENOR O IGUAL A")
                };
            (dgvSortLevels.Columns["sortFieldColumn"] as DataGridViewComboBoxColumn).DisplayMember = "Caption";
            (dgvSortLevels.Columns["sortFieldColumn"] as DataGridViewComboBoxColumn).ValueMember = "Name";
            (dgvSortLevels.Columns["sortFieldColumn"] as DataGridViewComboBoxColumn).DataSource = CurrentViewSettings.Fields;
            (dgvSortLevels.Columns["sortDirectionColumn"] as DataGridViewComboBoxColumn).DisplayMember = "Value";
            (dgvSortLevels.Columns["sortDirectionColumn"] as DataGridViewComboBoxColumn).ValueMember = "Key";
            (dgvSortLevels.Columns["sortDirectionColumn"] as DataGridViewComboBoxColumn).DataSource = new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("ASC", "Ascendente"),
                    new KeyValuePair<string, string>("DESC", "Descendente")
                };
            // Current settings load
            foreach (var filter in CurrentViewSettings.Filters)
            {
                dgvFilters.Rows.Add(filter.Field.Name, filter.ConditionID, filter.Value);
            }
            foreach (var level in CurrentViewSettings.SortLevels)
            {
                dgvSortLevels.Rows.Add(level.Field.Name, level.Direction.ToString());
            }
            rbnAllMustBeTrue.Checked = CurrentViewSettings.AllConditionsMustBeTrue;
            rbnOneMustBeTrue.Checked = !CurrentViewSettings.AllConditionsMustBeTrue;
            dgvFilters.AllowUserToAddRows = (dgvFilters.Rows.Count < CurrentViewSettings.MaxFilters);
            dgvSortLevels.AllowUserToAddRows = (dgvSortLevels.Rows.Count < CurrentViewSettings.MaxSortLevels);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<Filter> filters = new List<Filter>();
            List<SortLevel> sortLevels = new List<SortLevel>();
            bool validationPassed = true;
            foreach (DataGridViewRow filterRow in dgvFilters.Rows)
            {
                if (filterRow.IsNewRow)
                {
                    continue;
                }
                var fieldName = filterRow.Cells["fieldColumn"].Value;
                var conditionID = filterRow.Cells["conditionColumn"].Value;
                var value = filterRow.Cells["valueColumn"].Value;
                if (fieldName == null || conditionID == null || value == null)
                {
                    filterRow.DefaultCellStyle.BackColor = Color.FromArgb(184, 204, 228);
                    validationPassed = false;
                    continue;
                }
                var filter = new Filter(CurrentViewSettings.Fields.First(X => X.Name == (string)(fieldName)), (int)(conditionID), (string)(value));
                switch (filter.Field.FieldType)
                {
                    case DataFieldTypes.DateTime:
                        {
                            if (!ViewSettings.GenericConditions.ContainsKey(filter.ConditionID))
                            {
                                filterRow.DefaultCellStyle.BackColor = Color.FromArgb(230, 184, 183);
                                validationPassed = false;
                                continue;
                            }
                            if (!DateTime.TryParse(filter.Value, out DateTime result))
                            {
                                filterRow.DefaultCellStyle.BackColor = Color.FromArgb(248, 203, 173);
                                validationPassed = false;
                                continue;
                            }
                        }
                        break;
                    case DataFieldTypes.Integer:
                        {
                            if (!ViewSettings.GenericConditions.ContainsKey(filter.ConditionID))
                            {
                                filterRow.DefaultCellStyle.BackColor = Color.FromArgb(230, 184, 183);
                                validationPassed = false;
                                continue;
                            }
                            if (!int.TryParse(filter.Value, out int result))
                            {
                                filterRow.DefaultCellStyle.BackColor = Color.FromArgb(248, 203, 173);
                                validationPassed = false;
                                continue;
                            }
                        }
                        break;
                    case DataFieldTypes.String:
                        {
                            if (!ViewSettings.StringConditions.ContainsKey(filter.ConditionID))
                            {
                                filterRow.DefaultCellStyle.BackColor = Color.FromArgb(230, 184, 183);
                                validationPassed = false;
                                continue;
                            }
                        }
                        break;
                }
                filters.Add(filter);
                filterRow.DefaultCellStyle.BackColor = SystemColors.Window;
            }
            foreach (DataGridViewRow sortLevelRow in dgvSortLevels.Rows)
            {
                if (sortLevelRow.IsNewRow)
                {
                    continue;
                }
                var fieldName = sortLevelRow.Cells["sortFieldColumn"].Value;
                var direction = sortLevelRow.Cells["sortDirectionColumn"].Value;
                if (fieldName == null || direction == null)
                {
                    sortLevelRow.DefaultCellStyle.BackColor = Color.FromArgb(184, 204, 228);
                    validationPassed = false;
                    continue;
                }
                sortLevels.Add(new SortLevel(CurrentViewSettings.Fields.First(X => X.Name == (string)(fieldName)),
                    (SortDirection)(Enum.Parse(typeof(SortDirection), (string)(direction)))));
            }
            if (!validationPassed)
            {
                MessageBox.Show("No se pudo validar la lista de filtros y orden. El color de fondo indica el problema:"
                    + Environment.NewLine + Environment.NewLine + "Azul: faltan datos."
                    + Environment.NewLine + "Rojo: la condición no aplica para el campo seleccionado."
                    + Environment.NewLine + "Naranja: el valor no tiene el formato correcto.",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                CurrentViewSettings.SortLevels = sortLevels;
                CurrentViewSettings.Filters = filters;
                CurrentViewSettings.AllConditionsMustBeTrue = rbnAllMustBeTrue.Checked;
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        // NOTE: Below is only for fixing .NET Framework bug
        private void dgvFilters_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control != null)
            {
                e.CellStyle.BackColor = dgvFilters.DefaultCellStyle.BackColor;
            }
        }
        private void dgvSortLevels_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control != null)
            {
                e.CellStyle.BackColor = dgvSortLevels.DefaultCellStyle.BackColor;
            }
        }

        private void dgvFilters_SelectionChanged(object sender, EventArgs e)
        {
            dgvFilters.ClearSelection();
        }
        private void dgvFilters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                if ((!dgvFilters.Rows[e.RowIndex].IsNewRow) && (dgvFilters.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
                {
                    dgvFilters.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        private void dgvSortLevels_SelectionChanged(object sender, EventArgs e)
        {
            dgvSortLevels.ClearSelection();
        }
        private void dgvSortLevels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                if ((!dgvSortLevels.Rows[e.RowIndex].IsNewRow) && (dgvSortLevels.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
                {
                    dgvSortLevels.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        // For limiting conditions
        private void dgvFilters_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvFilters.AllowUserToAddRows = (dgvFilters.Rows.Count < CurrentViewSettings.MaxFilters);
        }
        private void dgvFilters_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dgvFilters.AllowUserToAddRows = (dgvFilters.Rows.Count < CurrentViewSettings.MaxFilters);
        }
        private void dgvSortLevels_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvSortLevels.AllowUserToAddRows = (dgvSortLevels.Rows.Count < CurrentViewSettings.MaxSortLevels);
        }
        private void dgvSortLevels_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dgvSortLevels.AllowUserToAddRows = (dgvSortLevels.Rows.Count < CurrentViewSettings.MaxSortLevels);
        }
    }
}
