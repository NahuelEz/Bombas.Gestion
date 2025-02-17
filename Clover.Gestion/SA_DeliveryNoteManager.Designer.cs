namespace Clover.Gestion
{
    partial class SA_DeliveryNoteManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SA_DeliveryNoteManager));
            this.dgvDeliveryNotes = new System.Windows.Forms.DataGridView();
            this.numberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsDeliveryNoteOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemShowDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemDeleteDeliveryNote = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMakeNote = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryNotes)).BeginInit();
            this.cmsDeliveryNoteOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDeliveryNotes
            // 
            this.dgvDeliveryNotes.AllowUserToAddRows = false;
            this.dgvDeliveryNotes.AllowUserToDeleteRows = false;
            this.dgvDeliveryNotes.AllowUserToResizeColumns = false;
            this.dgvDeliveryNotes.AllowUserToResizeRows = false;
            this.dgvDeliveryNotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDeliveryNotes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeliveryNotes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDeliveryNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliveryNotes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numberColumn,
            this.invoiceTypeColumn,
            this.invoiceNumberColumn,
            this.dateColumn});
            this.dgvDeliveryNotes.ContextMenuStrip = this.cmsDeliveryNoteOptions;
            this.dgvDeliveryNotes.Location = new System.Drawing.Point(12, 12);
            this.dgvDeliveryNotes.MultiSelect = false;
            this.dgvDeliveryNotes.Name = "dgvDeliveryNotes";
            this.dgvDeliveryNotes.ReadOnly = true;
            this.dgvDeliveryNotes.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.NullValue = "-";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvDeliveryNotes.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDeliveryNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeliveryNotes.ShowCellToolTips = false;
            this.dgvDeliveryNotes.Size = new System.Drawing.Size(760, 291);
            this.dgvDeliveryNotes.TabIndex = 0;
            this.dgvDeliveryNotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvDeliveryNotes_MouseDown);
            // 
            // numberColumn
            // 
            this.numberColumn.DataPropertyName = "Number";
            this.numberColumn.HeaderText = "N° de remito";
            this.numberColumn.Name = "numberColumn";
            this.numberColumn.ReadOnly = true;
            this.numberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // invoiceTypeColumn
            // 
            this.invoiceTypeColumn.DataPropertyName = "InvoiceType";
            this.invoiceTypeColumn.HeaderText = "Tipo factura";
            this.invoiceTypeColumn.Name = "invoiceTypeColumn";
            this.invoiceTypeColumn.ReadOnly = true;
            this.invoiceTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // invoiceNumberColumn
            // 
            this.invoiceNumberColumn.DataPropertyName = "MaskedInvoiceNumber";
            this.invoiceNumberColumn.HeaderText = "N° factura";
            this.invoiceNumberColumn.Name = "invoiceNumberColumn";
            this.invoiceNumberColumn.ReadOnly = true;
            this.invoiceNumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dateColumn
            // 
            this.dateColumn.DataPropertyName = "PrintingDate";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.dateColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.dateColumn.HeaderText = "Fecha";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cmsDeliveryNoteOptions
            // 
            this.cmsDeliveryNoteOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsDeliveryNoteOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemShowDetail,
            this.cmsItemDeleteDeliveryNote});
            this.cmsDeliveryNoteOptions.Name = "cmsDeliveryNoteOptions";
            this.cmsDeliveryNoteOptions.Size = new System.Drawing.Size(198, 82);
            // 
            // cmsItemShowDetail
            // 
            this.cmsItemShowDetail.Name = "cmsItemShowDetail";
            this.cmsItemShowDetail.Size = new System.Drawing.Size(197, 28);
            this.cmsItemShowDetail.Text = "Ver remito";
            this.cmsItemShowDetail.Click += new System.EventHandler(this.cmsItemShowDetail_Click);
            // 
            // cmsItemDeleteDeliveryNote
            // 
            this.cmsItemDeleteDeliveryNote.Name = "cmsItemDeleteDeliveryNote";
            this.cmsItemDeleteDeliveryNote.Size = new System.Drawing.Size(197, 28);
            this.cmsItemDeleteDeliveryNote.Text = "Eliminar remito";
            this.cmsItemDeleteDeliveryNote.Click += new System.EventHandler(this.cmsItemDeleteDeliveryNote_Click);
            // 
            // btnMakeNote
            // 
            this.btnMakeNote.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakeNote.Location = new System.Drawing.Point(572, 309);
            this.btnMakeNote.Name = "btnMakeNote";
            this.btnMakeNote.Size = new System.Drawing.Size(200, 40);
            this.btnMakeNote.TabIndex = 1;
            this.btnMakeNote.Text = "Generar remito";
            this.btnMakeNote.UseVisualStyleBackColor = true;
            this.btnMakeNote.Click += new System.EventHandler(this.btnMakeNote_Click);
            // 
            // SA_DeliveryNoteManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.btnMakeNote);
            this.Controls.Add(this.dgvDeliveryNotes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SA_DeliveryNoteManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestor de remitos";
            this.Load += new System.EventHandler(this.SA_DeliveryNoteManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryNotes)).EndInit();
            this.cmsDeliveryNoteOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeliveryNotes;
        private System.Windows.Forms.Button btnMakeNote;
        private System.Windows.Forms.ContextMenuStrip cmsDeliveryNoteOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemShowDetail;
        private System.Windows.Forms.ToolStripMenuItem cmsItemDeleteDeliveryNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
    }
}