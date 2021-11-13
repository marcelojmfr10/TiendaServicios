using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class Ejecuta : IRequest<LibreriaMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, LibreriaMaterialDto>
        {
            private readonly ContextoLibreria context;
            private readonly IMapper mapper;
            public Manejador(ContextoLibreria _context, IMapper _mapper)
            {
                this.context = _context;
                this.mapper = _mapper;
            }
            public async Task<LibreriaMaterialDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = await context.LibreriaMaterial.FirstOrDefaultAsync(x => x.LibreriaMaterialId == request.LibroId);
                if (libro == null)
                {
                    throw new Exception("No existe el libro");
                }
                var libroDto = mapper.Map<LibreriaMaterial, LibreriaMaterialDto>(libro);

                return libroDto;
            }
        }
    }
}
