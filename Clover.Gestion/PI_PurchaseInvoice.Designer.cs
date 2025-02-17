namespace Clover.Gestion
{
    partial class PI_PurchaseInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PI_PurchaseInvoice));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPurchaseInvoiceID = new System.Windows.Forms.TextBox();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.cboProvider = new System.Windows.Forms.ComboBox();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.cboInvoiceType = new System.Windows.Forms.ComboBox();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.nudTotalAmount = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.dgvPurchaseOrders = new System.Windows.Forms.DataGridView();
            this.purchaseOrderIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalBeforeTaxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencySymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsPurchaseOrderOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenPurchaseOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemUnlinkPurchaseOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLinkPurchaseOrder = new System.Windows.Forms.Button();
            this.pnlInvoiceIcon = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrders)).BeginInit();
            this.cmsPurchaseOrderOptions.SuspendLayout();
            this.SuspendLayout();
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(117, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 23);
            this.label5.TabIndex = 3;
            this.label5.Text = "Órdenes de compra asociadas:";
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
            // txtPurchaseInvoiceID
            // 
            this.txtPurchaseInvoiceID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseInvoiceID.Location = new System.Drawing.Point(330, 15);
            this.txtPurchaseInvoiceID.Name = "txtPurchaseInvoiceID";
            this.txtPurchaseInvoiceID.ReadOnly = true;
            this.txtPurchaseInvoiceID.Size = new System.Drawing.Size(270, 31);
            this.txtPurchaseInvoiceID.TabIndex = 17;
            this.txtPurchaseInvoiceID.Text = "< Generado automáticamente >";
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
            // cboProvider
            // 
            this.cboProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboProvider.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProvider.FormattingEnabled = true;
            this.cboProvider.Location = new System.Drawing.Point(330, 126);
            this.cboProvider.Name = "cboProvider";
            this.cboProvider.Size = new System.Drawing.Size(270, 31);
            this.cboProvider.TabIndex = 2;
            this.cboProvider.Validating += new System.ComponentModel.CancelEventHandler(this.cboProvider_Validating);
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
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNumber.Location = new System.Drawing.Point(330, 414);
            this.txtInvoiceNumber.MaxLength = 32;
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(270, 31);
            this.txtInvoiceNumber.TabIndex = 9;
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
            // dgvPurchaseOrders
            // 
            this.dgvPurchaseOrders.AllowUserToAddRows = false;
            this.dgvPurchaseOrders.AllowUserToDeleteRows = false;
            this.dgvPurchaseOrders.AllowUserToResizeColumns = false;
            this.dgvPurchaseOrders.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPurchaseOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.purchaseOrderIdColumn,
            this.totalBeforeTaxColumn,
            this.currencySymbolColumn});
            this.dgvPurchaseOrders.ContextMenuStrip = this.cmsPurchaseOrderOptions;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseOrders.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPurchaseOrders.Location = new System.Drawing.Point(121, 192);
            this.dgvPurchaseOrders.MultiSelect = false;
            this.dgvPurchaseOrders.Name = "dgvPurchaseOrders";
            this.dgvPurchaseOrders.ReadOnly = true;
            this.dgvPurchaseOrders.RowHeadersVisible = false;
            this.dgvPurchaseOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseOrders.ShowCellErrors = false;
            this.dgvPurchaseOrders.ShowCellToolTips = false;
            this.dgvPurchaseOrders.ShowEditingIcon = false;
            this.dgvPurchaseOrders.ShowRowErrors = false;
            this.dgvPurchaseOrders.Size = new System.Drawing.Size(479, 142);
            this.dgvPurchaseOrders.TabIndex = 4;
            // 
            // purchaseOrderIdColumn
            // 
            this.purchaseOrderIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.purchaseOrderIdColumn.DataPropertyName = "PurchaseOrderID";
            dataGridViewCellStyle2.Format = "D8";
            this.purchaseOrderIdColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.purchaseOrderIdColumn.HeaderText = "ID Orden";
            this.purchaseOrderIdColumn.Name = "purchaseOrderIdColumn";
            this.purchaseOrderIdColumn.ReadOnly = true;
            this.purchaseOrderIdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // cmsPurchaseOrderOptions
            // 
            this.cmsPurchaseOrderOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsPurchaseOrderOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenPurchaseOrder,
            this.cmsItemUnlinkPurchaseOrder});
            this.cmsPurchaseOrderOptions.Name = "cmsPurchaseOrderOptions";
            this.cmsPurchaseOrderOptions.Size = new System.Drawing.Size(301, 60);
            // 
            // cmsItemOpenPurchaseOrder
            // 
            this.cmsItemOpenPurchaseOrder.Name = "cmsItemOpenPurchaseOrder";
            this.cmsItemOpenPurchaseOrder.Size = new System.Drawing.Size(300, 28);
            this.cmsItemOpenPurchaseOrder.Text = "Ver orden de compra";
            this.cmsItemOpenPurchaseOrder.Click += new System.EventHandler(this.cmsItemOpenPurchaseOrder_Click);
            // 
            // cmsItemUnlinkPurchaseOrder
            // 
            this.cmsItemUnlinkPurchaseOrder.Name = "cmsItemUnlinkPurchaseOrder";
            this.cmsItemUnlinkPurchaseOrder.Size = new System.Drawing.Size(300, 28);
            this.cmsItemUnlinkPurchaseOrder.Text = "Desasociar orden de compra";
            this.cmsItemUnlinkPurchaseOrder.Click += new System.EventHandler(this.cmsItemUnlinkPurchaseOrder_Click);
            // 
            // btnLinkPurchaseOrder
            // 
            this.btnLinkPurchaseOrder.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinkPurchaseOrder.Location = new System.Drawing.Point(400, 340);
            this.btnLinkPurchaseOrder.Name = "btnLinkPurchaseOrder";
            this.btnLinkPurchaseOrder.Size = new System.Drawing.Size(200, 31);
            this.btnLinkPurchaseOrder.TabIndex = 5;
            this.btnLinkPurchaseOrder.Text = "Asociar orden";
            this.btnLinkPurchaseOrder.UseVisualStyleBackColor = true;
            this.btnLinkPurchaseOrder.Click += new System.EventHandler(this.btnLinkPurchaseOrder_Click);
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
            // PI_PurchaseInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 616);
            this.Controls.Add(this.btnLinkPurchaseOrder);
            this.Controls.Add(this.dgvPurchaseOrders);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.nudTotalAmount);
            this.Controls.Add(this.txtInvoiceNumber);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.cboInvoiceType);
            this.Controls.Add(this.dtpInvoiceDate);
            this.Controls.Add(this.cboProvider);
            this.Controls.Add(this.cboBusiness);
            this.Controls.Add(this.txtPurchaseInvoiceID);
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
            this.Name = "PI_PurchaseInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar factura de proveedor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PI_PurchaseInvoice_FormClosing);
            this.Load += new System.EventHandler(this.PI_PurchaseInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrders)).EndInit();
            this.cmsPurchaseOrderOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlInvoiceIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPurchaseInvoiceID;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.ComboBox cboProvider;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.ComboBox cboInvoiceType;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.NumericUpDown nudTotalAmount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.DataGridView dgvPurchaseOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseOrderIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalBeforeTaxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencySymbolColumn;
        private System.Windows.Forms.Button btnLinkPurchaseOrder;
        private System.Windows.Forms.ContextMenuStrip cmsPurchaseOrderOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenPurchaseOrder;
        private System.Windows.Forms.ToolStripMenuItem cmsItemUnlinkPurchaseOrder;
    }
}