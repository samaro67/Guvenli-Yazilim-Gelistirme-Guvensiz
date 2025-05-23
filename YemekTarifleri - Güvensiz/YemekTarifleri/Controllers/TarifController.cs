using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Data;
using YemekTarifleri.Models;
using System.Linq;

namespace YemekTarifleri.Controllers
{
    public class TarifController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TarifController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tarifler = _context.Tarifler
                .OrderByDescending(t => t.EklenmeTarihi)
                .ToList();

            return View(tarifler);
        }
    }
}
