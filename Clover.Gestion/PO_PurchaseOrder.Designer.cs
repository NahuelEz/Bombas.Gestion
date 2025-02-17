namespace Clover.Gestion
{
    partial class PO_PurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PO_PurchaseOrder));
            this.pnlPurchaseOrderIcon = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPurchaseOrderID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboProvider = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.btnMakePdf = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblItemsCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnShowItems = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtVatTotal = new System.Windows.Forms.TextBox();
            this.txtTotalBeforeTax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGrandTotal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboContact = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnCurrencyConverter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlPurchaseOrderIcon
            // 
            this.pnlPurchaseOrderIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPurchaseOrderIcon.BackgroundImage")));
            this.pnlPurchaseOrderIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlPurchaseOrderIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlPurchaseOrderIcon.Name = "pnlPurchaseOrderIcon";
            this.pnlPurchaseOrderIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlPurchaseOrderIcon.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 23);
            this.label1.TabIndex = 23;
            this.label1.Text = "ID Orden de compra:";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(330, 89);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(270, 31);
            this.dtpDate.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 23);
            this.label3.TabIndex = 27;
            this.label3.Text = "Fecha creación:";
            // 
            // txtPurchaseOrderID
            // 
            this.txtPurchaseOrderID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseOrderID.Location = new System.Drawing.Point(330, 15);
            this.txtPurchaseOrderID.Name = "txtPurchaseOrderID";
            this.txtPurchaseOrderID.ReadOnly = true;
            this.txtPurchaseOrderID.Size = new System.Drawing.Size(270, 31);
            this.txtPurchaseOrderID.TabIndex = 24;
            this.txtPurchaseOrderID.Text = "< Generado automáticamente >";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(117, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "Proveedor:";
            // 
            // cboProvider
            // 
            this.cboProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvider.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProvider.FormattingEnabled = true;
            this.cboProvider.Location = new System.Drawing.Point(330, 126);
            this.cboProvider.Name = "cboProvider";
            this.cboProvider.Size = new System.Drawing.Size(270, 31);
            this.cboProvider.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(117, 457);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 23);
            this.label11.TabIndex = 16;
            this.label11.Text = "Moneda:";
            // 
            // cboCurrency
            // 
            this.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(330, 454);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(270, 31);
            this.cboCurrency.TabIndex = 17;
            // 
            // btnMakePdf
            // 
            this.btnMakePdf.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakePdf.Location = new System.Drawing.Point(168, 565);
            this.btnMakePdf.Name = "btnMakePdf";
            this.btnMakePdf.Size = new System.Drawing.Size(150, 40);
            this.btnMakePdf.TabIndex = 21;
            this.btnMakePdf.Text = "Generar PDF";
            this.btnMakePdf.UseVisualStyleBackColor = true;
            this.btnMakePdf.Click += new System.EventHandler(this.btnMakePdf_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(512, 565);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(150, 40);
            this.btnAccept.TabIndex = 19;
            this.btnAccept.Text = "Guardar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblItemsCount
            // 
            this.lblItemsCount.AutoSize = true;
            this.lblItemsCount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.lblItemsCount.Location = new System.Drawing.Point(191, 300);
            this.lblItemsCount.Name = "lblItemsCount";
            this.lblItemsCount.Size = new System.Drawing.Size(81, 23);
            this.lblItemsCount.TabIndex = 8;
            this.lblItemsCount.Text = "0 ítem(s)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(117, 300);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "Detalle:";
            // 
            // btnShowItems
            // 
            this.btnShowItems.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowItems.Location = new System.Drawing.Point(330, 297);
            this.btnShowItems.Name = "btnShowItems";
            this.btnShowItems.Size = new System.Drawing.Size(270, 40);
            this.btnShowItems.TabIndex = 9;
            this.btnShowItems.Text = "Visualizar detalle";
            this.btnShowItems.UseVisualStyleBackColor = true;
            this.btnShowItems.Click += new System.EventHandler(this.btnShowItems_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Location = new System.Drawing.Point(12, 565);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(150, 40);
            this.btnSendEmail.TabIndex = 22;
            this.btnSendEmail.Text = "Enviar y guardar";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // cboBusiness
            // 
            this.cboBusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusiness.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusiness.FormattingEnabled = true;
            this.cboBusiness.Location = new System.Drawing.Point(330, 52);
            this.cboBusiness.Name = "cboBusiness";
            this.cboBusiness.Size = new System.Drawing.Size(270, 31);
            this.cboBusiness.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 23);
            this.label2.TabIndex = 25;
            this.label2.Text = "Empresa:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(120, 229);
            this.txtDescription.MaxLength = 64;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(480, 62);
            this.txtDescription.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(117, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Descripción:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(116, 383);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 23);
            this.label9.TabIndex = 12;
            this.label9.Text = "Importe IVA:";
            // 
            // txtVatTotal
            // 
            this.txtVatTotal.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatTotal.Location = new System.Drawing.Point(450, 380);
            this.txtVatTotal.Name = "txtVatTotal";
            this.txtVatTotal.ReadOnly = true;
            this.txtVatTotal.Size = new System.Drawing.Size(150, 31);
            this.txtVatTotal.TabIndex = 13;
            this.txtVatTotal.Text = "0,00";
            this.txtVatTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalBeforeTax
            // 
            this.txtTotalBeforeTax.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBeforeTax.Location = new System.Drawing.Point(450, 343);
            this.txtTotalBeforeTax.Name = "txtTotalBeforeTax";
            this.txtTotalBeforeTax.ReadOnly = true;
            this.txtTotalBeforeTax.Size = new System.Drawing.Size(150, 31);
            this.txtTotalBeforeTax.TabIndex = 11;
            this.txtTotalBeforeTax.Text = "0,00";
            this.txtTotalBeforeTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(116, 346);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 23);
            this.label8.TabIndex = 10;
            this.label8.Text = "Total:";
            // 
            // txtGrandTotal
            // 
            this.txtGrandTotal.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrandTotal.Location = new System.Drawing.Point(450, 417);
            this.txtGrandTotal.Name = "txtGrandTotal";
            this.txtGrandTotal.ReadOnly = true;
            this.txtGrandTotal.Size = new System.Drawing.Size(150, 31);
            this.txtGrandTotal.TabIndex = 15;
            this.txtGrandTotal.Text = "0,00";
            this.txtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(116, 420);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(146, 23);
            this.label10.TabIndex = 14;
            this.label10.Text = "Total IVA incluido:";
            // 
            // cboContact
            // 
            this.cboContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboContact.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboContact.FormattingEnabled = true;
            this.cboContact.Location = new System.Drawing.Point(330, 163);
            this.cboContact.Name = "cboContact";
            this.cboContact.Size = new System.Drawing.Size(270, 31);
            this.cboContact.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(117, 166);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 23);
            this.label12.TabIndex = 3;
            this.label12.Text = "Contacto:";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Enabled = false;
            this.btnSaveAs.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAs.Location = new System.Drawing.Point(324, 565);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(150, 40);
            this.btnSaveAs.TabIndex = 20;
            this.btnSaveAs.Text = "Guardar copia";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnCurrencyConverter
            // 
            this.btnCurrencyConverter.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrencyConverter.Location = new System.Drawing.Point(330, 491);
            this.btnCurrencyConverter.Name = "btnCurrencyConverter";
            this.btnCurrencyConverter.Size = new System.Drawing.Size(270, 31);
            this.btnCurrencyConverter.TabIndex = 18;
            this.btnCurrencyConverter.Text = "Convertidor de moneda";
            this.btnCurrencyConverter.UseVisualStyleBackColor = true;
            this.btnCurrencyConverter.Click += new System.EventHandler(this.btnCurrencyConverter_Click);
            // 
            // PO_PurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 617);
            this.Controls.Add(this.btnCurrencyConverter);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.cboContact);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtVatTotal);
            this.Controls.Add(this.txtTotalBeforeTax);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtGrandTotal);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboBusiness);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.lblItemsCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnShowItems);
            this.Controls.Add(this.btnMakePdf);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.cboProvider);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPurchaseOrderID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlPurchaseOrderIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PO_PurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generar orden de compra";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PO_PurchaseOrderUI_FormClosing);
            this.Load += new System.EventHandler(this.PO_PurchaseOrderUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPurchaseOrderIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPurchaseOrderID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboProvider;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.Button btnMakePdf;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblItemsCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnShowItems;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtVatTotal;
        private System.Windows.Forms.TextBox txtTotalBeforeTax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGrandTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboContact;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnCurrencyConverter;
    }
}