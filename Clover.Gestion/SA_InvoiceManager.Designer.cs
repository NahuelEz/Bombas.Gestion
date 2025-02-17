namespace Clover.Gestion
{
    partial class SA_InvoiceManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SA_InvoiceManager));
            this.btnAddInvoice = new System.Windows.Forms.Button();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.invoiceNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsInvoiceOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemDeleteInvoice = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.cmsInvoiceOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddInvoice
            // 
            this.btnAddInvoice.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddInvoice.Location = new System.Drawing.Point(572, 309);
            this.btnAddInvoice.Name = "btnAddInvoice";
            this.btnAddInvoice.Size = new System.Drawing.Size(200, 40);
            this.btnAddInvoice.TabIndex = 1;
            this.btnAddInvoice.Text = "Generar factura";
            this.btnAddInvoice.UseVisualStyleBackColor = true;
            this.btnAddInvoice.Click += new System.EventHandler(this.btnAddInvoice_Click);
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.AllowUserToResizeColumns = false;
            this.dgvInvoices.AllowUserToResizeRows = false;
            this.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoices.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.invoiceNumberColumn,
            this.dateColumn,
            this.totalAmountColumn,
            this.currencyColumn});
            this.dgvInvoices.ContextMenuStrip = this.cmsInvoiceOptions;
            this.dgvInvoices.Location = new System.Drawing.Point(12, 12);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.ReadOnly = true;
            this.dgvInvoices.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.NullValue = "-";
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvInvoices.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.ShowCellToolTips = false;
            this.dgvInvoices.Size = new System.Drawing.Size(760, 291);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvInvoices_MouseDown);
            // 
            // invoiceNumberColumn
            // 
            this.invoiceNumberColumn.DataPropertyName = "InvoiceNumber";
            this.invoiceNumberColumn.HeaderText = "N° de factura";
            this.invoiceNumberColumn.Name = "invoiceNumberColumn";
            this.invoiceNumberColumn.ReadOnly = true;
            this.invoiceNumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dateColumn
            // 
            this.dateColumn.DataPropertyName = "InvoiceDate";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.dateColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.dateColumn.HeaderText = "Fecha";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // totalAmountColumn
            // 
            this.totalAmountColumn.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.totalAmountColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.totalAmountColumn.HeaderText = "Importe";
            this.totalAmountColumn.Name = "totalAmountColumn";
            this.totalAmountColumn.ReadOnly = true;
            this.totalAmountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // currencyColumn
            // 
            this.currencyColumn.DataPropertyName = "CurrencySymbol";
            this.currencyColumn.HeaderText = "Moneda";
            this.currencyColumn.Name = "currencyColumn";
            this.currencyColumn.ReadOnly = true;
            this.currencyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cmsInvoiceOptions
            // 
            this.cmsInvoiceOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsInvoiceOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenInvoice,
            this.cmsItemDeleteInvoice});
            this.cmsInvoiceOptions.Name = "cmsInvoiceOptions";
            this.cmsInvoiceOptions.Size = new System.Drawing.Size(202, 60);
            // 
            // cmsItemOpenInvoice
            // 
            this.cmsItemOpenInvoice.Name = "cmsItemOpenInvoice";
            this.cmsItemOpenInvoice.Size = new System.Drawing.Size(201, 28);
            this.cmsItemOpenInvoice.Text = "Ver factura";
            this.cmsItemOpenInvoice.Click += new System.EventHandler(this.cmsItemOpenInvoice_Click);
            // 
            // cmsItemDeleteInvoice
            // 
            this.cmsItemDeleteInvoice.Name = "cmsItemDeleteInvoice";
            this.cmsItemDeleteInvoice.Size = new System.Drawing.Size(201, 28);
            this.cmsItemDeleteInvoice.Text = "Eliminar factura";
            this.cmsItemDeleteInvoice.Click += new System.EventHandler(this.cmsItemDeleteInvoice_Click);
            // 
            // SA_InvoiceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.btnAddInvoice);
            this.Controls.Add(this.dgvInvoices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SA_InvoiceManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestor de facturas";
            this.Load += new System.EventHandler(this.SA_InvoiceManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.cmsInvoiceOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddInvoice;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencyColumn;
        private System.Windows.Forms.ContextMenuStrip cmsInvoiceOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenInvoice;
        private System.Windows.Forms.ToolStripMenuItem cmsItemDeleteInvoice;
    }
}