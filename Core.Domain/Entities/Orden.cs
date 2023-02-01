using Core.Domain.Commons;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Orden : AuditableBase
    {
        public int Subtotal { get; set; }
        public string Estados { get; set; }
        public List<Plato> Platos { get; set; } = new();
        public int MesaId { get; set; }
        public Mesa Mesa { get; set; } = new();
    }
}
