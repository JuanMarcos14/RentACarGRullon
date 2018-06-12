using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class Alquilados
    {
        public string Vehiculo { get; set; }
        public string Empleado { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaRenta { get; set; }
    }
}
