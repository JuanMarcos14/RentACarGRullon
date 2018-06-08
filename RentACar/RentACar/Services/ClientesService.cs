using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services
{
    class ClientesService
    {
        private GrullonRCEntities _db = new GrullonRCEntities();

        public Clientes GetSingleCliente(int id)
        {
            return _db.Clientes.FirstOrDefault(x => x.Id == id);
        }
    }
}
