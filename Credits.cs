using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPS
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
            using (SoundPlayer soundPlayer = new SoundPlayer(@"C:\Windows\Media\Windows Background.wav"))
            {
                soundPlayer.Play();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Basicprogrammer10");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://connorcode.com");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://coolthing.connorcode.com/");
            MessageBox.Show("ヾ(≧▽≦*)", "Gottem");
        }
    }
}
