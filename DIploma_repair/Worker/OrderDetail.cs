using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DIploma_repair.Worker
{
    public partial class OrderDetail : Form
    {
        private string Login;
        public MySqlConnection conn;
        private int Worker_id;

        private List<int> Detail_ids = new List<int>();

        public OrderDetail(string login)
        {
            InitializeComponent();
            Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.getConnectInfo());
            conn.Open();
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
        }

        private void OrderDetail_Load(object sender, System.EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Detail_id, Detail_name, Prod_country FROM Detail;")
                };
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    Detail_ids.Add(reader2.GetInt32(0));
                    comboBox1.Items.Add(reader2.GetString(1) + "(" + reader2.GetString(2) + ")");
                }
                reader2.Close();
                //////////////////worker
                cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Worker_id, Worker_surname, Worker_name, Worker_fname FROM Worker WHERE Login='" + Login + "';")
                };
                reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    Worker_id = reader2.GetInt32(0);
                    textBox4.Text = reader2.GetString(1) + " " + reader2.GetString(2) + " " + reader2.GetString(3);
                }
                reader2.Close();
                //////////////////Date
                textBox5.Text = DateTime.Now.ToString("yyyy:MM:dd");
            }
            catch (Exception)
            {

            }
           
        }

        private void OrderDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
            WorkerRoom room = new WorkerRoom(Login);
            room.Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text != "" && textBox2.Text != "" &&
                    textBox1.Text != " " && textBox2.Text != " ")
                {
                    string sql = "Insert into Detail (Detail_id, Detail_name, Prod_country, Price) values (null,'" +
                   textBox1.Text + "','" + textBox2.Text + "'," +numericUpDown2.Value + ");";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Деталь додано!");
                    textBox1.Clear();
                    textBox2.Clear();
                    numericUpDown2.Value = 1;
                }
                else
                {
                    MessageBox.Show("Не всі поля заповнені!");
                }
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "" &&
                    textBox4.Text != " " && textBox5.Text != " " && comboBox1.Text != " ")
                {
                    string sql = "Insert into Detail_order (D_Order_id, Detail_id, Worker_id, Status_id, D_Order_date, D_Count, D_descriptions) values (null," +
                  Detail_ids[comboBox1.SelectedIndex] + "," + Worker_id + "," + 1 + ",'" + textBox5.Text + "','" + numericUpDown1.Value + "','" + richTextBox1.Text + "');";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Замовлення додано!");
                    comboBox1.SelectedIndex = 0;
                    numericUpDown1.Value = 1;
                    richTextBox1.Clear();
                }
                else
                {
                    MessageBox.Show("Не всі поля заповнені!");
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
