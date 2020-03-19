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
using System.Globalization;

namespace invoicing_project
{
    public partial class Form3 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        double i = 0.0, sum = 0.0;
        double total = 0.0, total2 = 0.0, total3 = 0.0;
        public int iii = 0;
        int ii = 1;
        int c_i = 1;
        int amount = 0;
        int C_id = 0;
        long N_id = 0;
        bool tick = false;
        public String Item = "";
        public int invoice = 0;
        public String Customer_phone = "";
        String close_date = "";
        DateTime today = DateTime.Now;
        public Form3()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MyDB.accdb; Jet OLEDB:Database Password=123456seerde;";
            connection.ConnectionString = db1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool i = false;
            ii = 1;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            if (textBox8.Text != "")
            {
                String qry = "SELECT c.C_id, c.C_name, c.C_phone, n.N_no, N_vat, N_total, N_state, N_close, n.N_id, N_end" +
                    " FROM(Orders INNER JOIN Invoice as n ON Orders.N_id = n.N_id) inner join Customers as c on c.C_id = n.C_id" +
                    " WHERE n.N_no =? order by n.N_id; ";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", textBox8.Text);
                i = true;
            }
            else if (textBox9.Text != "")
            {
                String qry = "SELECT c.C_id, c.C_name, c.C_phone, n.N_no, N_vat, N_total, N_state, N_close, n.N_id, N_end" +
                    " FROM(Orders INNER JOIN Invoice as n ON Orders.N_id = n.N_id) inner join Customers as c on c.C_id = n.C_id" +
                    " WHERE C_name =? order by n.N_id; ";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", textBox9.Text);
                i = true;
            }
            else if (textBox10.Text != "")
            {
                String qry = "SELECT c.C_id, c.C_name, c.C_phone, n.N_no, N_vat, N_total, N_state, N_close, n.N_id, N_end" +
                    " FROM(Orders INNER JOIN Invoice as n ON Orders.N_id = n.N_id) inner join Customers as c on c.C_id = n.C_id" +
                    " WHERE C_phone =? order by n.N_id; ";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", textBox10.Text);
                i = true;
            }
            else
            {
                
            }
            if(i)
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["C_id"].ToString();
                    textBox2.Text = reader["C_name"].ToString();
                    textBox3.Text = reader["C_phone"].ToString();

                    double price = (Double.Parse(reader["N_total"].ToString())) - (Double.Parse(reader["N_vat"].ToString()));

                    textBox4.Text = reader["N_vat"].ToString();
                    textBox5.Text = price.ToString();
                    textBox6.Text = reader["N_no"].ToString();
                    textBox7.Text = reader["N_total"].ToString();
                    N_id = (int)reader["N_id"];
                    C_id = (int)reader["C_id"];
                    if (reader["N_state"].ToString() == "False")
                    {
                        label8.Visible = true;
                        label8.Text = "الفاتورة مفتوحة";
                        label8.ForeColor = Color.Red;
                        label18.Visible = false;
                        button5.Enabled = true;
                    }
                    else
                    {
                        label8.Visible = true;
                        label8.Text = "الفاتورة مغلقة";
                        label8.ForeColor = Color.Blue;
                        label18.Visible = true;
                        label18.Text = reader["N_close"].ToString();
                        button5.Enabled = false;
                    }
                    label20.Visible = true;
                    label21.Visible = true;
                    label20.Text = reader["N_end"].ToString();
                }
                connection.Close();

                connection.Open();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                String qry = "Select * from Orders where N_id = ?";
                command2.CommandText = qry;
                command2.Parameters.AddWithValue("@p1", N_id);
                OleDbDataReader reader2 = command2.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (reader2.Read())
                {
                    dataGridView1.Rows.Add(ii,reader2["O_type"], reader2["O_price"], reader2["O_amount"], Int32.Parse(reader2["O_amount"].ToString()) * Int32.Parse(reader2["O_price"].ToString()));
                    ii++;
                }
                ii = 1;
                connection.Close();

                connection.Open();
                OleDbCommand command3 = new OleDbCommand();
                command3.Connection = connection;
                qry = "Select * from Invoice where C_id = ?";
                command3.CommandText = qry;
                command3.Parameters.AddWithValue("@p1", C_id);
                OleDbDataReader reader3 = command3.ExecuteReader();
                dataGridView2.Rows.Clear();
                while (reader3.Read())
                {
                    if (reader3["N_state"].ToString() == "False")
                    {
                        dataGridView2.Rows.Add(reader3["N_no"], reader3["N_date"], reader3["N_total"], "مفتوحة");
                        label20.Visible = true;
                        label21.Visible = true;
                    }
                    else if (reader3["N_state"].ToString() == "True")
                    {
                        dataGridView2.Rows.Add(reader3["N_no"], reader3["N_date"], reader3["N_total"], "مغلقة");
                        label20.Visible = false;
                        label21.Visible = false;
                    }
                }
                connection.Close();

                i = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this);
            f4.ShowDialog(this);
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add_items();
            }
        }

        public void add_items()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "select * from Items where T_id =?";
            command.CommandText = qry;
            command.Parameters.AddWithValue("@p1", textBox11.Text);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox12.Text = reader[1].ToString();
                textBox14.Text = reader[2].ToString();
            }
            amount = Int32.Parse(textBox13.Text);
            sum = Double.Parse(textBox14.Text);
            sum = sum * amount;
            textBox15.Text = sum.ToString();
            textBox13.Enabled = true;
            connection.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل انت متأكد؟", "إضافة", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
                dataGridView1.Rows.Add(ii, textBox12.Text, textBox14.Text, textBox13.Text, textBox15.Text);
                ii++;
                total = total + sum;
                textBox5.Text = total.ToString();
                total2 = total + (total * 0.0);
                textBox7.Text = total2.ToString();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = true;
            button7.Enabled = true; textBox10.Text = "";
            textBox1.Enabled = false; textBox1.Text = "";
            textBox2.Enabled = true; textBox2.Text = "";
            textBox3.Enabled = true; textBox3.Text = "";
            textBox11.Enabled = true; textBox5.Text = "0.0";
            textBox12.Enabled = true; textBox4.Text = "0.0";
            textBox13.Enabled = true; textBox7.Text = "0.0";
            textBox14.Enabled = true; textBox8.Text = "";
            textBox15.Enabled = true; textBox9.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "1";
            textBox14.Text = "0.0";
            textBox15.Text = "0.0";
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            label8.Visible = false;
            label18.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            groupBox3.Enabled = false;

            int i = 0;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "select COUNT(N_no) from Invoice";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                i = Int32.Parse(reader[0].ToString());
            }
            i++;
            textBox6.Text = i.ToString();
            connection.Close();

            connection.Open();
            OleDbCommand command2 = new OleDbCommand();
            command2.Connection = connection;

            qry = "select * from Customers";
            command2.CommandText = qry;
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                //c_i = (int)reader2["C_id"];
                c_i++;
            }
            textBox1.Text = c_i.ToString();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل انت متأكد؟", "طباعة", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
                if(textBox3.Text.Length == 10 && textBox7.Text != "0.0")
                {
                    OleDbCommand command = new OleDbCommand();
                    String qry = "";
                    if (tick == false)
                    {
                        connection.Open();
                        command.Connection = connection;
                        qry = "insert into Customers (C_name, C_phone, C_date) values (?,?,?)";
                        command.CommandText = qry;
                        command.Parameters.AddWithValue("@p1", textBox2.Text);
                        command.Parameters.AddWithValue("@p2", textBox3.Text);
                        command.Parameters.AddWithValue("@p3", DateTime.Now.ToString());
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    connection.Open();
                    command.Connection = connection;
                    qry = "select C_id from Customers where C_name=? and C_phone=?";
                    command.CommandText = qry;
                    command.Parameters.AddWithValue("@p1", textBox2.Text);
                    command.Parameters.AddWithValue("@p2", textBox3.Text);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        C_id = (int)reader[0];
                    }
                    //reader.Close();
                    connection.Close();

                    connection.Open();
                    OleDbCommand command2 = new OleDbCommand();
                    command2.Connection = connection;
                    qry = "insert into Invoice (N_no, C_id, N_date, N_vat, N_total, N_state, N_end) values (?,?,?,?,?,False,?)";
                    command2.CommandText = qry;
                    command2.Parameters.AddWithValue("@p1", textBox6.Text);
                    command2.Parameters.AddWithValue("@p2", C_id);
                    command2.Parameters.AddWithValue("@p3", DateTime.Now.ToString());
                    command2.Parameters.AddWithValue("@p4", 0.0);
                    command2.Parameters.AddWithValue("@p5", Double.Parse(textBox7.Text));
                    if(iii == 1)
                        command2.Parameters.AddWithValue("@p6", today.AddDays(7).ToShortDateString());
                    else
                        command2.Parameters.AddWithValue("@p6", today.AddDays(10).ToShortDateString());
                    command2.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    OleDbCommand command3 = new OleDbCommand();
                    command3.Connection = connection;
                    qry = "select N_id from Invoice where N_no=?";
                    command3.CommandText = qry;
                    command3.Parameters.AddWithValue("@p1", textBox6.Text);
                    reader = command3.ExecuteReader();
                    while (reader.Read())
                    {
                        N_id = (int)reader[0];
                    }
                    connection.Close();

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        connection.Open();
                        OleDbCommand command4 = new OleDbCommand();
                        command4.Connection = connection;
                        qry = "insert into Orders (O_type, O_amount, O_price, N_id) values (?,?,?,?)";
                        command4.CommandText = qry;
                        command4.Parameters.AddWithValue("@p1", dataGridView1.Rows[i].Cells[1].Value.ToString());
                        command4.Parameters.AddWithValue("@p2", dataGridView1.Rows[i].Cells[3].Value.ToString());
                        command4.Parameters.AddWithValue("@p3", dataGridView1.Rows[i].Cells[2].Value.ToString());
                        command4.Parameters.AddWithValue("@p4", N_id);
                        command4.ExecuteNonQuery();
                        connection.Close();
                    }
                    connection.Close();
                    button2.Enabled = true;
                    button5.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;

                    button6.Enabled = false;
                    button7.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox11.Enabled = false;
                    textBox12.Enabled = false;
                    textBox13.Enabled = false;
                    textBox14.Enabled = false;
                    textBox15.Enabled = false;

                    label8.Visible = true;
                    if (iii == 1)
                        label20.Text = today.AddDays(7).ToShortDateString();
                    else
                        label20.Text = today.AddDays(10).ToShortDateString();

                    label20.Visible = true;
                    label21.Visible = true;

                    ii = 1;
                    tick = false;
                    groupBox3.Enabled = true;

                    connection.Open();
                    OleDbCommand command5 = new OleDbCommand();
                    command5.Connection = connection;
                    qry = "Select * from Invoice where C_id = ?";
                    command5.CommandText = qry;
                    command5.Parameters.AddWithValue("@p1", C_id);
                    OleDbDataReader reader5 = command5.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    while (reader5.Read())
                    {
                        if (reader5["N_state"].ToString() == "False")
                            dataGridView2.Rows.Add(reader5["N_no"], reader5["N_date"], reader5["N_total"], "مفتوحة");
                        else if (reader5["N_state"].ToString() == "True")
                            dataGridView2.Rows.Add(reader5["N_no"], reader5["N_date"], reader5["N_total"], "مغلقة");
                    }
                    connection.Close();
                    printDocument1.Print();
                }
                else
                {
                    MessageBox.Show("يرجوا ادخال رقم العميل");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if(invoice != 0)
            {
                textBox8.Text = invoice.ToString();
                invoice = 0;
                button1.PerformClick();
            }
            if (Customer_phone != "")
            {
                textBox10.Text = Customer_phone.ToString();
                Customer_phone = "";
                button1.PerformClick();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(textBox3.Text.Length == 10 && button3.Enabled == true)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                String qry = "Select * from Customers where C_phone = ?";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", textBox3.Text);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    tick = true;
                while (reader.Read())
                {
                    textBox1.Text = reader["C_id"].ToString();
                    textBox2.Text = reader["C_name"].ToString();
                }
                connection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل انت متأكد؟", "إغلاق الفاتورة", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                String qry = "Update Invoice Set N_close = ?, N_state = True Where N_no = ?";
                command.CommandText = qry;
                command.Parameters.AddWithValue("@p1", DateTime.Now.ToString());
                command.Parameters.AddWithValue("@p2", textBox8.Text);
                command.ExecuteNonQuery();
                connection.Close();

                label8.Visible = true;
                label8.Text = "الفاتورة مغلقة";
                label8.ForeColor = Color.Blue;
                label18.Visible = true;
                label18.Text = DateTime.Now.ToString();
                button5.Enabled = false;
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView2.CurrentCell.ColumnIndex == 0)
                textBox8.Text = dataGridView2.CurrentCell.Value.ToString();
            button1.PerformClick();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button6.Enabled == true && textBox11.Text != "")
            {
                ii = 1;
                total = total - double.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString());
                textBox5.Text = total.ToString();
                total2 = total + (total * 0.0);
                textBox7.Text = total2.ToString();
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            Pen pen = new Pen(Color.Black, 1);
            Point point1 = new Point(180, 230);
            Point point2 = new Point(680, 230);

            //table no.1
            e.Graphics.DrawLine(pen, point1, point2); // >1
            point1 = new Point(180, 260);
            point2 = new Point(680, 260);
            e.Graphics.DrawLine(pen, point1, point2); // >2
            point1 = new Point(180, 290);
            point2 = new Point(680, 290);
            e.Graphics.DrawLine(pen, point1, point2); // >3
            point1 = new Point(560, 200);
            point2 = new Point(560, 290);
            e.Graphics.DrawLine(pen, point1, point2); // V
            e.Graphics.DrawString(" :اسم العميل", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(580, 205));
            e.Graphics.DrawString(textBox2.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(360, 205));
            e.Graphics.DrawString(" :رقم الجوال", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(580, 235));
            e.Graphics.DrawString(textBox3.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(330, 235));
            e.Graphics.DrawString(" :موعد الإستلام", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(570, 265));
            e.Graphics.DrawRectangle(pen, 180, 200, 500, 90);

            //table no.2
            point1 = new Point(180, 330);
            point2 = new Point(680, 330);
            e.Graphics.DrawLine(pen, point1, point2); // >1
            point1 = new Point(380, 300);
            point2 = new Point(380, 500);
            e.Graphics.DrawLine(pen, point1, point2); // V 1
            point1 = new Point(330, 300);
            point2 = new Point(330, 500);
            e.Graphics.DrawLine(pen, point1, point2); // V 2
            point1 = new Point(260, 300);
            point2 = new Point(260, 500);
            e.Graphics.DrawLine(pen, point1, point2); // V 3
            e.Graphics.DrawString("البيـــــــان", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(510, 305));
            e.Graphics.DrawString("العدد", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(335, 305));
            e.Graphics.DrawString("السعر", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(270, 305));
            e.Graphics.DrawString("الإجمالية", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(195, 305));
            e.Graphics.DrawRectangle(pen, 180, 300, 500, 200);

            int point02 = 335;

            for (int i=0; i < dataGridView1.RowCount; i++)
            {
                for (int ii = 0; ii <= 4; ii++)
                {
                    if (ii != 0)
                    {
                        if(ii == 1)
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[ii].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(565, point02), format);
                        else if(ii == 2)
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[ii].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(310, point02), format);
                        else if(ii == 3)
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[ii].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(360, point02), format);
                        else if(ii == 4)
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[ii].Value.ToString(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(240, point02), format);
                    }
                }
                point02 += 30;
            }

            e.Graphics.DrawString(textBox7.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(200, 505));
            e.Graphics.DrawString(" :المبلغ النهائي للفاتورة", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(245, 505));
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(30,30));
            e.Graphics.DrawString(DateTime.Now.ToString(new CultureInfo("ar-AE")), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(160, 170));
            e.Graphics.DrawString(" :رقم الفاتورة", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(630, 170));
            e.Graphics.DrawString(textBox6.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(580, 170));
            e.Graphics.DrawString(" :تاريخ انشاء الفاتورة", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(360, 170));

            if (iii == 1)
                e.Graphics.DrawString(today.AddDays(7).ToShortDateString(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(340, 265));
            else
                e.Graphics.DrawString(today.AddDays(10).ToShortDateString(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(340, 265));
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(button6.Enabled == true)
            {
                ii = 1;
                total = total - double.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString());
                textBox5.Text = total.ToString();
                total2 = total + (total * 0.0);
                textBox7.Text = total2.ToString();
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل انت متأكد؟", "إلغاء", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
                button2.Enabled = true;
                button3.Enabled = false; textBox5.Text = "0.0";
                button4.Enabled = false; textBox7.Text = "0.0";
                button6.Enabled = false; textBox6.Text = "";
                button7.Enabled = false; textBox1.Text = "0.0";
                textBox1.Enabled = false; textBox1.Text = "";
                textBox2.Enabled = false; textBox2.Text = "";
                textBox3.Enabled = false; textBox3.Text = "";
                textBox11.Enabled = false; textBox11.Text = "";
                textBox12.Enabled = false; textBox12.Text = "";
                textBox13.Enabled = false; textBox13.Text = "1";
                textBox14.Enabled = false; textBox14.Text = "0.0";
                textBox15.Enabled = false; textBox15.Text = "0.0";
                dataGridView1.Rows.Clear();
                groupBox3.Enabled = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                amount = Int32.Parse(textBox13.Text);
                sum = Double.Parse(textBox14.Text);
                sum = sum * amount;
                textBox15.Text = sum.ToString();
            }catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
