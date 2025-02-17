namespace Clover.Gestion
{
    partial class SA_Items_Product
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SA_Items_Product));
            this.btnAutoFormat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboVat = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pbxImagePreview = new System.Windows.Forms.PictureBox();
            this.sbxDescription = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nudDeliveryDelay = new System.Windows.Forms.NumericUpDown();
            this.chkDeliveryNotSpecified = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCurrencySymbol_3 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblCurrencySymbol_2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrencySymbol_1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudCost = new System.Windows.Forms.NumericUpDown();
            this.chkCostNotSpecified = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeliveryDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAutoFormat
            // 
            this.btnAutoFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAutoFormat.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoFormat.Location = new System.Drawing.Point(12, 527);
            this.btnAutoFormat.Name = "btnAutoFormat";
            this.btnAutoFormat.Size = new System.Drawing.Size(200, 40);
            this.btnAutoFormat.TabIndex = 25;
            this.btnAutoFormat.Text = "Formato automático";
            this.btnAutoFormat.UseVisualStyleBackColor = true;
            this.btnAutoFormat.Click += new System.EventHandler(this.btnAutoFormat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Imagen:";
            // 
            // cboVat
            // 
            this.cboVat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboVat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVat.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVat.FormattingEnabled = true;
            this.cboVat.Location = new System.Drawing.Point(200, 416);
            this.cboVat.Name = "cboVat";
            this.cboVat.Size = new System.Drawing.Size(250, 31);
            this.cboVat.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 419);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 23);
            this.label8.TabIndex = 17;
            this.label8.Text = "IVA aplicable:";
            // 
            // pbxImagePreview
            // 
            this.pbxImagePreview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbxImagePreview.ErrorImage = null;
            this.pbxImagePreview.Image = global::Clover.Gestion.Properties.Resources.EmptyImageIcon;
            this.pbxImagePreview.InitialImage = null;
            this.pbxImagePreview.Location = new System.Drawing.Point(12, 43);
            this.pbxImagePreview.Name = "pbxImagePreview";
            this.pbxImagePreview.Size = new System.Drawing.Size(125, 125);
            this.pbxImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxImagePreview.TabIndex = 63;
            this.pbxImagePreview.TabStop = false;
            // 
            // sbxDescription
            // 
            this.sbxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbxDescription.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbxDescription.Location = new System.Drawing.Point(200, 43);
            this.sbxDescription.Margin = new System.Windows.Forms.Padding(5);
            this.sbxDescription.MaxLength = 4096;
            this.sbxDescription.Multiline = true;
            this.sbxDescription.Name = "sbxDescription";
            this.sbxDescription.Size = new System.Drawing.Size(460, 180);
            this.sbxDescription.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(306, 456);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 23);
            this.label10.TabIndex = 21;
            this.label10.Text = "día(s) post-venta.";
            // 
            // nudDeliveryDelay
            // 
            this.nudDeliveryDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudDeliveryDelay.Enabled = false;
            this.nudDeliveryDelay.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDeliveryDelay.Location = new System.Drawing.Point(200, 453);
            this.nudDeliveryDelay.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudDeliveryDelay.Name = "nudDeliveryDelay";
            this.nudDeliveryDelay.Size = new System.Drawing.Size(100, 31);
            this.nudDeliveryDelay.TabIndex = 20;
            this.nudDeliveryDelay.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // chkDeliveryNotSpecified
            // 
            this.chkDeliveryNotSpecified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDeliveryNotSpecified.AutoSize = true;
            this.chkDeliveryNotSpecified.Checked = true;
            this.chkDeliveryNotSpecified.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeliveryNotSpecified.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDeliveryNotSpecified.Location = new System.Drawing.Point(510, 455);
            this.chkDeliveryNotSpecified.Name = "chkDeliveryNotSpecified";
            this.chkDeliveryNotSpecified.Size = new System.Drawing.Size(150, 27);
            this.chkDeliveryNotSpecified.TabIndex = 22;
            this.chkDeliveryNotSpecified.Text = "No especificado";
            this.chkDeliveryNotSpecified.UseVisualStyleBackColor = true;
            this.chkDeliveryNotSpecified.CheckedChanged += new System.EventHandler(this.chkDeliveryNotSpecified_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 456);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 23);
            this.label9.TabIndex = 19;
            this.label9.Text = "Fecha de entrega:";
            // 
            // lblCurrencySymbol_3
            // 
            this.lblCurrencySymbol_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrencySymbol_3.AutoSize = true;
            this.lblCurrencySymbol_3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencySymbol_3.Location = new System.Drawing.Point(456, 382);
            this.lblCurrencySymbol_3.Name = "lblCurrencySymbol_3";
            this.lblCurrencySymbol_3.Size = new System.Drawing.Size(40, 23);
            this.lblCurrencySymbol_3.TabIndex = 16;
            this.lblCurrencySymbol_3.Text = "ARS";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalAmount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(300, 379);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(150, 31);
            this.txtTotalAmount.TabIndex = 15;
            this.txtTotalAmount.Text = "0,00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 382);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 23);
            this.label7.TabIndex = 14;
            this.label7.Text = "Subtotal:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "Cantidad:";
            // 
            // nudQuantity
            // 
            this.nudQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudQuantity.DecimalPlaces = 2;
            this.nudQuantity.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQuantity.Location = new System.Drawing.Point(300, 268);
            this.nudQuantity.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(150, 31);
            this.nudQuantity.TabIndex = 6;
            this.nudQuantity.ThousandsSeparator = true;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // lblCurrencySymbol_2
            // 
            this.lblCurrencySymbol_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrencySymbol_2.AutoSize = true;
            this.lblCurrencySymbol_2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencySymbol_2.Location = new System.Drawing.Point(456, 345);
            this.lblCurrencySymbol_2.Name = "lblCurrencySymbol_2";
            this.lblCurrencySymbol_2.Size = new System.Drawing.Size(40, 23);
            this.lblCurrencySymbol_2.TabIndex = 13;
            this.lblCurrencySymbol_2.Text = "ARS";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 23);
            this.label6.TabIndex = 11;
            this.label6.Text = "Precio unitario:";
            // 
            // nudAmount
            // 
            this.nudAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudAmount.DecimalPlaces = 2;
            this.nudAmount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAmount.Location = new System.Drawing.Point(300, 342);
            this.nudAmount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(150, 31);
            this.nudAmount.TabIndex = 12;
            this.nudAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAmount.ThousandsSeparator = true;
            this.nudAmount.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudAmount.ValueChanged += new System.EventHandler(this.nudAmount_ValueChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(356, 527);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 40);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(512, 527);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(150, 40);
            this.btnAccept.TabIndex = 23;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(196, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descripción:";
            // 
            // txtProduct
            // 
            this.txtProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduct.Location = new System.Drawing.Point(200, 231);
            this.txtProduct.MaxLength = 32;
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(250, 31);
            this.txtProduct.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Código producto:";
            // 
            // lblCurrencySymbol_1
            // 
            this.lblCurrencySymbol_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrencySymbol_1.AutoSize = true;
            this.lblCurrencySymbol_1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencySymbol_1.Location = new System.Drawing.Point(456, 308);
            this.lblCurrencySymbol_1.Name = "lblCurrencySymbol_1";
            this.lblCurrencySymbol_1.Size = new System.Drawing.Size(40, 23);
            this.lblCurrencySymbol_1.TabIndex = 9;
            this.lblCurrencySymbol_1.Text = "ARS";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "Costo unitario:";
            // 
            // nudCost
            // 
            this.nudCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudCost.DecimalPlaces = 2;
            this.nudCost.Enabled = false;
            this.nudCost.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCost.Location = new System.Drawing.Point(300, 305);
            this.nudCost.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudCost.Name = "nudCost";
            this.nudCost.Size = new System.Drawing.Size(150, 31);
            this.nudCost.TabIndex = 8;
            this.nudCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCost.ThousandsSeparator = true;
            this.nudCost.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // chkCostNotSpecified
            // 
            this.chkCostNotSpecified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCostNotSpecified.AutoSize = true;
            this.chkCostNotSpecified.Checked = true;
            this.chkCostNotSpecified.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCostNotSpecified.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCostNotSpecified.Location = new System.Drawing.Point(510, 307);
            this.chkCostNotSpecified.Name = "chkCostNotSpecified";
            this.chkCostNotSpecified.Size = new System.Drawing.Size(150, 27);
            this.chkCostNotSpecified.TabIndex = 10;
            this.chkCostNotSpecified.Text = "No especificado";
            this.chkCostNotSpecified.UseVisualStyleBackColor = true;
            this.chkCostNotSpecified.CheckedChanged += new System.EventHandler(this.chkCostNotSpecified_CheckedChanged);
            // 
            // SA_Items_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 579);
            this.Controls.Add(this.chkCostNotSpecified);
            this.Controls.Add(this.lblCurrencySymbol_1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudCost);
            this.Controls.Add(this.btnAutoFormat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboVat);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pbxImagePreview);
            this.Controls.Add(this.sbxDescription);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nudDeliveryDelay);
            this.Controls.Add(this.chkDeliveryNotSpecified);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblCurrencySymbol_3);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.lblCurrencySymbol_2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudAmount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProduct);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(690, 618);
            this.Name = "SA_Items_Product";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Producto";
            this.Load += new System.EventHandler(this.SA_Items_Product_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeliveryDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAutoFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboVat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pbxImagePreview;
        private System.Windows.Forms.TextBox sbxDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudDeliveryDelay;
        private System.Windows.Forms.CheckBox chkDeliveryNotSpecified;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCurrencySymbol_3;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblCurrencySymbol_2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrencySymbol_1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudCost;
        private System.Windows.Forms.CheckBox chkCostNotSpecified;
    }
}