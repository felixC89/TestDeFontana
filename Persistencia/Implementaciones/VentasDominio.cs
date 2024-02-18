using Dominio.Entities;
using Dominio.Interfaces;
using Infraestructura.Model;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Implementaciones
{
    public class VentasDominio : IVentasDominio
    {
        readonly PruebaContext context;

        public VentasDominio()
        {
            context = new PruebaContext();
        }
        public async Task<IEnumerable<Local>> GetLocal()
        {

            return await context.Locals.ToListAsync();
        }

        public async Task<IEnumerable<Marca>> GetMarcas()
        {
            return await context.Marcas.ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetProductos()
        {
            return await context.Productos.ToListAsync();

        }

        public async Task<IEnumerable<Ventum>> GetVentas(DateTime fechai, DateTime fechaf)
        {

            return await context.Venta.Where(ventas => ventas.Fecha >= fechai && ventas.Fecha <= fechaf).ToListAsync();

        }

        public async Task<IEnumerable<VentaDetalle>> GetVentasDetalle()
        {
            return await context.VentaDetalles.ToListAsync();

        }
    }
}
