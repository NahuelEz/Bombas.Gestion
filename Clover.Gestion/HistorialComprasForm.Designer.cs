namespace Clover.Gestion
{
    partial class HistorialComprasForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistorialComprasForm));
            this.dgvHistorialCompras = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAgregarCompra = new System.Windows.Forms.Button();
            this.btnEliminarCompra = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialCompras)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHistorialCompras
            // 
            this.dgvHistorialCompras.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorialCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorialCompras.Location = new System.Drawing.Point(6, 83);
            this.dgvHistorialCompras.Name = "dgvHistorialCompras";
            this.dgvHistorialCompras.Size = new System.Drawing.Size(742, 594);
            this.dgvHistorialCompras.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(104, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 25);
            this.label1.TabIndex = 39;
            this.label1.Text = "Historial de Compras";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-7, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1628, 76);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(31, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 70);
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // btnAgregarCompra
            // 
            this.btnAgregarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnAgregarCompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarCompra.FlatAppearance.BorderSize = 0;
            this.btnAgregarCompra.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarCompra.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCompra.ForeColor = System.Drawing.Color.White;
            this.btnAgregarCompra.Location = new System.Drawing.Point(6, 683);
            this.btnAgregarCompra.Name = "btnAgregarCompra";
            this.btnAgregarCompra.Size = new System.Drawing.Size(187, 43);
            this.btnAgregarCompra.TabIndex = 45;
            this.btnAgregarCompra.Text = "Agregar Compra";
            this.btnAgregarCompra.UseVisualStyleBackColor = false;
            this.btnAgregarCompra.Click += new System.EventHandler(this.btnAgregarCompra_Click);
            // 
            // btnEliminarCompra
            // 
            this.btnEliminarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnEliminarCompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarCompra.FlatAppearance.BorderSize = 0;
            this.btnEliminarCompra.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminarCompra.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarCompra.ForeColor = System.Drawing.Color.White;
            this.btnEliminarCompra.Location = new System.Drawing.Point(213, 683);
            this.btnEliminarCompra.Name = "btnEliminarCompra";
            this.btnEliminarCompra.Size = new System.Drawing.Size(187, 43);
            this.btnEliminarCompra.TabIndex = 46;
            this.btnEliminarCompra.Text = "Eliminar Compra";
            this.btnEliminarCompra.UseVisualStyleBackColor = false;
            // 
            // HistorialComprasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(756, 740);
            this.Controls.Add(this.btnEliminarCompra);
            this.Controls.Add(this.btnAgregarCompra);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvHistorialCompras);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistorialComprasForm";
            this.Text = "Historial Compras";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialCompras)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistorialCompras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAgregarCompra;
        private System.Windows.Forms.Button btnEliminarCompra;
    }
}