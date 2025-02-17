namespace Clover.Gestion
{
    partial class PO_Items
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PO_Items));
            this.btnAddInput = new System.Windows.Forms.Button();
            this.btnAddConcept = new System.Windows.Forms.Button();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.cmsItemOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddInput
            // 
            this.btnAddInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddInput.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddInput.Location = new System.Drawing.Point(572, 509);
            this.btnAddInput.Name = "btnAddInput";
            this.btnAddInput.Size = new System.Drawing.Size(200, 40);
            this.btnAddInput.TabIndex = 1;
            this.btnAddInput.Text = "Agregar insumo";
            this.btnAddInput.UseVisualStyleBackColor = true;
            this.btnAddInput.Click += new System.EventHandler(this.btnAddInput_Click);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.quantityColumn,
            this.descriptionColumn,
            this.vatPercentageColumn,
            this.amountColumn,
            this.totalAmountColumn});
            this.dgvItems.ContextMenuStrip = this.cmsItemOptions;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItems.Location = new System.Drawing.Point(12, 12);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvItems.RowHeadersVisible = false;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.ShowCellErrors = false;
            this.dgvItems.ShowCellToolTips = false;
            this.dgvItems.ShowEditingIcon = false;
            this.dgvItems.ShowRowErrors = false;
            this.dgvItems.Size = new System.Drawing.Size(760, 491);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvItems_MouseDown);
            // 
            // quantityColumn
            // 
            this.quantityColumn.DataPropertyName = "Quantity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.Format = "N2";
            this.quantityColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.quantityColumn.HeaderText = "Cant.";
            this.quantityColumn.Name = "quantityColumn";
            this.quantityColumn.ReadOnly = true;
            this.quantityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.quantityColumn.Width = 80;
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionColumn.DataPropertyName = "Description";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.descriptionColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.descriptionColumn.HeaderText = "Detalle";
            this.descriptionColumn.Name = "descriptionColumn";
            this.descriptionColumn.ReadOnly = true;
            this.descriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // vatPercentageColumn
            // 
            this.vatPercentageColumn.DataPropertyName = "VatPercentage";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.Format = "N2";
            this.vatPercentageColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.vatPercentageColumn.HeaderText = "IVA (%)";
            this.vatPercentageColumn.Name = "vatPercentageColumn";
            this.vatPercentageColumn.ReadOnly = true;
            this.vatPercentageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.vatPercentageColumn.Width = 105;
            // 
            // amountColumn
            // 
            this.amountColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N2";
            this.amountColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.amountColumn.HeaderText = "P. unitario";
            this.amountColumn.Name = "amountColumn";
            this.amountColumn.ReadOnly = true;
            this.amountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.amountColumn.Width = 105;
            // 
            // totalAmountColumn
            // 
            this.totalAmountColumn.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N2";
            this.totalAmountColumn.DefaultCellStyle = dataGridViewCellStyle6;
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
            // PO_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.btnAddInput);
            this.Controls.Add(this.btnAddConcept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "PO_Items";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.cmsItemOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddInput;
        private System.Windows.Forms.Button btnAddConcept;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.ContextMenuStrip cmsItemOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsCopy;
        private System.Windows.Forms.ToolStripMenuItem cmsPaste;
        private System.Windows.Forms.ToolStripMenuItem cmsMoveUp;
        private System.Windows.Forms.ToolStripMenuItem cmsMoveDown;
        private System.Windows.Forms.ToolStripMenuItem cmsEdit;
        private System.Windows.Forms.ToolStripMenuItem cmsRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vatPercentageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAmountColumn;
    }
}