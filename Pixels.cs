using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Pixels
{
    public class Pixels
    {
        static Bitmap img;

        public static Bitmap Img
        {
            get { return img; }
            set { img = value; }
        }

        static Bitmap bitmap;

        public static Bitmap Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; }
        }

        static OpenFileDialog open;

        public static OpenFileDialog Open
        {
            get { return open; }
            set { open = value; }
        }

        static int progressY;

        public static int Progress
        {
            get { return progressY; }
            set { progressY = value; }
        }

        static int progressMaxValue;

        public static int ProgressMaxValue
        {
            get { return progressMaxValue; }
            set { progressMaxValue = value; }
        }

        static PictureBox picBox1;

        public static PictureBox PicBox1
        {
            get { return picBox1; }
            set { picBox1 = value; }
        }

        static PictureBox picBox2;

        public static PictureBox PicBox2
        {
            get { return picBox2; }
            set { picBox2 = value; }
        }

        public Bitmap Zoom(PictureBox pictureBox, double magnification)
        {
            Bitmap bmp = null;
            
            try
            {
                if (img.Width < 5000 && img.Height < 2000)
                {
                    double zoom = magnification / 100.0;
                    bmp = new Bitmap(Pixels.Bitmap, Convert.ToInt32(pictureBox.Width * zoom), Convert.ToInt32(pictureBox.Height * zoom));
                    Graphics g = Graphics.FromImage(bmp);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                }
                else
                {
                    double zoom = 100.0 / 100.0;
                    bmp = new Bitmap(Pixels.Bitmap, Convert.ToInt32(pictureBox.Width * zoom), Convert.ToInt32(pictureBox.Height * zoom));
                    Graphics g = Graphics.FromImage(bmp);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bmp;
        }

        public void SaveChanges(string savePath)
        {
            try
            {
                Bitmap bm = new Bitmap(bitmap);
                bm.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void ReworkImage(string gapInPixels, Color colorRGB)
        {
            int progressCount_i_Y = 0;
            int progressCount_j_Y = 0;

            int black = 0;
            int white = 0;

            try
            {
                //Y Axis
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color pixel = img.GetPixel(i, j);

                        if (pixel.Name != "ffffffff") //if is not white
                        {
                            black++;
                            if (white > 0 && white <= Convert.ToInt32(gapInPixels))
                            {
                                bitmap.SetPixel(i, j, colorRGB);

                                i = i - white - 1;
                                Color pixel1 = img.GetPixel(i, j);
                                if (pixel1.Name == "ff000000")
                                {
                                    j--;
                                    Color pixel2 = img.GetPixel(i, j);
                                    if (pixel2.Name == "ffffffff")
                                    {
                                        i = i + white + 1;
                                        Color pixel3 = img.GetPixel(i, j);
                                        if (pixel3.Name == "ff000000")
                                        {
                                            bitmap.SetPixel(i, j, colorRGB); //blue
                                            j++;
                                        }
                                        else
                                        {
                                            bitmap.SetPixel(i, j, Color.White); //white
                                            j++;
                                        }
                                    }
                                    else
                                    {
                                        i = i + white + 1;
                                        j++;
                                    }
                                }
                                else
                                {
                                    i = i + white + 1;
                                }
                            }

                            white = 0;
                        }

                        if (pixel.Name != "ff000000")
                        {
                            black = 0;
                            white++;
                        }
                        progressCount_j_Y = j;
                    }
                    progressMaxValue = img.Width + img.Height - 10;
                    progressCount_i_Y = i;
                }
                progressY = progressCount_i_Y + progressCount_j_Y;

                //X Axis
                for (int j = 0; j < img.Height; j++)
                {
                    for (int i = 0; i < img.Width; i++)
                    {
                        Color pixel = img.GetPixel(i, j);

                        if (pixel.Name != "ffffffff") //if is not white
                        {
                            black++;
                            if (white > 0 && white <= Convert.ToInt32(gapInPixels))
                            {
                                bitmap.SetPixel(i, j, colorRGB);

                                i = i - white - 1;
                                Color pixel1 = img.GetPixel(i, j);
                                if (pixel1.Name == "ff000000")
                                {
                                    j--;
                                    Color pixel2 = img.GetPixel(i, j);
                                    if (pixel2.Name == "ffffffff")
                                    {
                                        i = i + white + 1;
                                        Color pixel3 = img.GetPixel(i, j);
                                        if (pixel3.Name == "ff000000")
                                        {
                                            bitmap.SetPixel(i, j, colorRGB);  //blue
                                            j++;
                                        }
                                        else
                                        {
                                            bitmap.SetPixel(i, j, Color.White); //white
                                            j++;
                                        }
                                    }
                                    else
                                    {
                                        i = i + white + 1;
                                        j++;
                                    }
                                }
                                else
                                {
                                    i = i + white + 1;
                                }
                            }

                            white = 0;
                        }

                        if (pixel.Name != "ff000000")
                        {
                            black = 0;
                            white++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
