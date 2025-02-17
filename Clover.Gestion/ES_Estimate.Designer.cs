namespace Clover.Gestion
{
    partial class ES_Estimate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ES_Estimate));
            this.label1 = new System.Windows.Forms.Label();
            this.txtEstimateID = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nudValidFor = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboPayment = new System.Windows.Forms.ComboBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.txtTotalBeforeTax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGrandTotal = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlEstimateIcon = new System.Windows.Forms.Panel();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.rbnActive = new System.Windows.Forms.RadioButton();
            this.rbnRejected = new System.Windows.Forms.RadioButton();
            this.rbnSold = new System.Windows.Forms.RadioButton();
            this.lblExpiredWarning = new System.Windows.Forms.Label();
            this.btnShowItems = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblItemsCount = new System.Windows.Forms.Label();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.chkDeliveryNotSpecified = new System.Windows.Forms.CheckBox();
            this.nudDeliveryDelay = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlScrollableContainer = new System.Windows.Forms.Panel();
            this.chkDontTotalize = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlCondition = new System.Windows.Forms.Panel();
            this.rbnIsBlack = new System.Windows.Forms.RadioButton();
            this.rbnIsWhite = new System.Windows.Forms.RadioButton();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudDiscount = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.nudWarrantyMonths = new System.Windows.Forms.NumericUpDown();
            this.btnPercentageAdjustement = new System.Windows.Forms.Button();
            this.cboContact = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEditDescription = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtVatTotal = new System.Windows.Forms.TextBox();
            this.btnMakePDF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudValidFor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeliveryDelay)).BeginInit();
            this.pnlScrollableContainer.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarrantyMonths)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 23);
            this.label1.TabIndex = 41;
            this.label1.Text = "ID Presupuesto:";
            // 
            // txtEstimateID
            // 
            this.txtEstimateID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstimateID.Location = new System.Drawing.Point(219, 2);
            this.txtEstimateID.Name = "txtEstimateID";
            this.txtEstimateID.ReadOnly = true;
            this.txtEstimateID.Size = new System.Drawing.Size(270, 31);
            this.txtEstimateID.TabIndex = 42;
            this.txtEstimateID.Text = "< Generado automáticamente >";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(219, 39);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(270, 31);
            this.dtpDate.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 23);
            this.label2.TabIndex = 43;
            this.label2.Text = "Fecha de presupuesto:";
            // 
            // cboCustomer
            // 
            this.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCustomer.DropDownWidth = 400;
            this.cboCustomer.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(219, 150);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(270, 31);
            this.cboCustomer.TabIndex = 1;
            this.cboCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.cboCustomer_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Cliente:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "Descripción:";
            // 
            // cboCurrency
            // 
            this.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(219, 492);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(270, 31);
            this.cboCurrency.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(5, 495);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 23);
            this.label13.TabIndex = 21;
            this.label13.Text = "Moneda:";
            // 
            // nudValidFor
            // 
            this.nudValidFor.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudValidFor.Location = new System.Drawing.Point(219, 671);
            this.nudValidFor.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudValidFor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudValidFor.Name = "nudValidFor";
            this.nudValidFor.Size = new System.Drawing.Size(100, 31);
            this.nudValidFor.TabIndex = 33;
            this.nudValidFor.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(5, 674);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 23);
            this.label19.TabIndex = 32;
            this.label19.Text = "Validez:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(5, 711);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(195, 23);
            this.label21.TabIndex = 36;
            this.label21.Text = "Estado del presupuesto:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(5, 532);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 23);
            this.label14.TabIndex = 23;
            this.label14.Text = "Medio de pago:";
            // 
            // cboPayment
            // 
            this.cboPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayment.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPayment.FormattingEnabled = true;
            this.cboPayment.Location = new System.Drawing.Point(219, 529);
            this.cboPayment.Name = "cboPayment";
            this.cboPayment.Size = new System.Drawing.Size(270, 31);
            this.cboPayment.TabIndex = 24;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(512, 809);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(150, 40);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "Registrar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // txtTotalBeforeTax
            // 
            this.txtTotalBeforeTax.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBeforeTax.Location = new System.Drawing.Point(339, 381);
            this.txtTotalBeforeTax.Name = "txtTotalBeforeTax";
            this.txtTotalBeforeTax.ReadOnly = true;
            this.txtTotalBeforeTax.Size = new System.Drawing.Size(150, 31);
            this.txtTotalBeforeTax.TabIndex = 16;
            this.txtTotalBeforeTax.Text = "0,00";
            this.txtTotalBeforeTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(5, 384);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 23);
            this.label10.TabIndex = 15;
            this.label10.Text = "Total:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 23);
            this.label8.TabIndex = 12;
            this.label8.Text = "Descuento:";
            // 
            // txtGrandTotal
            // 
            this.txtGrandTotal.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrandTotal.Location = new System.Drawing.Point(339, 455);
            this.txtGrandTotal.Name = "txtGrandTotal";
            this.txtGrandTotal.ReadOnly = true;
            this.txtGrandTotal.Size = new System.Drawing.Size(150, 31);
            this.txtGrandTotal.TabIndex = 20;
            this.txtGrandTotal.Text = "0,00";
            this.txtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(5, 458);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 23);
            this.label12.TabIndex = 19;
            this.label12.Text = "Total IVA incluido:";
            // 
            // pnlEstimateIcon
            // 
            this.pnlEstimateIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlEstimateIcon.BackgroundImage")));
            this.pnlEstimateIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlEstimateIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlEstimateIcon.Name = "pnlEstimateIcon";
            this.pnlEstimateIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlEstimateIcon.TabIndex = 0;
            // 
            // cboBusiness
            // 
            this.cboBusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusiness.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusiness.FormattingEnabled = true;
            this.cboBusiness.Location = new System.Drawing.Point(219, 76);
            this.cboBusiness.Name = "cboBusiness";
            this.cboBusiness.Size = new System.Drawing.Size(270, 31);
            this.cboBusiness.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 23);
            this.label3.TabIndex = 45;
            this.label3.Text = "Empresa:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(5, 637);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 23);
            this.label17.TabIndex = 29;
            this.label17.Text = "Garantía:";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAs.Enabled = false;
            this.btnSaveAs.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAs.Location = new System.Drawing.Point(324, 809);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(150, 40);
            this.btnSaveAs.TabIndex = 3;
            this.btnSaveAs.Text = "Guardar copia";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // rbnActive
            // 
            this.rbnActive.AutoSize = true;
            this.rbnActive.Checked = true;
            this.rbnActive.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnActive.Location = new System.Drawing.Point(3, 3);
            this.rbnActive.Name = "rbnActive";
            this.rbnActive.Size = new System.Drawing.Size(77, 27);
            this.rbnActive.TabIndex = 0;
            this.rbnActive.TabStop = true;
            this.rbnActive.Text = "Activo";
            this.rbnActive.UseVisualStyleBackColor = true;
            // 
            // rbnRejected
            // 
            this.rbnRejected.AutoSize = true;
            this.rbnRejected.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnRejected.ForeColor = System.Drawing.Color.Red;
            this.rbnRejected.Location = new System.Drawing.Point(86, 3);
            this.rbnRejected.Name = "rbnRejected";
            this.rbnRejected.Size = new System.Drawing.Size(111, 27);
            this.rbnRejected.TabIndex = 1;
            this.rbnRejected.Text = "Rechazado";
            this.rbnRejected.UseVisualStyleBackColor = true;
            // 
            // rbnSold
            // 
            this.rbnSold.AutoSize = true;
            this.rbnSold.Enabled = false;
            this.rbnSold.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnSold.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbnSold.Location = new System.Drawing.Point(203, 3);
            this.rbnSold.Name = "rbnSold";
            this.rbnSold.Size = new System.Drawing.Size(91, 27);
            this.rbnSold.TabIndex = 2;
            this.rbnSold.Text = "Vendido";
            this.rbnSold.UseVisualStyleBackColor = true;
            // 
            // lblExpiredWarning
            // 
            this.lblExpiredWarning.AutoSize = true;
            this.lblExpiredWarning.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiredWarning.ForeColor = System.Drawing.Color.Red;
            this.lblExpiredWarning.Location = new System.Drawing.Point(384, 674);
            this.lblExpiredWarning.Name = "lblExpiredWarning";
            this.lblExpiredWarning.Size = new System.Drawing.Size(138, 23);
            this.lblExpiredWarning.TabIndex = 35;
            this.lblExpiredWarning.Text = "Presup. vencido.";
            this.lblExpiredWarning.Visible = false;
            // 
            // btnShowItems
            // 
            this.btnShowItems.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowItems.Location = new System.Drawing.Point(219, 261);
            this.btnShowItems.Name = "btnShowItems";
            this.btnShowItems.Size = new System.Drawing.Size(200, 40);
            this.btnShowItems.TabIndex = 8;
            this.btnShowItems.Text = "Visualizar detalle";
            this.btnShowItems.UseVisualStyleBackColor = true;
            this.btnShowItems.Click += new System.EventHandler(this.btnOpenDetail_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 264);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "Detalle:";
            // 
            // lblItemsCount
            // 
            this.lblItemsCount.AutoSize = true;
            this.lblItemsCount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(153)))), ((int)(((byte)(67)))));
            this.lblItemsCount.Location = new System.Drawing.Point(79, 264);
            this.lblItemsCount.Name = "lblItemsCount";
            this.lblItemsCount.Size = new System.Drawing.Size(81, 23);
            this.lblItemsCount.TabIndex = 7;
            this.lblItemsCount.Text = "0 ítem(s)";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendEmail.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Location = new System.Drawing.Point(12, 809);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(150, 40);
            this.btnSendEmail.TabIndex = 5;
            this.btnSendEmail.Text = "Enviar y guardar";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(5, 569);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(146, 23);
            this.label15.TabIndex = 25;
            this.label15.Text = "Fecha de entrega:";
            // 
            // chkDeliveryNotSpecified
            // 
            this.chkDeliveryNotSpecified.AutoSize = true;
            this.chkDeliveryNotSpecified.Checked = true;
            this.chkDeliveryNotSpecified.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeliveryNotSpecified.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDeliveryNotSpecified.Location = new System.Drawing.Point(219, 601);
            this.chkDeliveryNotSpecified.Name = "chkDeliveryNotSpecified";
            this.chkDeliveryNotSpecified.Size = new System.Drawing.Size(150, 27);
            this.chkDeliveryNotSpecified.TabIndex = 28;
            this.chkDeliveryNotSpecified.Text = "No especificado";
            this.chkDeliveryNotSpecified.UseVisualStyleBackColor = true;
            // 
            // nudDeliveryDelay
            // 
            this.nudDeliveryDelay.Enabled = false;
            this.nudDeliveryDelay.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDeliveryDelay.Location = new System.Drawing.Point(219, 566);
            this.nudDeliveryDelay.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudDeliveryDelay.Name = "nudDeliveryDelay";
            this.nudDeliveryDelay.Size = new System.Drawing.Size(100, 31);
            this.nudDeliveryDelay.TabIndex = 26;
            this.nudDeliveryDelay.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Enabled = false;
            this.label16.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(325, 569);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(229, 23);
            this.label16.TabIndex = 27;
            this.label16.Text = "día(s) a partir de aceptación.";
            // 
            // pnlScrollableContainer
            // 
            this.pnlScrollableContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlScrollableContainer.AutoScroll = true;
            this.pnlScrollableContainer.Controls.Add(this.chkDontTotalize);
            this.pnlScrollableContainer.Controls.Add(this.label24);
            this.pnlScrollableContainer.Controls.Add(this.pnlCondition);
            this.pnlScrollableContainer.Controls.Add(this.pnlStatus);
            this.pnlScrollableContainer.Controls.Add(this.label23);
            this.pnlScrollableContainer.Controls.Add(this.cboDepartment);
            this.pnlScrollableContainer.Controls.Add(this.txtTotalAmount);
            this.pnlScrollableContainer.Controls.Add(this.label22);
            this.pnlScrollableContainer.Controls.Add(this.label20);
            this.pnlScrollableContainer.Controls.Add(this.label9);
            this.pnlScrollableContainer.Controls.Add(this.nudDiscount);
            this.pnlScrollableContainer.Controls.Add(this.label18);
            this.pnlScrollableContainer.Controls.Add(this.nudWarrantyMonths);
            this.pnlScrollableContainer.Controls.Add(this.btnPercentageAdjustement);
            this.pnlScrollableContainer.Controls.Add(this.cboContact);
            this.pnlScrollableContainer.Controls.Add(this.label5);
            this.pnlScrollableContainer.Controls.Add(this.btnEditDescription);
            this.pnlScrollableContainer.Controls.Add(this.label11);
            this.pnlScrollableContainer.Controls.Add(this.txtVatTotal);
            this.pnlScrollableContainer.Controls.Add(this.label1);
            this.pnlScrollableContainer.Controls.Add(this.label16);
            this.pnlScrollableContainer.Controls.Add(this.txtEstimateID);
            this.pnlScrollableContainer.Controls.Add(this.nudDeliveryDelay);
            this.pnlScrollableContainer.Controls.Add(this.dtpDate);
            this.pnlScrollableContainer.Controls.Add(this.chkDeliveryNotSpecified);
            this.pnlScrollableContainer.Controls.Add(this.label2);
            this.pnlScrollableContainer.Controls.Add(this.label15);
            this.pnlScrollableContainer.Controls.Add(this.cboCustomer);
            this.pnlScrollableContainer.Controls.Add(this.label4);
            this.pnlScrollableContainer.Controls.Add(this.lblItemsCount);
            this.pnlScrollableContainer.Controls.Add(this.label7);
            this.pnlScrollableContainer.Controls.Add(this.label6);
            this.pnlScrollableContainer.Controls.Add(this.btnShowItems);
            this.pnlScrollableContainer.Controls.Add(this.cboCurrency);
            this.pnlScrollableContainer.Controls.Add(this.lblExpiredWarning);
            this.pnlScrollableContainer.Controls.Add(this.label13);
            this.pnlScrollableContainer.Controls.Add(this.nudValidFor);
            this.pnlScrollableContainer.Controls.Add(this.label19);
            this.pnlScrollableContainer.Controls.Add(this.label21);
            this.pnlScrollableContainer.Controls.Add(this.cboPayment);
            this.pnlScrollableContainer.Controls.Add(this.label14);
            this.pnlScrollableContainer.Controls.Add(this.label17);
            this.pnlScrollableContainer.Controls.Add(this.txtTotalBeforeTax);
            this.pnlScrollableContainer.Controls.Add(this.label10);
            this.pnlScrollableContainer.Controls.Add(this.label3);
            this.pnlScrollableContainer.Controls.Add(this.cboBusiness);
            this.pnlScrollableContainer.Controls.Add(this.label8);
            this.pnlScrollableContainer.Controls.Add(this.txtGrandTotal);
            this.pnlScrollableContainer.Controls.Add(this.label12);
            this.pnlScrollableContainer.Location = new System.Drawing.Point(111, 13);
            this.pnlScrollableContainer.Name = "pnlScrollableContainer";
            this.pnlScrollableContainer.Size = new System.Drawing.Size(560, 775);
            this.pnlScrollableContainer.TabIndex = 1;
            // 
            // chkDontTotalize
            // 
            this.chkDontTotalize.AutoSize = true;
            this.chkDontTotalize.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDontTotalize.Location = new System.Drawing.Point(219, 457);
            this.chkDontTotalize.Name = "chkDontTotalize";
            this.chkDontTotalize.Size = new System.Drawing.Size(118, 27);
            this.chkDontTotalize.TabIndex = 19;
            this.chkDontTotalize.Text = "No totalizar";
            this.chkDontTotalize.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(5, 748);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(91, 23);
            this.label24.TabIndex = 38;
            this.label24.Text = "Condición:";
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.rbnIsBlack);
            this.pnlCondition.Controls.Add(this.rbnIsWhite);
            this.pnlCondition.Location = new System.Drawing.Point(216, 742);
            this.pnlCondition.Name = "pnlCondition";
            this.pnlCondition.Size = new System.Drawing.Size(310, 31);
            this.pnlCondition.TabIndex = 39;
            // 
            // rbnIsBlack
            // 
            this.rbnIsBlack.AutoSize = true;
            this.rbnIsBlack.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsBlack.Location = new System.Drawing.Point(86, 3);
            this.rbnIsBlack.Name = "rbnIsBlack";
            this.rbnIsBlack.Size = new System.Drawing.Size(40, 27);
            this.rbnIsBlack.TabIndex = 1;
            this.rbnIsBlack.Text = "N";
            this.rbnIsBlack.UseVisualStyleBackColor = true;
            // 
            // rbnIsWhite
            // 
            this.rbnIsWhite.AutoSize = true;
            this.rbnIsWhite.Checked = true;
            this.rbnIsWhite.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsWhite.Location = new System.Drawing.Point(3, 3);
            this.rbnIsWhite.Name = "rbnIsWhite";
            this.rbnIsWhite.Size = new System.Drawing.Size(38, 27);
            this.rbnIsWhite.TabIndex = 0;
            this.rbnIsWhite.TabStop = true;
            this.rbnIsWhite.Text = "B";
            this.rbnIsWhite.UseVisualStyleBackColor = true;
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.rbnActive);
            this.pnlStatus.Controls.Add(this.rbnRejected);
            this.pnlStatus.Controls.Add(this.rbnSold);
            this.pnlStatus.Location = new System.Drawing.Point(216, 706);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(310, 31);
            this.pnlStatus.TabIndex = 37;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(5, 116);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(127, 23);
            this.label23.TabIndex = 47;
            this.label23.Text = "Departamento:";
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(219, 113);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(270, 31);
            this.cboDepartment.TabIndex = 48;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(339, 307);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(150, 31);
            this.txtTotalAmount.TabIndex = 11;
            this.txtTotalAmount.Text = "0,00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(5, 310);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 23);
            this.label22.TabIndex = 10;
            this.label22.Text = "Subtotal:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(325, 674);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 23);
            this.label20.TabIndex = 34;
            this.label20.Text = "día(s)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(495, 347);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 23);
            this.label9.TabIndex = 14;
            this.label9.Text = "%";
            // 
            // nudDiscount
            // 
            this.nudDiscount.DecimalPlaces = 2;
            this.nudDiscount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDiscount.Location = new System.Drawing.Point(339, 344);
            this.nudDiscount.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudDiscount.Name = "nudDiscount";
            this.nudDiscount.Size = new System.Drawing.Size(150, 31);
            this.nudDiscount.TabIndex = 13;
            this.nudDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDiscount.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudDiscount.ValueChanged += new System.EventHandler(this.nudDiscount_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(325, 637);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 23);
            this.label18.TabIndex = 31;
            this.label18.Text = "mes(es)";
            // 
            // nudWarrantyMonths
            // 
            this.nudWarrantyMonths.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudWarrantyMonths.Location = new System.Drawing.Point(219, 634);
            this.nudWarrantyMonths.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudWarrantyMonths.Name = "nudWarrantyMonths";
            this.nudWarrantyMonths.Size = new System.Drawing.Size(100, 31);
            this.nudWarrantyMonths.TabIndex = 30;
            // 
            // btnPercentageAdjustement
            // 
            this.btnPercentageAdjustement.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPercentageAdjustement.Location = new System.Drawing.Point(425, 261);
            this.btnPercentageAdjustement.Name = "btnPercentageAdjustement";
            this.btnPercentageAdjustement.Size = new System.Drawing.Size(64, 40);
            this.btnPercentageAdjustement.TabIndex = 9;
            this.btnPercentageAdjustement.Text = "+-%";
            this.btnPercentageAdjustement.UseVisualStyleBackColor = true;
            this.btnPercentageAdjustement.Click += new System.EventHandler(this.btnPercentageAdjustment_Click);
            // 
            // cboContact
            // 
            this.cboContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboContact.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboContact.FormattingEnabled = true;
            this.cboContact.Location = new System.Drawing.Point(219, 187);
            this.cboContact.Name = "cboContact";
            this.cboContact.Size = new System.Drawing.Size(270, 31);
            this.cboContact.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Contacto:";
            // 
            // btnEditDescription
            // 
            this.btnEditDescription.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDescription.Location = new System.Drawing.Point(219, 224);
            this.btnEditDescription.Name = "btnEditDescription";
            this.btnEditDescription.Size = new System.Drawing.Size(270, 31);
            this.btnEditDescription.TabIndex = 5;
            this.btnEditDescription.Text = "Editar descripción";
            this.btnEditDescription.UseVisualStyleBackColor = true;
            this.btnEditDescription.Click += new System.EventHandler(this.btnEditDescription_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 421);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 23);
            this.label11.TabIndex = 17;
            this.label11.Text = "Importe IVA:";
            // 
            // txtVatTotal
            // 
            this.txtVatTotal.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatTotal.Location = new System.Drawing.Point(339, 418);
            this.txtVatTotal.Name = "txtVatTotal";
            this.txtVatTotal.ReadOnly = true;
            this.txtVatTotal.Size = new System.Drawing.Size(150, 31);
            this.txtVatTotal.TabIndex = 18;
            this.txtVatTotal.Text = "0,00";
            this.txtVatTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnMakePDF
            // 
            this.btnMakePDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMakePDF.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakePDF.Location = new System.Drawing.Point(168, 809);
            this.btnMakePDF.Name = "btnMakePDF";
            this.btnMakePDF.Size = new System.Drawing.Size(150, 40);
            this.btnMakePDF.TabIndex = 4;
            this.btnMakePDF.Text = "Generar PDF";
            this.btnMakePDF.UseVisualStyleBackColor = true;
            this.btnMakePDF.Click += new System.EventHandler(this.btnMakePDF_Click);
            // 
            // ES_Estimate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 861);
            this.Controls.Add(this.pnlScrollableContainer);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnMakePDF);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.pnlEstimateIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ES_Estimate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generar presupuesto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ES_Estimate_FormClosing);
            this.Load += new System.EventHandler(this.ES_Estimate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudValidFor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeliveryDelay)).EndInit();
            this.pnlScrollableContainer.ResumeLayout(false);
            this.pnlScrollableContainer.PerformLayout();
            this.pnlCondition.ResumeLayout(false);
            this.pnlCondition.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarrantyMonths)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEstimateIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEstimateID;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudValidFor;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboPayment;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.TextBox txtTotalBeforeTax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGrandTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.RadioButton rbnActive;
        private System.Windows.Forms.RadioButton rbnRejected;
        private System.Windows.Forms.RadioButton rbnSold;
        private System.Windows.Forms.Label lblExpiredWarning;
        private System.Windows.Forms.Button btnShowItems;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblItemsCount;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkDeliveryNotSpecified;
        private System.Windows.Forms.NumericUpDown nudDeliveryDelay;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlScrollableContainer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtVatTotal;
        private System.Windows.Forms.ComboBox cboContact;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditDescription;
        private System.Windows.Forms.Button btnPercentageAdjustement;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudDiscount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudWarrantyMonths;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnMakePDF;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel pnlCondition;
        private System.Windows.Forms.RadioButton rbnIsBlack;
        private System.Windows.Forms.RadioButton rbnIsWhite;
        private System.Windows.Forms.CheckBox chkDontTotalize;
    }
}