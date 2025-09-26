using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap orig;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        { 

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            orig = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = orig;
        }

        private void uploadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            else
            {           
            Bitmap newPic = new Bitmap(orig.Width, orig.Height);

                for (int i = 0; i < orig.Width; i++)
                {
                    for (int j = 0; j < orig.Height; j++)
                    {
                        Color p = orig.GetPixel(i, j);
                        newPic.SetPixel(i, j, p);
                    }
                }
                pictureBox2.Image = newPic;
            }

        }

        private void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            else
            {
                Bitmap newPic = new Bitmap(orig.Width, orig.Height);
                
                for (int i = 0; i < orig.Width; i++)
                {
                    for (int j = 0; j < orig.Height; j++)
                    {
                        Color p = orig.GetPixel(i, j);
                        
                        int a = (p.R + p.G + p.B) / 3;
                        //int b = (int)((p.R * 0.3) + (p.G * 0.59) + (p.B * 0.11));
                 
                        Color g = Color.FromArgb(a,a,a);

                        newPic.SetPixel(i, j, g);
                    }
                }
                pictureBox2.Image = newPic;
            }
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            else
            {
                Bitmap newPic = new Bitmap(orig.Width, orig.Height);
                
                for(int i = 0; i < orig.Width; i++)
                {
                    for(int j = 0; j < orig.Height; j++)
                    {
                        Color p = orig.GetPixel(i, j);

                        int r = 255 - p.R;
                        int gg = 255 - p.G;
                        int b = 255 - p.B;

                        Color g = Color.FromArgb(r, gg, b);

                        newPic.SetPixel(i, j, g);
                    }
                }

                pictureBox2.Image = newPic;
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox2.Image.Save(saveFileDialog1.FileName);
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("No image");
                return;
            }

            saveFileDialog1.Title = "Save Image";
            saveFileDialog1.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap|*.bmp";

            saveFileDialog1.ShowDialog();
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            else
            {
                Bitmap newPic = new Bitmap(orig.Width, orig.Height);
                int[] counting = new int[256];
                for (int i = 0; i < orig.Width; i++)
                {
                    for(int j = 0; j < orig.Height; j++)
                    {
                        Color p = orig.GetPixel(i, j);

                        int a = (p.R + p.G + p.B) / 3;
                        //int b = (int)((p.R * 0.3) + (p.G * 0.59) + (p.B * 0.11));

                        Color g = Color.FromArgb(a, a, a);

                        newPic.SetPixel(i, j, g);


                        counting[a]++;//using array to count up pixles of same levels
                    }
                }


                // Plot the values of the array on a bitmap graph
                int graphWidth = 256;
                int graphHeight = 256;
                Bitmap histogram = new Bitmap(graphWidth, graphHeight);
                int maxCount = counting.Max(); 

                for (int x = 0; x < 256; x++)
                {
                                                // calculate height                      
                    int barHeight = (int)((counting[x] / (float)maxCount) * graphHeight); // <-- scales it to the pixel height of the bitmap

                    //starts from the bottom of the bitmap
                    for (int y = graphHeight - 1; y >= graphHeight - barHeight; y--)
                    {//draws the vertical bar for this graph
                        histogram.SetPixel(x, y, Color.Black);
                    }
                }

                pictureBox2.Image = histogram;

            }
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            else
            {
                Bitmap newPic = new Bitmap(orig.Width, orig.Height);

                for(int i = 0; i < orig.Width; i++)
                {
                    for(int j =0; j < orig.Height; j++)
                    {
                        Color p = orig.GetPixel(i, j);

                        int r = (int)(p.R * 0.393 + p.G * 0.769 + p.B * 0.189);
                        int gg = (int)(p.R * 0.349 + p.G * 0.686 + p.B * 0.168);
                        int b = (int)(p.R * 0.272 + p.G * 0.534 + p.B * 0.131);

                        // make sure none of these will go beyond 255
                        r = Math.Min(255, r);
                        gg = Math.Min(255, gg);
                        b = Math.Min(255, b);

                        Color s =  Color.FromArgb(r, gg, b);

                        newPic.SetPixel(i, j, s);
                    }
                }

                pictureBox2.Image = newPic;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

    

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            Bitmap newPic = (Bitmap)orig.Clone();
           Smooth(newPic,1);
            pictureBox2.Image = newPic;
        }

        public static bool Smooth(Bitmap b, int nWeight /* default to 1 */)

        {

            ConvMatrix m = new ConvMatrix();

            m.SetAll(1);
            m.Pixel = nWeight;
            m.Factor = nWeight + 8;
            return Conv3x3(b, m);

        }
        private void meanRemovalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            Bitmap newPic = (Bitmap)orig.Clone();
            meanRemoval(newPic, 9);
            pictureBox2.Image = newPic;
        }

        public static bool meanRemoval(Bitmap b, int nWeight /* 9 */)

        {

            ConvMatrix m = new ConvMatrix();

            m.SetAll(-1);
            m.Pixel = nWeight;
            m.Factor = nWeight - 8;
            m.Offset = 0;
            return Conv3x3(b, m);

        }
        private void sharpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            Bitmap newPic = (Bitmap)orig.Clone();
            Sharpen(newPic, 11);
            pictureBox2.Image = newPic;
        }

        public static bool Sharpen(Bitmap b, int nWeight /* 11 */)
        {
            ConvMatrix m = new ConvMatrix();

            m.TopLeft = 0;
            m.TopMid = -2;
            m.TopRight = 0;
            m.MidLeft = -2;
            m.MidRight = -2;
            m.BottomLeft = 0;
            m.BottomMid = -2;
            m.BottomRight = 0;
            m.Pixel = nWeight;
            m.Factor = nWeight - 8;
            m.Offset = 0;
            return Conv3x3(b, m);

        }


        private void gaussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            Bitmap newPic = (Bitmap)orig.Clone();
            GaussianBlur(newPic, 4);
            pictureBox2.Image = newPic;
        }
        public static bool GaussianBlur(Bitmap b, int nWeight /* 4 i tthink*/)
        {
            ConvMatrix m = new ConvMatrix();

            m.TopLeft = 1;
            m.TopMid = 2;
            m.TopRight = 1;
            m.MidLeft = 2;
            m.MidRight = 2;
            m.BottomLeft = 1;
            m.BottomMid = 2;
            m.BottomRight = 1;
            m.Pixel = nWeight;
            m.Factor = nWeight + 12;
            m.Offset = 0;
            return Conv3x3(b, m);

        }

        private void embossLapascianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (orig == null)
            {
                MessageBox.Show("No image");
                return;
            }
            Bitmap newPic = (Bitmap)orig.Clone();
            embossLapascian(newPic, 4);
            pictureBox2.Image = newPic;
        }
        public static bool embossLapascian(Bitmap b, int nWeight /* 4 */)
        {
            ConvMatrix m = new ConvMatrix();

            m.TopLeft = -1;
            m.TopMid = 0;
            m.TopRight = -1;
            m.MidLeft = 0;
            m.MidRight = 0;
            m.BottomLeft = -1;
            m.BottomMid = 0;
            m.BottomRight = -1;
            m.Pixel = nWeight;
            m.Factor = nWeight - 3;
            m.Offset = 127;
            return Conv3x3(b, m);

        }




        public static bool Conv3x3(Bitmap b, ConvMatrix m)

        {

            // Avoid divide by zero errors 
            if (0 == m.Factor)
                return false; Bitmap
            // GDI+ still lies to us - the return format is BGR, NOT RGB.  
            bSrc = (Bitmap)b.Clone();

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                                ImageLockMode.ReadWrite,
                                PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                               ImageLockMode.ReadWrite,
                               PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            int stride2 = stride * 2;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {

                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;



                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) +
                            (pSrc[5] * m.TopMid) +
                            (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) +
                            (pSrc[5 + stride] * m.Pixel) +
                            (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) +
                            (pSrc[5 + stride2] * m.BottomMid) +
                            (pSrc[8 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);



                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[5 + stride] = (byte)nPixel;



                        nPixel = ((((pSrc[1] * m.TopLeft) +
                            (pSrc[4] * m.TopMid) +
                            (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) +
                            (pSrc[4 + stride] * m.Pixel) +
                            (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) +
                            (pSrc[4 + stride2] * m.BottomMid) +
                            (pSrc[7 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);



                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) +
                                        (pSrc[3] * m.TopMid) +
                                        (pSrc[6] * m.TopRight) +
                                        (pSrc[0 + stride] * m.MidLeft) +
                                        (pSrc[3 + stride] * m.Pixel) +
                                        (pSrc[6 + stride] * m.MidRight) +
                                        (pSrc[0 + stride2] * m.BottomLeft) +
                                        (pSrc[3 + stride2] * m.BottomMid) +
                                        (pSrc[6 + stride2] * m.BottomRight))
                             / m.Factor) + m.Offset);


                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[3 + stride] = (byte)nPixel;
                        p += 3;
                        pSrc += 3;

                    }
                    p += nOffset;
                    pSrc += nOffset;

                }
            }
            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);
            return true;
        }


    }
}
