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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = AppData.CurrentSession.CurrentUser.Nombre;
            panel1.BackColor = Resources.Colors.Danger;
        }

        private void mantenimientoDeTiposDeVehículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.TiposVehiculosView =  new TiposVehiculos();
            AppData.ViewsRepository.TiposVehiculosView.Show();
        }
    }
}
