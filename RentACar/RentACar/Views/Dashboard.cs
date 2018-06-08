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

        private void mantenimientoDeTiposDeCombustiblesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.TiposCombustiblesView = new TiposCombustibles();
            AppData.ViewsRepository.TiposCombustiblesView.Show();
        }

        private void mantenimientoDeMarcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.MarcasView = new Marcas();
            AppData.ViewsRepository.MarcasView.Show();
        }

        private void mantenimientoDeModelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.ModelosView = new Modelos();
            AppData.ViewsRepository.ModelosView.Show();
        }

        private void mantenimientoDeVehículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.VehiculosView = new Vehiculos();
            AppData.ViewsRepository.VehiculosView.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.ClientesView = new Clientes();
            AppData.ViewsRepository.ClientesView.Show();
        }
    }
}
