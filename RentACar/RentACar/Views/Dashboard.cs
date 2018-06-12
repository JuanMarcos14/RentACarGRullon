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

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.EmpleadosView = new Empleados();
            AppData.ViewsRepository.EmpleadosView.Show();
        }

        private void rentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.RentasView = new Rentas();
            AppData.ViewsRepository.RentasView.Show();
        }

        private void vehiculosAúnAlquiladosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var _db = new Models.GrullonRCEntities())
            {
                var model = _db.Rentas.Where(x => x.Estado == 1)
                           .Select(renta => new Models.Alquilados
                           {
                               Vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Marcas.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Modelos.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).NoPlaca,
                               Cliente = _db.Clientes.FirstOrDefault(x => x.Id == renta.Cliente).Nombre,
                               Empleado = _db.Empleados.FirstOrDefault(x => x.Id == renta.Empleado).Nombre,
                               FechaRenta = (DateTime) renta.FechaRenta
                           }).ToList();


                Informes form = new Informes(model);
                form.Show();
            }
        }

        private void hoyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var _db = new Models.GrullonRCEntities())
            {
                var date = DateTime.Now.AddDays(-1);
                var model = _db.Rentas
                           .Where(x => x.FechaRenta > date)
                           .Select(renta => new Models.Alquilados
                           {
                               Vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Marcas.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Modelos.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).NoPlaca,
                               Cliente = _db.Clientes.FirstOrDefault(x => x.Id == renta.Cliente).Nombre,
                               Empleado = _db.Empleados.FirstOrDefault(x => x.Id == renta.Empleado).Nombre,
                               FechaRenta = (DateTime)renta.FechaRenta
                           }).ToList();


                Informes form = new Informes(model);
                form.Show();
            }
        }

        private void estaSemanaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var _db = new Models.GrullonRCEntities())
            {
                var date = DateTime.Now.AddDays(-7);
                var model = _db.Rentas
                           .Where(x => x.FechaRenta > date)
                           .Select(renta => new Models.Alquilados
                           {
                               Vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Marcas.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Modelos.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).NoPlaca,
                               Cliente = _db.Clientes.FirstOrDefault(x => x.Id == renta.Cliente).Nombre,
                               Empleado = _db.Empleados.FirstOrDefault(x => x.Id == renta.Empleado).Nombre,
                               FechaRenta = (DateTime)renta.FechaRenta
                           }).ToList();


                Informes form = new Informes(model);
                form.Show();
            }
        }

        private void esteMesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var _db = new Models.GrullonRCEntities())
            {
                var date = DateTime.Now.AddMonths(-1);
                var model = _db.Rentas
                           .Where(x => x.FechaRenta > date)
                           .Select(renta => new Models.Alquilados
                           {
                               Vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Marcas.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).Modelos.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Id).NoPlaca,
                               Cliente = _db.Clientes.FirstOrDefault(x => x.Id == renta.Cliente).Nombre,
                               Empleado = _db.Empleados.FirstOrDefault(x => x.Id == renta.Empleado).Nombre,
                               FechaRenta = (DateTime)renta.FechaRenta
                           }).ToList();


                Informes form = new Informes(model);
                form.Show();
            }
        }
    }
}
