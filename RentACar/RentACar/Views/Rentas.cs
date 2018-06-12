using RentACar.Models;
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
    public partial class Rentas : Form
    {
        GrullonRCEntities _db = new GrullonRCEntities();

        public void GetData()
        {
            if (!checkBox1.Checked)
            {
                dataGridView1.DataSource = _db.Rentas
                                      .Where(x => ((textBox1.Text == "") ||
                                      (_db.Empleados.FirstOrDefault(y => y.Id == x.Empleado).Nombre.Contains(textBox1.Text)
                                      || (_db.Clientes.FirstOrDefault(y => y.Id == x.Cliente).Nombre.Contains(textBox1.Text)))))
                                      .Select(r => new
                                      {
                                          r.Id,
                                          Cliente = _db.Clientes.FirstOrDefault(x => x.Id == r.Cliente).Nombre,
                                          Empleado = _db.Empleados.FirstOrDefault(x => x.Id == r.Empleado).Nombre,
                                          Vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == r.Vehiculo).Marcas.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == r.Vehiculo).Modelos.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == r.Vehiculo).NoPlaca,
                                          Dias = r.CantidadDias,
                                          r.MontoDiario,
                                          Total = r.MontoDiario * r.CantidadDias,
                                          Estado = (r.Estado == 1) ? "En Proceso" : "Finalizada"
                                      }).ToList();
            }
            else
            {
                dataGridView1.DataSource = _db.Rentas
                                      .Where(x => (x.Empleado == _db.Empleados.FirstOrDefault(z => z.Id == AppData.CurrentSession.CurrentUser.Id).Id) && ((textBox1.Text == "") ||
                                      (_db.Empleados.FirstOrDefault(y => y.Id == x.Empleado).Nombre.Contains(textBox1.Text)
                                      || (_db.Clientes.FirstOrDefault(y => y.Id == x.Cliente).Nombre.Contains(textBox1.Text)))))
                                      .Select(r => new
                                      {
                                          r.Id,
                                          Cliente = _db.Clientes.FirstOrDefault(x => x.Id == r.Cliente).Nombre,
                                          Empleado = _db.Empleados.FirstOrDefault(x => x.Id == r.Empleado).Nombre,
                                          Vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == r.Vehiculo).Marcas.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == r.Vehiculo).Modelos.Descripcion + " " + _db.Vehiculos.FirstOrDefault(x => x.Id == r.Vehiculo).NoPlaca,
                                          Dias = r.CantidadDias,
                                          r.MontoDiario,
                                          Total = r.MontoDiario * r.CantidadDias,
                                          Estado = (r.Estado == 1) ? "En Proceso" : "Finalizada"
                                      }).ToList();
            }
        }

        public Rentas()
        {
            InitializeComponent();
            panel1.BackColor = Resources.Colors.Primary;
            GetData();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void nuevoTipoDeVehículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppData.ViewsRepository.NuevaRentaView = new NuevaRenta();
            AppData.ViewsRepository.NuevaRentaView.Show();
        }

        private void finalizarRentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            var renta = _db.Rentas.FirstOrDefault(x => x.Id == id);
            renta.FechaDevolucion = DateTime.Now;
            renta.Estado = 2;

            var vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == renta.Vehiculo);
            vehiculo.Estado = 1;


            _db.SaveChanges();

            GetData();
        }
    }
}
