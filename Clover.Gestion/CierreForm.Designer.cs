namespace Clover.Gestion
{
    partial class CierreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CierreForm));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbEstadoCliente = new System.Windows.Forms.ComboBox();
            this.txtNotaCliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTipoFacturacion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNotaEntrega = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOrdenCompra = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPrepararPedido = new System.Windows.Forms.CheckBox();
            this.chkFacturar = new System.Windows.Forms.CheckBox();
            this.chkCobrar = new System.Windows.Forms.CheckBox();
            this.chkDespachar = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(85, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 25);
            this.label1.TabIndex = 42;
            this.label1.Text = "Formulario de Cierre";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-4, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1595, 79);
            this.panel1.TabIndex = 43;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 70);
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // cmbEstadoCliente
            // 
            this.cmbEstadoCliente.FormattingEnabled = true;
            this.cmbEstadoCliente.Location = new System.Drawing.Point(563, 384);
            this.cmbEstadoCliente.Name = "cmbEstadoCliente";
            this.cmbEstadoCliente.Size = new System.Drawing.Size(342, 21);
            this.cmbEstadoCliente.TabIndex = 44;
            // 
            // txtNotaCliente
            // 
            this.txtNotaCliente.Location = new System.Drawing.Point(563, 463);
            this.txtNotaCliente.Multiline = true;
            this.txtNotaCliente.Name = "txtNotaCliente";
            this.txtNotaCliente.Size = new System.Drawing.Size(342, 60);
            this.txtNotaCliente.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(559, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 20);
            this.label2.TabIndex = 46;
            this.label2.Text = "Estado del Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.RoyalBlue;
            this.label3.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(559, 440);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 20);
            this.label3.TabIndex = 47;
            this.label3.Text = "Nota del Cliente";
            // 
            // cmbProducto
            // 
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(12, 164);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(342, 21);
            this.cmbProducto.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 20);
            this.label4.TabIndex = 49;
            this.label4.Text = "Producto";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(12, 220);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(342, 20);
            this.nudCantidad.TabIndex = 50;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.RoyalBlue;
            this.label5.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 51;
            this.label5.Text = "Cantidad";
            // 
            // cmbTipoFacturacion
            // 
            this.cmbTipoFacturacion.FormattingEnabled = true;
            this.cmbTipoFacturacion.Location = new System.Drawing.Point(12, 282);
            this.cmbTipoFacturacion.Name = "cmbTipoFacturacion";
            this.cmbTipoFacturacion.Size = new System.Drawing.Size(197, 21);
            this.cmbTipoFacturacion.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.RoyalBlue;
            this.label6.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(8, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(197, 20);
            this.label6.TabIndex = 53;
            this.label6.Text = "Tipo de Facturacion";
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.FormattingEnabled = true;
            this.cmbFormaPago.Items.AddRange(new object[] {
            "Cheque",
            "Diferido"});
            this.cmbFormaPago.Location = new System.Drawing.Point(12, 345);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(197, 21);
            this.cmbFormaPago.TabIndex = 54;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.RoyalBlue;
            this.label7.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(8, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 20);
            this.label7.TabIndex = 55;
            this.label7.Text = "Forma de Pago";
            // 
            // txtNotaEntrega
            // 
            this.txtNotaEntrega.Location = new System.Drawing.Point(8, 483);
            this.txtNotaEntrega.Multiline = true;
            this.txtNotaEntrega.Name = "txtNotaEntrega";
            this.txtNotaEntrega.Size = new System.Drawing.Size(342, 60);
            this.txtNotaEntrega.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.RoyalBlue;
            this.label8.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(8, 460);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "Nota Entrega/Envio";
            // 
            // txtOrdenCompra
            // 
            this.txtOrdenCompra.Location = new System.Drawing.Point(12, 417);
            this.txtOrdenCompra.Name = "txtOrdenCompra";
            this.txtOrdenCompra.Size = new System.Drawing.Size(197, 20);
            this.txtOrdenCompra.TabIndex = 58;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.RoyalBlue;
            this.label9.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(8, 394);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 20);
            this.label9.TabIndex = 59;
            this.label9.Text = "Orden de Compra";
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox1.Location = new System.Drawing.Point(553, 327);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 235);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cliente";
            // 
            // chkPrepararPedido
            // 
            this.chkPrepararPedido.AutoSize = true;
            this.chkPrepararPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.chkPrepararPedido.ForeColor = System.Drawing.Color.White;
            this.chkPrepararPedido.Location = new System.Drawing.Point(18, 30);
            this.chkPrepararPedido.Name = "chkPrepararPedido";
            this.chkPrepararPedido.Size = new System.Drawing.Size(149, 24);
            this.chkPrepararPedido.TabIndex = 61;
            this.chkPrepararPedido.Text = "Preparar Pedido";
            this.chkPrepararPedido.UseVisualStyleBackColor = true;
            // 
            // chkFacturar
            // 
            this.chkFacturar.AutoSize = true;
            this.chkFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.chkFacturar.ForeColor = System.Drawing.Color.White;
            this.chkFacturar.Location = new System.Drawing.Point(18, 60);
            this.chkFacturar.Name = "chkFacturar";
            this.chkFacturar.Size = new System.Drawing.Size(91, 24);
            this.chkFacturar.TabIndex = 62;
            this.chkFacturar.Text = "Facturar";
            this.chkFacturar.UseVisualStyleBackColor = true;
            // 
            // chkCobrar
            // 
            this.chkCobrar.AutoSize = true;
            this.chkCobrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.chkCobrar.ForeColor = System.Drawing.Color.White;
            this.chkCobrar.Location = new System.Drawing.Point(18, 90);
            this.chkCobrar.Name = "chkCobrar";
            this.chkCobrar.Size = new System.Drawing.Size(79, 24);
            this.chkCobrar.TabIndex = 63;
            this.chkCobrar.Text = "Cobrar";
            this.chkCobrar.UseVisualStyleBackColor = true;
            // 
            // chkDespachar
            // 
            this.chkDespachar.AutoSize = true;
            this.chkDespachar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.chkDespachar.ForeColor = System.Drawing.Color.White;
            this.chkDespachar.Location = new System.Drawing.Point(18, 120);
            this.chkDespachar.Name = "chkDespachar";
            this.chkDespachar.Size = new System.Drawing.Size(110, 24);
            this.chkDespachar.TabIndex = 64;
            this.chkDespachar.Text = "Despachar";
            this.chkDespachar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDespachar);
            this.groupBox2.Controls.Add(this.chkPrepararPedido);
            this.groupBox2.Controls.Add(this.chkCobrar);
            this.groupBox2.Controls.Add(this.chkFacturar);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox2.Location = new System.Drawing.Point(553, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 172);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Casillas de Estado";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(149, 619);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(131, 43);
            this.btnCancelar.TabIndex = 63;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(146)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(12, 619);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(131, 43);
            this.btnGuardar.TabIndex = 62;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // CierreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(931, 702);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtOrdenCompra);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNotaEntrega);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbTipoFacturacion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbProducto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNotaCliente);
            this.Controls.Add(this.cmbEstadoCliente);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CierreForm";
            this.Text = "Cierre";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbEstadoCliente;
        private System.Windows.Forms.TextBox txtNotaCliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTipoFacturacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNotaEntrega;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOrdenCompra;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkPrepararPedido;
        private System.Windows.Forms.CheckBox chkFacturar;
        private System.Windows.Forms.CheckBox chkCobrar;
        private System.Windows.Forms.CheckBox chkDespachar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}