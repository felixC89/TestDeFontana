namespace Dominio.Entities
{
    public partial class Local
    {
        public Local()
        {
            Venta = new HashSet<Ventum>();
        }

        public long IdLocal { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
