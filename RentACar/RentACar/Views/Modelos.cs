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
    public partial class Modelos : Form
    {
        private readonly Services.ModelosService _service = new Services.ModelosService();

        public Modelos()
        {
            InitializeComponent();
            panel1.BackColor = Resources.Colors.Primary;
            GetData();
        }

        public void GetData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _service.GetTipos(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void nuevoTipoDeVehículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SingleModelo(SaveAction.Agregar);
            form.Show();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            SingleModelo form = new SingleModelo(SaveAction.Editar, _service.GetSingleTipo(id));
            form.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar el registro?", "Sistema de Gestión de Rent A Car", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                _service.Save(_service.GetSingleTipo(id), SaveAction.Eliminar);
                GetData();
            }
        }
    }
}
