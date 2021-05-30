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
        /// <summary>
        /// Define Colors
        /// </summary>
        Color DM_BG = Color.FromArgb(36, 41, 51);
        Color DM_FG = Color.FromArgb(236, 239, 244);

        Color LM_BG = Color.FromArgb(240, 240, 240);
        Color LM_FG = Color.FromArgb(0, 0, 0);


        private IKeyboardMouseEvents m_GlobalHook;
        private float clickTime;
        private float clickTimeR;
        public int clickMax = 0;

        public Form1()
        {
            InitializeComponent();
            leftToolStripMenuItem.Checked = true;
            Subscribe();
            timer1.Start();
            timer2.Start();
        }

        private void UpdateColors()
        {
            darkModeToolStripMenuItem.Checked = !darkModeToolStripMenuItem.Checked;
            if (darkModeToolStripMenuItem.Checked)
            {
                this.BackColor = LM_BG;
                label1.ForeColor = LM_FG;
                label2.ForeColor = LM_FG;
                CPSmax.ForeColor = LM_FG;
            }
            else
            {
                this.BackColor = DM_BG;
                label1.ForeColor = DM_FG;
                label2.ForeColor = DM_FG;
                CPSmax.ForeColor = DM_FG;
            }
        }

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (e.Button.ToString() == "Left")
            {
                timer1.Stop();
                int calc = (int)e.Timestamp - (int)clickTime;
                calc = 1000 / calc;
                if (calc > clickMax) CPSmax.Text = "MAX: " + calc.ToString();
                label1.Text = calc.ToString().PadLeft(2, '0');
                clickTime = e.Timestamp;
                timer1.Start();
            }
            if (e.Button.ToString() == "Right")
            {
                timer2.Stop();
                int calc = (int)e.Timestamp - (int)clickTimeR;
                calc = 1000 / calc;
                if (calc > clickMax) CPSmax.Text = "MAX: " + calc.ToString();
                label3.Text = calc.ToString().PadLeft(2, '0');
                clickTimeR = e.Timestamp;
                timer2.Start();
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
                label1.Text = "00";
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

            private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
            {
                UpdateColors();
            }

            private void showTitleBarToolStripMenuItem_Click(object sender, EventArgs e)
            {
                showTitleBarToolStripMenuItem.Checked = !showTitleBarToolStripMenuItem.Checked;
                this.FormBorderStyle = showTitleBarToolStripMenuItem.Checked ? FormBorderStyle.None : FormBorderStyle.FixedDialog;
                this.Size = showTitleBarToolStripMenuItem.Checked ? new Size(230, 80) : new Size(245, 120);
            }

            private void transparentalphaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                transparentalphaToolStripMenuItem.Checked = !transparentalphaToolStripMenuItem.Checked;

                if (transparentalphaToolStripMenuItem.Checked)
                {
                    this.BackColor = Color.FromArgb(1, 1, 1);
                    this.TransparencyKey = Color.FromArgb(1, 1, 1);
                    this.AllowTransparency = true;
                }
                else
                {
                    UpdateColors();
                    this.AllowTransparency = false;
                }
            }
        }
    }