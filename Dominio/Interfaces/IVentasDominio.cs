using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IVentasDominio
    {
        Task<IEnumerable<Ventum>> GetVentas(DateTime fechai, DateTime fechaf);
        Task<IEnumerable<VentaDetalle>> GetVentasDetalle();
        Task<IEnumerable<Producto>> GetProductos();
        Task<IEnumerable<Marca>> GetMarcas();
        Task<IEnumerable<Local>> GetLocal();
    }
}
