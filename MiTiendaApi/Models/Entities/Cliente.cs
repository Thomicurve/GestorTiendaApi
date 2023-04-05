namespace MiTiendaApi.Models.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public List<Producto> Productos { get; } = new();
        public List<ClienteProducto> ClientesProductos { get; } = new();

    }
}
