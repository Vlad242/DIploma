using CryptoMD5_XOR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DIploma_repair.LogIn
{
    public partial class UserRegister : Form
    {
        private string ConfirmPasswordGlobal = "masterkey";
        private bool ConfirmedAdmin = false;
        private bool ConfirmedLogin = false;
        private bool ConfirmedPassword = false;
        private bool ConfirmedEmail = false;

        private int UserType = 5;
        public MySqlConnection conn;

        public List<string> Users = new List<string>();
        public List<string> Office_names = new List<string>();
        public List<int> Office_ids = new List<int>();

        public UserRegister()
        {
            InitializeComponent();
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            ConfirmedAdmin = false;
            ConfirmedLogin = false;
            ConfirmedPassword = false;
            ConfirmedEmail = false;

            label12.Text = "#";
            label13.Text = "#";
            label14.Text = "#";
            label15.Text = "#";
            label12.ForeColor = Color.Black;
            label13.ForeColor = Color.Black;
            label14.ForeColor = Color.Black;
            label15.ForeColor = Color.Black;

            switch (comboBox1.Text)
            {
                case "Admin":
                    {
                        if (!ConfirmedAdmin)
                        {
                            this.ConfirmedAdmin = false;
                            groupBoxUser.Visible = true;
                            groupBoxWorker.Visible = false;
                            button1.Enabled = false;
                            textBox8.Visible = true;
                            groupBoxUser.Visible = true;
                            label18.Visible = true;
                            label18.Text = "CONFIRM!!!";
                            label18.ForeColor = Color.Red;
                            UserType = 0;
                        }
                        break;
                    }
                case "User":
                    {
                        button1.Enabled = true;
                        textBox8.Visible = false;
                        label18.Visible = false;
                        groupBoxUser.Visible = true;
                        groupBoxWorker.Visible = false;
                        UserType = 1;
                        break;
                    }
                case "Worker":
                    {
                        button1.Enabled = true;
                        textBox8.Visible = false;
                        label18.Visible = false;
                        groupBoxWorker.Visible = true;
                        groupBoxWorker.Enabled = true;
                        groupBoxUser.Visible = false;
                        UserType = 2;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void UserRegister_Load(object sender, EventArgs e)
        {
            textBox8.Visible = false;
            label18.Visible = false;
            groupBoxUser.Visible = false;
            groupBoxWorker.Visible = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy:MM:dd";

            try
            {
                ////////////Users
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Login FROM Users;")
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string res = reader.GetString(0);
                    Users.Add(res);
                }
                reader.Close();
                ///////////Workers
                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Login FROM Worker;")
                };
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    string res = reader1.GetString(0);
                    Users.Add(res);
                }
                reader1.Close();

                ///////////Offices(id, name)
                MySqlCommand cmd2 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Office_id, Office_name FROM Office;")
                };
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    Office_ids.Add(reader2.GetInt32(0));
                    Office_names.Add(reader2.GetString(1));
                }
                reader2.Close();

                foreach(var item in Office_names)
                {
                    comboBox3.Items.Add(item);
                }
                /////////////////Offices.end
            }
            catch (Exception)
            {

            }
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            if(textBox8.Text != "")
            {
                if (textBox8.Text == ConfirmPasswordGlobal)
                {
                    button1.Enabled = true;
                    textBox8.Visible = false;
                    label18.Visible = true;
                    label18.Text = "CONFIRMED!!!";
                    label18.ForeColor = Color.Green;
                    textBox8.Enabled = false;
                    textBox8.Visible = false;
                    this.ConfirmedAdmin = true;
                    textBox8.Clear();
                }
                else
                {
                    button1.Enabled = false;
                    textBox8.Visible = true;
                    label18.Visible = true;
                    label18.Text = "CONFIRM!!!";
                    label18.ForeColor = Color.Red;
                }
            }   
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            bool flag = true;

            if (!ConfirmedAdmin)
            {
                foreach (var item in Users)
                {
                    if (textBox3.Text == item)
                    {
                        flag = false;
                    }
                }
                if (textBox3.Text.Contains("Admin"))
                {
                    flag = false;
                }

                if (flag)
                {
                    label12.Text = "Allowed!";
                    label12.ForeColor = Color.Green;
                    this.ConfirmedLogin = true;
                }
                else
                {
                    label12.Text = "Busy!";
                    label12.ForeColor = Color.Red;
                    this.ConfirmedLogin = false;
                }
            }
            else
            {
                if (textBox3.Text.Contains("Admin"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (flag)
                {
                    label12.Text = "Allowed!";
                    label12.ForeColor = Color.Green;
                    this.ConfirmedLogin = true;
                }
                else
                {
                    label12.Text = "Busy!";
                    label12.ForeColor = Color.Red;
                    this.ConfirmedLogin = false;
                }
            }
            
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.TextLength < 8)
            {
                label14.Text = "So short!";
                label14.ForeColor = Color.Red;
            }
            else
            {
                label14.Text = "Allowed!";
                label14.ForeColor = Color.Green;
            }
            if (textBox6.Text == " ")
            {
                label13.Text = "Wrong!";
                label13.ForeColor = Color.Red;
            }
            else
            {

            }
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            if(textBox5.Text == textBox6.Text && textBox6.TextLength > 7)
            {
                label13.Text = "Confirm!";
                label13.ForeColor = Color.Green;
                this.ConfirmedPassword = true;
            }
            else
            {
                label13.Text = "Wrong!";
                label13.ForeColor = Color.Red;
                this.ConfirmedPassword = false;
            }
        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text.Contains("@"))
            {
                label15.Text = "Allowed!";
                label15.ForeColor = Color.Green;
                this.ConfirmedEmail = true;
            }
            else
            {
                label15.Text = "Wrong email!";
                label15.ForeColor = Color.Red;
                this.ConfirmedEmail = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            switch (UserType)
            {
                case 0://////ADMIN
                    {
                        if (ConfirmedAdmin)
                        {
                            if (ConfirmedLogin)
                            {
                                if (ConfirmedPassword)
                                {
                                    if (textBox1.Text != " " && textBox2.Text != " " &&
                                    textBox4.Text != " " && dateTimePicker1.Text != " " &&
                                    textBox7.Text != " " && textBox5.Text != " " && textBox3.Text != " " &&
                                     textBox9.Text != " " && textBox9.Text != " "
                                     &&
                                     textBox1.Text != "" && textBox2.Text != "" &&
                                    textBox4.Text != "" && dateTimePicker1.Text != "" &&
                                    textBox7.Text != "" && textBox5.Text != "" && textBox3.Text != "" &&
                                     textBox9.Text != "" && textBox9.Text != ""
                                     )
                                    {
                                        try
                                        {
                                            CryptoHash cr = new CryptoHash();
                                            string pass = cr.Crypto(textBox3.Text, textBox6.Text);
                                            /////////////////////Password

                                            string sql = "insert into Users(User_id, User_name, User_surname," +
                                                " User_fname, Birthdate, Phone, Login, Password, Adress, Email) values (null, '" +

                                              textBox1.Text + "', '" + textBox2.Text + "', '"
                                                + textBox4.Text + "', '" + dateTimePicker1.Text + "', '"
                                                + textBox7.Text + "', '" + textBox3.Text + "', '"
                                                + pass + "', '" + textBox9.Text + "', '" + textBox10.Text + "')";
                                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                                            cmd.ExecuteNonQuery();
                                            MessageBox.Show("Registration was successful!");
                                            conn.Close();
                                            LogIn lg = new LogIn();
                                            lg.Show();
                                            this.Close();
                                        }
                                        catch (Exception)
                                        {
                                            MessageBox.Show("Something wrong!");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Not all fields are full! Please check correct fields!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("PASSWORD INCORRECT!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("LOGIN INCORRECT!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ADMIN NOT CONFIRMED S PASSWORD!");
                        }
                        break;
                    }
                case 1://////USER
                    {
                        if (ConfirmedLogin)
                        {
                            if (ConfirmedPassword)
                            {
                                if (ConfirmedEmail)
                                {
                                    if (textBox1.Text != " " && textBox2.Text != " " &&
                                    textBox4.Text != " " && dateTimePicker1.Text != " " &&
                                    textBox7.Text != " " && textBox5.Text != " " && textBox3.Text != " " &&
                                     textBox9.Text != " " && textBox9.Text != " "
                                     &&
                                     textBox1.Text != "" && textBox2.Text != "" &&
                                    textBox4.Text != "" && dateTimePicker1.Text != "" &&
                                    textBox7.Text != "" && textBox5.Text != "" && textBox3.Text != "" &&
                                     textBox9.Text != "" && textBox9.Text != ""
                                     )
                                    {
                                        try
                                        {
                                            CryptoHash cr = new CryptoHash();
                                            string pass = cr.Crypto(textBox3.Text, textBox6.Text);
                                            /////////////////////Password

                                            string sql = "insert into Users(User_id, User_name, User_surname," +
                                                " User_fname, Birthdate, Phone, Login, Password, Adress, Email) values (null, '" +

                                              textBox1.Text + "', '" + textBox2.Text + "', '"
                                                + textBox4.Text + "', '" + dateTimePicker1.Text + "', '"
                                                + textBox7.Text + "', '" + textBox3.Text + "', '"
                                                + pass + "', '" + textBox9.Text + "', '" + textBox10.Text + "')";
                                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                                            cmd.ExecuteNonQuery();
                                            MessageBox.Show("Registration was successful!");
                                            conn.Close();
                                            LogIn lg = new LogIn();
                                            lg.Show();
                                            this.Close();
                                        }
                                        catch (Exception)
                                        {
                                            MessageBox.Show("Something wrong!");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Not all fields are full! Please check correct fields!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("EMAIL INCORRECT!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("PASSWORD INCORRECT!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("LOGIN INCORRECT!");
                        }
                        break;
                    }
                case 2://////WORKER
                    {
                        if (ConfirmedLogin)
                        {
                            if (ConfirmedPassword)
                            {
                                if (textBox1.Text != " " && textBox2.Text != " " &&
                                    textBox4.Text != " " && dateTimePicker1.Text != " " &&
                                    textBox7.Text != " " && textBox5.Text != " " && textBox3.Text != " " &&
                                     textBox11.Text != " " && comboBox3.Text != " "
                                     &&
                                     textBox1.Text != "" && textBox2.Text != "" &&
                                    textBox4.Text != "" && dateTimePicker1.Text != "" &&
                                    textBox7.Text != "" && textBox5.Text != "" && textBox3.Text != "" &&
                                     textBox11.Text != "" && comboBox3.Text != ""
                                     )
                                {
                                    try
                                    {
                                        CryptoHash cr = new CryptoHash();
                                        string pass = cr.Crypto(textBox3.Text, textBox6.Text);
                                        /////////////////////Password
                                        int index = Office_ids[comboBox3.SelectedIndex];

                                        string sql = "insert into Worker(Worker_id, Worker_name, Worker_surname," +
                                            "Worker_fname, Birthdate, Phone, Login, Password, Worker_proffession, Office_id) values (null, '" +

                                          textBox1.Text + "', '" + textBox2.Text + "', '"
                                            + textBox4.Text + "', '" + dateTimePicker1.Text + "', '"
                                            + textBox7.Text + "', '" + textBox3.Text + "', '"
                                            + pass + "', '" + textBox11.Text + "', '" + index + "')";
                                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                                        cmd.ExecuteNonQuery();
                                        //INSERT INTO Orders VALUES (null, 1, 1, 5, 'AN55SLRT12FF', 2, 'TEST', '2017:01:01', 'NULL', 10, 0);
                                        MessageBox.Show("Registration was successful!");

                                        int worker_id = 0;
                                        cmd = new MySqlCommand
                                        {
                                            Connection = conn,
                                            CommandText = string.Format("SELECT MAX(Worker_id) FROM Worker;")
                                        };
                                        MySqlDataReader reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            worker_id = reader.GetInt32(0);
                                        }
                                        sql = "iINSERT INTO Orders(Order_id, User_id, Service_id, Worker_id, Serial_number" +
                                            "Status_id,Description,Order_Date, Complete_set, Appearance, Order_price) VALUES (null, 1, 1, " + worker_id + ", 'AN55SLRT12FF', 2, 'TEST', '2017:01:01', 'NULL', 10, 0);";

                                        cmd = new MySqlCommand(sql, conn);
                                        cmd.ExecuteNonQuery();

                                        reader.Close();
                                        conn.Close();
                                        LogIn lg = new LogIn();
                                        lg.Show();
                                        this.Close();
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("Something wrong!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Not all fields are full! Please check correct fields!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("PASSWORD INCORRECT!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("LOGIN INCORRECT!");
                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show("USER NOT SELECTED!");
                        break;
                    }
            }
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, "[^0-9]"))
            {
                textBox7.Text = textBox7.Text.Remove(textBox7.Text.Length - 1);
            }

        }

        private void UserRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            this.Dispose();
        }
    }
}
