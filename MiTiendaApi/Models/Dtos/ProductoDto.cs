namespace MiTiendaApi.Models.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public float Precio { get; set; }
        public int ProveedorId { get; set; }
    }
}
