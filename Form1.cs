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
            MessageBox.Show("Image saved");
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
    }
}
