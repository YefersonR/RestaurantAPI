using Core.Application.ViewModels.Ingrediente;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Application.ViewModels.Platos
{
    public class PlatoSaveViewModel
    {
        [JsonIgnore]
        public int Id{ get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int CantidadPersonas { get; set; }
        public List<int> Ingredientes { get; set; }
        public int Categoria { get; set; }
    }
}
