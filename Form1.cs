using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
