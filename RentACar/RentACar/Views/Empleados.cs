using RentACar.Enums;
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
    public partial class Empleados : Form
    {
        Models.GrullonRCEntities _db = new Models.GrullonRCEntities();

        public Empleados()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            dataGridView1.DataSource = null;
            var Model = _db.Empleados.Where(x => !x.Deleted && ((textBox1.Text == "" || textBox1.Text == null
            || x.Cedula.Contains(textBox1.Text) || x.Nombre.Contains(textBox1.Text)))).ToList();

            dataGridView1.DataSource = Model.Select(x => new
            {
                x.Id,
                x.Nombre,
                Usuario = x.User,           
                x.Cedula,                
                Estado = (x.Estado) ? "Activo" : "Inactivo"
            }).ToList();
        }

        private void nuevoTipoDeVehículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingleEmpleado singleEmpleado = new SingleEmpleado(Enums.SaveAction.Agregar);
            singleEmpleado.Show();
        }

        private void Empleados_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            SingleEmpleado form = new SingleEmpleado(SaveAction.Ver, _db.Empleados.FirstOrDefault(c => c.Id == id));
            form.Show();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            SingleEmpleado form = new SingleEmpleado(SaveAction.Editar, _db.Empleados.FirstOrDefault(c => c.Id == id));
            form.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            var _empleado = _db.Empleados.FirstOrDefault(c => c.Id == id);
            _empleado.Deleted = true;

            _db.SaveChanges();
            GetData();
        }
    }
}