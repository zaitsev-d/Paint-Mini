using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Mini
{
    public partial class MainForm : Form
    {
        Graphics graphics;
        Point temp_point;
        Pen pen;
        SolidBrush solidBrush;

        public MainForm()
        {
            InitializeComponent();
            openFileDialog.Filter = saveFileDialog.Filter = "Graphics BMP|*.bmp|Graphics PNG|*.png|Graphics JPG|*.jpg";

            pen = new Pen(buttonColor.BackColor, (float)numericUpDownThickness.Value);
            pen.EndCap = pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            solidBrush = new SolidBrush(buttonFillColor.BackColor);

            newFileToolStripMenuItem_Click(null, null);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxImage.Image = Image.FromFile(openFileDialog.FileName);
                graphics = Graphics.FromImage(pictureBoxImage.Image);
                this.Text = "Paint - Mini // " + openFileDialog.FileName;
            }
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = new Bitmap(800, 600);
            graphics = Graphics.FromImage(pictureBoxImage.Image);
            graphics.Clear(Color.White);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName);
                ImageFormat imageFormat = ImageFormat.Bmp;
                switch(extension)
                {
                    case ".bmp": 
                        imageFormat = ImageFormat.Bmp;
                        break;

                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;

                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                }
                
                pictureBoxImage.Image.Save(saveFileDialog.FileName, imageFormat);
                this.Text = "Paint - Mini // " + saveFileDialog.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxImage_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                temp_point = e.Location;
        }

        private void pictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(radioButtonCurve.Checked)
                {
                    graphics.DrawLine(pen, temp_point, e.Location);
                    pictureBoxImage.Refresh();
                    temp_point = e.Location;
                }
            }
        }

        private void pictureBoxImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonCurve.Checked)
                {
                    graphics.DrawLine(pen, temp_point, e.Location);
                }
                else if (radioButtonLine.Checked)
                {
                    graphics.DrawLine(pen, temp_point, e.Location);
                }
                else if (radioButtonRectangle.Checked)
                {
                    graphics.DrawRectangle(pen, 
                                           Math.Min(temp_point.X, e.X),
                                           Math.Min(temp_point.Y, e.Y),
                                           Math.Abs(temp_point.X - e.X),
                                           Math.Abs(temp_point.Y - e.Y));
                    if(checkIsFilling.Checked)
                    {
                        graphics.FillRectangle(solidBrush,
                                           Math.Min(temp_point.X, e.X),
                                           Math.Min(temp_point.Y, e.Y),
                                           Math.Abs(temp_point.X - e.X),
                                           Math.Abs(temp_point.Y - e.Y));
                    }
                }
                else if (radioButtonEllipse.Checked)
                {
                    graphics.DrawEllipse(pen,
                                         Math.Min(temp_point.X, e.X),
                                         Math.Min(temp_point.Y, e.Y),
                                         Math.Abs(temp_point.X - e.X),
                                         Math.Abs(temp_point.Y - e.Y));

                    if (checkIsFilling.Checked)
                    {
                        graphics.FillEllipse(solidBrush,
                                           Math.Min(temp_point.X, e.X),
                                           Math.Min(temp_point.Y, e.Y),
                                           Math.Abs(temp_point.X - e.X),
                                           Math.Abs(temp_point.Y - e.Y));
                    }
                }

                pictureBoxImage.Refresh();
            }
        }

        private void numericUpDownThickness_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = (float)numericUpDownThickness.Value;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog.Color;
                pen.Color = colorDialog.Color;
            }
        }

        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonFillColor.BackColor = colorDialog.Color;
                solidBrush.Color = colorDialog.Color;
            }
        }
    }
}
