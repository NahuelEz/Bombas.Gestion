namespace Clover.Gestion
{
    partial class SA_CurrencyConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SA_CurrencyConverter));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.cboDestinationCurrency = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblDestinationCurrencySymbol = new System.Windows.Forms.Label();
            this.nudExchangeRate = new System.Windows.Forms.NumericUpDown();
            this.lblCurrencySymbol = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudExchangeRate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Moneda original:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Moneda a convertir:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cotización:";
            // 
            // txtCurrency
            // 
            this.txtCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrency.Location = new System.Drawing.Point(200, 12);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.ReadOnly = true;
            this.txtCurrency.Size = new System.Drawing.Size(270, 31);
            this.txtCurrency.TabIndex = 9;
            // 
            // cboDestinationCurrency
            // 
            this.cboDestinationCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestinationCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDestinationCurrency.FormattingEnabled = true;
            this.cboDestinationCurrency.Location = new System.Drawing.Point(200, 86);
            this.cboDestinationCurrency.Name = "cboDestinationCurrency";
            this.cboDestinationCurrency.Size = new System.Drawing.Size(270, 31);
            this.cboDestinationCurrency.TabIndex = 1;
            this.cboDestinationCurrency.SelectedIndexChanged += new System.EventHandler(this.cboDestinationCurrency_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(266, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(422, 197);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(150, 40);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblDestinationCurrencySymbol
            // 
            this.lblDestinationCurrencySymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDestinationCurrencySymbol.AutoSize = true;
            this.lblDestinationCurrencySymbol.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestinationCurrencySymbol.Location = new System.Drawing.Point(476, 126);
            this.lblDestinationCurrencySymbol.Name = "lblDestinationCurrencySymbol";
            this.lblDestinationCurrencySymbol.Size = new System.Drawing.Size(40, 23);
            this.lblDestinationCurrencySymbol.TabIndex = 5;
            this.lblDestinationCurrencySymbol.Text = "ARS";
            // 
            // nudExchangeRate
            // 
            this.nudExchangeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudExchangeRate.DecimalPlaces = 4;
            this.nudExchangeRate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudExchangeRate.Location = new System.Drawing.Point(320, 123);
            this.nudExchangeRate.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudExchangeRate.Name = "nudExchangeRate";
            this.nudExchangeRate.Size = new System.Drawing.Size(150, 31);
            this.nudExchangeRate.TabIndex = 4;
            this.nudExchangeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudExchangeRate.ThousandsSeparator = true;
            this.nudExchangeRate.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // lblCurrencySymbol
            // 
            this.lblCurrencySymbol.AutoSize = true;
            this.lblCurrencySymbol.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencySymbol.Location = new System.Drawing.Point(244, 126);
            this.lblCurrencySymbol.Name = "lblCurrencySymbol";
            this.lblCurrencySymbol.Size = new System.Drawing.Size(70, 23);
            this.lblCurrencySymbol.TabIndex = 3;
            this.lblCurrencySymbol.Text = "1 USD =";
            // 
            // SA_CurrencyConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 249);
            this.Controls.Add(this.lblCurrencySymbol);
            this.Controls.Add(this.lblDestinationCurrencySymbol);
            this.Controls.Add(this.nudExchangeRate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.cboDestinationCurrency);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SA_CurrencyConverter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Convertidor de moneda";
            this.Load += new System.EventHandler(this.ES_CurrencyConverter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudExchangeRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrency;
        private System.Windows.Forms.ComboBox cboDestinationCurrency;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblDestinationCurrencySymbol;
        private System.Windows.Forms.NumericUpDown nudExchangeRate;
        private System.Windows.Forms.Label lblCurrencySymbol;
    }
}