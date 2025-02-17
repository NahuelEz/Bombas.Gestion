namespace Clover.Gestion
{
    partial class FormTipoCheque
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTipoCheque));
            this.lblTipoCheque = new System.Windows.Forms.Label();
            this.rbChequeComun = new System.Windows.Forms.RadioButton();
            this.rbChequeDiferido = new System.Windows.Forms.RadioButton();
            this.dtpFechaCobro = new System.Windows.Forms.DateTimePicker();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTipoCheque
            // 
            this.lblTipoCheque.AutoSize = true;
            this.lblTipoCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lblTipoCheque.Location = new System.Drawing.Point(43, 26);
            this.lblTipoCheque.Name = "lblTipoCheque";
            this.lblTipoCheque.Size = new System.Drawing.Size(262, 24);
            this.lblTipoCheque.TabIndex = 0;
            this.lblTipoCheque.Text = "Seleccione el tipo de cheque:";
            // 
            // rbChequeComun
            // 
            this.rbChequeComun.AutoSize = true;
            this.rbChequeComun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.rbChequeComun.Location = new System.Drawing.Point(124, 78);
            this.rbChequeComun.Name = "rbChequeComun";
            this.rbChequeComun.Size = new System.Drawing.Size(75, 21);
            this.rbChequeComun.TabIndex = 1;
            this.rbChequeComun.TabStop = true;
            this.rbChequeComun.Text = "Cheque";
            this.rbChequeComun.UseVisualStyleBackColor = true;
            this.rbChequeComun.CheckedChanged += new System.EventHandler(this.rbChequeComun_CheckedChanged);
            // 
            // rbChequeDiferido
            // 
            this.rbChequeDiferido.AutoSize = true;
            this.rbChequeDiferido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.rbChequeDiferido.Location = new System.Drawing.Point(124, 118);
            this.rbChequeDiferido.Name = "rbChequeDiferido";
            this.rbChequeDiferido.Size = new System.Drawing.Size(128, 21);
            this.rbChequeDiferido.TabIndex = 2;
            this.rbChequeDiferido.TabStop = true;
            this.rbChequeDiferido.Text = "Cheque Diferido";
            this.rbChequeDiferido.UseVisualStyleBackColor = true;
            // 
            // dtpFechaCobro
            // 
            this.dtpFechaCobro.Location = new System.Drawing.Point(80, 179);
            this.dtpFechaCobro.Name = "dtpFechaCobro";
            this.dtpFechaCobro.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaCobro.TabIndex = 3;
            this.dtpFechaCobro.Visible = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(133, 235);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // FormTipoCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 329);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.dtpFechaCobro);
            this.Controls.Add(this.rbChequeDiferido);
            this.Controls.Add(this.rbChequeComun);
            this.Controls.Add(this.lblTipoCheque);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTipoCheque";
            this.Text = "Tipo de Cheque";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTipoCheque;
        private System.Windows.Forms.RadioButton rbChequeComun;
        private System.Windows.Forms.RadioButton rbChequeDiferido;
        private System.Windows.Forms.DateTimePicker dtpFechaCobro;
        private System.Windows.Forms.Button btnAceptar;
    }
}