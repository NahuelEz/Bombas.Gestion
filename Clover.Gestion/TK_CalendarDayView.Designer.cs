namespace Clover.Gestion
{
    partial class TK_CalendarDayView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TK_CalendarDayView));
            this.lblDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priorityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmsTaskOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemOpenTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemSetTaskAsCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemSetTaskAsNonCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemDeleteTask = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAccept = new System.Windows.Forms.Button();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.cmsShipmentOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemSetShipmentAsShipped = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemSetShipmentAsPending = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemGoToSaleFromShipment = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemGoToCustomerFromShipment = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExportTasks = new System.Windows.Forms.Button();
            this.customerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saleIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shippingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.cmsTaskOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.cmsShipmentOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(12, 9);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(37, 29);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "<>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tareas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Envíos programados:";
            // 
            // dgvTasks
            // 
            this.dgvTasks.AllowUserToAddRows = false;
            this.dgvTasks.AllowUserToDeleteRows = false;
            this.dgvTasks.AllowUserToResizeColumns = false;
            this.dgvTasks.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTasks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.descriptionColumn,
            this.priorityColumn,
            this.completedColumn});
            this.dgvTasks.ContextMenuStrip = this.cmsTaskOptions;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTasks.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTasks.Location = new System.Drawing.Point(12, 79);
            this.dgvTasks.MultiSelect = false;
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.ReadOnly = true;
            this.dgvTasks.RowHeadersVisible = false;
            this.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTasks.ShowCellErrors = false;
            this.dgvTasks.ShowCellToolTips = false;
            this.dgvTasks.ShowEditingIcon = false;
            this.dgvTasks.ShowRowErrors = false;
            this.dgvTasks.Size = new System.Drawing.Size(760, 195);
            this.dgvTasks.TabIndex = 2;
            this.dgvTasks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvDelta_MouseDown);
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionColumn.DataPropertyName = "Description";
            this.descriptionColumn.HeaderText = "Descripción";
            this.descriptionColumn.Name = "descriptionColumn";
            this.descriptionColumn.ReadOnly = true;
            this.descriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priorityColumn
            // 
            this.priorityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.priorityColumn.DataPropertyName = "Priority";
            this.priorityColumn.HeaderText = "Prioridad";
            this.priorityColumn.Name = "priorityColumn";
            this.priorityColumn.ReadOnly = true;
            this.priorityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.priorityColumn.Width = 110;
            // 
            // completedColumn
            // 
            this.completedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.completedColumn.DataPropertyName = "Completed";
            this.completedColumn.HeaderText = "Completada";
            this.completedColumn.Name = "completedColumn";
            this.completedColumn.ReadOnly = true;
            this.completedColumn.Width = 110;
            // 
            // cmsTaskOptions
            // 
            this.cmsTaskOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsTaskOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemOpenTask,
            this.cmsItemSetTaskAsCompleted,
            this.cmsItemSetTaskAsNonCompleted,
            this.cmsItemDeleteTask});
            this.cmsTaskOptions.Name = "cmsTaskOptions";
            this.cmsTaskOptions.Size = new System.Drawing.Size(278, 116);
            // 
            // cmsItemOpenTask
            // 
            this.cmsItemOpenTask.Name = "cmsItemOpenTask";
            this.cmsItemOpenTask.Size = new System.Drawing.Size(277, 28);
            this.cmsItemOpenTask.Text = "Visualizar tarea";
            this.cmsItemOpenTask.Click += new System.EventHandler(this.cmsItemOpenTask_Click);
            // 
            // cmsItemSetTaskAsCompleted
            // 
            this.cmsItemSetTaskAsCompleted.Name = "cmsItemSetTaskAsCompleted";
            this.cmsItemSetTaskAsCompleted.Size = new System.Drawing.Size(277, 28);
            this.cmsItemSetTaskAsCompleted.Text = "Marcar como completada";
            this.cmsItemSetTaskAsCompleted.Click += new System.EventHandler(this.cmsItemSetTaskAsCompleted_Click);
            // 
            // cmsItemSetTaskAsNonCompleted
            // 
            this.cmsItemSetTaskAsNonCompleted.Name = "cmsItemSetTaskAsNonCompleted";
            this.cmsItemSetTaskAsNonCompleted.Size = new System.Drawing.Size(277, 28);
            this.cmsItemSetTaskAsNonCompleted.Text = "Marcar como pendiente";
            this.cmsItemSetTaskAsNonCompleted.Click += new System.EventHandler(this.cmsItemSetTaskAsNonCompleted_Click);
            // 
            // cmsItemDeleteTask
            // 
            this.cmsItemDeleteTask.Name = "cmsItemDeleteTask";
            this.cmsItemDeleteTask.Size = new System.Drawing.Size(277, 28);
            this.cmsItemDeleteTask.Text = "Eliminar tarea";
            this.cmsItemDeleteTask.Click += new System.EventHandler(this.cmsItemDeleteTask_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(572, 509);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToDeleteRows = false;
            this.dgvSales.AllowUserToResizeColumns = false;
            this.dgvSales.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerColumn,
            this.saleIdColumn,
            this.dateColumn,
            this.shippingColumn,
            this.statusColumn});
            this.dgvSales.ContextMenuStrip = this.cmsShipmentOptions;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSales.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSales.Location = new System.Drawing.Point(12, 308);
            this.dgvSales.MultiSelect = false;
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersVisible = false;
            this.dgvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSales.ShowCellErrors = false;
            this.dgvSales.ShowCellToolTips = false;
            this.dgvSales.ShowEditingIcon = false;
            this.dgvSales.ShowRowErrors = false;
            this.dgvSales.Size = new System.Drawing.Size(760, 195);
            this.dgvSales.TabIndex = 4;
            this.dgvSales.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSales_CellFormatting);
            this.dgvSales.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvDelta_MouseDown);
            // 
            // cmsShipmentOptions
            // 
            this.cmsShipmentOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsShipmentOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemSetShipmentAsShipped,
            this.cmsItemSetShipmentAsPending,
            this.cmsItemGoToSaleFromShipment,
            this.cmsItemGoToCustomerFromShipment});
            this.cmsShipmentOptions.Name = "cmsShipmentOptions";
            this.cmsShipmentOptions.Size = new System.Drawing.Size(281, 116);
            // 
            // cmsItemSetShipmentAsShipped
            // 
            this.cmsItemSetShipmentAsShipped.Name = "cmsItemSetShipmentAsShipped";
            this.cmsItemSetShipmentAsShipped.Size = new System.Drawing.Size(280, 28);
            this.cmsItemSetShipmentAsShipped.Text = "Marcar como despachado";
            this.cmsItemSetShipmentAsShipped.Click += new System.EventHandler(this.cmsItemSetShipmentAsShipped_Click);
            // 
            // cmsItemSetShipmentAsPending
            // 
            this.cmsItemSetShipmentAsPending.Name = "cmsItemSetShipmentAsPending";
            this.cmsItemSetShipmentAsPending.Size = new System.Drawing.Size(280, 28);
            this.cmsItemSetShipmentAsPending.Text = "Marcar como pendiente";
            this.cmsItemSetShipmentAsPending.Click += new System.EventHandler(this.cmsItemSetShipmentAsPending_Click);
            // 
            // cmsItemGoToSaleFromShipment
            // 
            this.cmsItemGoToSaleFromShipment.Name = "cmsItemGoToSaleFromShipment";
            this.cmsItemGoToSaleFromShipment.Size = new System.Drawing.Size(280, 28);
            this.cmsItemGoToSaleFromShipment.Text = "Ir a la venta";
            this.cmsItemGoToSaleFromShipment.Click += new System.EventHandler(this.cmsItemGoToSaleFromShipment_Click);
            // 
            // cmsItemGoToCustomerFromShipment
            // 
            this.cmsItemGoToCustomerFromShipment.Name = "cmsItemGoToCustomerFromShipment";
            this.cmsItemGoToCustomerFromShipment.Size = new System.Drawing.Size(280, 28);
            this.cmsItemGoToCustomerFromShipment.Text = "Ir al cliente";
            this.cmsItemGoToCustomerFromShipment.Click += new System.EventHandler(this.cmsItemGoToCustomerFromShipment_Click);
            // 
            // btnExportTasks
            // 
            this.btnExportTasks.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportTasks.Location = new System.Drawing.Point(12, 509);
            this.btnExportTasks.Name = "btnExportTasks";
            this.btnExportTasks.Size = new System.Drawing.Size(200, 40);
            this.btnExportTasks.TabIndex = 6;
            this.btnExportTasks.Text = "Exportar tareas";
            this.btnExportTasks.UseVisualStyleBackColor = true;
            this.btnExportTasks.Click += new System.EventHandler(this.btnExportTasks_Click);
            // 
            // customerColumn
            // 
            this.customerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customerColumn.DataPropertyName = "CustomerName";
            this.customerColumn.HeaderText = "Cliente";
            this.customerColumn.Name = "customerColumn";
            this.customerColumn.ReadOnly = true;
            this.customerColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // saleIdColumn
            // 
            this.saleIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.saleIdColumn.DataPropertyName = "SaleID";
            dataGridViewCellStyle4.Format = "D8";
            this.saleIdColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.saleIdColumn.HeaderText = "ID Venta";
            this.saleIdColumn.Name = "saleIdColumn";
            this.saleIdColumn.ReadOnly = true;
            this.saleIdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.saleIdColumn.Width = 110;
            // 
            // dateColumn
            // 
            this.dateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateColumn.DataPropertyName = "Date";
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            this.dateColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.dateColumn.HeaderText = "Fecha venta";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dateColumn.Width = 110;
            // 
            // shippingColumn
            // 
            this.shippingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.shippingColumn.DataPropertyName = "ShippingName";
            this.shippingColumn.HeaderText = "Forma de envío";
            this.shippingColumn.Name = "shippingColumn";
            this.shippingColumn.ReadOnly = true;
            this.shippingColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.shippingColumn.Width = 150;
            // 
            // statusColumn
            // 
            this.statusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.statusColumn.HeaderText = "Estado";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            this.statusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.statusColumn.Width = 110;
            // 
            // TK_CalendarDayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnExportTasks);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.dgvTasks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TK_CalendarDayView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resumen diario";
            this.Load += new System.EventHandler(this.TK_CalendarDayView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.cmsTaskOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.cmsShipmentOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn completedColumn;
        private System.Windows.Forms.ContextMenuStrip cmsTaskOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemOpenTask;
        private System.Windows.Forms.ToolStripMenuItem cmsItemSetTaskAsCompleted;
        private System.Windows.Forms.ToolStripMenuItem cmsItemSetTaskAsNonCompleted;
        private System.Windows.Forms.ContextMenuStrip cmsShipmentOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemSetShipmentAsShipped;
        private System.Windows.Forms.ToolStripMenuItem cmsItemSetShipmentAsPending;
        private System.Windows.Forms.ToolStripMenuItem cmsItemGoToSaleFromShipment;
        private System.Windows.Forms.ToolStripMenuItem cmsItemGoToCustomerFromShipment;
        private System.Windows.Forms.ToolStripMenuItem cmsItemDeleteTask;
        private System.Windows.Forms.Button btnExportTasks;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn saleIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shippingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
    }
}