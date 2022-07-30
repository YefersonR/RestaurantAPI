using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Orden : AuditableBase
    {
        public int MesaId { get; set; }
        public int Subtotal { get; set; }
        public string Estados { get; set; }
        public List<OrdenesPlatos> Platos { get; set; }
    }
}
