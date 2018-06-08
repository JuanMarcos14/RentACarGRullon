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
    public partial class SingleVehiculo : Form
    {
        SaveAction currentAction = SaveAction.NuevaAccion;
        GrullonRCEntities _db = new GrullonRCEntities();
        Models.Vehiculos currentType = new Models.Vehiculos();

        public void FillCombos()
        {
            comboBox1.DataSource = _db.VehiculosTipos.Where(x => !x.Deleted && x.Estado).Select(x => x.Descripcion).ToList();
            comboBox3.DataSource = _db.Modelos.Where(x => !x.Deleted && x.Estado).Select(x => x.Descripcion).ToList();
            comboBox4.DataSource = _db.CombustiblesTipos.Where(x => !x.Deleted && x.Estado).Select(x => x.Descripcion).ToList();
            comboBox2.DataSource = _db.Marcas.Where(x => !x.Deleted && x.Estado).Select(x => x.Descripcion).ToList();
            comboBox3.Enabled = false;
        }

        public SingleVehiculo(SaveAction action)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();
            FillCombos();
        }

        public SingleVehiculo(SaveAction action, int vehiculoId)
        {
            currentAction = action;
            InitializeComponent();
            TypeInit();
            FillCombos();
            var _vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == vehiculoId);

            if (comboBox1.FindStringExact(_vehiculo.VehiculosTipos.Descripcion) == -1)
            {
                comboBox1.Items.Add(_vehiculo.VehiculosTipos.Descripcion);
            }
            comboBox1.SelectedIndex = comboBox1.FindStringExact(_vehiculo.VehiculosTipos.Descripcion);

            if (comboBox2.FindStringExact(_vehiculo.Marcas.Descripcion) == -1)
            {
                comboBox2.Items.Add(_vehiculo.Marcas.Descripcion);
            }
            comboBox2.SelectedIndex = comboBox2.FindStringExact(_vehiculo.Marcas.Descripcion);

            if (comboBox3.FindStringExact(_vehiculo.Modelos.Descripcion) == -1)
            {
                comboBox3.Items.Add(_vehiculo.Modelos.Descripcion);
            }
            comboBox3.SelectedIndex = comboBox3.FindStringExact(_vehiculo.Modelos.Descripcion);

            if (comboBox4.FindStringExact(_vehiculo.CombustiblesTipos.Descripcion) == -1)
            {
                comboBox4.Items.Add(_vehiculo.CombustiblesTipos.Descripcion);
            }
            comboBox4.SelectedIndex = comboBox4.FindStringExact(_vehiculo.CombustiblesTipos.Descripcion);

            textBox1.Text = _vehiculo.NoPlaca;
            textBox2.Text = _vehiculo.NoMotor;
            textBox3.Text = _vehiculo.NoChasis;
        }

        public void TypeInit()
        {
            BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
            button1.ForeColor = Color.White;
            button2.BackColor = Resources.Colors.Secundary;
            button2.ForeColor = Color.White;
            panel2.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : Resources.Colors.Warning;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentAction == SaveAction.Agregar)
            {
                var aa = new Models.Vehiculos();
                aa.TipoVehiculo = (int)_db.VehiculosTipos.FirstOrDefault(x => x.Descripcion == comboBox1.SelectedItem.ToString())?.Id;
                aa.Marca = (int)_db.Marcas.FirstOrDefault(x => x.Descripcion == comboBox2.SelectedItem.ToString())?.Id;
                aa.Modelo = (int)_db.Modelos.FirstOrDefault(x => x.Descripcion == comboBox3.SelectedItem.ToString())?.Id;
                aa.TipoCombustible = (int)_db.CombustiblesTipos.FirstOrDefault(x => x.Descripcion == comboBox4.SelectedItem.ToString())?.Id;
                aa.NoChasis = textBox3.Text;
                aa.NoMotor = textBox2.Text;
                aa.NoPlaca = textBox1.Text;
                aa.Deleted = false;
                aa.Estado = 1;
                _db.Vehiculos.Add(aa);
            }

            else if (currentAction == SaveAction.Editar)
            {
                var toUpdate = _db.Vehiculos.FirstOrDefault(x => x.Id == currentType.Id);

                toUpdate.TipoVehiculo = (int)_db.VehiculosTipos.FirstOrDefault(x => x.Descripcion == comboBox1.Text)?.Id;
                toUpdate.Marca = (int)_db.Marcas.FirstOrDefault(x => x.Descripcion == comboBox2.Text)?.Id;
                toUpdate.Modelo = (int)_db.Modelos.FirstOrDefault(x => x.Descripcion == comboBox3.Text)?.Id;
                toUpdate.TipoCombustible = (int)_db.CombustiblesTipos.FirstOrDefault(x => x.Descripcion == comboBox1.Text)?.Id;
                toUpdate.NoChasis = textBox3.Text;
                toUpdate.NoMotor = textBox2.Text;
                toUpdate.NoPlaca = textBox1.Text;
            }

            _db.SaveChanges();
            AppData.ViewsRepository.VehiculosView.GetData();
            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                comboBox3.Enabled = false;
                var marcad = comboBox2.SelectedItem?.ToString();
                var marca = _db.Marcas.FirstOrDefault(y => y.Descripcion == marcad)?.Id;
                var Model = _db.Modelos.Where(x => !x.Deleted && x.Estado && x.Marca == marca).Select(x => x.Descripcion).ToList();
                comboBox3.DataSource = Model;

                if (Model.Count > 0)
                {
                    comboBox3.SelectedItem = comboBox3.Items[0];
                }

                comboBox3.Enabled = true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
