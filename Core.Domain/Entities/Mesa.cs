using Core.Domain.Commons;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Mesa : AuditableBase
    {
        public int CantidadPersonas { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public List<Orden> Ordenes { get; set; } = new();
    }
}
