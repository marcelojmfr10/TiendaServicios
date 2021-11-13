using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<LibrosService> logger;
        public LibrosService(IHttpClientFactory _httpClient, ILogger<LibrosService> _logger)
        {
            this.httpClient = _httpClient;
            this.logger = _logger;
        }

        public async Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }; // para no tener problemas con mayúsculas y minúsculas
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
