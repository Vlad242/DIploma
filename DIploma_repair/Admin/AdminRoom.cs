using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DIploma_repair.Admin
{
    public partial class AdminRoom : Form
    {
        private string Login;
        public MySqlConnection conn;

        public AdminRoom(string login)
        {
            InitializeComponent();
            Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void Logout(object sender, EventArgs e)
        {
            LogIn.LogIn logIn = new LogIn.LogIn();
            logIn.Show();
            this.Dispose();
        }

        private void AdminRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogIn.LogIn logIn = new LogIn.LogIn();
            logIn.Show();
            this.Dispose();
        }

        private void AdminRoom_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("Select User_name, User_surname, User_fname, Email FROM Users Where Login = '" + Login + "';")
                };
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    this.Text = "Кабінет адміністратора " + reader.GetString(1) + " " + reader.GetString(0);
                    label8.Text = "Ім'я: " + reader.GetString(0);
                    label9.Text = "Прізвище: " + reader.GetString(1);
                    label10.Text = "По батькові: " + reader.GetString(2);
                    label11.Text = "E-mail: " + reader.GetString(3);
                }
                reader.Close();
                //////////////////////status
                List<string> status = new List<string>();
                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Status_name FROM Status;")
                };
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    status.Add(reader2.GetString(0));
                }
                reader2.Close();
                /////////////////count
                List<string> count = new List<string>();
                foreach (var item in status)
                {
                    cmd1 = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = string.Format("SELECT COUNT(Detail_order.D_Order_id) FROM Detail_order INNER JOIN Status on(Status.Status_id=Detail_order.Status_id) WHERE Status.Status_name='" + item + "';")
                    };
                    reader2 = cmd1.ExecuteReader();
                    while (reader2.Read())
                    {
                        count.Add(reader2.GetString(0));
                        listBox1.Items.Add("Заяв зі статусом '" + item + "' - " + reader2.GetString(0));
                    }
                    reader2.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        private void AddDevice(object sender, EventArgs e)
        {
            AddDevice device = new AddDevice(Login);
            device.Show();
            this.Dispose();
        }

        private void DetailList(object sender, EventArgs e)
        {
            DetailOrderList list = new DetailOrderList(Login);
            list.Show();
            this.Dispose();
        }

        private void AddDetail(object sender, EventArgs e)
        {
            AddDetail detail = new AddDetail(Login);
            detail.Show();
            this.Dispose();
        }
    }
}
