using VideojuegosApp.Data;
using VideojuegosApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// agregamos el servicio de Entity Framework Core SQL server y la cadena de conexi�n
builder.Services.AddDbContext<VideojuegoDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("VideojuegoDB")));

// agregamos el servicio de Identity
builder.Services.AddIdentity<VideojuegoUser, IdentityRole>()
    .AddEntityFrameworkStores<VideojuegoDbContext>()
    .AddDefaultTokenProviders();

//si quieren personalizar las reglas de validaci�n de copntrase�as comenten arriba
//y descomenten las siguientes lineas
//builder.Services.AddIdentity<FarmaciaUser, IdentityRole>(options =>
//{
//    // Personalizaci�n de contrase�as
//    options.Password.RequireDigit = false;             // No requiere n�mero
//    options.Password.RequiredLength = 8;               // Longitud m�nima: 8
//    options.Password.RequireNonAlphanumeric = false;   // No requiere s�mbolo (!, @, etc.)
//    options.Password.RequireUppercase = false;         // No requiere may�sculas
//    options.Password.RequireLowercase = false;         // No requiere min�sculas
//})
//.AddEntityFrameworkStores<FarmaciaDbContext>()
//.AddDefaultTokenProviders();


//Logica de permisos de acceso a vistas a base de roles
builder.Services.AddControllersWithViews();

var app = builder.Build();
//llenar datos en la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedUsers.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();