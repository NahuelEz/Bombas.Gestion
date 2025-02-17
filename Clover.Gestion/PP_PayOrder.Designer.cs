namespace Clover.Gestion
{
    partial class PP_PayOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PP_PayOrder));
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.txtPayOrderID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddPayment = new System.Windows.Forms.Button();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.paymentNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paymentTotalAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paymentCurrencySymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsPaymentOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemDeletePayment = new System.Windows.Forms.ToolStripMenuItem();
            this.cboProvider = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnMakePdf = new System.Windows.Forms.Button();
            this.btnLinkInvoice = new System.Windows.Forms.Button();
            this.dgvPurchaseInvoices = new System.Windows.Forms.DataGridView();
            this.invoiceNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceTotalAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceCurrencySymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsPurchaseInvoiceOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemUnlinkInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlPayOrderIcon = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.cmsPaymentOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseInvoices)).BeginInit();
            this.cmsPurchaseInvoiceOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(330, 89);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(270, 31);
            this.dtpDate.TabIndex = 17;
            // 
            // cboBusiness
            // 
            this.cboBusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusiness.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusiness.FormattingEnabled = true;
            this.cboBusiness.Location = new System.Drawing.Point(330, 52);
            this.cboBusiness.Name = "cboBusiness";
            this.cboBusiness.Size = new System.Drawing.Size(270, 31);
            this.cboBusiness.TabIndex = 15;
            // 
            // txtPayOrderID
            // 
            this.txtPayOrderID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayOrderID.Location = new System.Drawing.Point(330, 15);
            this.txtPayOrderID.Name = "txtPayOrderID";
            this.txtPayOrderID.ReadOnly = true;
            this.txtPayOrderID.Size = new System.Drawing.Size(270, 31);
            this.txtPayOrderID.TabIndex = 13;
            this.txtPayOrderID.Text = "< Generado automáticamente >";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 23);
            this.label2.TabIndex = 14;
            this.label2.Text = "Empresa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "ID Orden de pago:";
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPayment.Location = new System.Drawing.Point(400, 554);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(200, 31);
            this.btnAddPayment.TabIndex = 8;
            this.btnAddPayment.Text = "Agregar pago";
            this.btnAddPayment.UseVisualStyleBackColor = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            this.dgvPayments.AllowUserToResizeColumns = false;
            this.dgvPayments.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.paymentNameColumn,
            this.paymentTotalAmountColumn,
            this.paymentCurrencySymbolColumn});
            this.dgvPayments.ContextMenuStrip = this.cmsPaymentOptions;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPayments.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPayments.Location = new System.Drawing.Point(121, 406);
            this.dgvPayments.MultiSelect = false;
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersVisible = false;
            this.dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayments.ShowCellErrors = false;
            this.dgvPayments.ShowCellToolTips = false;
            this.dgvPayments.ShowEditingIcon = false;
            this.dgvPayments.ShowRowErrors = false;
            this.dgvPayments.Size = new System.Drawing.Size(479, 142);
            this.dgvPayments.TabIndex = 7;
            // 
            // paymentNameColumn
            // 
            this.paymentNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.paymentNameColumn.DataPropertyName = "PaymentName";
            this.paymentNameColumn.HeaderText = "Medio de pago";
            this.paymentNameColumn.Name = "paymentNameColumn";
            this.paymentNameColumn.ReadOnly = true;
            this.paymentNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // paymentTotalAmountColumn
            // 
            this.paymentTotalAmountColumn.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.paymentTotalAmountColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.paymentTotalAmountColumn.HeaderText = "Importe";
            this.paymentTotalAmountColumn.Name = "paymentTotalAmountColumn";
            this.paymentTotalAmountColumn.ReadOnly = true;
            this.paymentTotalAmountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.paymentTotalAmountColumn.Width = 150;
            // 
            // paymentCurrencySymbolColumn
            // 
            this.paymentCurrencySymbolColumn.DataPropertyName = "CurrencySymbol";
            this.paymentCurrencySymbolColumn.HeaderText = "Moneda";
            this.paymentCurrencySymbolColumn.Name = "paymentCurrencySymbolColumn";
            this.paymentCurrencySymbolColumn.ReadOnly = true;
            this.paymentCurrencySymbolColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.paymentCurrencySymbolColumn.Width = 150;
            // 
            // cmsPaymentOptions
            // 
            this.cmsPaymentOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsPaymentOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenPayment,
            this.cmsItemDeletePayment});
            this.cmsPaymentOptions.Name = "cmsPaymentOptions";
            this.cmsPaymentOptions.Size = new System.Drawing.Size(224, 60);
            // 
            // cmsItemOpenPayment
            // 
            this.cmsItemOpenPayment.Name = "cmsItemOpenPayment";
            this.cmsItemOpenPayment.Size = new System.Drawing.Size(223, 28);
            this.cmsItemOpenPayment.Text = "Ver medio de pago";
            this.cmsItemOpenPayment.Click += new System.EventHandler(this.cmsItemOpenPayment_Click);
            // 
            // cmsItemDeletePayment
            // 
            this.cmsItemDeletePayment.Name = "cmsItemDeletePayment";
            this.cmsItemDeletePayment.Size = new System.Drawing.Size(223, 28);
            this.cmsItemDeletePayment.Text = "Eliminar pago";
            this.cmsItemDeletePayment.Click += new System.EventHandler(this.cmsItemDeletePayment_Click);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(117, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Medios de pago:";
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
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(356, 629);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cerrar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(512, 629);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(150, 40);
            this.btnAccept.TabIndex = 9;
            this.btnAccept.Text = "Guardar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnMakePdf
            // 
            this.btnMakePdf.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakePdf.Location = new System.Drawing.Point(12, 629);
            this.btnMakePdf.Name = "btnMakePdf";
            this.btnMakePdf.Size = new System.Drawing.Size(150, 40);
            this.btnMakePdf.TabIndex = 11;
            this.btnMakePdf.Text = "Generar PDF";
            this.btnMakePdf.UseVisualStyleBackColor = true;
            this.btnMakePdf.Click += new System.EventHandler(this.btnMakePdf_Click);
            // 
            // btnLinkInvoice
            // 
            this.btnLinkInvoice.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinkInvoice.Location = new System.Drawing.Point(400, 340);
            this.btnLinkInvoice.Name = "btnLinkInvoice";
            this.btnLinkInvoice.Size = new System.Drawing.Size(200, 31);
            this.btnLinkInvoice.TabIndex = 5;
            this.btnLinkInvoice.Text = "Asociar factura";
            this.btnLinkInvoice.UseVisualStyleBackColor = true;
            this.btnLinkInvoice.Click += new System.EventHandler(this.btnLinkInvoice_Click);
            // 
            // dgvPurchaseInvoices
            // 
            this.dgvPurchaseInvoices.AllowUserToAddRows = false;
            this.dgvPurchaseInvoices.AllowUserToDeleteRows = false;
            this.dgvPurchaseInvoices.AllowUserToResizeColumns = false;
            this.dgvPurchaseInvoices.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPurchaseInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.invoiceNumberColumn,
            this.invoiceTotalAmountColumn,
            this.invoiceCurrencySymbolColumn});
            this.dgvPurchaseInvoices.ContextMenuStrip = this.cmsPurchaseInvoiceOptions;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseInvoices.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPurchaseInvoices.Location = new System.Drawing.Point(121, 192);
            this.dgvPurchaseInvoices.MultiSelect = false;
            this.dgvPurchaseInvoices.Name = "dgvPurchaseInvoices";
            this.dgvPurchaseInvoices.ReadOnly = true;
            this.dgvPurchaseInvoices.RowHeadersVisible = false;
            this.dgvPurchaseInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseInvoices.ShowCellErrors = false;
            this.dgvPurchaseInvoices.ShowCellToolTips = false;
            this.dgvPurchaseInvoices.ShowEditingIcon = false;
            this.dgvPurchaseInvoices.ShowRowErrors = false;
            this.dgvPurchaseInvoices.Size = new System.Drawing.Size(479, 142);
            this.dgvPurchaseInvoices.TabIndex = 4;
            // 
            // invoiceNumberColumn
            // 
            this.invoiceNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.invoiceNumberColumn.DataPropertyName = "InvoiceNumber";
            dataGridViewCellStyle5.Format = "D8";
            this.invoiceNumberColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.invoiceNumberColumn.HeaderText = "N° de factura";
            this.invoiceNumberColumn.Name = "invoiceNumberColumn";
            this.invoiceNumberColumn.ReadOnly = true;
            this.invoiceNumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // invoiceTotalAmountColumn
            // 
            this.invoiceTotalAmountColumn.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            this.invoiceTotalAmountColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.invoiceTotalAmountColumn.HeaderText = "Importe";
            this.invoiceTotalAmountColumn.Name = "invoiceTotalAmountColumn";
            this.invoiceTotalAmountColumn.ReadOnly = true;
            this.invoiceTotalAmountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.invoiceTotalAmountColumn.Width = 150;
            // 
            // invoiceCurrencySymbolColumn
            // 
            this.invoiceCurrencySymbolColumn.DataPropertyName = "CurrencySymbol";
            this.invoiceCurrencySymbolColumn.HeaderText = "Moneda";
            this.invoiceCurrencySymbolColumn.Name = "invoiceCurrencySymbolColumn";
            this.invoiceCurrencySymbolColumn.ReadOnly = true;
            this.invoiceCurrencySymbolColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.invoiceCurrencySymbolColumn.Width = 150;
            // 
            // cmsPurchaseInvoiceOptions
            // 
            this.cmsPurchaseInvoiceOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsPurchaseInvoiceOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenInvoice,
            this.cmsItemUnlinkInvoice});
            this.cmsPurchaseInvoiceOptions.Name = "cmsPurchaseInvoiceOptions";
            this.cmsPurchaseInvoiceOptions.Size = new System.Drawing.Size(224, 60);
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
            this.label5.Location = new System.Drawing.Point(117, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 23);
            this.label5.TabIndex = 3;
            this.label5.Text = "Facturas asociadas:";
            // 
            // pnlPayOrderIcon
            // 
            this.pnlPayOrderIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPayOrderIcon.BackgroundImage")));
            this.pnlPayOrderIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlPayOrderIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlPayOrderIcon.Name = "pnlPayOrderIcon";
            this.pnlPayOrderIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlPayOrderIcon.TabIndex = 0;
            // 
            // PP_PayOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 681);
            this.Controls.Add(this.btnLinkInvoice);
            this.Controls.Add(this.dgvPurchaseInvoices);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnMakePdf);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnAddPayment);
            this.Controls.Add(this.dgvPayments);
            this.Controls.Add(this.cboProvider);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cboBusiness);
            this.Controls.Add(this.txtPayOrderID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlPayOrderIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PP_PayOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar orden de pago";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PP_PayOrder_FormClosing);
            this.Load += new System.EventHandler(this.PP_PayOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.cmsPaymentOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseInvoices)).EndInit();
            this.cmsPurchaseInvoiceOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPayOrderIcon;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.TextBox txtPayOrderID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddPayment;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.ComboBox cboProvider;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.ContextMenuStrip cmsPaymentOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenPayment;
        private System.Windows.Forms.ToolStripMenuItem cmsItemDeletePayment;
        private System.Windows.Forms.Button btnMakePdf;
        private System.Windows.Forms.Button btnLinkInvoice;
        private System.Windows.Forms.DataGridView dgvPurchaseInvoices;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentTotalAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentCurrencySymbolColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceTotalAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceCurrencySymbolColumn;
        private System.Windows.Forms.ContextMenuStrip cmsPurchaseInvoiceOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenInvoice;
        private System.Windows.Forms.ToolStripMenuItem cmsItemUnlinkInvoice;
    }
}