namespace Clover.Gestion
{
    partial class SHA_ManageAttachments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SHA_ManageAttachments));
            this.lbxAttachments = new System.Windows.Forms.ListBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnAddAttachment = new System.Windows.Forms.Button();
            this.btnRemoveAttachment = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxAttachments
            // 
            this.lbxAttachments.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxAttachments.FormattingEnabled = true;
            this.lbxAttachments.IntegralHeight = false;
            this.lbxAttachments.ItemHeight = 23;
            this.lbxAttachments.Location = new System.Drawing.Point(13, 13);
            this.lbxAttachments.Name = "lbxAttachments";
            this.lbxAttachments.Size = new System.Drawing.Size(559, 290);
            this.lbxAttachments.TabIndex = 0;
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(422, 309);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(150, 40);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnAddAttachment
            // 
            this.btnAddAttachment.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAttachment.Location = new System.Drawing.Point(13, 309);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(150, 40);
            this.btnAddAttachment.TabIndex = 2;
            this.btnAddAttachment.Text = "Agregar";
            this.btnAddAttachment.UseVisualStyleBackColor = true;
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);
            // 
            // btnRemoveAttachment
            // 
            this.btnRemoveAttachment.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAttachment.Location = new System.Drawing.Point(169, 309);
            this.btnRemoveAttachment.Name = "btnRemoveAttachment";
            this.btnRemoveAttachment.Size = new System.Drawing.Size(150, 40);
            this.btnRemoveAttachment.TabIndex = 3;
            this.btnRemoveAttachment.Text = "Quitar";
            this.btnRemoveAttachment.UseVisualStyleBackColor = true;
            this.btnRemoveAttachment.Click += new System.EventHandler(this.btnRemoveAttachment_Click);
            // 
            // SHA_ManageAttachments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.btnRemoveAttachment);
            this.Controls.Add(this.btnAddAttachment);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbxAttachments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SHA_ManageAttachments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Archivos adjuntos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxAttachments;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnAddAttachment;
        private System.Windows.Forms.Button btnRemoveAttachment;
    }
}