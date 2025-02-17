namespace Clover.Gestion
{
    partial class ES_Items
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ES_Items));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.descColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vatPercentageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsItemOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddConcept = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnItemFinder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.cmsItemOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeColumns = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.quantityColumn,
            this.imageColumn,
            this.descColumn,
            this.vatPercentageColumn,
            this.amountColumn,
            this.totalAmountColumn});
            this.dgvItems.ContextMenuStrip = this.cmsItemOptions;
            this.dgvItems.Location = new System.Drawing.Point(12, 12);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(203)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.ShowCellToolTips = false;
            this.dgvItems.Size = new System.Drawing.Size(760, 491);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvItems_CellFormatting);
            this.dgvItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvItems_MouseDown);
            // 
            // quantityColumn
            // 
            this.quantityColumn.DataPropertyName = "Quantity";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle10.Format = "N2";
            this.quantityColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.quantityColumn.HeaderText = "Cant.";
            this.quantityColumn.Name = "quantityColumn";
            this.quantityColumn.ReadOnly = true;
            this.quantityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.quantityColumn.Width = 80;
            // 
            // imageColumn
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle11.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle11.NullValue")));
            this.imageColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.imageColumn.HeaderText = "Imagen";
            this.imageColumn.Name = "imageColumn";
            this.imageColumn.ReadOnly = true;
            this.imageColumn.Width = 125;
            // 
            // descColumn
            // 
            this.descColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descColumn.DataPropertyName = "Description";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.descColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.descColumn.HeaderText = "Detalle";
            this.descColumn.Name = "descColumn";
            this.descColumn.ReadOnly = true;
            this.descColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // vatPercentageColumn
            // 
            this.vatPercentageColumn.DataPropertyName = "VatPercentage";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle13.Format = "N2";
            this.vatPercentageColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.vatPercentageColumn.HeaderText = "IVA (%)";
            this.vatPercentageColumn.Name = "vatPercentageColumn";
            this.vatPercentageColumn.ReadOnly = true;
            this.vatPercentageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.vatPercentageColumn.Width = 105;
            // 
            // amountColumn
            // 
            this.amountColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle14.Format = "N2";
            this.amountColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.amountColumn.HeaderText = "P. unitario";
            this.amountColumn.Name = "amountColumn";
            this.amountColumn.ReadOnly = true;
            this.amountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.amountColumn.Width = 105;
            // 
            // totalAmountColumn
            // 
            this.totalAmountColumn.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle15.Format = "N2";
            this.totalAmountColumn.DefaultCellStyle = dataGridViewCellStyle15;
            this.totalAmountColumn.HeaderText = "Subtotal";
            this.totalAmountColumn.Name = "totalAmountColumn";
            this.totalAmountColumn.ReadOnly = true;
            this.totalAmountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.totalAmountColumn.Width = 105;
            // 
            // cmsItemOptions
            // 
            this.cmsItemOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsItemOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsEdit,
            this.cmsCopy,
            this.cmsPaste,
            this.cmsMoveUp,
            this.cmsMoveDown,
            this.cmsRemove});
            this.cmsItemOptions.Name = "cmsItemOptions";
            this.cmsItemOptions.Size = new System.Drawing.Size(182, 172);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(181, 28);
            this.cmsEdit.Text = "Modificar";
            this.cmsEdit.Click += new System.EventHandler(this.cmsEdit_Click);
            // 
            // cmsCopy
            // 
            this.cmsCopy.Name = "cmsCopy";
            this.cmsCopy.Size = new System.Drawing.Size(181, 28);
            this.cmsCopy.Text = "Copiar";
            this.cmsCopy.Click += new System.EventHandler(this.cmsCopy_Click);
            // 
            // cmsPaste
            // 
            this.cmsPaste.Name = "cmsPaste";
            this.cmsPaste.Size = new System.Drawing.Size(181, 28);
            this.cmsPaste.Text = "Pegar";
            this.cmsPaste.Click += new System.EventHandler(this.cmsPaste_Click);
            // 
            // cmsMoveUp
            // 
            this.cmsMoveUp.Name = "cmsMoveUp";
            this.cmsMoveUp.Size = new System.Drawing.Size(181, 28);
            this.cmsMoveUp.Text = "Mover arriba";
            this.cmsMoveUp.Click += new System.EventHandler(this.cmsMoveUp_Click);
            // 
            // cmsMoveDown
            // 
            this.cmsMoveDown.Name = "cmsMoveDown";
            this.cmsMoveDown.Size = new System.Drawing.Size(181, 28);
            this.cmsMoveDown.Text = "Mover abajo";
            this.cmsMoveDown.Click += new System.EventHandler(this.cmsMoveDown_Click);
            // 
            // cmsRemove
            // 
            this.cmsRemove.Name = "cmsRemove";
            this.cmsRemove.Size = new System.Drawing.Size(181, 28);
            this.cmsRemove.Text = "Quitar";
            this.cmsRemove.Click += new System.EventHandler(this.cmsRemove_Click);
            // 
            // btnAddConcept
            // 
            this.btnAddConcept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddConcept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddConcept.Location = new System.Drawing.Point(366, 509);
            this.btnAddConcept.Name = "btnAddConcept";
            this.btnAddConcept.Size = new System.Drawing.Size(200, 40);
            this.btnAddConcept.TabIndex = 2;
            this.btnAddConcept.Text = "Agregar concepto";
            this.btnAddConcept.UseVisualStyleBackColor = true;
            this.btnAddConcept.Click += new System.EventHandler(this.btnAddConcept_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProduct.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.Location = new System.Drawing.Point(572, 509);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(200, 40);
            this.btnAddProduct.TabIndex = 1;
            this.btnAddProduct.Text = "Agregar producto";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnItemFinder
            // 
            this.btnItemFinder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItemFinder.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemFinder.Location = new System.Drawing.Point(12, 509);
            this.btnItemFinder.Name = "btnItemFinder";
            this.btnItemFinder.Size = new System.Drawing.Size(200, 40);
            this.btnItemFinder.TabIndex = 3;
            this.btnItemFinder.Text = "Buscar existentes";
            this.btnItemFinder.UseVisualStyleBackColor = true;
            this.btnItemFinder.Click += new System.EventHandler(this.btnItemFinder_Click);
            // 
            // ES_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnItemFinder);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.btnAddConcept);
            this.Controls.Add(this.dgvItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ES_Items";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.cmsItemOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Button btnAddConcept;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.ContextMenuStrip cmsItemOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsMoveUp;
        private System.Windows.Forms.ToolStripMenuItem cmsMoveDown;
        private System.Windows.Forms.ToolStripMenuItem cmsEdit;
        private System.Windows.Forms.ToolStripMenuItem cmsRemove;
        private System.Windows.Forms.ToolStripMenuItem cmsCopy;
        private System.Windows.Forms.ToolStripMenuItem cmsPaste;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vatPercentageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAmountColumn;
        private System.Windows.Forms.Button btnItemFinder;
    }
}