using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Data;
using YemekTarifleri.Models;

namespace YemekTarifleri.Controllers
{
    // 🔓 OWASP AÇIĞI: Bu controller, şifreyi hashlemeden düz olarak kaydeder (Cryptographic Failure)
    public class InsecureRegisterController : Controller
    {
        private readonly InsecureDbContext _context;

        public InsecureRegisterController(InsecureDbContext context)
        {
            _context = context;
        }

        // GET: /InsecureRegister
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /InsecureRegister
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(InsecureUser model)
        {
            if (ModelState.IsValid)
            {
                // 🚨 Güvensiz işlem: Şifre doğrudan veritabanına yazılıyor (plaintext)
                _context.InsecureUsers.Add(model);
                _context.SaveChanges();

                return Content("Kayıt tamamlandı (şifre düz metin olarak kaydedildi!)");
            }

            return View(model);
        }
    }
}
