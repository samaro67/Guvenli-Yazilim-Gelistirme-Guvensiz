using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Models;

namespace YemekTarifleri.Controllers
{
    // 🔥 GÜVENSİZ: Herkes erişebilir, hiçbir kontrol yok
    public class AdminPanelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminPanelController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // 🔓 Tüm kullanıcılar bu sayfayı görebilir
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // 🔓 Herkes kullanıcıya rol atayabilir (POST)
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(role))
                return RedirectToAction("Index");

            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);
            await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction("Index");
        }

        // 🔓 Herkes kullanıcıya rol atayabilir (GET - test/demo için)
        [HttpGet]
        public async Task<IActionResult> AssignRoleTest(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(role))
                return Content("Geçersiz kullanıcı ya da rol.");

            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);
            await _userManager.AddToRoleAsync(user, role);

            return Content($"Rol '{role}' başarıyla '{user.UserName}' kullanıcısına atandı (GET - GÜVENSİZ TEST).");
        }

        // 🔓 Örnek: Admin'e özel yeni action, ama korumasız
        public IActionResult AdminLog()
        {
            return Content("Bu admin'e özel olması gereken log sayfası ama herkes görebilir.");
        }
    }
}
