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
    public partial class SingleCliente : Form
    {
        SaveAction currentAction = SaveAction.NuevaAccion;
        GrullonRCEntities _db = new GrullonRCEntities();
        Models.Clientes currentClient = new Models.Clientes();

        public void TypeInit()
        {
            BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : (currentAction == SaveAction.Editar) ? Resources.Colors.Warning : Resources.Colors.Info;
            button1.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : (currentAction == SaveAction.Editar) ? Resources.Colors.Warning : Resources.Colors.Info;
            button1.ForeColor = Color.White;
            button2.BackColor = Resources.Colors.Secundary;
            button2.ForeColor = Color.White;
            panel2.BackColor = (currentAction == SaveAction.Agregar) ? Resources.Colors.Success : (currentAction == SaveAction.Editar) ? Resources.Colors.Warning : Resources.Colors.Info;
        }

        public SingleCliente(SaveAction action)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();
        }

        public SingleCliente(SaveAction action, Models.Clientes cliente)
        {
            InitializeComponent();
            currentAction = action;
            TypeInit();
            currentClient = cliente;

            textBox1.Text = currentClient.Nombre;
            textBox3.Text = currentClient.Cedula;
            comboBox1.Text = (currentClient.TipoPersona == 1) ? "Persona Física" : "Persona Jurídica";
            textBox4.Text = currentClient.NoTarjeta;
            textBox5.Text = currentClient.LimiteCredito.ToString();
            checkBox1.Checked = currentClient.Estado;
            textBox1.Size = new Size(508, 25);

            if (currentAction == SaveAction.Ver)
            {
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                comboBox1.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                checkBox1.Enabled = false;
                button1.Enabled = false;
                button2.Text = "Salir";
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

            if (currentAction == SaveAction.Editar) { Edit(); }

            AppData.ViewsRepository.ClientesView.GetData();
            Close();
        }
        
        private void Edit()
        {
            try
            {
                var _currentClient = _db.Clientes.FirstOrDefault(x => x.Id == currentClient.Id);
                _currentClient.Nombre = textBox1.Text;
                _currentClient.Cedula = textBox3.Text;
                _currentClient.TipoPersona = (comboBox1.SelectedItem.ToString() == "Persona Física") ? 1 : 2;
                _currentClient.NoTarjeta = (textBox4.Text.Length > 16) ? textBox4.Text.Remove(16) : textBox4.Text;
                _currentClient.LimiteCredito = decimal.Parse(textBox5.Text);
                _currentClient.Deleted = false;
                _currentClient.Estado = checkBox1.Checked;

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Edit()
        {
            try
            {
                var cliente = _db.Clientes.FirstOrDefault(x => x.Id == currentClient.Id);

                cliente.Nombre = textBox1.Text + " " + textBox2.Text;
                cliente.Cedula = textBox3.Text;
                cliente.TipoPersona = (comboBox1.SelectedItem.ToString() == "Persona Física") ? 1 : 2;
                cliente.NoTarjeta = (textBox4.Text.Length > 16) ? textBox4.Text.Remove(16) : textBox1.Text;
                cliente.LimiteCredito = decimal.Parse(textBox5.Text);
                cliente.Deleted = false;
                cliente.Estado = checkBox1.Checked;               

                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Add()
        {
            try
            {
                _db.Clientes.Add(new Models.Clientes
                {
                    Nombre = textBox1.Text + " " + textBox2.Text,
                    Cedula = textBox3.Text,
                    TipoPersona = (comboBox1.SelectedItem.ToString() == "Persona Física") ? 1 : 2,
                    NoTarjeta = (textBox4.Text.Length > 16) ? textBox4.Text.Remove(16) : textBox4.Text,
                    LimiteCredito = decimal.Parse(textBox5.Text),
                    Deleted = false,
                    Estado = checkBox1.Checked
                });

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: no se pudieron guardar los datos. Verifique que estén en el formato correcto", "Sistema de Gestión de Rent A Car", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
