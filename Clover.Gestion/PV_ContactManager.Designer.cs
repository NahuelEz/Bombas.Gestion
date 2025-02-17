namespace Clover.Gestion
{
    partial class PV_ContactManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PV_ContactManager));
            this.btnAddContact = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.dgvContacts = new System.Windows.Forms.DataGridView();
            this.contactNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.greetingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsContactOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemEditContact = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsItemDeleteContact = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
            this.cmsContactOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddContact
            // 
            this.btnAddContact.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddContact.Location = new System.Drawing.Point(12, 309);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(200, 40);
            this.btnAddContact.TabIndex = 5;
            this.btnAddContact.Text = "Nuevo contacto";
            this.btnAddContact.UseVisualStyleBackColor = true;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(572, 309);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // dgvContacts
            // 
            this.dgvContacts.AllowUserToAddRows = false;
            this.dgvContacts.AllowUserToDeleteRows = false;
            this.dgvContacts.AllowUserToResizeColumns = false;
            this.dgvContacts.AllowUserToResizeRows = false;
            this.dgvContacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContacts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContacts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contactNameColumn,
            this.greetingColumn,
            this.phoneColumn,
            this.emailColumn});
            this.dgvContacts.ContextMenuStrip = this.cmsContactOptions;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvContacts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvContacts.Location = new System.Drawing.Point(12, 12);
            this.dgvContacts.MultiSelect = false;
            this.dgvContacts.Name = "dgvContacts";
            this.dgvContacts.ReadOnly = true;
            this.dgvContacts.RowHeadersVisible = false;
            this.dgvContacts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContacts.ShowCellToolTips = false;
            this.dgvContacts.ShowEditingIcon = false;
            this.dgvContacts.Size = new System.Drawing.Size(760, 291);
            this.dgvContacts.TabIndex = 3;
            this.dgvContacts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvContacts_MouseDown);
            // 
            // contactNameColumn
            // 
            this.contactNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.contactNameColumn.DataPropertyName = "ContactName";
            this.contactNameColumn.HeaderText = "Nombre contacto";
            this.contactNameColumn.Name = "contactNameColumn";
            this.contactNameColumn.ReadOnly = true;
            this.contactNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.contactNameColumn.Width = 150;
            // 
            // greetingColumn
            // 
            this.greetingColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.greetingColumn.DataPropertyName = "Greeting";
            this.greetingColumn.HeaderText = "Tratamiento";
            this.greetingColumn.Name = "greetingColumn";
            this.greetingColumn.ReadOnly = true;
            this.greetingColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.greetingColumn.Width = 150;
            // 
            // phoneColumn
            // 
            this.phoneColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.phoneColumn.DataPropertyName = "Phone";
            this.phoneColumn.HeaderText = "Teléfono";
            this.phoneColumn.Name = "phoneColumn";
            this.phoneColumn.ReadOnly = true;
            this.phoneColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.phoneColumn.Width = 150;
            // 
            // emailColumn
            // 
            this.emailColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.emailColumn.DataPropertyName = "Email";
            this.emailColumn.HeaderText = "Correo electrónico";
            this.emailColumn.Name = "emailColumn";
            this.emailColumn.ReadOnly = true;
            this.emailColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cmsContactOptions
            // 
            this.cmsContactOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsContactOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemEditContact,
            this.cmsItemDeleteContact});
            this.cmsContactOptions.Name = "cmsCustomerContactDetails";
            this.cmsContactOptions.Size = new System.Drawing.Size(214, 60);
            // 
            // cmsItemEditContact
            // 
            this.cmsItemEditContact.Name = "cmsItemEditContact";
            this.cmsItemEditContact.Size = new System.Drawing.Size(213, 28);
            this.cmsItemEditContact.Text = "Modificar";
            this.cmsItemEditContact.Click += new System.EventHandler(this.cmsItemEditContact_Click);
            // 
            // cmsItemDeleteContact
            // 
            this.cmsItemDeleteContact.Name = "cmsItemDeleteContact";
            this.cmsItemDeleteContact.Size = new System.Drawing.Size(213, 28);
            this.cmsItemDeleteContact.Text = "Eliminar contacto";
            this.cmsItemDeleteContact.Click += new System.EventHandler(this.cmsItemDeleteContact_Click);
            // 
            // PV_ContactManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.btnAddContact);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.dgvContacts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PV_ContactManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de contactos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.cmsContactOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.DataGridView dgvContacts;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn greetingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailColumn;
        private System.Windows.Forms.ContextMenuStrip cmsContactOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemEditContact;
        private System.Windows.Forms.ToolStripMenuItem cmsItemDeleteContact;
    }
}