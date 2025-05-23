using Microsoft.EntityFrameworkCore;
using YemekTarifleri.Data;
using YemekTarifleri.Models;
using Microsoft.AspNetCore.Identity;

namespace YemekTarifleri
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ GÜVENLİ DB CONTEXT (Identity için zorunlu)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 🔓 GÜVENSİZ DB CONTEXT (OWASP test için)
            builder.Services.AddDbContext<InsecureDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("InsecureDb")));

            // Identity + ROL sistemi aktif
            builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>() // Rolleri ekledik
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Razor Pages + MVC
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // ROL OLUŞTURMA: Admin, Uye, Misafir
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roller = { "Admin", "Uye", "Misafir" };

                foreach (var rol in roller)
                {
                    if (!await roleManager.RoleExistsAsync(rol))
                    {
                        await roleManager.CreateAsync(new IdentityRole(rol));
                    }
                }
            }

            // HTTP pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Giriş işlemleri için şart
            app.UseAuthorization();

            // ✅ /Tarifler → TarifController > Index
            app.MapControllerRoute(
                name: "tarifler",
                pattern: "Tarifler",
                defaults: new { controller = "Tarif", action = "Index" });

            // Varsayılan yönlendirme
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
