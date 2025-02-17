namespace Clover.Gestion
{
    partial class SI_SaleInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SI_SaleInvoice));
            this.btnLinkSale = new System.Windows.Forms.Button();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.saleIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalBeforeTaxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencySymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsSaleOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenSale = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemUnlinkSale = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.nudTotalAmount = new System.Windows.Forms.NumericUpDown();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.cboInvoiceType = new System.Windows.Forms.ComboBox();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.txtSaleInvoiceID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlInvoiceIcon = new System.Windows.Forms.Panel();
            this.btnSendReminder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.cmsSaleOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLinkSale
            // 
            this.btnLinkSale.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinkSale.Location = new System.Drawing.Point(400, 340);
            this.btnLinkSale.Name = "btnLinkSale";
            this.btnLinkSale.Size = new System.Drawing.Size(200, 31);
            this.btnLinkSale.TabIndex = 5;
            this.btnLinkSale.Text = "Asociar venta";
            this.btnLinkSale.UseVisualStyleBackColor = true;
            this.btnLinkSale.Click += new System.EventHandler(this.btnLinkSale_Click);
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToDeleteRows = false;
            this.dgvSales.AllowUserToResizeColumns = false;
            this.dgvSales.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.saleIdColumn,
            this.totalBeforeTaxColumn,
            this.currencySymbolColumn});
            this.dgvSales.ContextMenuStrip = this.cmsSaleOptions;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSales.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSales.Location = new System.Drawing.Point(121, 192);
            this.dgvSales.MultiSelect = false;
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersVisible = false;
            this.dgvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSales.ShowCellErrors = false;
            this.dgvSales.ShowCellToolTips = false;
            this.dgvSales.ShowEditingIcon = false;
            this.dgvSales.ShowRowErrors = false;
            this.dgvSales.Size = new System.Drawing.Size(479, 142);
            this.dgvSales.TabIndex = 4;
            // 
            // saleIdColumn
            // 
            this.saleIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.saleIdColumn.DataPropertyName = "SaleID";
            dataGridViewCellStyle2.Format = "D8";
            this.saleIdColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.saleIdColumn.HeaderText = "ID Venta";
            this.saleIdColumn.Name = "saleIdColumn";
            this.saleIdColumn.ReadOnly = true;
            this.saleIdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // totalBeforeTaxColumn
            // 
            this.totalBeforeTaxColumn.DataPropertyName = "TotalBeforeTax";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.totalBeforeTaxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.totalBeforeTaxColumn.HeaderText = "Total sin IVA";
            this.totalBeforeTaxColumn.Name = "totalBeforeTaxColumn";
            this.totalBeforeTaxColumn.ReadOnly = true;
            this.totalBeforeTaxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.totalBeforeTaxColumn.Width = 150;
            // 
            // currencySymbolColumn
            // 
            this.currencySymbolColumn.DataPropertyName = "CurrencySymbol";
            this.currencySymbolColumn.HeaderText = "Moneda";
            this.currencySymbolColumn.Name = "currencySymbolColumn";
            this.currencySymbolColumn.ReadOnly = true;
            this.currencySymbolColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.currencySymbolColumn.Width = 150;
            // 
            // cmsSaleOptions
            // 
            this.cmsSaleOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsSaleOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenSale,
            this.cmsItemUnlinkSale});
            this.cmsSaleOptions.Name = "cmsPurchaseOrderOptions";
            this.cmsSaleOptions.Size = new System.Drawing.Size(212, 60);
            // 
            // cmsItemOpenSale
            // 
            this.cmsItemOpenSale.Name = "cmsItemOpenSale";
            this.cmsItemOpenSale.Size = new System.Drawing.Size(211, 28);
            this.cmsItemOpenSale.Text = "Ver venta";
            this.cmsItemOpenSale.Click += new System.EventHandler(this.cmsItemOpenSale_Click);
            // 
            // cmsItemUnlinkSale
            // 
            this.cmsItemUnlinkSale.Name = "cmsItemUnlinkSale";
            this.cmsItemUnlinkSale.Size = new System.Drawing.Size(211, 28);
            this.cmsItemUnlinkSale.Text = "Desasociar venta";
            this.cmsItemUnlinkSale.Click += new System.EventHandler(this.cmsItemUnlinkSale_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(256, 564);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 40);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cerrar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(462, 564);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 14;
            this.btnAccept.Text = "Guardar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // nudTotalAmount
            // 
            this.nudTotalAmount.DecimalPlaces = 2;
            this.nudTotalAmount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTotalAmount.Location = new System.Drawing.Point(330, 451);
            this.nudTotalAmount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudTotalAmount.Name = "nudTotalAmount";
            this.nudTotalAmount.Size = new System.Drawing.Size(150, 31);
            this.nudTotalAmount.TabIndex = 11;
            this.nudTotalAmount.ThousandsSeparator = true;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNumber.Location = new System.Drawing.Point(330, 414);
            this.txtInvoiceNumber.MaxLength = 32;
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(270, 31);
            this.txtInvoiceNumber.TabIndex = 9;
            // 
            // cboCurrency
            // 
            this.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(330, 488);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(270, 31);
            this.cboCurrency.TabIndex = 13;
            // 
            // cboInvoiceType
            // 
            this.cboInvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvoiceType.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboInvoiceType.FormattingEnabled = true;
            this.cboInvoiceType.Location = new System.Drawing.Point(330, 377);
            this.cboInvoiceType.Name = "cboInvoiceType";
            this.cboInvoiceType.Size = new System.Drawing.Size(270, 31);
            this.cboInvoiceType.TabIndex = 7;
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(330, 89);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(270, 31);
            this.dtpInvoiceDate.TabIndex = 21;
            // 
            // cboCustomer
            // 
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(330, 126);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(270, 31);
            this.cboCustomer.TabIndex = 2;
            // 
            // cboBusiness
            // 
            this.cboBusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusiness.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusiness.FormattingEnabled = true;
            this.cboBusiness.Location = new System.Drawing.Point(330, 52);
            this.cboBusiness.Name = "cboBusiness";
            this.cboBusiness.Size = new System.Drawing.Size(270, 31);
            this.cboBusiness.TabIndex = 19;
            // 
            // txtSaleInvoiceID
            // 
            this.txtSaleInvoiceID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleInvoiceID.Location = new System.Drawing.Point(330, 15);
            this.txtSaleInvoiceID.Name = "txtSaleInvoiceID";
            this.txtSaleInvoiceID.ReadOnly = true;
            this.txtSaleInvoiceID.Size = new System.Drawing.Size(270, 31);
            this.txtSaleInvoiceID.TabIndex = 17;
            this.txtSaleInvoiceID.Text = "< Generado automáticamente >";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(117, 491);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 23);
            this.label9.TabIndex = 12;
            this.label9.Text = "Moneda:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(117, 454);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 23);
            this.label8.TabIndex = 10;
            this.label8.Text = "Importe:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(117, 417);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "N° de factura:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(117, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tipo factura:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(117, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 23);
            this.label5.TabIndex = 3;
            this.label5.Text = "Ventas asociadas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(117, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cliente:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "Fecha factura:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 23);
            this.label2.TabIndex = 18;
            this.label2.Text = "Empresa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "ID Factura:";
            // 
            // pnlInvoiceIcon
            // 
            this.pnlInvoiceIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlInvoiceIcon.BackgroundImage")));
            this.pnlInvoiceIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlInvoiceIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlInvoiceIcon.Name = "pnlInvoiceIcon";
            this.pnlInvoiceIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlInvoiceIcon.TabIndex = 0;
            // 
            // btnSendReminder
            // 
            this.btnSendReminder.Location = new System.Drawing.Point(39, 564);
            this.btnSendReminder.Name = "btnSendReminder";
            this.btnSendReminder.Size = new System.Drawing.Size(145, 37);
            this.btnSendReminder.TabIndex = 22;
            this.btnSendReminder.Text = "Enviar recordatorio";
            this.btnSendReminder.UseVisualStyleBackColor = true;
            this.btnSendReminder.Click += new System.EventHandler(this.button1_Click);
            // 
            // SI_SaleInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 616);
            this.Controls.Add(this.btnSendReminder);
            this.Controls.Add(this.btnLinkSale);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.nudTotalAmount);
            this.Controls.Add(this.txtInvoiceNumber);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.cboInvoiceType);
            this.Controls.Add(this.dtpInvoiceDate);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.cboBusiness);
            this.Controls.Add(this.txtSaleInvoiceID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlInvoiceIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SI_SaleInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar factura de venta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SI_SaleInvoice_FormClosing);
            this.Load += new System.EventHandler(this.SI_SaleInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.cmsSaleOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLinkSale;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.NumericUpDown nudTotalAmount;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.ComboBox cboInvoiceType;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.TextBox txtSaleInvoiceID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlInvoiceIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn saleIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalBeforeTaxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencySymbolColumn;
        private System.Windows.Forms.ContextMenuStrip cmsSaleOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenSale;
        private System.Windows.Forms.ToolStripMenuItem cmsItemUnlinkSale;
        private System.Windows.Forms.Button btnSendReminder;
    }
}