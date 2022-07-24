﻿using Core.Application.ViewModels.Ingrediente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Platos
{
    public class PlatoSaveViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int CantidadPersonas { get; set; }
        public List<IngredienteViewModel> Ingrediente { get; set; }
        public string Categoria { get; set; }
    }
}