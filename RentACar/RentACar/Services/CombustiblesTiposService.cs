using RentACar.Enums;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services
{
    class CombustiblesTiposService
    {

        public List<CombustiblesTipos> GetTipos(string search)
        {
            using (var ctx = new GrullonRCEntities())
            {
                return ctx.CombustiblesTipos
                      .Where(x => !x.Deleted && ((search == null || search == "") ||
                       (x.Descripcion.Contains(search))))
                      .ToList();
            }
        }

        public CombustiblesTipos GetSingleTipo(int id)
        {
            using (var ctx = new GrullonRCEntities())
            {
                return ctx.CombustiblesTipos.FirstOrDefault(v => v.Id == id);
            }
        }

        public void Save(CombustiblesTipos tipo, SaveAction accion)
        {
            using (var ctx = new GrullonRCEntities())
            {
                if (accion == SaveAction.Agregar)
                {
                    ctx.CombustiblesTipos.Add(tipo);
                }

                if (accion == SaveAction.Editar)
                {
                    var toUpdate = ctx.CombustiblesTipos.FirstOrDefault(x => x.Id == tipo.Id);
                    toUpdate.Descripcion = tipo.Descripcion;
                    toUpdate.Estado = tipo.Estado;
                }

                if (accion == SaveAction.Eliminar)
                {
                    var toDelete = ctx.CombustiblesTipos.FirstOrDefault(x => x.Id == tipo.Id);
                    toDelete.Deleted = true;
                }

                ctx.SaveChanges();
            }
        }
    }
}
