using Core.Application.Enums;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Mesa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteWebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MesaController : BaseApiController
    {
        private readonly IMesaService _mesaService;

        public MesaController(IMesaService mesaService)
        {
            _mesaService = mesaService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles ="administrador")]
        public async Task<IActionResult> Create(MesaSaveViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _mesaService.Add(viewModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MesaSaveViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Update(int id, MesaSaveViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _mesaService.Update(viewModel, id);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MesaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "administrador,mesero")]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await _mesaService.GetAllAsync();
                if (result == null || result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(MesaSaveViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "administrador,mesero")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _mesaService.GetById(id);
                if(result == null )
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        //[HttpGet("{id}")]
        ////[ProducesResponseType(StatusCodes.Status200OK,Type=typeof(MesaViewModel))]
        ////[ProducesResponseType(StatusCodes.Status404NotFound)]
        ////[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        ///[Authorize(Roles = "mesero")]
        ///
        //public async Task<IActionResult> GetTableOrden(int id)
        //{
        //    try
        //    {
        //        var result = await _mesaService.GetAllOrdenesAsync(id);

        //        if (result == null || result.Count == 0)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        ////[ProducesResponseType(StatusCodes.Status204NoContent)]
        ////[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        ///[Authorize(Roles = "mesero")]
        //public async Task<IActionResult> ChangeStatus(int id, int idEstado)
        //{
        //    try{

        //        MesaSaveViewModel mesa = await _mesaService.GetById(id);
        //        switch (idEstado)
        //        {
        //            case ((int)EstadosMesa.Disponible):
        //                mesa.Estados = EstadosMesa.Disponible.ToString();
        //            break;
        //            case ((int)EstadosMesa.En_Proceso_de_atencion):
        //                mesa.Estados = EstadosMesa.En_Proceso_de_atencion.ToString();
        //            break;
        //            case ((int)EstadosMesa.Atendida):
        //                mesa.Estados = EstadosMesa.Atendida.ToString();
        //            break;
        //        }
        //        await _mesaService.Update(mesa,mesa.Id);

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
        //    }
        //}
    }
}
