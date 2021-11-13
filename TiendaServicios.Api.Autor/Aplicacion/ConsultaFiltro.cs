using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class Ejecuta : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, AutorDto>
        {
            private readonly ContextoAutor context;
            private readonly IMapper mapper;
            public Manejador(ContextoAutor _context, IMapper _mapper)
            {
                this.context = _context;
                this.mapper = _mapper;
            }
            public async Task<AutorDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autor = await context.AutorLibro.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontró el autor");
                }
                var autorDto = mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
