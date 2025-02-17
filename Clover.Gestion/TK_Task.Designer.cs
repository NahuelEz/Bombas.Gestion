namespace Clover.Gestion
{
    partial class TK_Task
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TK_Task));
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTaskDate = new System.Windows.Forms.DateTimePicker();
            this.cboPriority = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmsDescriptionOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemInsertProviderInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlEventIcon = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rbnPendent = new System.Windows.Forms.RadioButton();
            this.rbnCompleted = new System.Windows.Forms.RadioButton();
            this.cmsItemInsertCustomerInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDescriptionOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(116, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha y hora:";
            // 
            // dtpTaskDate
            // 
            this.dtpTaskDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTaskDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTaskDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTaskDate.Location = new System.Drawing.Point(330, 15);
            this.dtpTaskDate.Name = "dtpTaskDate";
            this.dtpTaskDate.Size = new System.Drawing.Size(270, 31);
            this.dtpTaskDate.TabIndex = 2;
            // 
            // cboPriority
            // 
            this.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriority.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPriority.FormattingEnabled = true;
            this.cboPriority.Location = new System.Drawing.Point(330, 52);
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Size = new System.Drawing.Size(270, 31);
            this.cboPriority.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(116, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Prioridad:";
            // 
            // txtDescription
            // 
            this.txtDescription.ContextMenuStrip = this.cmsDescriptionOptions;
            this.txtDescription.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(120, 126);
            this.txtDescription.MaxLength = 256;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(480, 300);
            this.txtDescription.TabIndex = 6;
            // 
            // cmsDescriptionOptions
            // 
            this.cmsDescriptionOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsDescriptionOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemInsertProviderInformation,
            this.cmsItemInsertCustomerInformation});
            this.cmsDescriptionOptions.Name = "cmsDescriptionOptions";
            this.cmsDescriptionOptions.Size = new System.Drawing.Size(298, 82);
            // 
            // cmsItemInsertProviderInformation
            // 
            this.cmsItemInsertProviderInformation.Name = "cmsItemInsertProviderInformation";
            this.cmsItemInsertProviderInformation.Size = new System.Drawing.Size(297, 28);
            this.cmsItemInsertProviderInformation.Text = "Insertar datos de proveedor";
            this.cmsItemInsertProviderInformation.Click += new System.EventHandler(this.cmsItemInsertProviderInformation_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(116, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Descripción:";
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(422, 509);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 10;
            this.btnAccept.Text = "Registrar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(216, 509);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 40);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlEventIcon
            // 
            this.pnlEventIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlEventIcon.BackgroundImage")));
            this.pnlEventIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlEventIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlEventIcon.Name = "pnlEventIcon";
            this.pnlEventIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlEventIcon.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(116, 438);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Estado:";
            // 
            // rbnPendent
            // 
            this.rbnPendent.AutoSize = true;
            this.rbnPendent.Checked = true;
            this.rbnPendent.Enabled = false;
            this.rbnPendent.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnPendent.Location = new System.Drawing.Point(330, 436);
            this.rbnPendent.Name = "rbnPendent";
            this.rbnPendent.Size = new System.Drawing.Size(105, 27);
            this.rbnPendent.TabIndex = 8;
            this.rbnPendent.TabStop = true;
            this.rbnPendent.Text = "Pendiente";
            this.rbnPendent.UseVisualStyleBackColor = true;
            // 
            // rbnCompleted
            // 
            this.rbnCompleted.AutoSize = true;
            this.rbnCompleted.Enabled = false;
            this.rbnCompleted.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnCompleted.Location = new System.Drawing.Point(441, 436);
            this.rbnCompleted.Name = "rbnCompleted";
            this.rbnCompleted.Size = new System.Drawing.Size(120, 27);
            this.rbnCompleted.TabIndex = 9;
            this.rbnCompleted.Text = "Completada";
            this.rbnCompleted.UseVisualStyleBackColor = true;
            // 
            // cmsItemInsertCustomerInformation
            // 
            this.cmsItemInsertCustomerInformation.Name = "cmsItemInsertCustomerInformation";
            this.cmsItemInsertCustomerInformation.Size = new System.Drawing.Size(297, 28);
            this.cmsItemInsertCustomerInformation.Text = "Insertar datos de cliente";
            this.cmsItemInsertCustomerInformation.Click += new System.EventHandler(this.cmsItemInsertCustomerInformation_Click);
            // 
            // TK_Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 561);
            this.Controls.Add(this.rbnCompleted);
            this.Controls.Add(this.rbnPendent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboPriority);
            this.Controls.Add(this.dtpTaskDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlEventIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TK_Task";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar tarea";
            this.Load += new System.EventHandler(this.TK_Task_Load);
            this.cmsDescriptionOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlEventIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTaskDate;
        private System.Windows.Forms.ComboBox cboPriority;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbnPendent;
        private System.Windows.Forms.RadioButton rbnCompleted;
        private System.Windows.Forms.ContextMenuStrip cmsDescriptionOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemInsertProviderInformation;
        private System.Windows.Forms.ToolStripMenuItem cmsItemInsertCustomerInformation;
    }
}