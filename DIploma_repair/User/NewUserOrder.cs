using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DIploma_repair.User
{
    public partial class NewUserOrder : Form
    {
        private string Login;
        public MySqlConnection conn;
        private int User_id;

        public List<int> Select = new List<int>();

        public List<int> Service_id = new List<int>();
        public List<string> Service_name = new List<string>();

        public List<int> Office_id = new List<int>();
        public List<string> Office_name = new List<string>();

        public List<int> Worker_id = new List<int>();
        public List<int> Worker_count = new List<int>();
        public List<string> Worker_name = new List<string>(); 

        public List<int> Manufacturer_id = new List<int>();
        public List<string> Manufacturer_name = new List<string>();

        public List<int> Type_id = new List<int>();
        public List<string> Type_name = new List<string>();

        public List<int> Item_id = new List<int>();
        public List<string> Item_name = new List<string>();

        public List<int> Model_id = new List<int>();
        public List<string> Model_name = new List<string>();

        public List<int> Detail_id = new List<int>();
        public List<string> Detail_name = new List<string>();

        public NewUserOrder(string login)
        {
            InitializeComponent();
            this.Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.getConnectInfo());
            conn.Open();
        }

        private void NewUserOrder_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                Service_id.Clear();
                Service_name.Clear();

                //////////////////////////////////////services
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Service_id, Service_name FROM Service;")
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Service_id.Add(reader.GetInt32(0));
                    Service_name.Add(reader.GetString(1));
                    comboBox1.Items.Add(reader.GetString(1));
                }
                reader.Close();
                //////////////////////////////////////offices
                cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Office_id, Office_name, Address FROM Office;")
                };
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Office_id.Add(reader.GetInt32(0));
                    Office_name.Add(reader.GetString(1) + "(" + reader.GetString(2) + ")");
                    comboBox7.Items.Add(reader.GetString(1) + "(" + reader.GetString(2) + ")");
                }
                reader.Close();
                //////////////////////////////////////Manufacturer
                cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT M_id, M_name FROM Manufacturer;")
                };
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Manufacturer_id.Add(reader.GetInt32(0));
                    Manufacturer_name.Add(reader.GetString(1));
                    comboBox2.Items.Add(reader.GetString(1));
                }
                reader.Close();

                //////////////////////////////////////ITEM TYPE
                cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Type_id, Type_name FROM Type_of_item;")
                };
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Type_id.Add(reader.GetInt32(0));
                    Type_name.Add(reader.GetString(1));
                    comboBox3.Items.Add(reader.GetString(1));
                }
                reader.Close();
                //////////////////////////////////////User_id
                cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT User_id FROM Users Where Login= '"+ Login + "';")
                };
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User_id = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }

            textBox2.ReadOnly = true;
            textBox2.Text = DateTime.Now.ToString("yyy:MM:dd");
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Worker_id.Clear();
                Worker_name.Clear();
                Worker_count.Clear();

                //////////////////////////////////////WORKER
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Worker.Worker_id, COUNT(Orders.Order_id), Worker.Worker_name, Worker.Worker_surname FROM Worker INNER JOIN Orders on(Worker.Worker_id=Orders.Worker_id) WHERE Office_id='" + Office_id[comboBox7.SelectedIndex] + "';")
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        Worker_id.Add(reader.GetInt32(0));
                        Worker_count.Add(reader.GetInt32(1));
                        Worker_name.Add(reader.GetString(2) + " " + reader.GetString(3));
                    }
                    catch(Exception ex)
                    {

                    }
                  
                }
                reader.Close();

                int index = 0;
                int min = Worker_count.Min();
                for(int i= 0; i<Worker_count.Count; i++)
                {
                    if (Worker_count[i] == min)
                    {
                        index = i;
                        break;
                    }
                }
                textBox1.Text = Worker_name[index];
            }
            catch (Exception ex)
            {
                textBox1.Clear();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != " " && comboBox2.Text != "")
            {
                try
                {
                    comboBox4.Items.Clear();
                    Item_id.Clear();
                    Item_name.Clear();

                    int M_id = Manufacturer_id[comboBox2.SelectedIndex];
                    int Item_type = Type_id[comboBox3.SelectedIndex];
                    //////////////////////////////////////Item
                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = string.Format("SELECT Item_id, Item_name FROM Item WHERE M_id=" + M_id + " and Type_id= " + Item_type +";")
                    };
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Item_id.Add(reader.GetInt32(0));
                        Item_name.Add(reader.GetString(1));
                        comboBox4.Items.Add(reader.GetString(1));
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {

                }
            }
            else
            {

                MessageBox.Show("Please select Manufacturer :)");
            }

        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox3.Text != " " && comboBox3.Text != "")
            {
                try
                {
                    comboBox5.Items.Clear();
                    Model_id.Clear();
                    Model_name.Clear();

                    int id = Item_id[comboBox4.SelectedIndex];
                    //////////////////////////////////////Item
                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = string.Format("SELECT Model_id, Model_name FROM Model WHERE Item_id= " + id + ";")
                    };
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Model_id.Add(reader.GetInt32(0));
                        Model_name.Add(reader.GetString(1));
                        comboBox5.Items.Add(reader.GetString(1));
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("Please select Item type :)");
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox6.Items.Clear();
                Detail_id.Clear();
                Detail_name.Clear();

                int id = Model_id[comboBox5.SelectedIndex];
                //////////////////////////////////////Item
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Detail.Detail_id, Detail.Detail_name, Detail.Prod_country FROM Detail INNER JOIN consist_of on(Detail.Detail_id=consist_of.Detail_id) WHERE consist_of.Model_id =" + id + ";")
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Detail_id.Add(reader.GetInt32(0));
                    Detail_name.Add(reader.GetString(1) + "(" + reader.GetString(2) + ")");
                    comboBox6.Items.Add(reader.GetString(1) + "(" + reader.GetString(2) + ")");
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = true;
            foreach(var item in listBox1.Items)
            {
                if (item == comboBox6.SelectedItem)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                listBox1.Items.Add(comboBox6.SelectedItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> prices = new List<int>();
            listBox2.Items.Clear();
            try
            {
                //////////////////////////////////////servicesPrice
                int servPrice = 0;
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Service_price FROM Service WHERE Service_id =" + Service_id[comboBox1.SelectedIndex]+ ";")
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   servPrice = reader.GetInt32(0);
                   listBox2.Items.Add("Service " + Service_name[comboBox1.SelectedIndex] + " : " + reader.GetString(0));
                }
                reader.Close();
                //////////////////////////////////////DetailsPrice
                if (listBox1.Items.Count > 0)
                {
                    foreach(var item in listBox1.Items)
                    {
                        int index = 0;
                        for (int i = 0; i < Detail_name.Count; i++)
                        { 
                            if(Detail_name[i] == item.ToString())
                            {
                                index = i;
                                Select.Add(Detail_id[i]);
                                break;
                            }
                        }
                        cmd = new MySqlCommand
                        {
                            Connection = conn,
                            CommandText = string.Format("SELECT Price FROM Detail WHERE Detail_id=" + Detail_id[index] +";")
                        };
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            prices.Add(reader.GetInt32(0));
                            listBox2.Items.Add("Detail " + Detail_name[index] + " : " + reader.GetString(0));
                        }
                        reader.Close();
                    }
                }
                foreach(var item in prices)
                {
                    servPrice += item;
                }
                textBox3.Text = servPrice.ToString();
                button2.Enabled = false;
            }
            catch (Exception ex)
            {

            }
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AllFieldsAreFull())
            {
               if(listBox2.Items.Count > 0)
                {
                    string sql = "Insert into Orders (Order_id, User_id, " +
                        "Service_id, Worker_id, Model_id, Status_id, Description, " +
                        "Order_Date, Complete_set, Appearance, Order_price) values (null," +
                        User_id + "," + Service_id[comboBox1.SelectedIndex] + "," + GetWorkerId() + "," +
                        Model_id[comboBox5.SelectedIndex] + "," + 1 + ",'" + richTextBox2.Text + "','" +
                        textBox2.Text + "','" + richTextBox1.Text + "'," + numericUpDown1.Value + "," + textBox3.Text + ");";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    foreach (var item in Select)
                    {
                        sql = "insert into is_for(Detail_id, Order_id) values(" + item + ", (SELECT MAX(Order_id) FROM Orders));";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    Send();
                    MessageBox.Show("Заявку прийнято!");
                    this.Close();
                }
                else
                {
                    string sql = "Insert into Orders (Order_id, User_id, " +
                        "Service_id, Worker_id, Model_id, Status_id, Description, " +
                        "Order_Date, Complete_set, Appearance, Order_price) values (null" +
                        User_id + "," + Service_id[comboBox1.SelectedIndex] + "," + GetWorkerId() + "," + 
                        Model_id[comboBox5.SelectedIndex] + "," + 1 + ",'" + richTextBox2.Text + "'," +
                        textBox2.Text + ",'" + richTextBox1.Text + "','" + numericUpDown1.Value + "','" + textBox3.Text + "');";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    Send();
                    MessageBox.Show("Заявку прийнято!");
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Please check required fiqlds mark * !");
            }
        }

        private void Send()
        {
            //////////////////get_orderID
            int orderId = 0;
            MySqlCommand cmd1 = new MySqlCommand
            {
                Connection = conn,
                CommandText = string.Format("SELECT MAX(Order_id) FROM Orders;")
            };
            MySqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                orderId = reader.GetInt32(0);
            }
            reader.Close();
            /////////////////////USer Email
            string userMail = "";
            cmd1 = new MySqlCommand
            {
                Connection = conn,
                CommandText = string.Format("SELECT Email FROM Users WHERE Login='" + Login + "';")
            };
            reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                userMail = reader.GetString(0);
            }
            reader.Close();

            Mailer.Generator generator = new Mailer.Generator();
            string body = generator.GenerateBody(Login, orderId, "ServiCEntre");
            string subject = generator.GenerateSubject("ServiCEntre", orderId);
            Mailer.Mailer mailer = new Mailer.Mailer();
            mailer.SendMail(userMail, "example@gmail.com", "", subject, body);
        }

        private bool AllFieldsAreFull()
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" &&
                comboBox3.Text != "" && comboBox4.Text != "" &&
                comboBox5.Text != "" && comboBox7.Text != "" &&
                textBox1.Text != "" && textBox2.Text != "" &&
                textBox3.Text != "" && richTextBox1.Text != "" &&

                comboBox1.Text != " " && comboBox2.Text != " " &&
                comboBox3.Text != " " && comboBox4.Text != " " &&
                comboBox5.Text != " " && comboBox7.Text != " " &&
                textBox1.Text != " " && textBox2.Text != " " &&
                textBox3.Text != " " && richTextBox1.Text != " " &&
                numericUpDown1.Value > 0 )
            {
                if(comboBox1.Text == "replacement parts" || comboBox1.Text == "order parts")
                {
                    if (comboBox6.Text != "" && comboBox6.Text != " ")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "replacement parts" || comboBox1.Text == "order parts")
            {
                label13.Text = "Detail *";
            }
            else
            {
                label13.Text = "Detail";
            }
        }

        private int GetWorkerId()
        {
            int index = 0;
            for(int i = 0; i< Worker_name.Count; i++)
            {
                if(Worker_name[i] == textBox1.Text)
                {
                    index = i;
                }
            }
            return Worker_id[index];
        }

        private void NewUserOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            User.UserRoom room = new UserRoom(Login);
            room.Show();
            this.Dispose();
        }
    }
}
