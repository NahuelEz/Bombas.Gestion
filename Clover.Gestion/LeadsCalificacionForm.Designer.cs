namespace Clover.Gestion
{
    partial class LeadsCalificacionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeadsCalificacionForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvLeadsCalificacion = new System.Windows.Forms.DataGridView();
            this.txtBuscarNombre = new System.Windows.Forms.TextBox();
            this.cmbFiltrarCasilla = new System.Windows.Forms.ComboBox();
            this.btnAplicarFiltros = new System.Windows.Forms.Button();
            this.btnResetearFiltros = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeadsCalificacion)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1609, 75);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(104, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 39;
            this.label1.Text = "CALIFICACION";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(31, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 54);
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // dgvLeadsCalificacion
            // 
            this.dgvLeadsCalificacion.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeadsCalificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeadsCalificacion.Location = new System.Drawing.Point(12, 81);
            this.dgvLeadsCalificacion.Name = "dgvLeadsCalificacion";
            this.dgvLeadsCalificacion.Size = new System.Drawing.Size(1489, 619);
            this.dgvLeadsCalificacion.TabIndex = 2;
            // 
            // txtBuscarNombre
            // 
            this.txtBuscarNombre.Location = new System.Drawing.Point(36, 726);
            this.txtBuscarNombre.Name = "txtBuscarNombre";
            this.txtBuscarNombre.Size = new System.Drawing.Size(445, 20);
            this.txtBuscarNombre.TabIndex = 3;
            // 
            // cmbFiltrarCasilla
            // 
            this.cmbFiltrarCasilla.FormattingEnabled = true;
            this.cmbFiltrarCasilla.Location = new System.Drawing.Point(511, 725);
            this.cmbFiltrarCasilla.Name = "cmbFiltrarCasilla";
            this.cmbFiltrarCasilla.Size = new System.Drawing.Size(363, 21);
            this.cmbFiltrarCasilla.TabIndex = 4;
            // 
            // btnAplicarFiltros
            // 
            this.btnAplicarFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnAplicarFiltros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicarFiltros.FlatAppearance.BorderSize = 0;
            this.btnAplicarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAplicarFiltros.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicarFiltros.ForeColor = System.Drawing.Color.White;
            this.btnAplicarFiltros.Location = new System.Drawing.Point(920, 712);
            this.btnAplicarFiltros.Name = "btnAplicarFiltros";
            this.btnAplicarFiltros.Size = new System.Drawing.Size(131, 43);
            this.btnAplicarFiltros.TabIndex = 43;
            this.btnAplicarFiltros.Text = "Aplicar Filtro";
            this.btnAplicarFiltros.UseVisualStyleBackColor = false;
            this.btnAplicarFiltros.Click += new System.EventHandler(this.btnAplicarFiltros_Click);
            // 
            // btnResetearFiltros
            // 
            this.btnResetearFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnResetearFiltros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetearFiltros.FlatAppearance.BorderSize = 0;
            this.btnResetearFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnResetearFiltros.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetearFiltros.ForeColor = System.Drawing.Color.White;
            this.btnResetearFiltros.Location = new System.Drawing.Point(1057, 712);
            this.btnResetearFiltros.Name = "btnResetearFiltros";
            this.btnResetearFiltros.Size = new System.Drawing.Size(131, 43);
            this.btnResetearFiltros.TabIndex = 44;
            this.btnResetearFiltros.Text = "Reset Filtro";
            this.btnResetearFiltros.UseVisualStyleBackColor = false;
            this.btnResetearFiltros.Click += new System.EventHandler(this.btnResetearFiltros_Click);
            // 
            // LeadsCalificacionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1514, 769);
            this.Controls.Add(this.btnResetearFiltros);
            this.Controls.Add(this.btnAplicarFiltros);
            this.Controls.Add(this.cmbFiltrarCasilla);
            this.Controls.Add(this.txtBuscarNombre);
            this.Controls.Add(this.dgvLeadsCalificacion);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LeadsCalificacionForm";
            this.Text = "LeadsCalificacion";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeadsCalificacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvLeadsCalificacion;
        private System.Windows.Forms.TextBox txtBuscarNombre;
        private System.Windows.Forms.ComboBox cmbFiltrarCasilla;
        private System.Windows.Forms.Button btnAplicarFiltros;
        private System.Windows.Forms.Button btnResetearFiltros;
    }
}