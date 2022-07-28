using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class MesaOrdenes : AuditableBase
    {
        public int OrdenId { get; set; }
        public Orden Orden { get; set; }

        public int PlatoId { get; set; }
        public Plato Plato { get; set; }

    }
}
