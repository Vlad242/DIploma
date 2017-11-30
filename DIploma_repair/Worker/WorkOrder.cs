using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DIploma_repair.Worker
{
    public partial class WorkOrder : Form
    {
        public MySqlConnection conn;
        private string Login;
        private int Index;
        private string StatusName;

        private List<int> id = new List<int>();
        private List<int> price = new List<int>();

        public WorkOrder(string login, int index)
        {
            InitializeComponent();
            Login = login;
            Index = index;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            this.Text = "Замовлення №" + Index;
        }

        private void WorkOrder_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Сервіс:";
                label2.Text = "Пристрій:";
                label3.Text = "Серійний номер:";
                label4.Text = "Оцінка стану:";
                label5.Text = "Повний комплект:";
                label6.Text = "Коментар:";


                MySqlCommand cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Service.Service_name, Item.Item_name, Manufacturer.M_name, Model.Model_name, Serial.Serial_number, Orders.Appearance, Orders.Complete_set, Orders.Description, Status.Status_name FROM Orders INNER JOIN Service on(Orders.Service_id=Service.Service_id) INNER JOIN Status on(Orders.Status_id=Status.Status_id) INNER JOIN Model on(Model.Model_id=Orders.Model_id) INNER JOIN Serial on(Model.Model_id=Serial.Model_id) INNER JOIN Item on(Model.Item_id =Item.Item_id) INNER JOIN Manufacturer on(Manufacturer.M_id=Item.M_id) WHERE Orders.Order_id=" + Index + ";")
                };
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    label7.Text = reader.GetString(0);
                    label8.Text = reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3);
                    label9.Text = reader.GetString(4);
                    label10.Text = reader.GetString(5);
                    richTextBox1.Text = reader.GetString(6);
                    richTextBox2.Text = reader.GetString(7);
                    StatusName = reader.GetString(8);
                    label13.Text = StatusName;
                }
                reader.Close();

                if (label13.Text == "Order processing")
                {
                    label13.ForeColor = Color.Orange;
                }
                else if (label13.Text == "Complete")
                {
                    label13.ForeColor = Color.Green;
                }
                else if (label13.Text == "In the process of repair")
                {
                    label13.ForeColor = Color.Blue;
                }
                else if (label13.Text == "Purchase")
                {
                    label13.ForeColor = Color.Purple;
                }

                listBox1.Items.Clear();
                listBox1.Items.Add("Назва -> Країна виробник -> Ціна");
                cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT  Detail.Detail_name, Detail.Prod_country, Detail.Price FROM Detail INNER JOIN is_for on(Detail.Detail_id = is_for.Detail_id) WHERE is_for.Order_id =" + Index + ";")
                };
                reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                }
                reader.Close();

                comboBox1.Items.Clear();
                cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Detail.Detail_id, Detail.Detail_name, Detail.Prod_country, Detail.Price FROM Detail INNER JOIN consist_of on (Detail.Detail_id = consist_of.Detail_id) WHERE consist_of.Model_id = (SELECT Model_id FROM Orders WHERE Order_id=" + Index + ");")
                };
                reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3));
                    id.Add(reader.GetInt32(0));
                    price.Add(reader.GetInt32(3));
                }
                reader.Close();

            }
            catch (Exception)
            {

            }
        }

        private void WorkOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            WOrderList list = new WOrderList(Login);
            list.Show();
            this.Dispose();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            { 
                if(comboBox2.SelectedIndex > 0)
                {
                    string sql = "UPDATE Orders SET Status_id=(SELECT Status_id FROM Status WHERE Status_name= '" + comboBox2.SelectedItem  + "') WHERE Order_id= " + Index + ";";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    if (comboBox2.SelectedItem.ToString() == "Complete")
                    {
                        button2.Enabled = false;
                        button1.Enabled = false;
                        Send();
                    }
                    WorkOrder_Load(null, null);
                    MessageBox.Show("Status changed to " + comboBox2.SelectedItem);
                }
                else
                {
                    MessageBox.Show("Select status!");
                }
               
            }
            catch(Exception)
            {
            }
        }

        private void Send()
        {
            string userMail = "";
            MySqlCommand cmd1 = new MySqlCommand
            {
                Connection = conn,
                CommandText = string.Format("SELECT Users.Email FROM Users INNER JOIN Orders on(Orders.User_id=Users.User_id) WHERE Orders.Order_id='" + Index + "';")
            };
            MySqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                userMail = reader.GetString(0);
            }
            reader.Close();

            Mailer.Generator generator = new Mailer.Generator();
            string body = generator.GenerateCompleteBody(Login, Index, "ServiCEntre");
            string subject = generator.GenerateSubject("ServiCEntre", Index);
            Mailer.Mailer mailer = new Mailer.Mailer();
            mailer.SendMail(userMail, "example@gmail.com", "", subject, body);

            MessageBox.Show("Order complete!");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Insert into is_for (Detail_id, Order_id) values (" +
                      id[comboBox1.SelectedIndex] + "," + Index + ");";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                /////////select general price
                int currentPrice = 0;
                MySqlCommand cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Order_price FROM Orders WHERE Order_id=" + Index + ";")
                };
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    currentPrice = reader.GetInt32(0);
                }
                reader.Close();
                ////////////////////////////

                sql = "UPDATE Orders SET Order_price=" + (currentPrice + price[comboBox1.SelectedIndex]) + " WHERE Order_id= " + Index + ";";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Detail " + comboBox1.Text + " was addet to list!General price updated!");
                WorkOrder_Load(null, null);
                comboBox1.SelectedIndex = 0;
            }
            catch(Exception)
            {

            }
        }
    }
}
