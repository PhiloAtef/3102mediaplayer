using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class mp4Form : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();

        public mp4Form()
        {
            InitializeComponent();

        }

    
       

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Mp4 Files|*.mp4|WAV|*.wav";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string FileName = ofd.FileName;  //retreiving path of mp4 file to FileName
                axWindowsMediaPlayer1.URL = FileName;
                axWindowsMediaPlayer1.Ctlcontrols.play();

            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
            mediaform mediaform = new mediaform();
            mediaform.Show();
        }
    }
}
