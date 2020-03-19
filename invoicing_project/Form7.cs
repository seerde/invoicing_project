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
    public partial class Form7 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form7()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MyDB.accdb; Jet OLEDB:Database Password=123456seerde;";
            connection.ConnectionString = db1;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Update_datagrid();
        }

        private void Update_datagrid()
        {
            dataGridView1.Rows.Clear();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String qry = "";

            if (textBox1.Text != "")
            {
                qry = "select * from Customers where C_name = ? order by C_id desc";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", textBox1.Text);
            }
            else if(textBox2.Text != "")
            {
                qry = "select * from Customers where C_phone = ? order by C_id desc";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", textBox2.Text);
            }
            else
            {
                qry = "select * from Customers order by C_id desc";
                command.CommandText = qry;
            }

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["C_id"], reader["C_name"], reader["C_phone"], reader["C_date"]);
            }
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Update_datagrid();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length  == 10)
            {
                Update_datagrid();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Update_datagrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                Form3 f3 = new Form3();
                f3.Customer_phone = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
                this.Close();
                f3.ShowDialog();
            }
        }
    }
}
