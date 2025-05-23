using VideojuegosApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VideojuegosApp.Data
{
    public class VideojuegoDbContext : IdentityDbContext<VideojuegoUser>
    {

        public VideojuegoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Juego> Juegos { get; set; }

    }
}
