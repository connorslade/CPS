using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace CPS
{
    public partial class Form1 : Form
    {
        private IKeyboardMouseEvents m_GlobalHook;
        private float clickTime;
        public int clickMax = 0;
        
        public Form1()
        {
            InitializeComponent();
            leftToolStripMenuItem.Checked = true;
            Subscribe();
            timer1.Start();
        }

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            timer1.Stop();
            string mouseButton;
            if (rightToolStripMenuItem.Checked)
            {
                mouseButton = "Right";
            }
            else
            {
                mouseButton = "Left";
            }
            if (e.Button.ToString() == mouseButton)
            {
                int calc = (int)e.Timestamp - (int)clickTime;
                calc = 1000 / calc;
                if (calc > clickMax)
                {
                    clickMax = calc;
                    if (clickMax.ToString().Length < 2)
                    {
                    CPSmax.Text = "MAX: 0" + calc.ToString();
                    }
                    else
                    {
                        CPSmax.Text = "MAX: " + calc.ToString();
                    }
                }
                if (calc.ToString().Length < 2)
                {
                    label1.Text = "CPS: 0" + calc.ToString();
                }
                else
                {
                    label1.Text = "CPS: " + calc.ToString();
                }
                clickTime = e.Timestamp;
                timer1.Start();

            }

        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            contextMenuStrip1.Show(ptLowerLeft);
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits credits = new Credits();
            credits.Show();
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (leftToolStripMenuItem.Checked)
            {
                leftToolStripMenuItem.Checked = false;
                rightToolStripMenuItem.Checked = true;
            }
            else
            {
                leftToolStripMenuItem.Checked = true;
                rightToolStripMenuItem.Checked = false;
            }
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rightToolStripMenuItem.Checked)
            {
                rightToolStripMenuItem.Checked = false;
                leftToolStripMenuItem.Checked = true;
            }
            else
            {
                rightToolStripMenuItem.Checked = true;
                leftToolStripMenuItem.Checked = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "CPS: 00";
        }

        private void maxCPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (maxCPSToolStripMenuItem.Checked)
            {
                maxCPSToolStripMenuItem.Checked = false;
                CPSmax.Visible = false;
            }
            else
            {
                maxCPSToolStripMenuItem.Checked = true;
                CPSmax.Visible = true;
            }
        }

        private void maxCPSResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickMax = 0;
            CPSmax.Text = "MAX: 00";
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stayOnTopToolStripMenuItem.Checked)
            {
                stayOnTopToolStripMenuItem.Checked = false;
                this.TopMost = false;
            }
            else
            {
                stayOnTopToolStripMenuItem.Checked = true;
                this.TopMost = true;
            }
        }
    }
}
