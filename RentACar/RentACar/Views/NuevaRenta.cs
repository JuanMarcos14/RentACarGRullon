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
    public partial class NuevaRenta : Form
    {
        private Models.GrullonRCEntities _db = new Models.GrullonRCEntities();
        Models.Vehiculos currentVehiculo;
        Models.Clientes currentClient;
        Models.Inspecciones currentInspeccion;

        public NuevaRenta()
        {
            InitializeComponent();
            BackColor = Resources.Colors.Success;
            panel2.BackColor = BackColor;
            button1.BackColor = BackColor;
            button2.BackColor = Resources.Colors.Secundary;
            button1.ForeColor = Color.White;
            button2.ForeColor = Color.White;
        }

        public void SetCliente(Models.Clientes cliente)
        {
            currentClient = cliente;
            textBox1.Text = cliente.Nombre;
        }

        public void SetVehiculo(int id)
        {
            currentVehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == id);
            textBox2.Text = $"{currentVehiculo.Marcas.Descripcion} {currentVehiculo.Modelos.Descripcion} {currentVehiculo.NoPlaca}";
        }

        public void setInspeccion(Models.Inspecciones inspeccion)
        {
            currentInspeccion = inspeccion;
            textBox3.Text = "¡Inspección Lista!";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppData.ViewsRepository.ClientesView = new Clientes(true);
            AppData.ViewsRepository.ClientesView.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppData.ViewsRepository.VehiculosView = new Vehiculos(true);
            AppData.ViewsRepository.VehiculosView.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((currentInspeccion == null) || (MessageBox.Show("Ya tiene una inspección de renta asociada a esta transacción. ¿Desea remplazarla por una nueva?", "Sistema de Gestión de Rent A Car", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                if ((currentClient != null) && (currentVehiculo != null))
                {
                    Inspeccion form = new Inspeccion(currentClient, currentVehiculo, 1);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Debe haber seleccionado el cliente y el vehículo para realizar la inspección.", "Sistema de Gestión de Rent A Car", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var _cliente = (textBox1.Text == "");
            var _vehiculo = (textBox2.Text == "");
            var _inspeccion = (textBox3.Text == "");
            var _monto = (textBox4.Text == "");
            var _dias = (textBox5.Text == "");

            if (_cliente || _vehiculo || _inspeccion || _monto || _dias)
            {
                MessageBox.Show("Debe haber completado todos los datos requeridos para hacer la renta.", "Sistema de Gestión de Rent A Car", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var _renta = new Models.Rentas
                {
                    Cliente = currentClient.Id,
                    Empleado = AppData.CurrentSession.CurrentUser.Id,
                    Vehiculo = currentVehiculo.Id,
                    InspeccionRenta = currentInspeccion.Id,
                    FechaRenta = dateTimePicker1.Value,
                    CantidadDias = int.Parse(textBox5.Text),
                    MontoDiario = decimal.Parse(textBox4.Text),
                    Estado = 1,
                    Comentario = richTextBox1.Text
                };

                var vehiculo = _db.Vehiculos.FirstOrDefault(x => x.Id == currentVehiculo.Id);
                vehiculo.Estado = 2;

                _db.Rentas.Add(_renta);
                _db.SaveChanges();

                AppData.ViewsRepository.RentasView.GetData();
                Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!Resources.Validator.StringToDecimal(textBox4.Text))
            {
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!Resources.Validator.StringToInteger(textBox5.Text))
            {
                textBox5.Text = textBox5.Text.Remove((textBox5.Text.Length == 0) ? 0 : textBox5.Text.Length - 1);
            }
        }
    }

}
