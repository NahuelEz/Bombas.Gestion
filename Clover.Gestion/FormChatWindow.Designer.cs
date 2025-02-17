namespace Clover.Gestion
{
    partial class FormChatWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChatWindow));
            this.listBoxChatMessages = new System.Windows.Forms.ListBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxChatMessages
            // 
            this.listBoxChatMessages.FormattingEnabled = true;
            this.listBoxChatMessages.Location = new System.Drawing.Point(12, 12);
            this.listBoxChatMessages.Name = "listBoxChatMessages";
            this.listBoxChatMessages.Size = new System.Drawing.Size(768, 329);
            this.listBoxChatMessages.TabIndex = 0;
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.txtMessage.Location = new System.Drawing.Point(103, 365);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(480, 26);
            this.txtMessage.TabIndex = 1;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.BackColor = System.Drawing.Color.OliveDrab;
            this.btnSendMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendMessage.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSendMessage.FlatAppearance.BorderSize = 0;
            this.btnSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.Location = new System.Drawing.Point(621, 357);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSendMessage.Size = new System.Drawing.Size(98, 42);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Enviar";
            this.btnSendMessage.UseVisualStyleBackColor = false;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // FormChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.listBoxChatMessages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormChatWindow";
            this.Text = "Ventana de Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxChatMessages;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendMessage;
    }
}