using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediapfunctions;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;



namespace WindowsFormsApp3
{
    public partial class mediaform : Form
    {

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        public bool PlayButtonClicked = false;

        private Mp3 mp3playe = new Mp3();
        public mediaform()
        {
            InitializeComponent();
        }

      
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Mp3 Files|*.mp3";
            

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog1.FileName;
                outputDevice = new WaveOutEvent();
                audioFile = new AudioFileReader(FileName);
            }  

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PlayButtonClicked==false)
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                PlayButtonClicked = true;
            }
            else
            {
                outputDevice?.Stop();
                PlayButtonClicked = false;
            }
            
            
            
            
        }
    }
}
