﻿using Core.Application.Enums;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.MesaOrdenes;
using Core.Application.ViewModels.Orden;
using Core.Application.ViewModels.Ordenes;
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
    [Authorize(Roles = "mesero")]
    public class OrdenController : BaseApiController
    {
        private readonly IOrdenService _ordenService;
        public readonly IMesaOrdenesService _MesaOrdenesService;

        public OrdenController(IOrdenService ordenService, IMesaOrdenesService MesaOrdenesService)
        {
            _ordenService = ordenService;
            _MesaOrdenesService = MesaOrdenesService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(OrdenSaveViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                
                viewModel.Estados = EstadosMesa.En_Proceso_de_atencion.ToString();
                var ordenesmesa = viewModel.OrdenesporMesa;
                viewModel = await _ordenService.Add(viewModel);
                foreach (MesaOrdenSaveViewModel orden in ordenesmesa)
                {
                     orden.Ordenid = viewModel.MesaId;
                    await _MesaOrdenesService.Add(orden);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id,OrdenSaveViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _ordenService.Update(viewModel,id);

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(OrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await _ordenService.GetAllAsync();
                if(result == null || result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(OrdenSaveViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _ordenService.GetById(id);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _ordenService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
