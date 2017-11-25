using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DIploma_repair.LogIn
{
    public partial class LogIn : Form
    {
        private InternetConection internetConection = new InternetConection();

        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, System.EventArgs e)
        {
            switch (internetConection.GetInetStaus())
            {
                case "Без доступу до інтернету":
                    {
                        label1.Text = "Без доступу до інтернету";
                        label1.ForeColor = Color.Red;
                        pictureBox1.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\DebugImage\\disconnect.png");
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        button2.Enabled = false;
                        MessageBox.Show("Please check internet connection and restart application!");
                        break;
                    }
                case "Обмежений доступ":
                    {
                        label1.Text = "Обмежений доступ";
                        label1.ForeColor = Color.Orange;
                        pictureBox1.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\Image\\disconnect.png");
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        button2.Enabled = false;
                        MessageBox.Show("Please check internet connection and restart application!");
                        break;
                    }
                case "Доступ до інтернету":
                    {
                        label1.Text = "Доступ до інтернету";
                        label1.ForeColor = Color.Green;
                        pictureBox1.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\DebugImage\\connect.png");
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        break;
                    }
            }
        }
    }
}
