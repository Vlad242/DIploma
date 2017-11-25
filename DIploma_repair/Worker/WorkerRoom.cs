using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIploma_repair.Worker
{
    public partial class WorkerRoom : Form
    {
        private string Login;

        public WorkerRoom(string Login)
        {
            InitializeComponent();
            this.Login = Login;
        }
    }
}
