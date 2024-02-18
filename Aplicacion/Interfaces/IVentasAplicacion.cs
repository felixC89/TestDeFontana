using Aplicacion.DTOs;

namespace Aplicacion.Interfaces
{
    public interface IVentasAplicacion
    {
        Task<IEnumerable<VentaDto>> GetVentas(int dias);
        Task<IEnumerable<VentaDetalleDto>> GetVentasDetalle();
        Task<IEnumerable<ProductoDto>> GetProductos();
        Task<IEnumerable<MarcaDto>> GetMarcas();
        Task<IEnumerable<LocalDto>> GetLocal();
    }
}
