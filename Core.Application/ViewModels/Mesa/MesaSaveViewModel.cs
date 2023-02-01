using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Mesa
{
    public class MesaSaveViewModel
    {
        public int CantidadPersonas { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public string Estado { get; set; }

    }
}
