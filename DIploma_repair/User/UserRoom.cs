using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DIploma_repair.User
{
    public partial class UserRoom : Form
    {
        private string Login;
        public MySqlConnection conn;

        public UserRoom(string login)
        {
            InitializeComponent();
            this.Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();
        }
        private void LogOut(object sender, EventArgs e)
        {
            conn.Close();
            LogIn.LogIn l = new LogIn.LogIn();
            l.Show();
            this.Dispose();
        }

        private void ProgClose(object sender, EventArgs e)
        {
            conn.Close();
            Application.Exit();
        }

        private void UserRoom_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("Select User_name, User_surname, User_fname, Birthdate, Phone, Adress, Email FROM Users Where Login = '" + Login + "';")
                };
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    this.Text = "Кабінет користувача " + reader.GetString(0) + " " + reader.GetString(1);
                    label1.Text = "Ім'я: " + reader.GetString(0);
                    label2.Text = "Прізвище: " + reader.GetString(1);
                    label3.Text = "По батькові: " + reader.GetString(2);
                    label4.Text = "Дата народження: " + reader.GetString(3).Remove(10); ;
                    label5.Text = "Номер телефону: " + reader.GetString(4);
                    label6.Text = "Адреса: " + reader.GetString(5);
                    label7.Text = "E-mail: " + reader.GetString(6);
                }
                reader.Close();


                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT COUNT(Order_id) FROM Orders WHERE User_id = (SELECT User_id FROM Users WHERE Login = '" + Login + "');")
                };
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    label9.Text = reader2.GetString(0);
                }
                reader2.Close();
                
                dataGridView1.Columns.Clear();
                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT DISTINCT Service.Service_name, Manufacturer.M_name, Model.Model_name, Orders.Order_Date, Status.Status_name FROM Orders INNER JOIN Service on (Service.Service_id=Orders.Service_id) INNER JOIN Model on (Orders.Model_id=Model.Model_id) INNER JOIN Item on(Item.Item_id=Model.Item_id) INNER JOIN Manufacturer on(Manufacturer.M_id=Item.M_id) INNER JOIN Status on(Orders.Status_id=Status.Status_id) WHERE Orders.User_id = (SELECT User_id FROM Users WHERE Login= '"+Login+"') limit 8;", conn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Orders");
                dataGridView1.DataSource = ds.Tables["Orders"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                /////columns names
                dataGridView1.Columns[0].HeaderText = "Сервіс";
                dataGridView1.Columns[1].HeaderText = "Бренд";
                dataGridView1.Columns[2].HeaderText = "Модель";
                dataGridView1.Columns[3].HeaderText = "Дата замовлення";
                dataGridView1.Columns[4].HeaderText = "Статус замовлення";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                ReColorGrid();
            }
            catch (Exception)
            {

            }
        }

        private void ReColorGrid()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                switch (dataGridView1.Rows[i].Cells[4].Value)
                {
                    case "Order processing":
                        {
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Orange;
                            break;
                        }
                    case "Complete":
                        {
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Green;
                            break;
                        }
                    case "In the process of repair":
                        {
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Cyan;
                            break;
                        }
                    case "Purchase":
                        {
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Yellow;
                            break;
                        }
                }
            }
        }

        private void UserRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
                conn.Close();
                LogIn.LogIn l = new LogIn.LogIn();
                l.Show();
                this.Dispose();
        }

        private void ServicesList(object sender, EventArgs e)
        {
            Services s = new Services(Login);
            s.Show();
            this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            UserRoom_Load(null,null);
        }

        private void NewActionClick(object sender, EventArgs e)
        {
            NewUserOrder userOrder = new NewUserOrder(Login);
            userOrder.Show();
            this.Dispose();
        }

        private void OrderListClick(object sender, EventArgs e)
        {
            OrderList order = new OrderList(Login);
            order.Show();
            this.Dispose();
        }

        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ReColorGrid();
        }
    }
}
