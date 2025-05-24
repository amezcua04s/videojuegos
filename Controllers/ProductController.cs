using VideojuegosApp.Models;
using VideojuegosApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace VideojuegosApp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly VideojuegoDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductosController(VideojuegoDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Juegos.ToListAsync());
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(Juego juego, IFormFile RutaImagen)
        {
            if (ModelState.IsValid)
            {
                if (RutaImagen != null)
                {
                    var fileName = Path.GetFileName(RutaImagen.FileName);
                    var path = Path.Combine(_env.WebRootPath, "images", fileName);
                    using var stream = new FileStream(path, FileMode.Create);
                    await RutaImagen.CopyToAsync(stream);
                    juego.RutaImagen = "/images/" + fileName;
                }

                _context.Add(juego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(juego);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var juego = await _context.Juegos.FindAsync(id);
            return juego == null ? NotFound() : View(juego);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, Juego juego, IFormFile RutaImagen)
        {
            if (id != juego.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (RutaImagen != null)
                    {
                        var fileName = Path.GetFileName(RutaImagen.FileName);
                        var path = Path.Combine(_env.WebRootPath, "images", fileName);
                        using var stream = new FileStream(path, FileMode.Create);
                        await RutaImagen.CopyToAsync(stream);
                        juego.RutaImagen = "/images/" + fileName;
                    }
                    _context.Update(juego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Juegos.Any(m => m.Id == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(juego);
        }

        [AllowAnonymous] // Permite acceso sin login
        public IActionResult IndexPublico()
        {
            var juegos = _context.Juegos.ToList();
            return View(juegos);
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var juego = await _context.Juegos.FindAsync(id);
            if (juego != null)
            {
                _context.Juegos.Remove(juego);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}

