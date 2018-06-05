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
    public partial class SingleTipoVehiculo : Form
    {
        SaveAction currentAction = SaveAction.NuevaAccion;
        private readonly Services.VehiculosTiposService _service = new Services.VehiculosTiposService();
        VehiculosTipos currentType; 
        public void TypeInit()
        {
            BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.ForeColor = Color.White;
            button2.BackColor = Resources.Colors.Secundary;
            button2.ForeColor = Color.White;
        }

        public SingleTipoVehiculo(SaveAction action)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();
            currentType = new VehiculosTipos();
        }

        public SingleTipoVehiculo(SaveAction action, VehiculosTipos tipo)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentType.Estado = checkBox1.Checked;
            currentType.Descripcion = textBox1.Text;

            _service.Save(currentType, currentAction);
            AppData.ViewsRepository.TiposVehiculosView.GetData();
            this.Hide();
        }
    }
}
