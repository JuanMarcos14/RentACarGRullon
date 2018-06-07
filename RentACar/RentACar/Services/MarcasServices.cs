using RentACar.Enums;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public class MarcasServices
    {
        public List<Marcas> GetTipos(string search, bool combo = false)
        {
            using (var ctx = new GrullonRCEntities())
            {

                if (combo)
                {
                    ctx.Marcas
                      .Where(x => !x.Deleted && x.Estado)
                      .ToList();
                }

                return ctx.Marcas
                      .Where(x => !x.Deleted && ((search == null || search == "") ||
                       (x.Descripcion.Contains(search))))
                      .ToList();
            }
        }

        public Marcas GetSingleTipo(int id)
        {
            using (var ctx = new GrullonRCEntities())
            {
                return ctx.Marcas.FirstOrDefault(v => v.Id == id);
            }
        }

        public Marcas GetSingleTipo(string descripcion)
        {
            using (var ctx = new GrullonRCEntities())
            {
                return ctx.Marcas.FirstOrDefault(v => v.Descripcion == descripcion);
            }
        }

        public void Save(Marcas tipo, SaveAction accion)
        {
            using (var ctx = new GrullonRCEntities())
            {
                if (accion == SaveAction.Agregar)
                {
                    ctx.Marcas.Add(tipo);
                }

                if (accion == SaveAction.Editar)
                {
                    var toUpdate = ctx.Marcas.FirstOrDefault(x => x.Id == tipo.Id);
                    toUpdate.Descripcion = tipo.Descripcion;
                    toUpdate.Estado = tipo.Estado;
                }

                if (accion == SaveAction.Eliminar)
                {
                    var toDelete = ctx.Marcas.FirstOrDefault(x => x.Id == tipo.Id);
                    toDelete.Deleted = true;
                }

                ctx.SaveChanges();
            }
        }
    }
}
