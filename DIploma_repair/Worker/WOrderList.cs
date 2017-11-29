using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DIploma_repair.Worker
{
    public partial class WOrderList : Form
    {
        public MySqlConnection conn;
        private string Login;
        public string Search = "";

        List<string> indexes = new List<string>();
        List<string> Searces = new List<string>();

        public WOrderList(string login)
        {
            InitializeComponent();
            Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.getConnectInfo());
            conn.Open();


            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyy:MM:dd";
            dateTimePicker1.Visible = false;
        }

        private void WOrderList_Load(object sender, EventArgs e)
        {
            try
            {
                ////////////////////////////////
                dataGridView1.Columns.Clear();
                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT Orders.Order_id, Service.Service_name, Manufacturer.M_name, Model.Model_name, Orders.Order_Date, Status.Status_name FROM Orders INNER JOIN Service on(Orders.Service_id=Service.Service_id) INNER JOIN Model on(Model.Model_id=Orders.Model_id) INNER JOIN Item on(Model.Item_id=Item.Item_id) INNER JOIN Manufacturer on(Manufacturer.M_id=Item.M_id) INNER JOIN Status on(Status.Status_id=Orders.Status_id) WHERE Orders.User_id = (SELECT Worker_id FROM Worker WHERE Login='" + Login + "');", conn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Orders");
                dataGridView1.DataSource = ds.Tables["Orders"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                /////columns names
                dataGridView1.Columns[0].HeaderText = "Ідентифікатор";
                dataGridView1.Columns[1].HeaderText = "Назва сервісу";
                dataGridView1.Columns[2].HeaderText = "Бренд";
                dataGridView1.Columns[3].HeaderText = "Модель";
                dataGridView1.Columns[4].HeaderText = "Дата замовлення";
                dataGridView1.Columns[5].HeaderText = "Статус замовлення";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                ReColorGrid();
            }
            catch (Exception ex)
            {

            }
        }

        private void ReColorGrid()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                switch (dataGridView1.Rows[i].Cells[5].Value)
                {
                    case "Order processing":
                        {
                            dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Orange;
                            break;
                        }
                    case "Complete":
                        {
                            dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Green;
                            break;
                        }
                    case "In the process of repair":
                        {
                            dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Cyan;
                            break;
                        }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReSearch();
            ////////////////////////////////
            dataGridView1.Columns.Clear();
            MySqlDataAdapter mda = new MySqlDataAdapter("SELECT Orders.Order_id, Service.Service_name, Manufacturer.M_name, Model.Model_name, Orders.Order_Date, Status.Status_name FROM Orders INNER JOIN Service on(Orders.Service_id=Service.Service_id) INNER JOIN Model on(Model.Model_id=Orders.Model_id) INNER JOIN Item on(Model.Item_id=Item.Item_id) INNER JOIN Manufacturer on(Manufacturer.M_id=Item.M_id) INNER JOIN Status on(Status.Status_id=Orders.Status_id) WHERE Orders.User_id = (SELECT Worker_id FROM Worker WHERE Login='" + Login + "') " + Search + ";", conn);
            DataSet ds = new DataSet();
            mda.Fill(ds, "Orders");
            dataGridView1.DataSource = ds.Tables["Orders"];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            /////columns names
            dataGridView1.Columns[0].HeaderText = "Ідентифікатор";
            dataGridView1.Columns[1].HeaderText = "Назва сервісу";
            dataGridView1.Columns[2].HeaderText = "Бренд";
            dataGridView1.Columns[3].HeaderText = "Модель";
            dataGridView1.Columns[4].HeaderText = "Дата замовлення";
            dataGridView1.Columns[5].HeaderText = "Статус замовлення";

            ReColorGrid();
        }


        public void ReSearch()
        {
            string listText = "";
            string addSearch = "";
            if (comboBox1.Text != "" && textBox2.Text != "")
            {
                string operation = "";
                switch (comboBox2.Text)
                {
                    case ">":
                        operation = ">";
                        break;
                    case "<":
                        operation = "<";
                        break;
                    case "=":
                        operation = "=";
                        break;
                    case ">=":
                        operation = ">=";
                        break;
                    case "<=":
                        operation = "<=";
                        break;
                    case "!=":
                        operation = "!=";
                        break;
                    default:
                        operation = " LIKE ";
                        break;
                }
                switch (comboBox1.Text)
                {
                    case "ID замовлення":
                        {
                            addSearch += "and Orders.Order_id" + operation + "'" + textBox2.Text + "' ";
                            listText = "ID замовлення " + operation + " " + textBox2.Text;
                            break;
                        }
                    case "Назва сервісу":
                        {
                            addSearch += "and Service.Service_name" + operation + "'" + textBox2.Text + "' ";
                            listText = "Назва сервісу " + operation + " " + textBox2.Text;
                            break;
                        }
                    case "Виробник":
                        {
                            addSearch += "and Manufacturer.M_name" + operation + "'" + textBox2.Text + "' ";
                            listText = "Виробник " + operation + " " + textBox2.Text;
                            break;
                        }
                    case "Модель":
                        {
                            addSearch += "and Model.Model_name" + operation + "'" + textBox2.Text + "' ";
                            listText = "Модель " + operation + " " + textBox2.Text;
                            break;
                        }
                    case "Дата":
                        {
                            addSearch += "and Orders.Order_Date='" + textBox2.Text + "' ";
                            listText = "Дата = " + textBox2.Text;
                            break;
                        }
                    case "Статус":
                        {
                            addSearch += "and Status.Status_name" + operation + "'" + textBox2.Text + "' ";
                            listText = "Статус " + operation + " " + textBox2.Text;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                bool flag = true;
                foreach (var item in listBox1.Items)
                {
                    if (item.ToString() == listText)
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    Search += addSearch;
                    Searces.Add(addSearch);
                    indexes.Add(listText);
                    listBox1.Items.Add(listText);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Searces.Clear();
            listBox1.Items.Clear();
            Search = "";

            WOrderList_Load(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count != 0)
                {
                    for (int i = 0; i < indexes.Count; i++)
                    {
                        if (indexes[i] == listBox1.SelectedItem.ToString())
                        {
                            int index = Search.IndexOf(Searces[i]);
                            int lenght = Searces[i].Length;
                            Search.Remove(index, lenght);
                            indexes.Remove(indexes[i]);
                            Searces.Remove(Searces[i]);
                            listBox1.Items.Remove(listBox1.SelectedItem);
                        }
                    }
                }
                WOrderList_Load(null, null);
            }
            catch (Exception ex)
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkOrder workOrder = new WorkOrder(Login, (int)dataGridView1.CurrentRow.Cells[0].Value);
            workOrder.Show();
            this.Dispose();
        }

        private void WOrderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            WorkerRoom room = new WorkerRoom(Login);
            room.Show();
            this.Dispose();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentCell.Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Дата")
            {
                dateTimePicker1.Visible = true;
            }
            else
            {
                dateTimePicker1.Visible = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker1.Text;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ReColorGrid();
        }
    }
}
