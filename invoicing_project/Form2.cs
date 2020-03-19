using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invoicing_project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MdiClient ctlMDI;

            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }

            this.WindowState = FormWindowState.Maximized;
            label2.Text = Form1.usr;
            label3.Text = DateTime.Now.ToString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 chForm = new Form3();
            Form2 f2 = new Form2();
            chForm.MdiParent = this;
            chForm.Size = f2.Size;
            chForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 chForm = new Form5();
            chForm.MdiParent = this;
            chForm.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form6 chForm = new Form6();
            chForm.MdiParent = this;
            chForm.Show();
        }

        private void عرضالمنتجاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 chForm = new Form6();
            chForm.MdiParent = this;
            chForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 chForm = new Form7();
            chForm.MdiParent = this;
            chForm.Show();
        }
    }
}
