using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DIploma_repair.Admin
{
    public partial class AddDevice : Form
    {
        private string Login;
        public MySqlConnection conn;

        private List<int> type_item = new List<int>();
        private List<int> manufacturer = new List<int>();
        private List<int> item = new List<int>();
        private List<int> model = new List<int>();

        public AddDevice(string login)
        {
            InitializeComponent();
            Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();
        }

        private void AddDevice_Load(object sender, EventArgs e)
        {
            try
            {
                //////////////////////Manufacturer
                comboBox1.Items.Clear();
                comboBox6.Items.Clear();
                manufacturer.Clear();
                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT M_id, M_name FROM Manufacturer;")
                };
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    manufacturer.Add(reader2.GetInt32(0));
                    comboBox1.Items.Add(reader2.GetString(1));
                    comboBox6.Items.Add(reader2.GetString(1));
                }
                reader2.Close();
                ///////////////////////type_of_items
                comboBox2.Items.Clear();
                comboBox5.Items.Clear();
                type_item.Clear();
                cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Type_id, Type_name FROM Type_of_item;")
                };
                reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    type_item.Add(reader2.GetInt32(0));
                    comboBox2.Items.Add(reader2.GetString(1));
                    comboBox5.Items.Add(reader2.GetString(1));
                }
                reader2.Close();
            }
            catch (Exception)
            {

            }
            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {  ///////////////////////items
                    comboBox3.Items.Clear();
                    item.Clear();
                    MySqlCommand cmd1 = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = string.Format("SELECT Item_id, Item_name FROM Item Where M_id=" + manufacturer[comboBox1.SelectedIndex] + " and Type_id= " + type_item[comboBox2.SelectedIndex] + ";")
                    };
                    MySqlDataReader reader2 = cmd1.ExecuteReader();
                    while (reader2.Read())
                    {
                        item.Add(reader2.GetInt32(0));
                        comboBox3.Items.Add(reader2.GetString(1));
                    }
                    reader2.Close();
            }
            catch (Exception)
            {

            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ///////////////////////items
                    comboBox4.Items.Clear();
                    item.Clear();
                    MySqlCommand cmd1 = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = string.Format("SELECT Item_id, Item_name FROM Item Where M_id=" + manufacturer[comboBox6.SelectedIndex] + " and Type_id= " + type_item[comboBox5.SelectedIndex] + ";")
                    };
                    MySqlDataReader reader2 = cmd1.ExecuteReader();
                    while (reader2.Read())
                    {
                        item.Add(reader2.GetInt32(0));
                        comboBox4.Items.Add(reader2.GetString(1));
                    }
                    reader2.Close();
            
            }
            catch (Exception)
            {

            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ///////////////////////Model
                comboBox7.Items.Clear();
                model.Clear();
                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Model_id, Model_name FROM Model Where Item_id=" + item[comboBox4.SelectedIndex] + ";")
                };
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    model.Add(reader2.GetInt32(0));
                    comboBox7.Items.Add(reader2.GetString(1));
                }
                reader2.Close();
            }
            catch (Exception)
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Insert into Model (Model_id, Item_id, Model_name) values (null,'" +
                     item[comboBox3.SelectedIndex] + "','" + textBox1.Text + "');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Model " + textBox1.Text + " was added to database!");
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                textBox1.Clear();
            }
            catch (Exception)
            {

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Insert into Serial (Model_id, Serial_number) values (" +
                      model[comboBox7.SelectedIndex] + ",'" + textBox2.Text + "');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Device with serial " + textBox2.Text + " was added to database!");
                comboBox7.SelectedIndex = -1;
                comboBox6.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                textBox2.Clear();
            }
            catch (Exception)
            {

            }

        }

        private void AddDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminRoom room = new AdminRoom(Login);
            room.Show();
            this.Dispose();
        }
    }
}
