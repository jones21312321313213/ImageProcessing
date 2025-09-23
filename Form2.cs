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
    public partial class Form2 : Form
    {
        Bitmap imageB, imageA, colorgreen;
        public Form2()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = imageB;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void loadImage(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }


        private void loadBackground(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = imageA;
        }

        private void subtract(object sender,EventArgs e)
        {

            if (imageA == null && imageB == null)
            {
                MessageBox.Show("No image");
                return;
            }
            Color mygreen = Color.FromArgb(0, 255, 0);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;

            Bitmap resultImage = new Bitmap(imageB.Width, imageB.Height);
            for (int x = 0; x < imageB.Width; x++)
            {
                for(int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backpixel = imageA.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);
                    if(subtractvalue > threshold)
                    {
                        //resultImage.SetPixel(x, y, backpixel);
                        resultImage.SetPixel(x, y, pixel);
                    }
                    else
                    {
                        //resultImage.SetPixel(x, y, pixel);
                        resultImage.SetPixel(x, y, backpixel);
                    }
                }
            }
            pictureBox3.Image = resultImage;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox3.Image.Save(saveFileDialog1.FileName);
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageA == null && imageB == null)
            {
                MessageBox.Show("No image");
                return;
            }

            saveFileDialog1.Title = "Save Image";
            saveFileDialog1.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap|*.bmp";

            saveFileDialog1.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
