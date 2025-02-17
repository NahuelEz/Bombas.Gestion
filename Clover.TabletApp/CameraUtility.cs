using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;
using Clover.Shared;

namespace Clover.TabletApp
{
    public partial class CameraUtility : Form
    {
        public Bitmap CapturedImage = null;

        public CameraUtility()
        {
            InitializeComponent();
        }

        private void CameraUtility_Load(object sender, EventArgs e)
        {
            try
            {
                var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                var camera = new VideoCaptureDevice(videoDevices[1].MonikerString); // Rear camera
                camera.VideoResolution = camera.VideoCapabilities[1]; // 640x640 at 30 FPS
                videoSourcePlayer.VideoSource = camera;
                videoSourcePlayer.NewFrame += VideoSourcePlayer_NewFrame;
                videoSourcePlayer.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al iniciar la cámara del dispositivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }
        private void CameraUtility_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.Stop();
            }
        }

        private void VideoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        private void btnTakePicture_Click(object sender, EventArgs e)
        {
            btnTakePicture.Enabled = false;

            Bitmap picture = videoSourcePlayer.GetCurrentVideoFrame();
            pbxPhotoPreview.Image = picture;

            videoSourcePlayer.Visible = false;
            pbxPhotoPreview.Visible = true;

            videoSourcePlayer.Stop();

            btnAccept.Enabled = true;
            btnRetry.Enabled = true;
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            CapturedImage = pbxPhotoPreview.Image.Resize(125, 125);
            this.Close();
        }
        private void btnRetry_Click(object sender, EventArgs e)
        {
            btnAccept.Enabled = false;
            btnRetry.Enabled = false;

            videoSourcePlayer.Start();

            pbxPhotoPreview.Visible = false;
            videoSourcePlayer.Visible = true;

            pbxPhotoPreview.Image = null;

            btnTakePicture.Enabled = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
