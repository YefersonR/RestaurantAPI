using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class PlatoIngredientes : AuditableBase
    {
        public Ingrediente Ingrediente { get; set; }
        public int IngredienteId { get; set; }

        public Plato Plato{ get; set; }
        public int PlatoId { get; set; }

    }
}
