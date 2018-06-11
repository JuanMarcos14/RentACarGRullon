using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public class ReportesService
    {
        GrullonRCEntities _db = new GrullonRCEntities();

        public dynamic Comisiones(int periodo)
        {
            var hoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
            ///Hoy
            if (periodo == 1)
            {
            }

            //Esta Semana
            if (periodo == 2) { }

            //Este Mes
            if (periodo == 3) { }

            return null;
        }
    }
}
