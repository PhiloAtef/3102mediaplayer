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
        #region defined properties/variables

        //defining our private objects of outputdevice and audio files to send to the sound card
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        //indicator for pause button
        public bool PlayButtonClicked = false;

        #endregion

        #region initializer

        public mediaform()
        {
            InitializeComponent();
        }

        #endregion

        #region Exit toolstrip

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region OpenfileDialouge for retreiving the mp3 file

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //filter for mp3 type files only
            openFileDialog1.Filter = "Mp3 Files|*.mp3";

            //Openfiledialoguecode
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //retreiving path of mp3 file to FileName
                string FileName = openFileDialog1.FileName;
                //initializing a output device of type waveoutevent
                outputDevice = new WaveOutEvent();
                //initializing an object of audiofile of type audiofilereader the refrences the path of the mp3 file
                audioFile = new AudioFileReader(FileName);
            }


        }

        #endregion

        #region PlayAndPause button

        private void button1_Click(object sender, EventArgs e)
        {
            if (PlayButtonClicked == false) //checks if playbutton was clicked and if not then it plays file
            {
                PlayButtonClicked = true; //to indicate if the playbutton was clicked so that next time it pauses instead

                try //due to exception occuring when trying to rewind then play we added this try/catch 
                {
                    if (outputDevice==null)
                    {
                        openFileDialog1.Filter = "Mp3 Files|*.mp3";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK) 
                        {
                   
                            string FileName = openFileDialog1.FileName;//retreiving path of mp3 file to FileName
                            outputDevice = new WaveOutEvent(); //initializing a output device of type waveoutevent
                            audioFile = new AudioFileReader(FileName); //initializing an object of audiofile of type audiofilereader the refrences the path of the mp3 file

                        }
                    }
                    outputDevice.Init(audioFile); //loads the audiofile to the sound card
                    outputDevice.Play();//plays the audio

                }
                catch (System.InvalidOperationException)
                {
                   
                }
               
            }
            else
            {
                outputDevice?.Stop(); //checks to see if the output device running the stops if it find it
                PlayButtonClicked = false; ///to indicate if the playbutton was clicked so that next time it plays instead
            }
        }

        #endregion

        #region rewind button

        private void rewind_Click(object sender, EventArgs e)
        {
            try
            {
                outputDevice.Stop(); //stops playing
                audioFile.Position = 0; //adjusts position of file back to 0 (to rewind) 
                outputDevice.Init(audioFile); //loads the audiofile to the soundcard(output device) again
                outputDevice.Play(); //plays audio
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Open file first");
              
            }
            
        }


        #endregion

        #region rewindback15sec
        private void skipback10_Click(object sender, EventArgs e)
        {
            outputDevice.Stop(); //stops playing
            audioFile.CurrentTime = audioFile.CurrentTime.Subtract(TimeSpan.FromSeconds(15));//subtracts 15 seconds from current time to rewind back 15 seconds
            outputDevice.Play(); //plays audio
        }
        #endregion

        #region skipahead15sec
        private void skipahead10_Click(object sender, EventArgs e)
        {
            outputDevice.Stop(); //stops playing
            audioFile.CurrentTime = audioFile.CurrentTime.Add(TimeSpan.FromSeconds(15));//adds 15 seconds from current time to skip ahead 15 seconds
            outputDevice.Play(); //plays audio
        }
        #endregion
    }
}
