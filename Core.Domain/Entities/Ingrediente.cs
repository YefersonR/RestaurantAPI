using Core.Domain.Commons;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Ingrediente : AuditableBase
    {
        public string  Nombre { get; set; }
        public List<Plato> Platos { get; set; } = new();

    }
}
