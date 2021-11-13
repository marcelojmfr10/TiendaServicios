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
    public class Consulta
    {
        public class Ejecuta : IRequest<List<AutorDto>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<AutorDto>>
        {
            private readonly ContextoAutor context;
            private readonly IMapper mapper;
            public Manejador(ContextoAutor _context, IMapper _mapper)
            {
                this.context = _context;
                this.mapper = _mapper;
            }

            public async Task<List<AutorDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autores = await context.AutorLibro.ToListAsync();
                var autoresDto = mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);

                return autoresDto;
                // return autores;
            }
        }
    }
}
