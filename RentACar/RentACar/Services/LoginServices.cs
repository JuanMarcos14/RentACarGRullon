using Siuben.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services
{
    class LoginServices
    {
        public bool Login(string username, string password)
        {
            try
            {
                Models.RentACarDBDataSet.UsuariosDataTable Usuarios = new Models.RentACarDBDataSet.UsuariosDataTable();
                return Usuarios.Any(x => x.User == username && x.Password == password);
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
            }
            return false;
        }
    }
}
