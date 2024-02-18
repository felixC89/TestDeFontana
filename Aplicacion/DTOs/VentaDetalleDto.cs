namespace Aplicacion.DTOs
{
    public class VentaDetalleDto
    {
        public long IdVentaDetalle { get; set; }
        public long IdVenta { get; set; }
        public int PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public int TotalLinea { get; set; }
        public long IdProducto { get; set; }
    }
}
