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
        }

        public void SetCliente(Models.Clientes cliente)
        {
            currentClient = cliente;
            textBox1.Text = cliente.Nombre;
        }

        public void SetCliente(Models.Vehiculos vehiculo)
        {
            currentVehiculo = vehiculo;
            textBox2.Text = $"{vehiculo.Marca} {vehiculo.Modelo} {vehiculo.NoPlaca}";
        }

        public void setInspeccion(Models.Inspecciones inspeccion)
        {
            currentInspeccion = inspeccion;
            textBox3.Text = "¡Inspección Lista!";
        }
    }
}
