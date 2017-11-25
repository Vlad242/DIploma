using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIploma_repair.Admin
{
    public partial class AdminRoom : Form
    {
        private string Login;

        public AdminRoom(string login)
        {
            InitializeComponent();
            this.Login = login;
        }
    }
}
