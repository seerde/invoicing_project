using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace invoicing_project
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public static String usr = "";
        public static bool U_admin = false;
        public Form1()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MyDB.accdb; Jet OLEDB:Database Password=123456seerde;";
            connection.ConnectionString = db1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String qry = "select Count(*) from [Users] where U_username=? and U_password=? and U_admin = True";

            using (OleDbCommand cmd = new OleDbCommand(qry, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    usr = textBox1.Text;
                    U_admin = true;
                    MessageBox.Show("Welcome " + usr + "!\n" + "U_admin: " + U_admin);
                    //this.Hide();
                    Form2 f2 = new Form2();
                    f2.StartPosition = FormStartPosition.Manual;
                    f2.Location = this.Location;
                    f2.ShowDialog();
                    this.Close();
                }
                else
                    MessageBox.Show("Wrong Password or Username");
            }
            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                DateTime today = DateTime.Now;
                label3.Text = "Connected! " + today;
                connection.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error " + ee);
            }
        }
    }
}
