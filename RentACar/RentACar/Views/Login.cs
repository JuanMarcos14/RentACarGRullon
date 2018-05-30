using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar.Views
{
    public partial class Login : Form
    {
        private readonly Services.LoginServices _service = new Services.LoginServices();
        public Login()
        {
            InitializeComponent();
            button1.ForeColor = Resources.Colors.LightTextColor;
            button1.BackColor = Resources.Colors.Primary;
        }

        private void button1_Click(object sender, EventArgs e)
        {      
            if (_service.Login(textBox1.Text, textBox2.Text))
            {
                AppData.ViewsRepository.DashboardView = (AppData.ViewsRepository.DashboardView == null) ? new Dashboard() : AppData.ViewsRepository.DashboardView;
                this.Hide();
                AppData.ViewsRepository.DashboardView.Show();
            }
            else
            {
                MessageBox.Show("NO");
            }
        }
    }
}
