using RentACar.Enums;
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
    public partial class SingleMarca : Form
    {
        SaveAction currentAction = SaveAction.NuevaAccion;
        private readonly Services.MarcasServices _service = new Services.MarcasServices();
        Models.Marcas currentType;

        public SingleMarca(SaveAction action)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();
            currentType = new Models.Marcas();
        }

        public SingleMarca(SaveAction action, Models.Marcas tipo)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();

            //Codigo de asignación
            textBox1.Text = tipo.Descripcion;
            checkBox1.Checked = tipo.Estado;
            //

            currentType = tipo;
        }

        public void TypeInit()
        {
            BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.ForeColor = Color.White;
            button2.BackColor = Resources.Colors.Secundary;
            button2.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentType.Estado = checkBox1.Checked;
            currentType.Descripcion = textBox1.Text;

            _service.Save(currentType, currentAction);
            AppData.ViewsRepository.MarcasView.GetData();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
