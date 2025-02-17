namespace Clover.Gestion
{
    partial class ISN_IssuedNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ISN_IssuedNote));
            this.txtNoteID = new System.Windows.Forms.TextBox();
            this.pnlNoteIcon = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.nudTotalAmount = new System.Windows.Forms.NumericUpDown();
            this.txtNoteNumber = new System.Windows.Forms.TextBox();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.cboNoteType = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.cboBusiness = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbnIsDebit = new System.Windows.Forms.RadioButton();
            this.rbnIsCredit = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlDebitCredit = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).BeginInit();
            this.pnlDebitCredit.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNoteID
            // 
            this.txtNoteID.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoteID.Location = new System.Drawing.Point(330, 15);
            this.txtNoteID.Name = "txtNoteID";
            this.txtNoteID.ReadOnly = true;
            this.txtNoteID.Size = new System.Drawing.Size(270, 31);
            this.txtNoteID.TabIndex = 18;
            this.txtNoteID.Text = "< Generado automáticamente >";
            // 
            // pnlNoteIcon
            // 
            this.pnlNoteIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlNoteIcon.BackgroundImage")));
            this.pnlNoteIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlNoteIcon.Location = new System.Drawing.Point(12, 12);
            this.pnlNoteIcon.Name = "pnlNoteIcon";
            this.pnlNoteIcon.Size = new System.Drawing.Size(80, 80);
            this.pnlNoteIcon.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(256, 564);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 40);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cerrar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(462, 564);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(200, 40);
            this.btnAccept.TabIndex = 15;
            this.btnAccept.Text = "Guardar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // nudTotalAmount
            // 
            this.nudTotalAmount.DecimalPlaces = 2;
            this.nudTotalAmount.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTotalAmount.Location = new System.Drawing.Point(330, 274);
            this.nudTotalAmount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudTotalAmount.Name = "nudTotalAmount";
            this.nudTotalAmount.Size = new System.Drawing.Size(150, 31);
            this.nudTotalAmount.TabIndex = 10;
            this.nudTotalAmount.ThousandsSeparator = true;
            // 
            // txtNoteNumber
            // 
            this.txtNoteNumber.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoteNumber.Location = new System.Drawing.Point(330, 237);
            this.txtNoteNumber.MaxLength = 32;
            this.txtNoteNumber.Name = "txtNoteNumber";
            this.txtNoteNumber.Size = new System.Drawing.Size(270, 31);
            this.txtNoteNumber.TabIndex = 8;
            // 
            // cboCurrency
            // 
            this.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrency.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(330, 311);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(270, 31);
            this.cboCurrency.TabIndex = 12;
            // 
            // cboNoteType
            // 
            this.cboNoteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNoteType.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNoteType.FormattingEnabled = true;
            this.cboNoteType.Location = new System.Drawing.Point(330, 200);
            this.cboNoteType.Name = "cboNoteType";
            this.cboNoteType.Size = new System.Drawing.Size(270, 31);
            this.cboNoteType.TabIndex = 6;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(330, 89);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(270, 31);
            this.dtpDate.TabIndex = 22;
            // 
            // cboCustomer
            // 
            this.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCustomer.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(330, 126);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(270, 31);
            this.cboCustomer.TabIndex = 2;
            this.cboCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.cboCustomer_Validating);
            // 
            // cboBusiness
            // 
            this.cboBusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBusiness.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusiness.FormattingEnabled = true;
            this.cboBusiness.Location = new System.Drawing.Point(330, 52);
            this.cboBusiness.Name = "cboBusiness";
            this.cboBusiness.Size = new System.Drawing.Size(270, 31);
            this.cboBusiness.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(117, 314);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 23);
            this.label9.TabIndex = 11;
            this.label9.Text = "Moneda:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(117, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 23);
            this.label8.TabIndex = 9;
            this.label8.Text = "Importe:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(117, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "N° de nota:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(117, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tipo nota:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(117, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cliente:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 23);
            this.label3.TabIndex = 21;
            this.label3.Text = "Fecha nota:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(117, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 23);
            this.label2.TabIndex = 19;
            this.label2.Text = "Empresa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 23);
            this.label1.TabIndex = 17;
            this.label1.Text = "ID Nota:";
            // 
            // rbnIsDebit
            // 
            this.rbnIsDebit.AutoSize = true;
            this.rbnIsDebit.Checked = true;
            this.rbnIsDebit.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsDebit.Location = new System.Drawing.Point(3, 3);
            this.rbnIsDebit.Name = "rbnIsDebit";
            this.rbnIsDebit.Size = new System.Drawing.Size(79, 27);
            this.rbnIsDebit.TabIndex = 0;
            this.rbnIsDebit.TabStop = true;
            this.rbnIsDebit.Text = "Débito";
            this.rbnIsDebit.UseVisualStyleBackColor = true;
            // 
            // rbnIsCredit
            // 
            this.rbnIsCredit.AutoSize = true;
            this.rbnIsCredit.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIsCredit.Location = new System.Drawing.Point(88, 3);
            this.rbnIsCredit.Name = "rbnIsCredit";
            this.rbnIsCredit.Size = new System.Drawing.Size(84, 27);
            this.rbnIsCredit.TabIndex = 1;
            this.rbnIsCredit.Text = "Crédito";
            this.rbnIsCredit.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(117, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 23);
            this.label5.TabIndex = 3;
            this.label5.Text = "Débito/Crédito:";
            // 
            // pnlDebitCredit
            // 
            this.pnlDebitCredit.Controls.Add(this.rbnIsDebit);
            this.pnlDebitCredit.Controls.Add(this.rbnIsCredit);
            this.pnlDebitCredit.Location = new System.Drawing.Point(330, 163);
            this.pnlDebitCredit.Name = "pnlDebitCredit";
            this.pnlDebitCredit.Size = new System.Drawing.Size(270, 31);
            this.pnlDebitCredit.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(117, 351);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 23);
            this.label10.TabIndex = 13;
            this.label10.Text = "Motivo:";
            // 
            // txtReason
            // 
            this.txtReason.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(121, 385);
            this.txtReason.MaxLength = 1024;
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(479, 124);
            this.txtReason.TabIndex = 14;
            // 
            // ISN_IssuedNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 616);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pnlDebitCredit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.nudTotalAmount);
            this.Controls.Add(this.txtNoteNumber);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.cboNoteType);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.cboBusiness);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlNoteIcon);
            this.Controls.Add(this.txtNoteID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ISN_IssuedNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar nota de débito/crédito";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ISN_IssuedNote_FormClosing);
            this.Load += new System.EventHandler(this.ISN_IssuedNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).EndInit();
            this.pnlDebitCredit.ResumeLayout(false);
            this.pnlDebitCredit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNoteID;
        private System.Windows.Forms.Panel pnlNoteIcon;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.NumericUpDown nudTotalAmount;
        private System.Windows.Forms.TextBox txtNoteNumber;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.ComboBox cboNoteType;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.ComboBox cboBusiness;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbnIsDebit;
        private System.Windows.Forms.RadioButton rbnIsCredit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlDebitCredit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtReason;
    }
}