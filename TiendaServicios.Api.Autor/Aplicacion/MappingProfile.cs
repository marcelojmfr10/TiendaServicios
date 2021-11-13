using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class MappingProfile : Profile
    {
        // mapeos entre una clase de EF y una clase DTO
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
