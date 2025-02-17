namespace Clover.Gestion
{
    partial class LeadCard
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLeadStatus = new System.Windows.Forms.Label();
            this.lblLeadName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLeadStatus
            // 
            this.lblLeadStatus.AutoSize = true;
            this.lblLeadStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblLeadStatus.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.lblLeadStatus.ForeColor = System.Drawing.Color.White;
            this.lblLeadStatus.Location = new System.Drawing.Point(3, 34);
            this.lblLeadStatus.Name = "lblLeadStatus";
            this.lblLeadStatus.Size = new System.Drawing.Size(55, 13);
            this.lblLeadStatus.TabIndex = 1;
            this.lblLeadStatus.Text = "Estado:";
            // 
            // lblLeadName
            // 
            this.lblLeadName.AutoSize = true;
            this.lblLeadName.BackColor = System.Drawing.Color.Transparent;
            this.lblLeadName.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.lblLeadName.ForeColor = System.Drawing.Color.White;
            this.lblLeadName.Location = new System.Drawing.Point(3, 12);
            this.lblLeadName.Name = "lblLeadName";
            this.lblLeadName.Size = new System.Drawing.Size(62, 13);
            this.lblLeadName.TabIndex = 0;
            this.lblLeadName.Text = "Nombre:";
            // 
            // LeadCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLeadName);
            this.Controls.Add(this.lblLeadStatus);
            this.Name = "LeadCard";
            this.Size = new System.Drawing.Size(161, 84);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLeadStatus;
        private System.Windows.Forms.Label lblLeadName;
    }
}
