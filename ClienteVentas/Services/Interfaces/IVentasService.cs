using ClienteVentas.Models;

namespace ClienteVentas.Services.Interfaces
{
    public interface IVentasService
    {
        Task<IEnumerable<Ventas>> GetVentasAsync(int dias);
        Task<IEnumerable<VentasDetalle>> GetVentasDetallesAsync();
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<IEnumerable<Local>> GetLocalAsync();
        Task<IEnumerable<Marca>> GetMarcaAsync();
    }
}
