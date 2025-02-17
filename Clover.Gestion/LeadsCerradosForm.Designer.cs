namespace Clover.Gestion
{
    partial class LeadsCerradosForm
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
            this.dgvLeadsCerrados = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeadsCerrados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLeadsCerrados
            // 
            this.dgvLeadsCerrados.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeadsCerrados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeadsCerrados.Location = new System.Drawing.Point(12, 12);
            this.dgvLeadsCerrados.Name = "dgvLeadsCerrados";
            this.dgvLeadsCerrados.Size = new System.Drawing.Size(1554, 814);
            this.dgvLeadsCerrados.TabIndex = 0;
            // 
            // LeadsCerradosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1578, 838);
            this.Controls.Add(this.dgvLeadsCerrados);
            this.Name = "LeadsCerradosForm";
            this.Text = "Leads Cerrados";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeadsCerrados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLeadsCerrados;
    }
}