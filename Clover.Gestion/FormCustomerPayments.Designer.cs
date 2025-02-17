namespace Clover.Gestion
{
    partial class FormCustomerPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCustomerPayments));
            this.dgvCustomerPayments = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCustomerPayments
            // 
            this.dgvCustomerPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerPayments.Location = new System.Drawing.Point(2, 0);
            this.dgvCustomerPayments.Name = "dgvCustomerPayments";
            this.dgvCustomerPayments.Size = new System.Drawing.Size(1266, 570);
            this.dgvCustomerPayments.TabIndex = 0;
            this.dgvCustomerPayments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomerPayments_CellContentClick);
            // 
            // FormCustomerPayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 723);
            this.Controls.Add(this.dgvCustomerPayments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCustomerPayments";
            this.Text = "Cartera de Cheques";
            this.Load += new System.EventHandler(this.FormCustomerPayments_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerPayments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCustomerPayments;
    }
}