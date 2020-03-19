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
    public partial class Form5 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form5()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MyDB.accdb; Jet OLEDB:Database Password=123456seerde;";
            connection.ConnectionString = db1;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView1.Rows.Clear();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String qry = "select * from Invoice order by N_id desc";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if(reader["N_state"].ToString() == "False")
                    dataGridView1.Rows.Add(reader["N_no"], reader["N_date"], reader["N_total"], "مفتوحة");
                else if (reader["N_state"].ToString() == "True")
                    dataGridView1.Rows.Add(reader["N_no"], reader["N_date"], reader["N_total"], "مغلقة");
            }
            connection.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                Form3 f3 = new Form3();
                f3.invoice = Int32.Parse(dataGridView1.CurrentCell.Value.ToString());
                this.Close();
                f3.ShowDialog();
            }
        }
    }
}
