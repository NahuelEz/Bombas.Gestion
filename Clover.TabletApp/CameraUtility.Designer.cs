namespace Clover.TabletApp
{
    partial class CameraUtility
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraUtility));
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.pbxPhotoPreview = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnTakePicture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPhotoPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Location = new System.Drawing.Point(12, 12);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(500, 500);
            this.videoSourcePlayer.TabIndex = 0;
            this.videoSourcePlayer.VideoSource = null;
            // 
            // pbxPhotoPreview
            // 
            this.pbxPhotoPreview.ErrorImage = null;
            this.pbxPhotoPreview.InitialImage = null;
            this.pbxPhotoPreview.Location = new System.Drawing.Point(12, 12);
            this.pbxPhotoPreview.Name = "pbxPhotoPreview";
            this.pbxPhotoPreview.Size = new System.Drawing.Size(500, 500);
            this.pbxPhotoPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxPhotoPreview.TabIndex = 1;
            this.pbxPhotoPreview.TabStop = false;
            this.pbxPhotoPreview.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.Location = new System.Drawing.Point(12, 519);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 70);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRetry.BackgroundImage")));
            this.btnRetry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRetry.Enabled = false;
            this.btnRetry.Location = new System.Drawing.Point(138, 519);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(120, 70);
            this.btnRetry.TabIndex = 4;
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAccept.BackgroundImage")));
            this.btnAccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(264, 519);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(120, 70);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnTakePicture
            // 
            this.btnTakePicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTakePicture.BackgroundImage")));
            this.btnTakePicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTakePicture.Location = new System.Drawing.Point(390, 519);
            this.btnTakePicture.Name = "btnTakePicture";
            this.btnTakePicture.Size = new System.Drawing.Size(120, 70);
            this.btnTakePicture.TabIndex = 2;
            this.btnTakePicture.UseVisualStyleBackColor = true;
            this.btnTakePicture.Click += new System.EventHandler(this.btnTakePicture_Click);
            // 
            // CameraUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 601);
            this.Controls.Add(this.btnTakePicture);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.videoSourcePlayer);
            this.Controls.Add(this.pbxPhotoPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraUtility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cámara";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraUtility_FormClosing);
            this.Load += new System.EventHandler(this.CameraUtility_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPhotoPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.PictureBox pbxPhotoPreview;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnTakePicture;
    }
}