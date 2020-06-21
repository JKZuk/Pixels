using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;


namespace Pixels
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Pixels pix = new Pixels();

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSaveIMG.Text = Properties.Settings.Default.savePath;

            label4.Visible = false;
            label5.Visible = false;
            label3.Visible = false;

            panel1.AutoScroll = true;
            panel2.AutoScroll = true;

            Pixels.PicBox1 = pictureBox1;
            Pixels.PicBox2 = pictureBox2;

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;

            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy ||backgroundWorker3.IsBusy)
                {
                    MessageBox.Show("I'm Busy!\nBe patient!", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    OpenFileDialog open = new OpenFileDialog();
                    Pixels.Open = open;
                    Pixels.Open.Filter = "Image Files(*.bmp)|*.bmp";

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap img = new Bitmap(Pixels.Open.FileName);
                        Bitmap bitmap = img.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        Pixels.Img = img;
                        Pixels.Bitmap = bitmap;

                        pictureBox1.Visible = false;
                        pictureBox2.Visible = false;

                        button2.Enabled = false;
                        button3.Enabled = false;

                        label8.Text = "Opening and Reworking Image..please wait...";

                        backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker3.IsBusy || backgroundWorker2.IsBusy ||backgroundWorker1.IsBusy)
            {
                MessageBox.Show("I'm Busy!\nBe patient!", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                label8.Text = "Reworking Image..please wait...";
                backgroundWorker3.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            pix.ReworkImage(txtPixels.Text, Color.Blue);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;

            pictureBox1.Image = Pixels.Bitmap;
            pictureBox2.Image = Pixels.Img;

            var imageHeight = Pixels.Img.Height;
            var imageWidth = Pixels.Img.Width;
            label1.Text = "X = " + imageWidth.ToString() + " px";
            label2.Text = " |  Y = " + imageHeight.ToString() + " px";

            pictureBox1.Image = pix.Zoom(pictureBox1, Convert.ToDouble(txtZoom.Text));

            label8.Text = "";

            button2.Enabled = true;
            button3.Enabled = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.IsBusy || backgroundWorker1.IsBusy ||backgroundWorker3.IsBusy)
            {
                MessageBox.Show("I'm Busy!\nBe patient!", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                label8.Text = "Applying Changes..please wait...";
                txtSaveIMG.ReadOnly = true;
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            pix.ReworkImage(txtPixels.Text, Color.White);
            pix.SaveChanges(txtSaveIMG.Text);
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Image = pix.Zoom(pictureBox1, 100.0);
            label8.Text = "";
            txtSaveIMG.ReadOnly = false;
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap img = new Bitmap(Pixels.Open.FileName);
            Bitmap bitmap = img.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Pixels.Img = img;
            Pixels.Bitmap = bitmap;

            pix.ReworkImage(txtPixels.Text, Color.Blue);
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Image = pix.Zoom(pictureBox1, 100.0);
            label8.Text = "";
        }

        private void txtSaveIMG_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.savePath = txtSaveIMG.Text;
            Properties.Settings.Default.Save();
        }
    }
}
