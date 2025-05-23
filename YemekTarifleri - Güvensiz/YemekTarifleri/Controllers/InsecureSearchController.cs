using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using YemekTarifleri.Data;
using YemekTarifleri.Models;
using System.Linq;

namespace YemekTarifleri.Controllers
{
    public class InsecureSearchController : Controller
    {
        private readonly InsecureDbContext _context;

        public InsecureSearchController(InsecureDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string q)
        {
            if (string.IsNullOrEmpty(q))
                return View(null);

            // 🔥 SQL Injection'a açık sorgu
            var result = _context.InsecureUsers
                .FromSqlRaw($"SELECT * FROM InsecureUsers WHERE Username = '{q}'")
                .ToList();

            return View(result);
        }
    }
}

