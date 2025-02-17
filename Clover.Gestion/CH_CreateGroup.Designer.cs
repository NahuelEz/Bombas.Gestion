namespace Clover.Gestion
{
    partial class CH_CreateGroup
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
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.clbUsers = new System.Windows.Forms.CheckedListBox();
            this.btnCreateGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(83, 52);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(100, 20);
            this.txtGroupName.TabIndex = 0;
            // 
            // clbUsers
            // 
            this.clbUsers.FormattingEnabled = true;
            this.clbUsers.Location = new System.Drawing.Point(104, 124);
            this.clbUsers.Name = "clbUsers";
            this.clbUsers.Size = new System.Drawing.Size(120, 94);
            this.clbUsers.TabIndex = 1;
            // 
            // btnCreateGroup
            // 
            this.btnCreateGroup.Location = new System.Drawing.Point(59, 224);
            this.btnCreateGroup.Name = "btnCreateGroup";
            this.btnCreateGroup.Size = new System.Drawing.Size(75, 23);
            this.btnCreateGroup.TabIndex = 2;
            this.btnCreateGroup.Text = "button1";
            this.btnCreateGroup.UseVisualStyleBackColor = true;
            // 
            // CH_CreateGroup
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnCreateGroup);
            this.Controls.Add(this.clbUsers);
            this.Controls.Add(this.txtGroupName);
            this.Name = "CH_CreateGroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.CheckedListBox clbUsers;
        private System.Windows.Forms.Button btnCreateGroup;
    }
}