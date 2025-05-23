namespace VideojuegosApp.Models
{
    public class Juego
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? RutaImagen { get; set; }
        public int? Precio { get; set; }
        public int? Stock { get; set; }
    }
}