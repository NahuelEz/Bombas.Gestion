namespace Clover.TabletApp
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMainBanner = new System.Windows.Forms.Panel();
            this.dgvRepairOrders = new System.Windows.Forms.DataGridView();
            this.roRepairOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblLoginInformation = new System.Windows.Forms.Label();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnUpdateOrder = new System.Windows.Forms.Button();
            this.btnSignOut = new System.Windows.Forms.Button();
            this.tmrAutoUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnOpenOrder = new System.Windows.Forms.Button();
            this.tmrAutoSingOut = new System.Windows.Forms.Timer(this.components);
            this.lblConnectionError = new System.Windows.Forms.Label();
            this.lblOrderInformation = new System.Windows.Forms.Label();
            this.lblFilterWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepairOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMainBanner
            // 
            this.pnlMainBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMainBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(76)))), ((int)(((byte)(47)))));
            this.pnlMainBanner.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMainBanner.BackgroundImage")));
            this.pnlMainBanner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlMainBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlMainBanner.Name = "pnlMainBanner";
            this.pnlMainBanner.Size = new System.Drawing.Size(600, 100);
            this.pnlMainBanner.TabIndex = 0;
            // 
            // dgvRepairOrders
            // 
            this.dgvRepairOrders.AllowUserToAddRows = false;
            this.dgvRepairOrders.AllowUserToDeleteRows = false;
            this.dgvRepairOrders.AllowUserToResizeColumns = false;
            this.dgvRepairOrders.AllowUserToResizeRows = false;
            this.dgvRepairOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRepairOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvRepairOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRepairOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roRepairOrderID,
            this.roCustomerName,
            this.roDescription,
            this.roStatus});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRepairOrders.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRepairOrders.Location = new System.Drawing.Point(12, 147);
            this.dgvRepairOrders.MultiSelect = false;
            this.dgvRepairOrders.Name = "dgvRepairOrders";
            this.dgvRepairOrders.ReadOnly = true;
            this.dgvRepairOrders.RowHeadersVisible = false;
            this.dgvRepairOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRepairOrders.Size = new System.Drawing.Size(560, 480);
            this.dgvRepairOrders.TabIndex = 3;
            this.dgvRepairOrders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRepairOrders_CellFormatting);
            // 
            // roRepairOrderID
            // 
            dataGridViewCellStyle8.Format = "D4";
            this.roRepairOrderID.DefaultCellStyle = dataGridViewCellStyle8;
            this.roRepairOrderID.HeaderText = "ID";
            this.roRepairOrderID.Name = "roRepairOrderID";
            this.roRepairOrderID.ReadOnly = true;
            this.roRepairOrderID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // roCustomerName
            // 
            this.roCustomerName.HeaderText = "Cliente";
            this.roCustomerName.Name = "roCustomerName";
            this.roCustomerName.ReadOnly = true;
            this.roCustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.roCustomerName.Width = 300;
            // 
            // roDescription
            // 
            this.roDescription.HeaderText = "Equipo";
            this.roDescription.Name = "roDescription";
            this.roDescription.ReadOnly = true;
            this.roDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.roDescription.Width = 400;
            // 
            // roStatus
            // 
            this.roStatus.HeaderText = "Estado";
            this.roStatus.Name = "roStatus";
            this.roStatus.ReadOnly = true;
            this.roStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.roStatus.Width = 400;
            // 
            // lblLoginInformation
            // 
            this.lblLoginInformation.AutoEllipsis = true;
            this.lblLoginInformation.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(84)))), ((int)(((byte)(110)))));
            this.lblLoginInformation.Location = new System.Drawing.Point(12, 112);
            this.lblLoginInformation.Name = "lblLoginInformation";
            this.lblLoginInformation.Size = new System.Drawing.Size(354, 23);
            this.lblLoginInformation.TabIndex = 1;
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddOrder.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddOrder.Location = new System.Drawing.Point(372, 663);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(200, 40);
            this.btnAddOrder.TabIndex = 5;
            this.btnAddOrder.Text = "Nuevo [Ctrl. + N]";
            this.btnAddOrder.UseVisualStyleBackColor = true;
            this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteOrder.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteOrder.Location = new System.Drawing.Point(12, 709);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(200, 40);
            this.btnDeleteOrder.TabIndex = 6;
            this.btnDeleteOrder.Text = "Borrar selecc. [SUPR.]";
            this.btnDeleteOrder.UseVisualStyleBackColor = true;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // btnUpdateOrder
            // 
            this.btnUpdateOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateOrder.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateOrder.Location = new System.Drawing.Point(372, 709);
            this.btnUpdateOrder.Name = "btnUpdateOrder";
            this.btnUpdateOrder.Size = new System.Drawing.Size(200, 40);
            this.btnUpdateOrder.TabIndex = 7;
            this.btnUpdateOrder.Text = "Actualizar [Ctrl. + A]";
            this.btnUpdateOrder.UseVisualStyleBackColor = true;
            this.btnUpdateOrder.Click += new System.EventHandler(this.btnUpdateOrder_Click);
            // 
            // btnSignOut
            // 
            this.btnSignOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignOut.Enabled = false;
            this.btnSignOut.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignOut.Location = new System.Drawing.Point(372, 106);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(200, 35);
            this.btnSignOut.TabIndex = 2;
            this.btnSignOut.Text = "Cerrar sesión [ESC.]";
            this.btnSignOut.UseVisualStyleBackColor = true;
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
            // 
            // tmrAutoUpdate
            // 
            this.tmrAutoUpdate.Interval = 60000;
            this.tmrAutoUpdate.Tick += new System.EventHandler(this.tmrAutoUpdate_Tick);
            // 
            // btnOpenOrder
            // 
            this.btnOpenOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenOrder.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenOrder.Location = new System.Drawing.Point(12, 663);
            this.btnOpenOrder.Name = "btnOpenOrder";
            this.btnOpenOrder.Size = new System.Drawing.Size(200, 40);
            this.btnOpenOrder.TabIndex = 4;
            this.btnOpenOrder.Text = "Ver selecc. [ENT.]";
            this.btnOpenOrder.UseVisualStyleBackColor = true;
            this.btnOpenOrder.Click += new System.EventHandler(this.btnOpenOrder_Click);
            // 
            // tmrAutoSingOut
            // 
            this.tmrAutoSingOut.Interval = 300000;
            this.tmrAutoSingOut.Tick += new System.EventHandler(this.tmrAutoSingOut_Tick);
            // 
            // lblConnectionError
            // 
            this.lblConnectionError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnectionError.AutoSize = true;
            this.lblConnectionError.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionError.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionError.Location = new System.Drawing.Point(12, 630);
            this.lblConnectionError.Name = "lblConnectionError";
            this.lblConnectionError.Size = new System.Drawing.Size(161, 23);
            this.lblConnectionError.TabIndex = 8;
            this.lblConnectionError.Text = "Error de conexión...";
            this.lblConnectionError.Visible = false;
            // 
            // lblOrderInformation
            // 
            this.lblOrderInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderInformation.AutoSize = true;
            this.lblOrderInformation.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderInformation.Location = new System.Drawing.Point(429, 630);
            this.lblOrderInformation.Name = "lblOrderInformation";
            this.lblOrderInformation.Size = new System.Drawing.Size(143, 23);
            this.lblOrderInformation.TabIndex = 10;
            this.lblOrderInformation.Text = "Orden inteligente";
            // 
            // lblFilterWarning
            // 
            this.lblFilterWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilterWarning.AutoSize = true;
            this.lblFilterWarning.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(84)))), ((int)(((byte)(110)))));
            this.lblFilterWarning.Location = new System.Drawing.Point(280, 630);
            this.lblFilterWarning.Name = "lblFilterWarning";
            this.lblFilterWarning.Size = new System.Drawing.Size(120, 23);
            this.lblFilterWarning.TabIndex = 9;
            this.lblFilterWarning.Text = "Filtro activado";
            this.lblFilterWarning.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 761);
            this.Controls.Add(this.lblFilterWarning);
            this.Controls.Add(this.lblOrderInformation);
            this.Controls.Add(this.lblConnectionError);
            this.Controls.Add(this.btnOpenOrder);
            this.Controls.Add(this.btnSignOut);
            this.Controls.Add(this.btnUpdateOrder);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.lblLoginInformation);
            this.Controls.Add(this.dgvRepairOrders);
            this.Controls.Add(this.pnlMainBanner);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Main";
            this.Text = "Clover Gestión";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepairOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainBanner;
        private System.Windows.Forms.DataGridView dgvRepairOrders;
        private System.Windows.Forms.Label lblLoginInformation;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnUpdateOrder;
        private System.Windows.Forms.Button btnSignOut;
        private System.Windows.Forms.Timer tmrAutoUpdate;
        private System.Windows.Forms.Button btnOpenOrder;
        private System.Windows.Forms.Timer tmrAutoSingOut;
        private System.Windows.Forms.Label lblConnectionError;
        private System.Windows.Forms.DataGridViewTextBoxColumn roRepairOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn roCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn roDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn roStatus;
        private System.Windows.Forms.Label lblOrderInformation;
        private System.Windows.Forms.Label lblFilterWarning;
    }
}