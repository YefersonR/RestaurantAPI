using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Mesa : AuditableBase
    {
        public int CantidadPersonas { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public List<Orden> Ordenes { get; set; }
    }
}
