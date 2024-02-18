namespace Dominio.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
        }

        public long IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public long IdMarca { get; set; }
        public string Modelo { get; set; } = null!;
        public int CostoUnitario { get; set; }

        public virtual Marca IdMarcaNavigation { get; set; } = null!;
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
    }
}
