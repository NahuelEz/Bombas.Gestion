namespace Clover.Gestion
{
    partial class PR_Product
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PR_Product));
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtPartCode = new System.Windows.Forms.TextBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.txtInitialStock = new System.Windows.Forms.TextBox();
            this.lbl8 = new System.Windows.Forms.Label();
            this.nudChangeStock = new System.Windows.Forms.NumericUpDown();
            this.lbl9 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lbl10 = new System.Windows.Forms.Label();
            this.nudUnitPrice = new System.Windows.Forms.NumericUpDown();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ofdOpenImage = new System.Windows.Forms.OpenFileDialog();
            this.pbxImagePreview = new System.Windows.Forms.PictureBox();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.lbl7 = new System.Windows.Forms.Label();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.lbl2 = new System.Windows.Forms.Label();
            this.rbnIsSeal = new System.Windows.Forms.RadioButton();
            this.rbnIsOther = new System.Windows.Forms.RadioButton();
            this.cboTypeOfSeal = new System.Windows.Forms.ComboBox();
            this.lbl4 = new System.Windows.Forms.Label();
            this.sbxDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(153, 18);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(136, 23);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Código de parte:";
            // 
            // txtPartCode
            // 
            this.txtPartCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPartCode.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartCode.Location = new System.Drawing.Point(366, 15);
            this.txtPartCode.MaxLength = 32;
            this.txtPartCode.Name = "txtPartCode";
            this.txtPartCode.Size = new System.Drawing.Size(270, 31);
            this.txtPartCode.TabIndex = 1;
            this.txtPartCode.Text = "MEC-";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.Location = new System.Drawing.Point(153, 92);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(110, 23);
            this.lbl3.TabIndex = 5;
            this.lbl3.Text = "Tipo de sello:";
            // 
            // txtInitialStock
            // 
            this.txtInitialStock.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitialStock.Location = new System.Drawing.Point(366, 461);
            this.txtInitialStock.Name = "txtInitialStock";
            this.txtInitialStock.ReadOnly = true;
            this.txtInitialStock.Size = new System.Drawing.Size(270, 31);
            this.txtInitialStock.TabIndex = 17;
            this.txtInitialStock.Text = "0,00";
            // 
            // lbl8
            // 
            this.lbl8.AutoSize = true;
            this.lbl8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl8.Location = new System.Drawing.Point(153, 464);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(107, 23);
            this.lbl8.TabIndex = 16;
            this.lbl8.Text = "Stock actual:";
            // 
            // nudChangeStock
            // 
            this.nudChangeStock.DecimalPlaces = 2;
            this.nudChangeStock.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudChangeStock.Location = new System.Drawing.Point(366, 498);
            this.nudChangeStock.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudChangeStock.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.nudChangeStock.Name = "nudChangeStock";
            this.nudChangeStock.Size = new System.Drawing.Size(270, 31);
            this.nudChangeStock.TabIndex = 19;
            this.nudChangeStock.ValueChanged += new System.EventHandler(this.nudChangeStock_ValueChanged);
            // 
            // lbl9
            // 
            this.lbl9.AutoSize = true;
            this.lbl9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl9.Location = new System.Drawing.Point(153, 500);
            this.lbl9.Name = "lbl9";
            this.lbl9.Size = new System.Drawing.Size(156, 23);
            this.lbl9.TabIndex = 18;
            this.lbl9.Text = "Agregar / Sustraer:";
            // 
            // txtStock
            // 
            this.txtStock.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.Location = new System.Drawing.Point(366, 535);
            this.txtStock.Name = "txtStock";
            this.txtStock.ReadOnly = true;
            this.txtStock.Size = new System.Drawing.Size(270, 31);
            this.txtStock.TabIndex = 21;
            this.txtStock.Text = "0,00";
            // 
            // lbl10
            // 
            this.lbl10.AutoSize = true;
            this.lbl10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl10.Location = new System.Drawing.Point(153, 538);
            this.lbl10.Name = "lbl10";
            this.lbl10.Size = new System.Drawing.Size(94, 23);
            this.lbl10.TabIndex = 20;
            this.lbl10.Text = "Stock final:";
            // 
            // nudUnitPrice
            // 
            this.nudUnitPrice.DecimalPlaces = 2;
            this.nudUnitPrice.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudUnitPrice.Location = new System.Drawing.Point(366, 387);
            this.nudUnitPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudUnitPrice.Name = "nudUnitPrice";
            this.nudUnitPrice.Size = new System.Drawing.Size(270, 31);
            this.nudUnitPrice.TabIndex = 13;
            this.nudUnitPrice.ThousandsSeparator = true;
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl6.Location = new System.Drawing.Point(153, 390);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(184, 23);
            this.lbl6.TabIndex = 12;
            this.lbl6.Text = "Precio unitario sin IVA:";
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl5.Location = new System.Drawing.Point(153, 317);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(73, 23);
            this.lbl5.TabIndex = 9;
            this.lbl5.Text = "Imagen:";
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(572, 609);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 22;
            this.btnAccept.Text = "Registrar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(366, 609);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 40);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ofdOpenImage
            // 
            this.ofdOpenImage.DefaultExt = "jpg";
            this.ofdOpenImage.Filter = "Imagenes|*.jpg;*.png";
            // 
            // pbxImagePreview
            // 
            this.pbxImagePreview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbxImagePreview.ErrorImage = null;
            this.pbxImagePreview.Image = global::Clover.Gestion.Properties.Resources.EmptyImageIcon;
            this.pbxImagePreview.InitialImage = null;
            this.pbxImagePreview.Location = new System.Drawing.Point(12, 12);
            this.pbxImagePreview.Name = "pbxImagePreview";
            this.pbxImagePreview.Size = new System.Drawing.Size(125, 125);
            this.pbxImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxImagePreview.TabIndex = 19;
            this.pbxImagePreview.TabStop = false;
            // 
            // cboCurrency
            // 
            this.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(366, 424);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(270, 31);
            this.cboCurrency.TabIndex = 15;
            // 
            // lbl7
            // 
            this.lbl7.AutoSize = true;
            this.lbl7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl7.Location = new System.Drawing.Point(153, 427);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(118, 23);
            this.lbl7.TabIndex = 14;
            this.lbl7.Text = "Expresado en:";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadImage.Location = new System.Drawing.Point(366, 313);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(200, 31);
            this.btnLoadImage.TabIndex = 10;
            this.btnLoadImage.Text = "Cargar";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnClearImage
            // 
            this.btnClearImage.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearImage.Location = new System.Drawing.Point(572, 313);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(200, 31);
            this.btnClearImage.TabIndex = 11;
            this.btnClearImage.Text = "Borrar";
            this.btnClearImage.UseVisualStyleBackColor = true;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(153, 55);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(146, 23);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "Tipo de producto:";
            // 
            // rbnIsSeal
            // 
            this.rbnIsSeal.AutoSize = true;
            this.rbnIsSeal.Checked = true;
            this.rbnIsSeal.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsSeal.Location = new System.Drawing.Point(366, 53);
            this.rbnIsSeal.Name = "rbnIsSeal";
            this.rbnIsSeal.Size = new System.Drawing.Size(141, 27);
            this.rbnIsSeal.TabIndex = 3;
            this.rbnIsSeal.TabStop = true;
            this.rbnIsSeal.Text = "Sello mecánico";
            this.rbnIsSeal.UseVisualStyleBackColor = true;
            this.rbnIsSeal.CheckedChanged += new System.EventHandler(this.rbnIsSeal_CheckedChanged);
            // 
            // rbnIsOther
            // 
            this.rbnIsOther.AutoSize = true;
            this.rbnIsOther.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsOther.Location = new System.Drawing.Point(508, 53);
            this.rbnIsOther.Name = "rbnIsOther";
            this.rbnIsOther.Size = new System.Drawing.Size(64, 27);
            this.rbnIsOther.TabIndex = 4;
            this.rbnIsOther.Text = "Otro";
            this.rbnIsOther.UseVisualStyleBackColor = true;
            this.rbnIsOther.CheckedChanged += new System.EventHandler(this.rbnIsOther_CheckedChanged);
            // 
            // cboTypeOfSeal
            // 
            this.cboTypeOfSeal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeOfSeal.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTypeOfSeal.FormattingEnabled = true;
            this.cboTypeOfSeal.Location = new System.Drawing.Point(366, 89);
            this.cboTypeOfSeal.Name = "cboTypeOfSeal";
            this.cboTypeOfSeal.Size = new System.Drawing.Size(270, 31);
            this.cboTypeOfSeal.TabIndex = 6;
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Enabled = false;
            this.lbl4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl4.Location = new System.Drawing.Point(153, 129);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(105, 23);
            this.lbl4.TabIndex = 7;
            this.lbl4.Text = "Descripción:";
            // 
            // sbxDescription
            // 
            this.sbxDescription.Enabled = false;
            this.sbxDescription.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbxDescription.Location = new System.Drawing.Point(366, 126);
            this.sbxDescription.Margin = new System.Windows.Forms.Padding(5);
            this.sbxDescription.MaxLength = 4096;
            this.sbxDescription.Multiline = true;
            this.sbxDescription.Name = "sbxDescription";
            this.sbxDescription.Size = new System.Drawing.Size(406, 181);
            this.sbxDescription.TabIndex = 8;
            // 
            // PR_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.sbxDescription);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.cboTypeOfSeal);
            this.Controls.Add(this.rbnIsOther);
            this.Controls.Add(this.rbnIsSeal);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.lbl7);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.pbxImagePreview);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.nudUnitPrice);
            this.Controls.Add(this.lbl10);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.lbl9);
            this.Controls.Add(this.nudChangeStock);
            this.Controls.Add(this.lbl8);
            this.Controls.Add(this.txtInitialStock);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.txtPartCode);
            this.Controls.Add(this.lbl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PR_Product";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registro de producto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PR_Product_FormClosing);
            this.Load += new System.EventHandler(this.PR_Product_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtPartCode;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.TextBox txtInitialStock;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.NumericUpDown nudChangeStock;
        private System.Windows.Forms.Label lbl9;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lbl10;
        private System.Windows.Forms.NumericUpDown nudUnitPrice;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog ofdOpenImage;
        private System.Windows.Forms.PictureBox pbxImagePreview;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.RadioButton rbnIsSeal;
        private System.Windows.Forms.RadioButton rbnIsOther;
        private System.Windows.Forms.ComboBox cboTypeOfSeal;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.TextBox sbxDescription;
    }
}