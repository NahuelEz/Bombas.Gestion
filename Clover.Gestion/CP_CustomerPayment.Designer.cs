namespace Clover.Gestion
{
    partial class CP_CustomerPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CP_CustomerPayment));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.txtCustomerPaymentID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCustomerPaymentIcon = new System.Windows.Forms.Panel();
            this.txtAdditionalInformation = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudTotalAmount = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnLinkInvoice = new System.Windows.Forms.Button();
            this.dgvAssociatedInvoices = new System.Windows.Forms.DataGridView();
            this.siInvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siCurrencySymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsInvoiceOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemUnlinkInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlCondition = new System.Windows.Forms.Panel();
            this.rbnIsBlack = new System.Windows.Forms.RadioButton();
            this.rbnIsWhite = new System.Windows.Forms.RadioButton();
            this.pnlLinkInvoices = new System.Windows.Forms.Panel();
            this.pnlLinkSales = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvAssociatedSales = new System.Windows.Forms.DataGridView();
            this.saSaleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saTotalBeforeTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saCurrencySymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsSaleOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenSale = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemUnlinkSale = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLinkSale = new System.Windows.Forms.Button();
            this.dateTimePickerFechaChequeDiferido = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociatedInvoices)).BeginInit();
            this.cmsInvoiceOptions.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            this.pnlLinkInvoices.SuspendLayout();
            this.pnlLinkSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociatedSales)).BeginInit();
            this.cmsSaleOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboCustomer
            // 
            this.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCustomer.DropDownWidth = 400;
            this.cboCustomer.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(330, 126);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(270, 31);
            this.cboCustomer.TabIndex = 2;
            this.cboCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.cboCustomer_Validating);
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
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(330, 89);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(270, 31);
            this.dtpDate.TabIndex = 21;
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
            // txtCustomerPaymentID
            // 
            this.txtCustomerPaymentID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerPaymentID.Location = new System.Drawing.Point(330, 15);
            this.txtCustomerPaymentID.Name = "txtCustomerPaymentID";
            this.txtCustomerPaymentID.ReadOnly = true;
            this.txtCustomerPaymentID.Size = new System.Drawing.Size(270, 31);
            this.txtCustomerPaymentID.TabIndex = 17;
            this.txtCustomerPaymentID.Text = "< Generado automáticamente >";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "Fecha:";
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
            this.label1.Size = new System.Drawing.Size(84, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "ID Cobro:";
            // 
            // pnlCustomerPaymentIcon
            // 
            this.pnlCustomerPaymentIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCustomerPaymentIcon.BackgroundImage")));
            this.pnlCustomerPaymentIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlCustomerPaymentIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlCustomerPaymentIcon.Name = "pnlCustomerPaymentIcon";
            this.pnlCustomerPaymentIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlCustomerPaymentIcon.TabIndex = 0;
            // 
            // txtAdditionalInformation
            // 
            this.txtAdditionalInformation.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdditionalInformation.Location = new System.Drawing.Point(117, 543);
            this.txtAdditionalInformation.MaxLength = 64;
            this.txtAdditionalInformation.Multiline = true;
            this.txtAdditionalInformation.Name = "txtAdditionalInformation";
            this.txtAdditionalInformation.Size = new System.Drawing.Size(483, 77);
            this.txtAdditionalInformation.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(117, 517);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(182, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "Información adicional:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(117, 417);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Cuenta:";
            // 
            // cboAccount
            // 
            this.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(330, 414);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(270, 31);
            this.cboAccount.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(117, 665);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 23);
            this.label9.TabIndex = 12;
            this.label9.Text = "Moneda:";
            // 
            // cboCurrency
            // 
            this.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(330, 663);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(270, 31);
            this.cboCurrency.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(117, 628);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 23);
            this.label8.TabIndex = 10;
            this.label8.Text = "Importe:";
            // 
            // nudTotalAmount
            // 
            this.nudTotalAmount.DecimalPlaces = 2;
            this.nudTotalAmount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTotalAmount.Location = new System.Drawing.Point(330, 626);
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
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(256, 737);
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
            this.btnAccept.Location = new System.Drawing.Point(462, 737);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 14;
            this.btnAccept.Text = "Guardar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnLinkInvoice
            // 
            this.btnLinkInvoice.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinkInvoice.Location = new System.Drawing.Point(286, 177);
            this.btnLinkInvoice.Name = "btnLinkInvoice";
            this.btnLinkInvoice.Size = new System.Drawing.Size(200, 31);
            this.btnLinkInvoice.TabIndex = 2;
            this.btnLinkInvoice.Text = "Asociar factura";
            this.btnLinkInvoice.UseVisualStyleBackColor = true;
            this.btnLinkInvoice.Click += new System.EventHandler(this.btnLinkInvoice_Click);
            // 
            // dgvAssociatedInvoices
            // 
            this.dgvAssociatedInvoices.AllowUserToAddRows = false;
            this.dgvAssociatedInvoices.AllowUserToDeleteRows = false;
            this.dgvAssociatedInvoices.AllowUserToResizeColumns = false;
            this.dgvAssociatedInvoices.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAssociatedInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAssociatedInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssociatedInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.siInvoiceNumber,
            this.siTotalAmount,
            this.siCurrencySymbol});
            this.dgvAssociatedInvoices.ContextMenuStrip = this.cmsInvoiceOptions;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAssociatedInvoices.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvAssociatedInvoices.Location = new System.Drawing.Point(7, 29);
            this.dgvAssociatedInvoices.MultiSelect = false;
            this.dgvAssociatedInvoices.Name = "dgvAssociatedInvoices";
            this.dgvAssociatedInvoices.ReadOnly = true;
            this.dgvAssociatedInvoices.RowHeadersVisible = false;
            this.dgvAssociatedInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssociatedInvoices.ShowCellErrors = false;
            this.dgvAssociatedInvoices.ShowCellToolTips = false;
            this.dgvAssociatedInvoices.ShowEditingIcon = false;
            this.dgvAssociatedInvoices.ShowRowErrors = false;
            this.dgvAssociatedInvoices.Size = new System.Drawing.Size(479, 142);
            this.dgvAssociatedInvoices.TabIndex = 1;
            // 
            // siInvoiceNumber
            // 
            this.siInvoiceNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.siInvoiceNumber.DataPropertyName = "InvoiceNumber";
            this.siInvoiceNumber.HeaderText = "N° de factura";
            this.siInvoiceNumber.Name = "siInvoiceNumber";
            this.siInvoiceNumber.ReadOnly = true;
            this.siInvoiceNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // siTotalAmount
            // 
            this.siTotalAmount.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            this.siTotalAmount.DefaultCellStyle = dataGridViewCellStyle12;
            this.siTotalAmount.HeaderText = "Importe";
            this.siTotalAmount.Name = "siTotalAmount";
            this.siTotalAmount.ReadOnly = true;
            this.siTotalAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.siTotalAmount.Width = 150;
            // 
            // siCurrencySymbol
            // 
            this.siCurrencySymbol.DataPropertyName = "CurrencySymbol";
            this.siCurrencySymbol.HeaderText = "Moneda";
            this.siCurrencySymbol.Name = "siCurrencySymbol";
            this.siCurrencySymbol.ReadOnly = true;
            this.siCurrencySymbol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.siCurrencySymbol.Width = 150;
            // 
            // cmsInvoiceOptions
            // 
            this.cmsInvoiceOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsInvoiceOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenInvoice,
            this.cmsItemUnlinkInvoice});
            this.cmsInvoiceOptions.Name = "cmsPurchaseOrderOptions";
            this.cmsInvoiceOptions.Size = new System.Drawing.Size(224, 60);
            // 
            // cmsItemOpenInvoice
            // 
            this.cmsItemOpenInvoice.Name = "cmsItemOpenInvoice";
            this.cmsItemOpenInvoice.Size = new System.Drawing.Size(223, 28);
            this.cmsItemOpenInvoice.Text = "Ver factura";
            this.cmsItemOpenInvoice.Click += new System.EventHandler(this.cmsItemOpenInvoice_Click);
            // 
            // cmsItemUnlinkInvoice
            // 
            this.cmsItemUnlinkInvoice.Name = "cmsItemUnlinkInvoice";
            this.cmsItemUnlinkInvoice.Size = new System.Drawing.Size(223, 28);
            this.cmsItemUnlinkInvoice.Text = "Desasociar factura";
            this.cmsItemUnlinkInvoice.Click += new System.EventHandler(this.cmsItemUnlinkInvoice_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Facturas asociadas:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(117, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 23);
            this.label10.TabIndex = 3;
            this.label10.Text = "Condición:";
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.rbnIsBlack);
            this.pnlCondition.Controls.Add(this.rbnIsWhite);
            this.pnlCondition.Location = new System.Drawing.Point(330, 163);
            this.pnlCondition.Name = "pnlCondition";
            this.pnlCondition.Size = new System.Drawing.Size(270, 31);
            this.pnlCondition.TabIndex = 4;
            // 
            // rbnIsBlack
            // 
            this.rbnIsBlack.AutoSize = true;
            this.rbnIsBlack.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsBlack.Location = new System.Drawing.Point(86, 3);
            this.rbnIsBlack.Name = "rbnIsBlack";
            this.rbnIsBlack.Size = new System.Drawing.Size(40, 27);
            this.rbnIsBlack.TabIndex = 1;
            this.rbnIsBlack.Text = "N";
            this.rbnIsBlack.UseVisualStyleBackColor = true;
            // 
            // rbnIsWhite
            // 
            this.rbnIsWhite.AutoSize = true;
            this.rbnIsWhite.Checked = true;
            this.rbnIsWhite.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsWhite.Location = new System.Drawing.Point(3, 3);
            this.rbnIsWhite.Name = "rbnIsWhite";
            this.rbnIsWhite.Size = new System.Drawing.Size(38, 27);
            this.rbnIsWhite.TabIndex = 0;
            this.rbnIsWhite.TabStop = true;
            this.rbnIsWhite.Text = "B";
            this.rbnIsWhite.UseVisualStyleBackColor = true;
            // 
            // pnlLinkInvoices
            // 
            this.pnlLinkInvoices.Controls.Add(this.label5);
            this.pnlLinkInvoices.Controls.Add(this.dgvAssociatedInvoices);
            this.pnlLinkInvoices.Controls.Add(this.btnLinkInvoice);
            this.pnlLinkInvoices.Location = new System.Drawing.Point(114, 200);
            this.pnlLinkInvoices.Name = "pnlLinkInvoices";
            this.pnlLinkInvoices.Size = new System.Drawing.Size(490, 212);
            this.pnlLinkInvoices.TabIndex = 5;
            // 
            // pnlLinkSales
            // 
            this.pnlLinkSales.Controls.Add(this.label11);
            this.pnlLinkSales.Controls.Add(this.dgvAssociatedSales);
            this.pnlLinkSales.Controls.Add(this.btnLinkSale);
            this.pnlLinkSales.Location = new System.Drawing.Point(114, 200);
            this.pnlLinkSales.Name = "pnlLinkSales";
            this.pnlLinkSales.Size = new System.Drawing.Size(490, 212);
            this.pnlLinkSales.TabIndex = 5;
            this.pnlLinkSales.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 23);
            this.label11.TabIndex = 0;
            this.label11.Text = "Ventas asociadas";
            // 
            // dgvAssociatedSales
            // 
            this.dgvAssociatedSales.AllowUserToAddRows = false;
            this.dgvAssociatedSales.AllowUserToDeleteRows = false;
            this.dgvAssociatedSales.AllowUserToResizeColumns = false;
            this.dgvAssociatedSales.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAssociatedSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAssociatedSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssociatedSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.saSaleID,
            this.saTotalBeforeTax,
            this.saCurrencySymbol});
            this.dgvAssociatedSales.ContextMenuStrip = this.cmsSaleOptions;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAssociatedSales.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvAssociatedSales.Location = new System.Drawing.Point(7, 29);
            this.dgvAssociatedSales.MultiSelect = false;
            this.dgvAssociatedSales.Name = "dgvAssociatedSales";
            this.dgvAssociatedSales.ReadOnly = true;
            this.dgvAssociatedSales.RowHeadersVisible = false;
            this.dgvAssociatedSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssociatedSales.ShowCellErrors = false;
            this.dgvAssociatedSales.ShowCellToolTips = false;
            this.dgvAssociatedSales.ShowEditingIcon = false;
            this.dgvAssociatedSales.ShowRowErrors = false;
            this.dgvAssociatedSales.Size = new System.Drawing.Size(479, 142);
            this.dgvAssociatedSales.TabIndex = 1;
            // 
            // saSaleID
            // 
            this.saSaleID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.saSaleID.DataPropertyName = "SaleID";
            dataGridViewCellStyle13.Format = "D8";
            this.saSaleID.DefaultCellStyle = dataGridViewCellStyle13;
            this.saSaleID.HeaderText = "ID Venta";
            this.saSaleID.Name = "saSaleID";
            this.saSaleID.ReadOnly = true;
            this.saSaleID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // saTotalBeforeTax
            // 
            this.saTotalBeforeTax.DataPropertyName = "TotalBeforeTax";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "N2";
            this.saTotalBeforeTax.DefaultCellStyle = dataGridViewCellStyle17;
            this.saTotalBeforeTax.HeaderText = "Importe";
            this.saTotalBeforeTax.Name = "saTotalBeforeTax";
            this.saTotalBeforeTax.ReadOnly = true;
            this.saTotalBeforeTax.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.saTotalBeforeTax.Width = 150;
            // 
            // saCurrencySymbol
            // 
            this.saCurrencySymbol.DataPropertyName = "CurrencySymbol";
            this.saCurrencySymbol.HeaderText = "Moneda";
            this.saCurrencySymbol.Name = "saCurrencySymbol";
            this.saCurrencySymbol.ReadOnly = true;
            this.saCurrencySymbol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.saCurrencySymbol.Width = 150;
            // 
            // cmsSaleOptions
            // 
            this.cmsSaleOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsSaleOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenSale,
            this.cmsItemUnlinkSale});
            this.cmsSaleOptions.Name = "cmsSaleOptions";
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
            // btnLinkSale
            // 
            this.btnLinkSale.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinkSale.Location = new System.Drawing.Point(286, 177);
            this.btnLinkSale.Name = "btnLinkSale";
            this.btnLinkSale.Size = new System.Drawing.Size(200, 31);
            this.btnLinkSale.TabIndex = 2;
            this.btnLinkSale.Text = "Asociar venta";
            this.btnLinkSale.UseVisualStyleBackColor = true;
            this.btnLinkSale.Click += new System.EventHandler(this.btnLinkSale_Click);
            // 
            // dateTimePickerFechaChequeDiferido
            // 
            this.dateTimePickerFechaChequeDiferido.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerFechaChequeDiferido.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerFechaChequeDiferido.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFechaChequeDiferido.Location = new System.Drawing.Point(330, 486);
            this.dateTimePickerFechaChequeDiferido.Name = "dateTimePickerFechaChequeDiferido";
            this.dateTimePickerFechaChequeDiferido.Size = new System.Drawing.Size(270, 31);
            this.dateTimePickerFechaChequeDiferido.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label12.Location = new System.Drawing.Point(330, 466);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(228, 17);
            this.label12.TabIndex = 24;
            this.label12.Text = "Selecciona fecha a cobrar cheque:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // CP_CustomerPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 786);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dateTimePickerFechaChequeDiferido);
            this.Controls.Add(this.pnlLinkSales);
            this.Controls.Add(this.pnlLinkInvoices);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pnlCondition);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.txtAdditionalInformation);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboAccount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudTotalAmount);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cboBusiness);
            this.Controls.Add(this.txtCustomerPaymentID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlCustomerPaymentIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CP_CustomerPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar cobro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CP_CustomerPayment_FormClosing);
            this.Load += new System.EventHandler(this.CP_CustomerPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociatedInvoices)).EndInit();
            this.cmsInvoiceOptions.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            this.pnlCondition.PerformLayout();
            this.pnlLinkInvoices.ResumeLayout(false);
            this.pnlLinkInvoices.PerformLayout();
            this.pnlLinkSales.ResumeLayout(false);
            this.pnlLinkSales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssociatedSales)).EndInit();
            this.cmsSaleOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.TextBox txtCustomerPaymentID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCustomerPaymentIcon;
        private System.Windows.Forms.TextBox txtAdditionalInformation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudTotalAmount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnLinkInvoice;
        private System.Windows.Forms.DataGridView dgvAssociatedInvoices;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip cmsInvoiceOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenInvoice;
        private System.Windows.Forms.ToolStripMenuItem cmsItemUnlinkInvoice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlCondition;
        private System.Windows.Forms.RadioButton rbnIsBlack;
        private System.Windows.Forms.RadioButton rbnIsWhite;
        private System.Windows.Forms.Panel pnlLinkInvoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn siInvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn siTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn siCurrencySymbol;
        private System.Windows.Forms.Panel pnlLinkSales;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvAssociatedSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn saSaleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn saTotalBeforeTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn saCurrencySymbol;
        private System.Windows.Forms.Button btnLinkSale;
        private System.Windows.Forms.ContextMenuStrip cmsSaleOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenSale;
        private System.Windows.Forms.ToolStripMenuItem cmsItemUnlinkSale;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaChequeDiferido;
        private System.Windows.Forms.Label label12;
    }
}