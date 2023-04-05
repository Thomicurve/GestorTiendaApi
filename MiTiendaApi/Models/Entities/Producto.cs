namespace MiTiendaApi.Models.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public float Precio { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }
        public List<Cliente> Clientes { get; } = new();
        public List<ClienteProducto> ClientesProductos { get; } = new();
    }
}
