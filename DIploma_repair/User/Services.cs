using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace DIploma_repair.User
{
    public partial class Services : Form
    {
        private string Login;
        public MySqlConnection conn;

        public Services(string login)
        {
            InitializeComponent();
            this.Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();
        }

        private void Services_Load(object sender, System.EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT Service_name, Service_price FROM Service;", conn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Service");
                dataGridView1.DataSource = ds.Tables["Service"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                /////columns names
                dataGridView1.Columns[0].HeaderText = "Name";
                dataGridView1.Columns[1].HeaderText = "Price (without details)";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch(Exception)
            {

            }
        }

        private void Services_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserRoom room = new UserRoom(Login);
            room.Show();
            this.Dispose();
        }
    }
}
