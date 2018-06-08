using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Views;

namespace RentACar.AppData
{
    class ViewsRepository
    {
        public static Login LoginView { get; set; }
        public static Dashboard DashboardView { get; set; }
        public static TiposVehiculos TiposVehiculosView { get; set; }
        public static TiposCombustibles TiposCombustiblesView { get; set; }
        public static Marcas MarcasView { get; set; }
        public static Modelos ModelosView { get; set; }
        public static Vehiculos VehiculosView { get; set; }
        public static Clientes ClientesView { get; set; }
    }
}
