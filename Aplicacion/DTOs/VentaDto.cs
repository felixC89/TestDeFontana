namespace Aplicacion.DTOs
{
    public class VentaDto
    {
        public long IdVenta { get; set; }
        public int Total { get; set; }
        public DateTime Fecha { get; set; }
        public long IdLocal { get; set; }
    }
}
