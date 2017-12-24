using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System;
using CryptoMD5_XOR;

namespace DIploma_repair.LogIn
{
    public partial class LogIn : Form
    {
        private InternetConection internetConection = new InternetConection();
        public MySqlConnection conn;

        public LogIn()
        {
            InitializeComponent();
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();
        }

        private void LogIn_Load(object sender, System.EventArgs e)
        {
            switch (internetConection.GetInetStaus())
            {
                case "No access to the Internet":
                    {
                        label1.Text = "Без доступу до інтернету";
                        label1.ForeColor = Color.Red;
                        pictureBox1.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\DebugImage\\disconnect.png");
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        button2.Enabled = false;
                        MessageBox.Show("Будь ласка, перевірте підключення до інтернету, та перезавантажте додаток!");
                        break;
                    }
                case "Limited access":
                    {
                        label1.Text = "Обмежений доступ";
                        label1.ForeColor = Color.Orange;
                        pictureBox1.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\Image\\disconnect.png");
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        button2.Enabled = false;
                        MessageBox.Show("Будь ласка, перевірте підключення до інтернету, та перезавантажте додаток!");
                        break;
                    }
                case "Internet connection":
                    {
                        label1.Text = "Доступ до інтернету";
                        label1.ForeColor = Color.Green;
                        pictureBox1.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\DebugImage\\connect.png");
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        break;
                    }
            }

            ////////////////Login from users
            try
            {
                comboBox1.Items.Clear();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Login FROM Users;")
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string res1 = reader.GetString(0);
                    comboBox1.Items.Add(res1);
                }
                reader.Close();

                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Login FROM Worker;")
                };
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    string res1 = reader1.GetString(0);
                    comboBox1.Items.Add(res1);
                }
                reader1.Close();
            }
            catch (Exception)
            {
            }
        }

        private void CheckBox3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)/////login button
        {
            if (comboBox1.Text != "" && comboBox1.Text != " " && textBox2.Text != "" && textBox2.Text != " ")
            {
                string Login = comboBox1.Text;
                string password = textBox2.Text;

                bool flag = false;
                foreach (string item in comboBox1.Items)
                {
                    if (Login == item)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == true)
                {
                    if (!SearchPass(Login, password, true))
                    {
                        if (SearchPass(Login, password, false))///// worker redirect
                        {
                            conn.Close();
                            MessageBox.Show("Авторизація успішна!Вітаю Вас працівник " + Login + "!");
                            Worker.WorkerRoom work = new Worker.WorkerRoom(Login);
                            work.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Помилка авторизації, будь ласка перевірте корректність введення інформації!");
                        }
                    }
                    else
                    {
                        if (Login.Contains("Admin"))////////admin redirect
                        {
                            conn.Close();
                            MessageBox.Show("Авторизація успішна!Вітаю Вас адміністратр :)");
                            Admin.AdminRoom adm = new Admin.AdminRoom(Login);
                            adm.Show();
                            this.Hide();
                        }
                        else ///// user redirect
                        {
                            conn.Close();
                            MessageBox.Show("Авторизація успішна!Вітаю Вас користувач " + Login +"!");
                            User.UserRoom user = new User.UserRoom(Login); 
                            user.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Даний користувач не зареєстрований в системі!");
                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля!");
            }
        }

        public bool SearchPass(string Login, string password, bool flag)
        {
            CryptoHash cr = new CryptoHash();
            password = cr.Crypto(Login, password);
            int result = 0;
            try
            {
                string command = "";
                if (flag)
                {
                     command = "Select COUNT(User_id) FROM Users Where Login = '" + Login + "' and password = '" + password + "';";

                }
                else
                {
                    command = "Select COUNT(Worker_id) FROM Worker Where Login = '" + Login + "' and password = '" + password + "';";
                }

                MySqlCommand cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format(command)
                };
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception)
            {
            }
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                conn.Close();
                UserRegister register = new UserRegister();
                register.Show();
                this.Hide();
            }
        }

        private void LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
