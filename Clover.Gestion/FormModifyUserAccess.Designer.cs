namespace Clover.Gestion
{
    partial class FormModifyUserAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModifyUserAccess));
            this.cboUsers = new System.Windows.Forms.ComboBox();
            this.btnModifyAccess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboAccessLevel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboUsers
            // 
            this.cboUsers.FormattingEnabled = true;
            this.cboUsers.Location = new System.Drawing.Point(12, 66);
            this.cboUsers.Name = "cboUsers";
            this.cboUsers.Size = new System.Drawing.Size(229, 21);
            this.cboUsers.TabIndex = 0;
            // 
            // btnModifyAccess
            // 
            this.btnModifyAccess.BackColor = System.Drawing.Color.Olive;
            this.btnModifyAccess.Location = new System.Drawing.Point(17, 307);
            this.btnModifyAccess.Name = "btnModifyAccess";
            this.btnModifyAccess.Size = new System.Drawing.Size(131, 34);
            this.btnModifyAccess.TabIndex = 3;
            this.btnModifyAccess.Text = "CONFIRMAR";
            this.btnModifyAccess.UseVisualStyleBackColor = false;
            this.btnModifyAccess.Click += new System.EventHandler(this.btnModifyAccess_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label1.Location = new System.Drawing.Point(8, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selecciona el Usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.Location = new System.Drawing.Point(8, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Asignar Rango:";
            // 
            // cboAccessLevel
            // 
            this.cboAccessLevel.FormattingEnabled = true;
            this.cboAccessLevel.Location = new System.Drawing.Point(12, 145);
            this.cboAccessLevel.Name = "cboAccessLevel";
            this.cboAccessLevel.Size = new System.Drawing.Size(229, 21);
            this.cboAccessLevel.TabIndex = 6;
            // 
            // FormModifyUserAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(462, 450);
            this.Controls.Add(this.cboAccessLevel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnModifyAccess);
            this.Controls.Add(this.cboUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormModifyUserAccess";
            this.Text = "Asignar Rango";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboUsers;
        private System.Windows.Forms.Button btnModifyAccess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboAccessLevel;
    }
}