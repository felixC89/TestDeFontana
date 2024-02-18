namespace Aplicacion.DTOs
{
    public class ProductoDto
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public long IdMarca { get; set; }
        public string Modelo { get; set; } = null!;
        public int CostoUnitario { get; set; }
    }
}
