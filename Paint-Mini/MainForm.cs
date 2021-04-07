using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public MainForm()
        {
            InitializeComponent();
            openFileDialog.Filter = saveFileDialog.Filter = "Graphics BMP|*.bmp|Graphics PNG|*.png|Graphics JPG|*.jpg";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxImage.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxImage.Image.Save(saveFileDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
