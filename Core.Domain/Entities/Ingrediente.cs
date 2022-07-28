using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Ingrediente : AuditableBase
    {
        public string  Nombre { get; set; }
        public List<PlatoIngredientes> Platos { get; set; }

    }
}
