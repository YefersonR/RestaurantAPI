using Core.Application.ViewModels.Ingrediente;
using Core.Application.ViewModels.Ordenes;
using System.Collections.Generic;

namespace Core.Application.ViewModels.Platos
{
    public class PlatoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int CantidadPersonas { get; set; }
        public List<IngredienteViewModel> Ingredientes { get; set; } = new();
        public string Categoria{ get; set; }
    }
}
