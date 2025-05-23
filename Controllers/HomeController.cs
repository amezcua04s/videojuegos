using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideojuegosApp.Data;
using VideojuegosApp.Models;


namespace VideojuegosApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VideojuegoDbContext _context; // Suponiendo que tienes un DbContext para tu base de datos


        public HomeController(ILogger<HomeController> logger, VideojuegoDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Obtén todos los juegos de la base de datos
            List<Juego> juegos = _context.Juegos.ToList(); // _context.Juegos es tu DbSet<Juego>

            // Pasa la lista de juegos a la vista
            return View(juegos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
