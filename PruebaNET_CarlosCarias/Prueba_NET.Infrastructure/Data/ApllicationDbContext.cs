using Microsoft.EntityFrameworkCore;
using Prueba_NET.Domain.Entities;

namespace Prueba_NET.Infrastructure.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
