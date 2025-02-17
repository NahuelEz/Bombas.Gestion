namespace Clover.TabletApp
{
    partial class RO_ProgressManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RO_ProgressManager));
            this.dgvProgressUpdates = new System.Windows.Forms.DataGridView();
            this.puDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puUpdateTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnOpenUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgressUpdates)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProgressUpdates
            // 
            this.dgvProgressUpdates.AllowUserToAddRows = false;
            this.dgvProgressUpdates.AllowUserToDeleteRows = false;
            this.dgvProgressUpdates.AllowUserToResizeColumns = false;
            this.dgvProgressUpdates.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProgressUpdates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProgressUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProgressUpdates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.puDate,
            this.puUserName,
            this.puUpdateTypeName});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProgressUpdates.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProgressUpdates.Location = new System.Drawing.Point(12, 12);
            this.dgvProgressUpdates.MultiSelect = false;
            this.dgvProgressUpdates.Name = "dgvProgressUpdates";
            this.dgvProgressUpdates.ReadOnly = true;
            this.dgvProgressUpdates.RowHeadersVisible = false;
            this.dgvProgressUpdates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProgressUpdates.Size = new System.Drawing.Size(560, 291);
            this.dgvProgressUpdates.TabIndex = 0;
            // 
            // puDate
            // 
            this.puDate.DataPropertyName = "Date";
            dataGridViewCellStyle2.Format = "dd/MM/yy HH:mm";
            this.puDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.puDate.HeaderText = "Fecha y hora";
            this.puDate.Name = "puDate";
            this.puDate.ReadOnly = true;
            this.puDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.puDate.Width = 250;
            // 
            // puUserName
            // 
            this.puUserName.DataPropertyName = "UserName";
            this.puUserName.HeaderText = "Usuario";
            this.puUserName.Name = "puUserName";
            this.puUserName.ReadOnly = true;
            this.puUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.puUserName.Width = 250;
            // 
            // puUpdateTypeName
            // 
            this.puUpdateTypeName.DataPropertyName = "UpdateTypeName";
            this.puUpdateTypeName.HeaderText = "Tipo actualización";
            this.puUpdateTypeName.Name = "puUpdateTypeName";
            this.puUpdateTypeName.ReadOnly = true;
            this.puUpdateTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.puUpdateTypeName.Width = 500;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(12, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cerrar [ESC.]";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(216, 309);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(200, 40);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Actualizar [Ctrl. + A]";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnOpenUpdate
            // 
            this.btnOpenUpdate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenUpdate.Location = new System.Drawing.Point(422, 309);
            this.btnOpenUpdate.Name = "btnOpenUpdate";
            this.btnOpenUpdate.Size = new System.Drawing.Size(150, 40);
            this.btnOpenUpdate.TabIndex = 1;
            this.btnOpenUpdate.Text = "Ver selecc. [ENT.]";
            this.btnOpenUpdate.UseVisualStyleBackColor = true;
            this.btnOpenUpdate.Click += new System.EventHandler(this.btnOpenUpdate_Click);
            // 
            // ProgressManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.btnOpenUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dgvProgressUpdates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Proceso";
            this.Load += new System.EventHandler(this.RO_ProgressManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgressUpdates)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProgressUpdates;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnOpenUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn puDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn puUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn puUpdateTypeName;
    }
}