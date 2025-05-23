using Microsoft.AspNetCore.Identity;
using VideojuegosApp.Models;

namespace VideojuegosApp.Data
{
    public class SeedUsers
    {
        public static async Task Initialize(IServiceProvider serviceProvider) {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<VideojuegoUser>>();

            string[] roles = new[] { "Administrador", "Cliente" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Crear usuario administrador por defecto
            string adminEmail = "admin@video.com";
            string password = "Admin123.juego";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new VideojuegoUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nombre = "Admin",
                    Materno = "Principal",
                    Registro = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Administrador");
            }


        }



    }
}
