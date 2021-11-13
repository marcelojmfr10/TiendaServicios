﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator mediator;
        public LibroMaterialController(IMediator _mediator)
        {
            this.mediator = _mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaMaterialDto>>> Consulta()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaMaterialDto>> ConsultaFiltro(Guid id)
        {
            return await mediator.Send(new ConsultaFiltro.Ejecuta { LibroId = id });
        }
    }
}
