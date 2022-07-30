using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Plato : AuditableBase
    {
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int CantidadPersonas { get; set; }
        public List<PlatoIngredientes> Ingredientes { get; set; }
        public List<OrdenesPlatos> Ordens { get; set; } 
        public string Categoria { get; set; }


    }
}
