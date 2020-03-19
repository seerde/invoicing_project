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
    public partial class Form6 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form6()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MyDB.accdb; Jet OLEDB:Database Password=123456seerde;";
            connection.ConnectionString = db1;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Update_dataGrid();
        }

        private void Update_dataGrid()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "select * from Items";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0], reader[1], reader[2]);
                i = (int)reader[0];
            }
            i++;
            textBox3.Text = i.ToString();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            //textBox3.Enabled = false;
            button1.Enabled = false;

            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "insert into Items (T_type, T_price) values (?,?)";
            command.CommandText = qry;
            command.Parameters.AddWithValue("@p1", textBox1.Text);
            command.Parameters.AddWithValue("@p2", textBox2.Text);
            command.ExecuteNonQuery();
            connection.Close();
            Update_dataGrid();
        }

        private void إضافةجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            //textBox3.Enabled = true;
            button1.Enabled = true;
        }

        private void حذفالمحددToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل انت متأكد؟", "حذف", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                String qry = "delete from Items where T_id =?";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", dataGridView1.CurrentCell.Value);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("تم الحذف");
                Update_dataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
    }
}
