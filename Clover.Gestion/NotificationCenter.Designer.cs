namespace Clover.Gestion
{
    partial class NotificationCenter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationCenter));
            this.dgvRegistry = new System.Windows.Forms.DataGridView();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistry)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRegistry
            // 
            this.dgvRegistry.AllowUserToAddRows = false;
            this.dgvRegistry.AllowUserToDeleteRows = false;
            this.dgvRegistry.AllowUserToResizeColumns = false;
            this.dgvRegistry.AllowUserToResizeRows = false;
            this.dgvRegistry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRegistry.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRegistry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRegistry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateColumn,
            this.userColumn,
            this.descriptionColumn});
            this.dgvRegistry.Location = new System.Drawing.Point(12, 12);
            this.dgvRegistry.MultiSelect = false;
            this.dgvRegistry.Name = "dgvRegistry";
            this.dgvRegistry.ReadOnly = true;
            this.dgvRegistry.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvRegistry.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRegistry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRegistry.ShowCellToolTips = false;
            this.dgvRegistry.Size = new System.Drawing.Size(760, 537);
            this.dgvRegistry.TabIndex = 10;
            this.dgvRegistry.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistry_CellDoubleClick);
            this.dgvRegistry.SelectionChanged += new System.EventHandler(this.dgvRegistry_SelectionChanged);
            // 
            // dateColumn
            // 
            this.dateColumn.DataPropertyName = "Time";
            dataGridViewCellStyle2.Format = "HH:mm";
            this.dateColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.dateColumn.HeaderText = "Hora";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dateColumn.Width = 150;
            // 
            // userColumn
            // 
            this.userColumn.DataPropertyName = "UserName";
            this.userColumn.HeaderText = "Usuario";
            this.userColumn.Name = "userColumn";
            this.userColumn.ReadOnly = true;
            this.userColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.userColumn.Width = 150;
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionColumn.DataPropertyName = "Description";
            this.descriptionColumn.HeaderText = "Detalle";
            this.descriptionColumn.Name = "descriptionColumn";
            this.descriptionColumn.ReadOnly = true;
            this.descriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NotificationCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dgvRegistry);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "NotificationCenter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Centro de notificaciones";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRegistry;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
    }
}