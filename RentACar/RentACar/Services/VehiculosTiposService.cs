using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RentACar.Enums;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services
{
    class VehiculosTiposService
    {
        public List<VehiculosTipos> GetTipos(string search)
        {
            using (var ctx = new GrullonRCEntities())
            {
                return ctx.VehiculosTipos
                      .Where(x => !x.Deleted && ((search == null || search == "") ||
                       (x.Descripcion.Contains(search))))
                      .ToList();
            }
        }

        public VehiculosTipos GetSingleTipo(int id)
        {
            using (var ctx = new GrullonRCEntities())
            {
                return ctx.VehiculosTipos.FirstOrDefault(v => v.Id == id);
            }
        }

        public void Save(VehiculosTipos tipo, SaveAction accion)
        {
            using (var ctx = new GrullonRCEntities())
            {
                if (accion == SaveAction.Agregar)
                {
                    ctx.VehiculosTipos.Add(tipo);
                }

                if (accion == SaveAction.Editar)
                {
                    var toUpdate = ctx.VehiculosTipos.FirstOrDefault(x => x.Id == tipo.Id);
                    toUpdate.Descripcion = tipo.Descripcion;
                    toUpdate.Estado = tipo.Estado;
                }

                if (accion == SaveAction.Eliminar)
                {
                    var toDelete = ctx.VehiculosTipos.FirstOrDefault(x => x.Id == tipo.Id);
                    toDelete.Deleted = true;
                }

                ctx.SaveChanges();
            }
        }
    }
}
