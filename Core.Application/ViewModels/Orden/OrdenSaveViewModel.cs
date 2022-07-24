using Core.Application.ViewModels.Mesa;
using Core.Application.ViewModels.Platos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Orden
{
    public class OrdenSaveViewModel
    {
        public int Id { get; set; }
        public MesaViewModel Mesa { get; set; }
        public List<PlatoViewModel> Platos { get; set; }
        public int Subtotal { get; set; }
        public string Estados { get; set; }
    }
}
