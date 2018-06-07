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
    public partial class SingleModelo : Form
    {
        SaveAction currentAction = SaveAction.NuevaAccion;
        private readonly Services.ModelosService _service = new Services.ModelosService();
        private readonly Services.MarcasServices _marcasService = new Services.MarcasServices();
        Models.Modelos currentType;

        public void TypeInit()
        {
            BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.ForeColor = Color.White;
            button2.BackColor = Resources.Colors.Secundary;
            button2.ForeColor = Color.White;
        }

        public void FillMarcas()
        {
            comboBox1.DataSource = _marcasService.GetTipos("", true).Select(x => x.Descripcion).ToList();
        }

        public SingleModelo(SaveAction action)
        {
            InitializeComponent();
            currentAction = action;
            FillMarcas();
            TypeInit();
            currentType = new Models.Modelos();
        }

        public SingleModelo(SaveAction action, Models.Modelos tipo)
        {
            InitializeComponent();
            currentAction = action;
            FillMarcas();
            TypeInit();

            //Codigo de asignación
            textBox1.Text = tipo.Descripcion;
            checkBox1.Checked = tipo.Estado;
            if (comboBox1.FindStringExact(tipo.Marcas.Descripcion) == -1)
            {
                comboBox1.Items.Add(tipo.Marcas.Descripcion);
            }
            comboBox1.SelectedIndex = comboBox1.FindStringExact(tipo.Marcas.Descripcion);
            //

            currentType = tipo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentType.Descripcion = textBox1.Text;
            currentType.Marca = _marcasService.GetSingleTipo(comboBox1.Text).Id;
            currentType.Estado = checkBox1.Checked;

            _service.Save(currentType, currentAction);
            AppData.ViewsRepository.ModelosView.GetData();
            Close();
        }
    }
}
