using Microsoft.EntityFrameworkCore;

namespace ef
{
    public class ProductDbContext : DbContext
    {
        // Keep this constructor as-is
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        // Uncomment in exercise 1
        public DbSet<DbProduct> Products { get; set; }
        public DbSet<DbVat> Vat
        { get; set;}
    }
   
}
