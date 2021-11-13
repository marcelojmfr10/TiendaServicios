using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ListaProductos { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public CarritoContexto contexto { get; set; }
            public Manejador(CarritoContexto _context)
            {
                this.contexto = _context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                contexto.CarritoSesion.Add(carritoSesion);
                var resultado = await contexto.SaveChangesAsync();

                if (resultado == 0)
                {
                    throw new Exception("Errores en la inserción del carrito de compras");
                }

                int id = carritoSesion.CarritoSesionId;

                foreach (var obj in request.ListaProductos)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = obj
                    };

                    contexto.CarritoSesionDetalle.Add(detalleSesion);
                }

                resultado = await contexto.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }
}
