using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        // hay que agregar un constructor para el unit test
        public ContextoLibreria()
        {

        }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options)
        {

        }

        // virtual: se puede sobreescribir a futuro, si no se coloca genera errores al hacer el unit test
        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
        // dotnet ef migrations add MigrationSqlServerInicial --project TiendaServicios.Api.Libro
        // dotnet ef database update --project TiendaServicios.Api.Libro
    }
}
