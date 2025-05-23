using Microsoft.AspNetCore.Identity;

namespace VideojuegosApp.Models
{
    public class VideojuegoUser : IdentityUser
    {

        public string Nombre { get; set; }
        public string Materno { get; set; }
        public DateTime Registro { get; set; } = DateTime.Now;
        public DateTime? Modificacion { get; set; }

    }
}
