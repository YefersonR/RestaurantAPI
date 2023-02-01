using Core.Application.ViewModels.Platos;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Application.ViewModels.Orden
{
    public class OrdenSaveViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int MesaId { get; set; }
        public List<int> Platos { get; set; } = new();
        [JsonIgnore]
        public int Subtotal { get; set; }
        [JsonIgnore]
        public string Estados { get; set; }
    }
}
