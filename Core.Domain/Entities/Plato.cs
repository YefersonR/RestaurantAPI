using Core.Domain.Commons;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Plato : AuditableBase
    {
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int CantidadPersonas { get; set; }
        public List<Ingrediente> Ingredientes { get; set; } = new();
        public List<Orden> Ordenes { get; set; } = new();
        public string Categoria { get; set; }


    }
}
