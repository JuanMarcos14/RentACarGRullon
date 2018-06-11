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
    public partial class Vehiculos : Form
    {
        GrullonRCEntities _db = new GrullonRCEntities();

        public void GetData()
        {
            var model = _db.Vehiculos.Where(x => !x.Deleted && (
                x.Marcas.Descripcion.Contains(textBox1.Text) ||
                x.Modelos.Descripcion.Contains(textBox1.Text)
            ))
                       .Select(v => new
                       {
                           v.Id,
                           Descripcion = v.Marcas.Descripcion + " " + v.Modelos.Descripcion + " " + v.NoPlaca,
                           v.NoMotor,
                           v.NoChasis,
                           Estado = (v.Estado == 1) ? "Disponible" : (v.Estado == 2) ? "Alquilado" : "Desactivado",
                       }).ToList();

            dataGridView1.DataSource = model;
        }

        public Vehiculos()
        {
            InitializeComponent();
            GetData();
        }

        private void nuevoTipoDeVehículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingleVehiculo form = new SingleVehiculo(Enums.SaveAction.Agregar);
            form.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            SingleVehiculo form = new SingleVehiculo(Enums.SaveAction.Editar, id);
            form.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar el registro?", "Sistema de Gestión de Rent A Car", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                var toDelete = _db.Vehiculos.FirstOrDefault(x => x.Id == id);
                _db.Vehiculos.Remove(toDelete);
                _db.SaveChanges();
                GetData();
            }
        }
    }
}
