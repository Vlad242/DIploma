using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DIploma_repair.User
{
    public partial class CurrentOrderInfo : Form
    {
        public MySqlConnection conn;
        private string Login;
        private int Index;

        public CurrentOrderInfo(string login, int index)
        {
            InitializeComponent();
            Login = login;
            Index = index;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.getConnectInfo());
            conn.Open();
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            this.Text = "Замовлення №" + Index;
        }

        private void CurrentOrderInfo_Load(object sender, System.EventArgs e)
        {
            try
            {
                MySqlCommand cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Service.Service_name, Service.Service_price,Worker.Worker_surname, Worker.Worker_name, Worker.Worker_fname, Office.Office_name, Office.Address, Item.Item_name, Manufacturer.M_name, Model.Model_name, Serial.Serial_number, Orders.Complete_set, Orders.Appearance, Orders.Order_price, Orders.Order_Date, Orders.Description, Status.Status_name FROM Orders INNER JOIN Service on(Orders.Service_id=Service.Service_id) INNER JOIN Status on(Orders.Status_id=Status.Status_id) INNER JOIN Model on(Model.Model_id=Orders.Model_id) INNER JOIN Serial on(Model.Model_id=Serial.Model_id) INNER JOIN Item on(Model.Item_id =Item.Item_id) INNER JOIN Manufacturer on(Manufacturer.M_id=Item.M_id) INNER JOIN Worker on(Worker.Worker_id= Orders.Worker_id) INNER JOIN Office on(Worker.Office_id=Office.Office_id) WHERE Orders.Order_id=" + Index + ";")
                };
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {

                    label12.Text = "ID замовлення:";
                    label13.Text = "Сервіс:";
                    label14.Text = "Замовлення обслуговує:";
                    label15.Text = "Офіс:";
                    label16.Text = "Ваш пристрі:";
                    label17.Text = "Серійний номер:";
                    label18.Text = "Оцінка стану:";
                    label19.Text = "Загальна ціна замовлення:";
                    label20.Text = "Статус замовлення:";
                    label7.Text = "Повний комплект:";
                    label11.Text = "Опис:";
                    label21.Text = "Дата замовлення:";

                    label1.Text = Index.ToString();
                    label2.Text = reader.GetString(0) + " (ціна:" + reader.GetString(1) + ")";
                    label3.Text = reader.GetString(2) + " " + reader.GetString(3) + " " + reader.GetString(4);
                    label4.Text = reader.GetString(5) + " (за адресою:" + reader.GetString(6) + ")";
                    label5.Text = reader.GetString(7) + " " + reader.GetString(8) + " " + reader.GetString(9);
                    label6.Text = reader.GetString(10);
                    richTextBox1.Text = reader.GetString(11);
                    label8.Text = reader.GetString(12);
                    label9.Text = reader.GetString(13);
                    label10.Text = reader.GetString(16);
                    richTextBox2.Text = reader.GetString(15);
                    label22.Text = reader.GetString(14).Remove(10);
                }
                reader.Close();

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

                if(label10.Text == "Order processing")
                {
                    label10.ForeColor = Color.Orange;
                }
                else if(label10.Text == "Complete")
                {
                    label10.ForeColor = Color.Green;
                }
                else if(label10.Text == "In the process of repair")
                {
                    label10.ForeColor = Color.Blue;
                }

                if(listBox1.Items.Count <= 1)
                {
                    this.Size = new Size(567, 438);
                }
                else
                {
                    this.Size = new Size(838, 438);
                }
            }
            catch (Exception)
            {

            }
        }

        private void CurrentOrderInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            OrderList list = new OrderList(Login);
            list.Show();
            this.Dispose();
        }
    }
}
