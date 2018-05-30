using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Models;

namespace RentACar.Services
{
    class LoginServices
    {
        public bool Login(string username, string password)
        {
            using (var ctx = new GrullonRCEntities())
            {
                var empleado = ctx.Empleados.FirstOrDefault(user => user.User == username && user.Password == password);

                if (empleado != null)
                {
                    AppData.CurrentSession.CurrentUser = empleado;
                    return true;
                }

                return false;
            }
        }
    }
}
