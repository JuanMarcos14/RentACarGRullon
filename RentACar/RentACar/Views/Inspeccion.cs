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
    public partial class Inspeccion : Form
    {
        Models.Clientes _cliente;
        Models.Vehiculos _vehiculo;
        Models.GrullonRCEntities _db = new Models.GrullonRCEntities();
        int _tipo = 0;

        public Inspeccion(Models.Clientes cliente, Models.Vehiculos vehiculo, int tipo)
        {
            InitializeComponent();
            BackColor = Resources.Colors.Info;
            panel2.BackColor = BackColor;
            button1.BackColor = BackColor;
            button1.ForeColor = Color.White;
            button2.BackColor = Resources.Colors.Secundary;
            button2.ForeColor = Color.White;
            _tipo = tipo;
            _vehiculo = vehiculo;
            _cliente = cliente;

            textBox1.Text = _cliente.Nombre;
            textBox2.Text = $"{_vehiculo.NoPlaca}";
        }

        private static int NivelGasolina(string nivel)
        {
            switch (nivel)
            {
                case "1/4 tanque":
                    return 1;

                case "1/2 tanque":
                    return 2;

                case "3/4 tanque":
                    return 3;

                case "Tanque lleno":
                    return 4;

                default:
                    return 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var _inspeccion = new Models.Inspecciones
            {
                Vehiculo = _vehiculo.Id,
                Cliente = _cliente.Id,
                Empleado = AppData.CurrentSession.CurrentUser.Id,
                Ralladuras = chbRalladuras.Checked,
                NivelCombustible = NivelGasolina(comboBox1.Text),
                GomaRespuesta = chbRepuesta.Checked,
                Gato = chbGato.Checked,
                RoturasCristal = chbCrsital.Checked,
                Fecha = DateTime.Now,
                TipoInspeccion = _tipo,
                GomaDelanteraDerecha = chbDD.Checked,
                GomaDelanteraIzquierda = chbDI.Checked,
                GomaTraseraDerecha = chbTD.Checked,
                GomaTraseraIzquierda = chbTI.Checked,
            };

            _db.Inspecciones.Add(_inspeccion);

            _db.SaveChanges();

            if (_tipo == 1)
            {
                AppData.ViewsRepository.NuevaRentaView.setInspeccion(_inspeccion);
                Close();
            }
        }
    }
}
