using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCamLib;
namespace ImageProcessing
{
    public partial class Form3 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        Device myDevice;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach(FilterInfo filterInfo in filterInfoCollection)
            {
                cboCamera.Items.Add(filterInfo.Name);
            }
            cboCamera.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Device[] devices = DeviceManager.GetAllDevices();

            //if (devices.Length > 0)
            //{
            //    myDevice = devices[0];

            //    myDevice.ShowWindow(pictureBox1);
            //}

            //foreach (var d in devices)
            //{
            //    MessageBox.Show(d.Name); 
            //}

            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();

        }
        bool grayscaleOn = false;
        bool colorInversionOn = false;
        bool sepiaOn = false;
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = new Bitmap(eventArgs.Frame);
            
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();

            if (grayscaleOn)
            {
                greyScale2(sender, frame);
               
            }

            if (colorInversionOn)
            {
                colorInversion(sender, frame);
                
            }

            if (sepiaOn)
            {
                sepia(sender, frame);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //if (myDevice != null) { 
            //    myDevice.Stop();
            //}
           
            if(videoCaptureDevice.IsRunning == true)
            {
                videoCaptureDevice.Stop();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        
        private void greyscaleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            colorInversionOn = false;
            sepiaOn = false;
            grayscaleOn = !grayscaleOn;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grayscaleOn = false;
            sepiaOn = false;
            colorInversionOn = !colorInversionOn;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grayscaleOn = false;
            colorInversionOn = false;
            sepiaOn = !sepiaOn;
        }

        private void greyScale2(object sender,Bitmap frame)
        {
            
            Bitmap grayFrame = new Bitmap(frame.Width, frame.Height);

            for (int x = 0; x < frame.Width; x++)
            {
                for (int y = 0; y < frame.Height; y++)
                {
                    Color p = frame.GetPixel(x, y);
                    int gray = (p.R + p.G + p.B) / 3;
                    grayFrame.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

            pictureBox2.Image = grayFrame;

        }

        private void colorInversion(object sender, Bitmap frame)
        {

            Bitmap newPic = new Bitmap(frame.Width, frame.Height);

            for (int i = 0; i < frame.Width; i++)
            {
                for (int j = 0; j < frame.Height; j++)
                {
                    Color p = frame.GetPixel(i, j);

                    int r = 255 - p.R;
                    int gg = 255 - p.G;
                    int b = 255 - p.B;

                    Color g = Color.FromArgb(r, gg, b);

                    newPic.SetPixel(i, j, g);
                }
            }

            pictureBox2.Image = newPic;

        }
        private void sepia(object sender, Bitmap frame)
        {

            Bitmap newPic = new Bitmap(frame.Width, frame.Height);

            for (int i = 0; i < frame.Width; i++)
            {
                for (int j = 0; j < frame.Height; j++)
                {
                    Color p = frame.GetPixel(i, j);

                    int r = (int)(p.R * 0.393 + p.G * 0.769 + p.B * 0.189);
                    int gg = (int)(p.R * 0.349 + p.G * 0.686 + p.B * 0.168);
                    int b = (int)(p.R * 0.272 + p.G * 0.534 + p.B * 0.131);

                    // make sure none of these will go beyond 255
                    r = Math.Min(255, r);
                    gg = Math.Min(255, gg);
                    b = Math.Min(255, b);

                    Color s = Color.FromArgb(r, gg, b);

                    newPic.SetPixel(i, j, s);
                }
            }

            pictureBox2.Image = newPic;

        }

    }
}
