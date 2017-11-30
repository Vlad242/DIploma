using AForge.Video.DirectShow;
using AForge.Video;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZXing;
using System.Drawing;
using System.Media;

namespace DIploma_repair.Admin
{
    public partial class AddDetail : Form
    {
        private string Login;
        public MySqlConnection conn;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private ZXing.BarcodeReader reader;
        delegate void SetStringDelegate(string parameter);

        private List<int> detail = new List<int>();
        private List<int> model = new List<int>();
        private List<int> order = new List<int>();

        public AddDetail(string login)
        {
            InitializeComponent();
            Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.getConnectInfo());
            conn.Open();
        }

        private void AddDetail_Load(object sender, EventArgs e)
        {
            try
            {
                //////////////////////detail
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
                detail.Clear();
                MySqlCommand cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Detail_id, Detail_name FROM Detail;")
                };
                MySqlDataReader reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    detail.Add(reader2.GetInt32(0));
                    comboBox1.Items.Add(reader2.GetString(1));
                    comboBox3.Items.Add(reader2.GetString(1));
                }
                reader2.Close();
                ///////////////////////model
                comboBox2.Items.Clear();
                model.Clear();
                cmd1 = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = string.Format("SELECT Model_id, Model_name FROM Model;")
                };
                reader2 = cmd1.ExecuteReader();
                while (reader2.Read())
                {
                    model.Add(reader2.GetInt32(0));
                    comboBox2.Items.Add(reader2.GetString(1));
                }
                reader2.Close();

                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                reader = new ZXing.BarcodeReader();

                if (videoDevices.Count > 0)
                {
                    foreach (FilterInfo device in videoDevices)
                    {
                        comboBox4.Items.Add(device.Name);
                    }
                    comboBox4.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != " " && textBox2.Text != " " &&
                textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    string sql = "Insert into Detail (Detail_id, Detail_name, Prod_country, Price) values (null,'" +
                    textBox1.Text + "','" + textBox2.Text + "','" + numericUpDown1.Value + "');";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Деталь " + textBox1.Text + " додано!");
                    AddDetail_Load(null, null);
                }
                catch(Exception)
                {

                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля форми!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Insert into consist_of (Detail_id, Model_id) values (" +
               detail[comboBox1.SelectedIndex] + "," + model[comboBox2.SelectedIndex] + ");";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Прив'язку створено!");
            }
            catch (Exception)
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                reader = new ZXing.BarcodeReader();
                comboBox5.Enabled = false;
            }
            else
            {
                if (comboBox5.SelectedIndex > 0)
                {
                    reader.Options.PossibleFormats = new List<BarcodeFormat>
                     {
                             (BarcodeFormat)comboBox1.SelectedValue
                      };
                }
                comboBox5.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (videoSource == null)
            {
                videoSource = new VideoCaptureDevice(videoDevices[comboBox4.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("You have runnable scanning proccess!");
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bitmap;
            ZXing.Result result = reader.Decode((Bitmap)eventArgs.Frame.Clone());
            if (result != null)
            {
                SetResult(result.Text);
            }
        }

        private void AddDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null)
            {
                videoSource.Stop();
            }
            AdminRoom room = new AdminRoom(Login);
            room.Show();
            this.Dispose();
        }

        void SetResult(string result)
        {
            if (!InvokeRequired)
            {
                textBox3.Text = result;
                SystemSounds.Beep.Play();
            }
            else
            {
                Invoke(new SetStringDelegate(SetResult), new object[] { result });
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                pictureBox1.Image = null;
                videoSource = null;
            }
            else
            {
                MessageBox.Show("Opened reader stream no detected!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox3.Text != "" && textBox3.Text != " ")
            {
                try
                {
                    string sql = "Insert into Detail_serial (D_serial_id, Detail_id) values ('" +
                      textBox3.Text + "'," + detail[comboBox3.SelectedIndex] + ");";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Деталь додано до бази даних!");
                    listBox1.Items.Add(comboBox3.SelectedItem.ToString() + " - " + textBox3.Text );
                    textBox3.Clear();
                    /////////////////////
                    int count = 0;
                    listBox2.Items.Clear();
                    MySqlCommand cmd1 = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = string.Format("SELECT Detail_order.D_Order_id, Detail.Detail_name, Detail_order.D_Count, Status.Status_name FROM Detail_order INNER JOIN Detail on(Detail_order.Detail_id=Detail.Detail_id) INNER JOIN Status on(Detail_order.Status_id=Status.Status_id) WHERE Status.Status_name = 'Purchase' and Detail_order.Detail_id=" + detail[comboBox3.SelectedIndex] + " limit 1;")
                    };
                    MySqlDataReader reader2 = cmd1.ExecuteReader();
                    while (reader2.Read())
                    {
                        order.Add(reader2.GetInt32(0));
                        count = reader2.GetInt32(2);
                        listBox2.Items.Add(reader2.GetString(1) + " - " + reader2.GetString(2) + "(" + reader2.GetString(3) + ")");
                    }
                    reader2.Close();
                    /////////////////////
                    count = --count;
                    if (count > 0)
                    {
                        sql = "UPDATE Detail_order SET D_Count= " + count + " WHERE D_order_id= " + order[0] + ";";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql = "UPDATE Detail_order SET Status_id = 2 WHERE D_order_id= " + order[0] + ";";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                        ///////////////////
                        listBox2.Items.Clear();
                        cmd1 = new MySqlCommand
                        {
                            Connection = conn,
                            CommandText = string.Format("SELECT Detail_order.D_Order_id, Detail.Detail_name, Detail_order.D_Count, Status.Status_name FROM Detail_order INNER JOIN Detail on(Detail_order.Detail_id=Detail.Detail_id) INNER JOIN Status on(Detail_order.Status_id=Status.Status_id) WHERE Status.Status_name = 'Purchase' and Detail_order.Detail_id=" + detail[comboBox3.SelectedIndex] + " limit 1;")
                        };
                        reader2 = cmd1.ExecuteReader();
                        while (reader2.Read())
                        {
                            listBox2.Items.Add(reader2.GetString(1) + " - " + reader2.GetString(2) + "(" + reader2.GetString(3) + ")");
                        }
                        reader2.Close();
                    }
                   
                }
                catch (Exception)
                {

                }
            }

        }
    }
}
