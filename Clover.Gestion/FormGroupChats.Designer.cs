namespace Clover.Gestion
{
    partial class FormGroupChats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGroupChats));
            this.btnOpenChat_Click = new System.Windows.Forms.Button();
            this.listBoxGroupChats = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditGroup = new System.Windows.Forms.Button();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.btnRemoveUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenChat_Click
            // 
            this.btnOpenChat_Click.BackColor = System.Drawing.Color.OliveDrab;
            this.btnOpenChat_Click.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnOpenChat_Click.Location = new System.Drawing.Point(6, 438);
            this.btnOpenChat_Click.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenChat_Click.Name = "btnOpenChat_Click";
            this.btnOpenChat_Click.Size = new System.Drawing.Size(257, 64);
            this.btnOpenChat_Click.TabIndex = 1;
            this.btnOpenChat_Click.Text = "Abrir Chat";
            this.btnOpenChat_Click.UseVisualStyleBackColor = false;
            this.btnOpenChat_Click.Click += new System.EventHandler(this.btnOpenChat_Click_Click);
            // 
            // listBoxGroupChats
            // 
            this.listBoxGroupChats.FormattingEnabled = true;
            this.listBoxGroupChats.ItemHeight = 16;
            this.listBoxGroupChats.Location = new System.Drawing.Point(32, 65);
            this.listBoxGroupChats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxGroupChats.Name = "listBoxGroupChats";
            this.listBoxGroupChats.Size = new System.Drawing.Size(304, 340);
            this.listBoxGroupChats.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(28, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Selecciona un Grupo:";
            // 
            // btnEditGroup
            // 
            this.btnEditGroup.BackColor = System.Drawing.Color.OliveDrab;
            this.btnEditGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnEditGroup.Location = new System.Drawing.Point(271, 438);
            this.btnEditGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditGroup.Name = "btnEditGroup";
            this.btnEditGroup.Size = new System.Drawing.Size(257, 64);
            this.btnEditGroup.TabIndex = 4;
            this.btnEditGroup.Text = "Editar Nombre";
            this.btnEditGroup.UseVisualStyleBackColor = false;
            this.btnEditGroup.Click += new System.EventHandler(this.btnEditGroup_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.BackColor = System.Drawing.Color.OliveDrab;
            this.btnDeleteGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnDeleteGroup.Location = new System.Drawing.Point(537, 438);
            this.btnDeleteGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(257, 64);
            this.btnDeleteGroup.TabIndex = 5;
            this.btnDeleteGroup.Text = "Eliminar Grupo";
            this.btnDeleteGroup.UseVisualStyleBackColor = false;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.BackColor = System.Drawing.Color.OliveDrab;
            this.btnRemoveUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnRemoveUser.Location = new System.Drawing.Point(802, 438);
            this.btnRemoveUser.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(257, 64);
            this.btnRemoveUser.TabIndex = 6;
            this.btnRemoveUser.Text = "Eliminar Miembro";
            this.btnRemoveUser.UseVisualStyleBackColor = false;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // FormGroupChats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnRemoveUser);
            this.Controls.Add(this.btnDeleteGroup);
            this.Controls.Add(this.btnEditGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxGroupChats);
            this.Controls.Add(this.btnOpenChat_Click);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormGroupChats";
            this.Text = "FormGroupChats";
            this.Load += new System.EventHandler(this.FormGroupChats_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOpenChat_Click;
        private System.Windows.Forms.ListBox listBoxGroupChats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditGroup;
        private System.Windows.Forms.Button btnDeleteGroup;
        private System.Windows.Forms.Button btnRemoveUser;
    }
}