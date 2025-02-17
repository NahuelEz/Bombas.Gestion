namespace Clover.Gestion
{
    partial class FormAddUsersToGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddUsersToGroup));
            this.clbUsers = new System.Windows.Forms.CheckedListBox();
            this.btnAddSelectedUsers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clbUsers
            // 
            this.clbUsers.FormattingEnabled = true;
            this.clbUsers.Location = new System.Drawing.Point(12, 90);
            this.clbUsers.Name = "clbUsers";
            this.clbUsers.Size = new System.Drawing.Size(259, 214);
            this.clbUsers.TabIndex = 0;
            // 
            // btnAddSelectedUsers
            // 
            this.btnAddSelectedUsers.Location = new System.Drawing.Point(44, 310);
            this.btnAddSelectedUsers.Name = "btnAddSelectedUsers";
            this.btnAddSelectedUsers.Size = new System.Drawing.Size(186, 35);
            this.btnAddSelectedUsers.TabIndex = 1;
            this.btnAddSelectedUsers.Text = "Agregar Usuarios Seleccionados";
            this.btnAddSelectedUsers.UseVisualStyleBackColor = true;
            this.btnAddSelectedUsers.Click += new System.EventHandler(this.btnAddSelectedUsers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selecciona los usuarios que quieres agregar al grupo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label2.Location = new System.Drawing.Point(51, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Agregar Usuarios";
            // 
            // FormAddUsersToGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 415);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddSelectedUsers);
            this.Controls.Add(this.clbUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddUsersToGroup";
            this.Text = "Añadir Usuarios";
            this.Load += new System.EventHandler(this.FormAddUsersToGroup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbUsers;
        private System.Windows.Forms.Button btnAddSelectedUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}