namespace Clover.Gestion
{
    partial class SHA_ViewSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SHA_ViewSettings));
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvFilters = new System.Windows.Forms.DataGridView();
            this.fieldColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.conditionColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbnAllMustBeTrue = new System.Windows.Forms.RadioButton();
            this.rbnOneMustBeTrue = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvSortLevels = new System.Windows.Forms.DataGridView();
            this.sortFieldColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sortDirectionColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sortDeleteColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(622, 559);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 40);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Aceptar";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgvFilters
            // 
            this.dgvFilters.AllowUserToDeleteRows = false;
            this.dgvFilters.AllowUserToResizeColumns = false;
            this.dgvFilters.AllowUserToResizeRows = false;
            this.dgvFilters.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldColumn,
            this.conditionColumn,
            this.valueColumn,
            this.deleteColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFilters.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFilters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvFilters.Location = new System.Drawing.Point(12, 35);
            this.dgvFilters.MultiSelect = false;
            this.dgvFilters.Name = "dgvFilters";
            this.dgvFilters.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFilters.Size = new System.Drawing.Size(760, 210);
            this.dgvFilters.TabIndex = 1;
            this.dgvFilters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilters_CellContentClick);
            this.dgvFilters.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvFilters_EditingControlShowing);
            this.dgvFilters.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvFilters_RowsRemoved);
            this.dgvFilters.SelectionChanged += new System.EventHandler(this.dgvFilters_SelectionChanged);
            this.dgvFilters.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvFilters_UserAddedRow);
            // 
            // fieldColumn
            // 
            this.fieldColumn.HeaderText = "Campo";
            this.fieldColumn.Name = "fieldColumn";
            this.fieldColumn.Width = 200;
            // 
            // conditionColumn
            // 
            this.conditionColumn.HeaderText = "Condición";
            this.conditionColumn.Name = "conditionColumn";
            this.conditionColumn.Width = 200;
            // 
            // valueColumn
            // 
            this.valueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valueColumn.HeaderText = "Valor";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // deleteColumn
            // 
            this.deleteColumn.HeaderText = "Borrar";
            this.deleteColumn.Name = "deleteColumn";
            this.deleteColumn.Text = "Borrar";
            this.deleteColumn.UseColumnTextForButtonValue = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(466, 559);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbnAllMustBeTrue
            // 
            this.rbnAllMustBeTrue.AutoSize = true;
            this.rbnAllMustBeTrue.Checked = true;
            this.rbnAllMustBeTrue.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAllMustBeTrue.Location = new System.Drawing.Point(16, 533);
            this.rbnAllMustBeTrue.Name = "rbnAllMustBeTrue";
            this.rbnAllMustBeTrue.Size = new System.Drawing.Size(284, 27);
            this.rbnAllMustBeTrue.TabIndex = 5;
            this.rbnAllMustBeTrue.TabStop = true;
            this.rbnAllMustBeTrue.Text = "Todos los filtros deben cumplirse.";
            this.rbnAllMustBeTrue.UseVisualStyleBackColor = true;
            // 
            // rbnOneMustBeTrue
            // 
            this.rbnOneMustBeTrue.AutoSize = true;
            this.rbnOneMustBeTrue.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnOneMustBeTrue.Location = new System.Drawing.Point(16, 566);
            this.rbnOneMustBeTrue.Name = "rbnOneMustBeTrue";
            this.rbnOneMustBeTrue.Size = new System.Drawing.Size(252, 27);
            this.rbnOneMustBeTrue.TabIndex = 6;
            this.rbnOneMustBeTrue.Text = "Solo un filtro debe cumplirse.";
            this.rbnOneMustBeTrue.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtros:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 507);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Opciones de filtrado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Orden:";
            // 
            // dgvSortLevels
            // 
            this.dgvSortLevels.AllowUserToDeleteRows = false;
            this.dgvSortLevels.AllowUserToResizeColumns = false;
            this.dgvSortLevels.AllowUserToResizeRows = false;
            this.dgvSortLevels.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSortLevels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSortLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSortLevels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sortFieldColumn,
            this.sortDirectionColumn,
            this.sortDeleteColumn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSortLevels.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSortLevels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSortLevels.Location = new System.Drawing.Point(12, 284);
            this.dgvSortLevels.MultiSelect = false;
            this.dgvSortLevels.Name = "dgvSortLevels";
            this.dgvSortLevels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSortLevels.Size = new System.Drawing.Size(760, 210);
            this.dgvSortLevels.TabIndex = 3;
            this.dgvSortLevels.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSortLevels_CellContentClick);
            this.dgvSortLevels.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvSortLevels_EditingControlShowing);
            this.dgvSortLevels.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvSortLevels_RowsRemoved);
            this.dgvSortLevels.SelectionChanged += new System.EventHandler(this.dgvSortLevels_SelectionChanged);
            this.dgvSortLevels.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvSortLevels_UserAddedRow);
            // 
            // sortFieldColumn
            // 
            this.sortFieldColumn.HeaderText = "Campo";
            this.sortFieldColumn.Name = "sortFieldColumn";
            this.sortFieldColumn.Width = 200;
            // 
            // sortDirectionColumn
            // 
            this.sortDirectionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sortDirectionColumn.HeaderText = "Dirección";
            this.sortDirectionColumn.Name = "sortDirectionColumn";
            // 
            // sortDeleteColumn
            // 
            this.sortDeleteColumn.HeaderText = "Borrar";
            this.sortDeleteColumn.Name = "sortDeleteColumn";
            this.sortDeleteColumn.Text = "Borrar";
            this.sortDeleteColumn.UseColumnTextForButtonValue = true;
            // 
            // SHA_ViewSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 611);
            this.Controls.Add(this.dgvSortLevels);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbnOneMustBeTrue);
            this.Controls.Add(this.rbnAllMustBeTrue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvFilters);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SHA_ViewSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filtros y orden";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dgvFilters;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbnAllMustBeTrue;
        private System.Windows.Forms.RadioButton rbnOneMustBeTrue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewComboBoxColumn fieldColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn conditionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvSortLevels;
        private System.Windows.Forms.DataGridViewComboBoxColumn sortFieldColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn sortDirectionColumn;
        private System.Windows.Forms.DataGridViewButtonColumn sortDeleteColumn;
    }
}