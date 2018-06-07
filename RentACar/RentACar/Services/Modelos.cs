using RentACar.Enums;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services
{
    class ModelosService
    {
        GrullonRCEntities ctx = new GrullonRCEntities();
        public dynamic GetTipos(string search)
        {
            var li = ctx.Modelos
                  .Where(x => !x.Deleted && ((search == null || search == "") || (x.Marcas.Descripcion.Contains(search)) ||
                   (x.Descripcion.Contains(search))))
                  .ToList()
                  .Select(x => new
                  {
                      x.Id,
                      Marca = x.Marcas.Descripcion,
                      Modelo = x.Descripcion,
                      Estado = (x.Estado) ? "Activo" : "Inactivo"
                  }).ToList();
            return li;

        }

        public Modelos GetSingleTipo(int id)
        {
            return ctx.Modelos.FirstOrDefault(v => v.Id == id);
        }

        public void Save(Modelos tipo, SaveAction accion)
        {
            if (accion == SaveAction.Agregar)
            {
                ctx.Modelos.Add(tipo);
            }

            if (accion == SaveAction.Editar)
            {
                var toUpdate = ctx.Modelos.FirstOrDefault(x => x.Id == tipo.Id);
                toUpdate.Descripcion = tipo.Descripcion;
                toUpdate.Estado = tipo.Estado;
                toUpdate.Marca = tipo.Marca;
            }

            if (accion == SaveAction.Eliminar)
            {
                var toDelete = ctx.Modelos.FirstOrDefault(x => x.Id == tipo.Id);
                toDelete.Deleted = true;
            }

            ctx.SaveChanges();
        }
    }
}
