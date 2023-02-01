using Core.Application.ViewModels.Mesa;
using Core.Application.ViewModels.Platos;
using System.Collections.Generic;

namespace Core.Application.ViewModels.Ordenes
{
    public class OrdenViewModel
    {
        public int Id { get; set; }
        public List<PlatoViewModel> Platos { get; set; } = new();
        public int Subtotal { get; set; }
        public string Estados { get; set; }
        public MesaViewModel Mesa { get; set; } = new();

    }
}
