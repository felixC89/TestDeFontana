using ClienteVentas.Helpers;
using ClienteVentas.Models;
using ClienteVentas.Services.Interfaces;
using System.Text.Json;

namespace ClienteVentas.Services.Implementaciones
{
    public class VentasService : IVentasService
    {
         private  string? urlBase;
        public VentasService()
        {
            var rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "\\Settings/appsettings.json";
            string json = File.ReadAllText(rutaArchivo);
            JsonDocument jsonDocument = JsonDocument.Parse(json);
            JsonElement root = jsonDocument.RootElement;

            urlBase = root.GetProperty("UrlBackend").GetString();
        }

        public async Task<IEnumerable<Local>> GetLocalAsync()
        {
            IEnumerable<Local>? result;
            try
            {
                result = await ApiConsumer.SendAsync<IEnumerable<Local>>(urlBase, "Ventas/GetLocal", ApiConsumer.MethodType.GET);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public async Task<IEnumerable<Marca>> GetMarcaAsync()
        {
            IEnumerable<Marca>? result;
            try
            {
                result = await ApiConsumer.SendAsync<IEnumerable<Marca>>(urlBase, "Ventas/GetMarcas", ApiConsumer.MethodType.GET);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            IEnumerable<Producto>? result;
            try
            {
                result = await ApiConsumer.SendAsync<IEnumerable<Producto>>(urlBase, "Ventas/GetProductos", ApiConsumer.MethodType.GET);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public async Task<IEnumerable<Ventas>> GetVentasAsync(int dias)
        {
            IEnumerable<Ventas>? result;

            try
            {
                result = await ApiConsumer.SendAsync<IEnumerable<Ventas>>(urlBase, $"Ventas/GetVentasPorDias/{dias}", ApiConsumer.MethodType.GET);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public async Task<IEnumerable<VentasDetalle>> GetVentasDetallesAsync()
        {
            IEnumerable<VentasDetalle>? result;

            try
            {
                return result = await ApiConsumer.SendAsync<IEnumerable<VentasDetalle>>(urlBase, "Ventas/GetVentasDetalle", ApiConsumer.MethodType.GET);
            }
            catch (Exception)
            {
                result = null;
            }

            return result ;
        }
    }
}
