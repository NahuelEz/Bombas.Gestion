namespace Clover.Gestion
{
    partial class CH_PrivateChat
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CH_PrivateChat));
            this.txtChatInput = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.lblAddresseeName = new System.Windows.Forms.Label();
            this.cmsMessageHistoryOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsItemClearHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbChatMessages = new System.Windows.Forms.RichTextBox();
            this.cmsMessageHistoryOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtChatInput
            // 
            this.txtChatInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChatInput.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChatInput.Location = new System.Drawing.Point(13, 287);
            this.txtChatInput.MaxLength = 1024;
            this.txtChatInput.Multiline = true;
            this.txtChatInput.Name = "txtChatInput";
            this.txtChatInput.Size = new System.Drawing.Size(403, 62);
            this.txtChatInput.TabIndex = 0;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMessage.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.Location = new System.Drawing.Point(422, 287);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(150, 35);
            this.btnSendMessage.TabIndex = 1;
            this.btnSendMessage.Text = "Enviar";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // lblAddresseeName
            // 
            this.lblAddresseeName.AutoSize = true;
            this.lblAddresseeName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddresseeName.Location = new System.Drawing.Point(12, 9);
            this.lblAddresseeName.Name = "lblAddresseeName";
            this.lblAddresseeName.Size = new System.Drawing.Size(0, 26);
            this.lblAddresseeName.TabIndex = 2;
            // 
            // cmsMessageHistoryOptions
            // 
            this.cmsMessageHistoryOptions.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsMessageHistoryOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemClearHistory});
            this.cmsMessageHistoryOptions.Name = "cmsMessageHistoryOptions";
            this.cmsMessageHistoryOptions.Size = new System.Drawing.Size(197, 32);
            // 
            // cmsItemClearHistory
            // 
            this.cmsItemClearHistory.Name = "cmsItemClearHistory";
            this.cmsItemClearHistory.Size = new System.Drawing.Size(196, 28);
            this.cmsItemClearHistory.Text = "Borrar historial";
            this.cmsItemClearHistory.Click += new System.EventHandler(this.cmsItemClearHistory_Click);
            // 
            // rtbChatMessages
            // 
            this.rtbChatMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbChatMessages.ContextMenuStrip = this.cmsMessageHistoryOptions;
            this.rtbChatMessages.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbChatMessages.Location = new System.Drawing.Point(12, 38);
            this.rtbChatMessages.Name = "rtbChatMessages";
            this.rtbChatMessages.ReadOnly = true;
            this.rtbChatMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbChatMessages.Size = new System.Drawing.Size(560, 243);
            this.rtbChatMessages.TabIndex = 3;
            this.rtbChatMessages.Text = "";
            // 
            // CH_PrivateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.rtbChatMessages);
            this.Controls.Add(this.lblAddresseeName);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtChatInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "CH_PrivateChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat privado";
            this.Load += new System.EventHandler(this.CH_PrivateChat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CH_PrivateChat_KeyDown);
            this.cmsMessageHistoryOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtChatInput;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Label lblAddresseeName;
        private System.Windows.Forms.ContextMenuStrip cmsMessageHistoryOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsItemClearHistory;
        private System.Windows.Forms.RichTextBox rtbChatMessages;
    }
}