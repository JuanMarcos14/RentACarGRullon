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
    public partial class SingleEmpleado : Form
    {
        SaveAction currentAction = SaveAction.NuevaAccion;
        GrullonRCEntities _db = new GrullonRCEntities();
        Models.Empleados currentClient = new Models.Empleados();

        public void TypeInit()
        {
            BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : (currentAction == SaveAction.Editar) ? Resources.Colors.Warning : Resources.Colors.Info;
            button1.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : (currentAction == SaveAction.Editar) ? Resources.Colors.Warning : Resources.Colors.Info;
            button1.ForeColor = Color.White;
            button2.BackColor = Resources.Colors.Secundary;
            button2.ForeColor = Color.White;
            panel2.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : (currentAction == SaveAction.Editar) ? Resources.Colors.Warning : Resources.Colors.Info;
        }

        /// <summary>
        /// Método para instanciar un objeto de tipo SingleEmpleado cuando se va a agregar un nuevo empleado.
        /// </summary>
        /// <param name="action">Acción ha realizar</param>
        public SingleEmpleado(SaveAction action)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();
        }

        public SingleEmpleado(SaveAction action, Models.Empleados empleado)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();
            currentClient = empleado;

            textBox1.Text = currentClient.Nombre;
            textBox3.Text = currentClient.Cedula;
            comboBox1.Text = (currentClient.TandaLabor == 1) ? "Matutina" : (currentClient.TandaLabor == 2) ? "Vespertina" : "Nocturna";
            textBox4.Text = currentClient.PorcientoComision.ToString();
            dateTimePicker1.Value = currentClient.FechaIngreso;
            checkBox1.Checked = currentClient.Estado;
            textBox1.Size = new Size(508, 25);
            
            if (action == SaveAction.Ver)
            {
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                comboBox1.Enabled = false;
                textBox4.Enabled = false;
                dateTimePicker1.Enabled = false;
                checkBox1.Enabled = false;
                button1.Enabled = false;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 11)
            {
                if (!Resources.Validator.ValidarCedula(textBox3.Text))
                {
                    textBox3.Text = "";
                    MessageBox.Show("La cédula introducida no es válida.", "Sistema de Gestión de Rent A Car", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (textBox3.Text.Length > 11)
            {
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentAction == SaveAction.Agregar) { Add(); }
            if (currentAction == SaveAction.Editar) { Edit(); }

            AppData.ViewsRepository.EmpleadosView.GetData();
            Close();
        }

        private void Edit()
        {
            try
            {
                var _currentEmployee = _db.Empleados.FirstOrDefault(e => e.Id == currentClient.Id);

                _currentEmployee.Nombre = textBox1.Text;
                _currentEmployee.Cedula = textBox3.Text;
                _currentEmployee.TandaLabor = getTanda(comboBox1.Text);
                _currentEmployee.PorcientoComision = decimal.Parse(textBox4.Text);
                _currentEmployee.FechaIngreso = dateTimePicker1.Value;
                _currentEmployee.Estado = checkBox1.Checked;

                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private void Add()
        {
            try
            {
                var _currentEmployee = new Models.Empleados();

                _currentEmployee.Nombre = $"{textBox1.Text} {textBox2.Text}";
                _currentEmployee.Cedula = textBox3.Text;
                _currentEmployee.TandaLabor = getTanda(comboBox1.Text);
                _currentEmployee.PorcientoComision = decimal.Parse(textBox4.Text);
                _currentEmployee.FechaIngreso = dateTimePicker1.Value;
                _currentEmployee.Estado = checkBox1.Checked;

                ///
                _currentEmployee.User = UserGenerator(textBox1.Text, textBox2.Text);
                _currentEmployee.Password = _currentEmployee.Cedula;
                _currentEmployee.CreatedAt = DateTime.Now;
                _currentEmployee.Deleted = false;
                ///

                _db.Empleados.Add(_currentEmployee);

                _db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UserGenerator(string nombre, string apellidos)
        {
            try
            {
                string user = nombre[0] + apellidos.Split(' ').FirstOrDefault();
                return user.ToLower();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getTanda(string tanda)
        {
            return (tanda == "Matutina") ? 1 : (tanda == "Vespertina") ? 2 : (tanda == "Noctura") ? 3 : -1;  
        }   
    }
}
