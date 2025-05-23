using Microsoft.EntityFrameworkCore;
using YemekTarifleri.Models;

namespace YemekTarifleri.Data
{
    public class InsecureDbContext : DbContext
    {
        public InsecureDbContext(DbContextOptions<InsecureDbContext> options) : base(options) { }

        public DbSet<InsecureUser> InsecureUsers { get; set; }
    }
}

