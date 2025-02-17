namespace Clover.Gestion
{
    partial class DelegarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelegarForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Mainbannertitulo = new System.Windows.Forms.Label();
            this.cmbUsuarios = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelegar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.Mainbannertitulo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-4, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 82);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 68);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Mainbannertitulo
            // 
            this.Mainbannertitulo.AutoSize = true;
            this.Mainbannertitulo.Font = new System.Drawing.Font("Verdana", 18.75F, System.Drawing.FontStyle.Bold);
            this.Mainbannertitulo.ForeColor = System.Drawing.SystemColors.Control;
            this.Mainbannertitulo.Location = new System.Drawing.Point(86, 28);
            this.Mainbannertitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Mainbannertitulo.Name = "Mainbannertitulo";
            this.Mainbannertitulo.Size = new System.Drawing.Size(226, 31);
            this.Mainbannertitulo.TabIndex = 2;
            this.Mainbannertitulo.Text = "DELEGAR LEAD";
            // 
            // cmbUsuarios
            // 
            this.cmbUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.cmbUsuarios.FormattingEnabled = true;
            this.cmbUsuarios.ItemHeight = 25;
            this.cmbUsuarios.Location = new System.Drawing.Point(12, 160);
            this.cmbUsuarios.Name = "cmbUsuarios";
            this.cmbUsuarios.Size = new System.Drawing.Size(331, 33);
            this.cmbUsuarios.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(13, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seleccionar Usuario";
            // 
            // btnDelegar
            // 
            this.btnDelegar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnDelegar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelegar.FlatAppearance.BorderSize = 0;
            this.btnDelegar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelegar.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelegar.ForeColor = System.Drawing.Color.White;
            this.btnDelegar.Location = new System.Drawing.Point(12, 312);
            this.btnDelegar.Name = "btnDelegar";
            this.btnDelegar.Size = new System.Drawing.Size(131, 43);
            this.btnDelegar.TabIndex = 42;
            this.btnDelegar.Text = "DELEGAR";
            this.btnDelegar.UseVisualStyleBackColor = false;
            this.btnDelegar.Click += new System.EventHandler(this.btnDelegar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(212, 312);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(131, 43);
            this.btnCancelar.TabIndex = 43;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // DelegarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(355, 397);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnDelegar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbUsuarios);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DelegarForm";
            this.Text = "Delegar";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Mainbannertitulo;
        private System.Windows.Forms.ComboBox cmbUsuarios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelegar;
        private System.Windows.Forms.Button btnCancelar;
    }
}