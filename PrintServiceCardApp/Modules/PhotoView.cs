using DirectX.Capture;
using PrintServiceCardApp.Model.Helper;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintServiceCardApp.Modules
{
    public partial class PhotoView : UserControl
    {
        public byte[] Image { get; set; }

        private Filters _InputParameters;

        public PhotoView()
        {
            _InputParameters = new Filters();
            InitializeComponent(); 
        }

        DateTime _StartTime;
        Filter _Camera;
        DirectX.Capture.Capture _CaptureInfo;
        private int _Counter = 0;
        private int _CounterFrames = 0;
        string _PathVideo = string.Empty;
        private Filters _Dispositivos = new Filters();
        private int _NombreDeCapture = 0;


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pbPhoto.Image == null)
                return;

            Task.Delay(1000);

            Image = Model.Helper.ImageUtil.BitmapToByte(pbPhoto.Image);

            ((Form)this.TopLevelControl).Close();
        }


        void LoadDriverVideo()
        {
            try
            {
                cmbDriverVideo.Items.Clear();
                var devices = _InputParameters.VideoInputDevices;

                foreach (Filter inputVdo in devices)
                {
                    cmbDriverVideo.Items.Add(inputVdo.Name);
                }

                cmbDriverVideo.SelectedIndex = devices.Count > 0 ? 0 : -1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void PhotoView_Load(object sender, EventArgs e)
        {
            LoadDriverVideo();

            ofd = new OpenFileDialog();

            ofd.Filter = "Fichiers images|*.JPEG;*.jpg;*.png;*.gif";
        }

        string selectedDevice;
        int selectedIndexDevice;

        private void cmbDriverVideo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDevice = ((ComboBox)sender).Text;
            selectedIndexDevice = ((ComboBox)sender).SelectedIndex;

            if (selectedDevice == null)
            {
                MessageBox.Show("Aucun périphérique de capture vidéo trouvé .");
                _CaptureInfo.Dispose();
                this.Dispose();
            }
        }

        public void capturerNow(string text)
        {
            if (text == "Prendre une photo")
            {
                _Camera = _Dispositivos.VideoInputDevices[selectedIndexDevice];
                _CaptureInfo = new DirectX.Capture.Capture(_Camera, _Dispositivos.AudioInputDevices[0]);
                _CaptureInfo.PreviewWindow = pbPhoto;
                _Counter = 1;
                _CounterFrames = 1;
                ConfigureDevice();
                _CaptureInfo.FrameCaptureComplete += NewCaptureReady;
                lblStatus.Text = "Capturer la photo";
            }
            else if (text == "Capturer la photo")
            {
                lblStatus.Text = "Prendre une photo";
                _StartTime = DateTime.Now;
                _CaptureInfo.CaptureFrame();

                PictureBox pc = pbPhoto;
                // permet de lire le son pendant la capture 
                Audio LireSon = new Audio();
                LireSon.Play(Properties.Resources.TakePhoto, Microsoft.VisualBasic.AudioPlayMode.Background);
            }
        }

        private void NewCaptureReady(PictureBox Frame)
        {
            PictureBox imgePicture = Frame as PictureBox;
            System.TimeSpan RunLength = DateTime.Now.Subtract(_StartTime);
            int TimeElapsCapture = RunLength.Milliseconds;
            _NombreDeCapture = _NombreDeCapture + 1;
            pbPhoto.Image = imgePicture.Image;
            _CaptureInfo.PreviewWindow = null;
        }

        private void ConfigureDevice()
        {
            _CaptureInfo.VideoCompressor = _Dispositivos.VideoCompressors[1];
            _CaptureInfo.FrameSize = new Size(1280, 720);
            _CaptureInfo.FrameRate = ((ulong)(0ul));
            _CaptureInfo.RightToLeftLayout = false;
            _CaptureInfo.RenderPreview();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if(selectedDevice != null)
                capturerNow(lblStatus.Text);
        }

        OpenFileDialog ofd;

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    pbPhoto.Image = ImageUtil.BitmapToBitmapImage(new Bitmap(ofd.FileName));
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
