using Core.Application.ViewModels.Ingrediente;
using Core.Application.ViewModels.PlatoIngrediente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Platos
{
    public class PlatoViewModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int CantidadPersonas { get; set; }
        public List<PlatoIngredientesViewModel> Ingredientes { get; set; }
        public string Categoria { get; set; }
    }
}
