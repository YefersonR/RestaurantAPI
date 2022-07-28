using Core.Application.ViewModels.Mesa;
using Core.Application.ViewModels.MesaOrdenes;
using Core.Application.ViewModels.Platos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Ordenes
{
    public class OrdenViewModel
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public List<MesaOrdenViewModel> Platos { get; set; }
        public List<MesaViewModel> Mesa { get; set; }

        public int Subtotal { get; set; }
        public string Estados { get; set; }
    }
}
