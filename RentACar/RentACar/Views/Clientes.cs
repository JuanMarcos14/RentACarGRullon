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
    public partial class Clientes : Form
    {
        Models.GrullonRCEntities _db = new Models.GrullonRCEntities();
        bool FromRenta;
        public void GetData()
        {
            dataGridView1.DataSource = null;
            var Model = _db.Clientes.Where(x => !x.Deleted && ((textBox1.Text == "" || textBox1.Text == null
            || x.Cedula.Contains(textBox1.Text) || x.Nombre.Contains(textBox1.Text)))).ToList();

            dataGridView1.DataSource = Model.Select(x => new {
                x.Id,
                x.Nombre,
                TipoPersona = (x.TipoPersona == 1) ? "Física" : "Jurídica",
                x.Cedula,
                Estado = (x.Estado) ? "Activo" : "Inactivo"                
            }).ToList();
        }

        public Clientes(bool fromRenta = false)
        {
            FromRenta = fromRenta;
            InitializeComponent();
            panel1.BackColor = Resources.Colors.Primary;
            GetData();
        }

        private void nuevoTipoDeVehículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingleCliente form = new SingleCliente(SaveAction.Agregar);
            form.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            SingleCliente form = new SingleCliente(SaveAction.Ver, _db.Clientes.FirstOrDefault(c => c.Id == id));
            form.Show();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            SingleCliente form = new SingleCliente(SaveAction.Editar, _db.Clientes.FirstOrDefault(c => c.Id == id));
            form.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
    }
}
