namespace Clover.Gestion
{
    partial class PV_Provider
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PV_Provider));
            this.pnlCUITSelection = new System.Windows.Forms.Panel();
            this.rbnIsCUIT = new System.Windows.Forms.RadioButton();
            this.rbnIsDNI = new System.Windows.Forms.RadioButton();
            this.btnManageContacts = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.cboTaxGroup = new System.Windows.Forms.ComboBox();
            this.cboCountry = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudPaymentTerm = new System.Windows.Forms.NumericUpDown();
            this.cboDistrict = new System.Windows.Forms.ComboBox();
            this.txtIdentityNumber = new System.Windows.Forms.TextBox();
            this.txtAntiquity = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.dtpRegistryDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.txtProviderName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProviderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_2 = new System.Windows.Forms.Panel();
            this.pnl_1 = new System.Windows.Forms.Panel();
            this.pnlProviderIcon = new System.Windows.Forms.Panel();
            this.pnlCUITSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaymentTerm)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCUITSelection
            // 
            this.pnlCUITSelection.Controls.Add(this.rbnIsCUIT);
            this.pnlCUITSelection.Controls.Add(this.rbnIsDNI);
            this.pnlCUITSelection.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCUITSelection.Location = new System.Drawing.Point(330, 87);
            this.pnlCUITSelection.Name = "pnlCUITSelection";
            this.pnlCUITSelection.Size = new System.Drawing.Size(270, 35);
            this.pnlCUITSelection.TabIndex = 5;
            // 
            // rbnIsCUIT
            // 
            this.rbnIsCUIT.AutoSize = true;
            this.rbnIsCUIT.Checked = true;
            this.rbnIsCUIT.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsCUIT.Location = new System.Drawing.Point(3, 3);
            this.rbnIsCUIT.Name = "rbnIsCUIT";
            this.rbnIsCUIT.Size = new System.Drawing.Size(65, 27);
            this.rbnIsCUIT.TabIndex = 0;
            this.rbnIsCUIT.TabStop = true;
            this.rbnIsCUIT.Text = "CUIT";
            this.rbnIsCUIT.UseVisualStyleBackColor = true;
            // 
            // rbnIsDNI
            // 
            this.rbnIsDNI.AutoSize = true;
            this.rbnIsDNI.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsDNI.Location = new System.Drawing.Point(130, 3);
            this.rbnIsDNI.Name = "rbnIsDNI";
            this.rbnIsDNI.Size = new System.Drawing.Size(58, 27);
            this.rbnIsDNI.TabIndex = 1;
            this.rbnIsDNI.Text = "DNI";
            this.rbnIsDNI.UseVisualStyleBackColor = true;
            // 
            // btnManageContacts
            // 
            this.btnManageContacts.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageContacts.Location = new System.Drawing.Point(330, 459);
            this.btnManageContacts.Name = "btnManageContacts";
            this.btnManageContacts.Size = new System.Drawing.Size(270, 31);
            this.btnManageContacts.TabIndex = 24;
            this.btnManageContacts.Text = "Gestionar contactos";
            this.btnManageContacts.UseVisualStyleBackColor = true;
            this.btnManageContacts.Click += new System.EventHandler(this.btnManageContacts_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(117, 462);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 23);
            this.label11.TabIndex = 23;
            this.label11.Text = "Contactos:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(117, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 23);
            this.label6.TabIndex = 12;
            this.label6.Text = "Asignado a:";
            // 
            // cboBusiness
            // 
            this.cboBusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusiness.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusiness.FormattingEnabled = true;
            this.cboBusiness.Location = new System.Drawing.Point(330, 237);
            this.cboBusiness.Name = "cboBusiness";
            this.cboBusiness.Size = new System.Drawing.Size(270, 31);
            this.cboBusiness.TabIndex = 13;
            // 
            // txtCity
            // 
            this.txtCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCity.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(330, 348);
            this.txtCity.MaxLength = 64;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(270, 31);
            this.txtCity.TabIndex = 20;
            // 
            // cboTaxGroup
            // 
            this.cboTaxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaxGroup.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTaxGroup.FormattingEnabled = true;
            this.cboTaxGroup.Location = new System.Drawing.Point(330, 163);
            this.cboTaxGroup.Name = "cboTaxGroup";
            this.cboTaxGroup.Size = new System.Drawing.Size(270, 31);
            this.cboTaxGroup.TabIndex = 9;
            // 
            // cboCountry
            // 
            this.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCountry.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.Location = new System.Drawing.Point(330, 274);
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.Size = new System.Drawing.Size(270, 31);
            this.cboCountry.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(117, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Plazo de pago en días:";
            // 
            // nudPaymentTerm
            // 
            this.nudPaymentTerm.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPaymentTerm.Location = new System.Drawing.Point(330, 200);
            this.nudPaymentTerm.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudPaymentTerm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPaymentTerm.Name = "nudPaymentTerm";
            this.nudPaymentTerm.Size = new System.Drawing.Size(270, 31);
            this.nudPaymentTerm.TabIndex = 11;
            this.nudPaymentTerm.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // cboDistrict
            // 
            this.cboDistrict.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDistrict.FormattingEnabled = true;
            this.cboDistrict.Location = new System.Drawing.Point(330, 311);
            this.cboDistrict.MaxLength = 50;
            this.cboDistrict.Name = "cboDistrict";
            this.cboDistrict.Size = new System.Drawing.Size(270, 31);
            this.cboDistrict.TabIndex = 18;
            // 
            // txtIdentityNumber
            // 
            this.txtIdentityNumber.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentityNumber.Location = new System.Drawing.Point(330, 126);
            this.txtIdentityNumber.MaxLength = 13;
            this.txtIdentityNumber.Name = "txtIdentityNumber";
            this.txtIdentityNumber.Size = new System.Drawing.Size(270, 31);
            this.txtIdentityNumber.TabIndex = 6;
            // 
            // txtAntiquity
            // 
            this.txtAntiquity.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAntiquity.Location = new System.Drawing.Point(330, 533);
            this.txtAntiquity.Name = "txtAntiquity";
            this.txtAntiquity.ReadOnly = true;
            this.txtAntiquity.Size = new System.Drawing.Size(270, 31);
            this.txtAntiquity.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(117, 536);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 23);
            this.label13.TabIndex = 27;
            this.label13.Text = "Antiguedad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(117, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 23);
            this.label7.TabIndex = 14;
            this.label7.Text = "País:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(117, 314);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 23);
            this.label8.TabIndex = 17;
            this.label8.Text = "Provincia o Distrito:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(117, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 23);
            this.label9.TabIndex = 19;
            this.label9.Text = "Localidad:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(117, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 23);
            this.label10.TabIndex = 21;
            this.label10.Text = "Calle y altura:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(121, 422);
            this.txtAddress.MaxLength = 128;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(479, 31);
            this.txtAddress.TabIndex = 22;
            // 
            // dtpRegistryDate
            // 
            this.dtpRegistryDate.CustomFormat = "dd/MM/yy";
            this.dtpRegistryDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRegistryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRegistryDate.Location = new System.Drawing.Point(330, 496);
            this.dtpRegistryDate.Name = "dtpRegistryDate";
            this.dtpRegistryDate.Size = new System.Drawing.Size(270, 31);
            this.dtpRegistryDate.TabIndex = 26;
            this.dtpRegistryDate.ValueChanged += new System.EventHandler(this.dtpRegistryDate_ValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(256, 609);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 40);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cerrar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(462, 609);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 29;
            this.btnAccept.Text = "Guardar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // txtProviderName
            // 
            this.txtProviderName.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProviderName.Location = new System.Drawing.Point(330, 52);
            this.txtProviderName.MaxLength = 64;
            this.txtProviderName.Name = "txtProviderName";
            this.txtProviderName.Size = new System.Drawing.Size(270, 31);
            this.txtProviderName.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(117, 498);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 23);
            this.label12.TabIndex = 25;
            this.label12.Text = "Fecha de alta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(117, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Condición frente al IVA:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "CUIT/DNI:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre del proveedor:";
            // 
            // txtProviderID
            // 
            this.txtProviderID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProviderID.Location = new System.Drawing.Point(330, 15);
            this.txtProviderID.Name = "txtProviderID";
            this.txtProviderID.ReadOnly = true;
            this.txtProviderID.Size = new System.Drawing.Size(270, 31);
            this.txtProviderID.TabIndex = 32;
            this.txtProviderID.Text = "< Generado automáticamente >";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 23);
            this.label1.TabIndex = 31;
            this.label1.Text = "Código interno:";
            // 
            // pnl_2
            // 
            this.pnl_2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_2.BackgroundImage")));
            this.pnl_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl_2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_2.Location = new System.Drawing.Point(614, 126);
            this.pnl_2.Name = "pnl_2";
            this.pnl_2.Size = new System.Drawing.Size(31, 31);
            this.pnl_2.TabIndex = 7;
            this.pnl_2.Visible = false;
            this.pnl_2.Click += new System.EventHandler(this.pnl_2_Click);
            // 
            // pnl_1
            // 
            this.pnl_1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_1.BackgroundImage")));
            this.pnl_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl_1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_1.Location = new System.Drawing.Point(614, 52);
            this.pnl_1.Name = "pnl_1";
            this.pnl_1.Size = new System.Drawing.Size(31, 31);
            this.pnl_1.TabIndex = 3;
            this.pnl_1.Visible = false;
            this.pnl_1.Click += new System.EventHandler(this.pnl_1_Click);
            // 
            // pnlProviderIcon
            // 
            this.pnlProviderIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlProviderIcon.BackgroundImage")));
            this.pnlProviderIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlProviderIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlProviderIcon.Name = "pnlProviderIcon";
            this.pnlProviderIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlProviderIcon.TabIndex = 0;
            // 
            // PV_Provider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 661);
            this.Controls.Add(this.pnlCUITSelection);
            this.Controls.Add(this.btnManageContacts);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboBusiness);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.cboTaxGroup);
            this.Controls.Add(this.cboCountry);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudPaymentTerm);
            this.Controls.Add(this.cboDistrict);
            this.Controls.Add(this.txtIdentityNumber);
            this.Controls.Add(this.txtAntiquity);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pnl_2);
            this.Controls.Add(this.pnl_1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.dtpRegistryDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.pnlProviderIcon);
            this.Controls.Add(this.txtProviderName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProviderID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PV_Provider";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Datos del proveedor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PV_Provider_FormClosing);
            this.Load += new System.EventHandler(this.PV_Provider_Load);
            this.pnlCUITSelection.ResumeLayout(false);
            this.pnlCUITSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaymentTerm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlCUITSelection;
        private System.Windows.Forms.RadioButton rbnIsCUIT;
        private System.Windows.Forms.RadioButton rbnIsDNI;
        private System.Windows.Forms.Button btnManageContacts;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.ComboBox cboTaxGroup;
        private System.Windows.Forms.ComboBox cboCountry;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudPaymentTerm;
        private System.Windows.Forms.ComboBox cboDistrict;
        private System.Windows.Forms.TextBox txtIdentityNumber;
        private System.Windows.Forms.TextBox txtAntiquity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnl_2;
        private System.Windows.Forms.Panel pnl_1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.DateTimePicker dtpRegistryDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Panel pnlProviderIcon;
        private System.Windows.Forms.TextBox txtProviderName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProviderID;
        private System.Windows.Forms.Label label1;
    }
}