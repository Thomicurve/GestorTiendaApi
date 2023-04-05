namespace MiTiendaApi.Models.Inputs
{
    public class ProductoInput
    {
        public string Nombre { get; set; } = string.Empty;
        public float Precio { get; set; }
        public int ProveedorId { get; set; }
        public int Stock { get; set; }

    }
}
