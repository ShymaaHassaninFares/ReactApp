using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProductAPI.Data
{
    public class DBContext : DbContext
    {
        public static IConfiguration Configuration { get; private set; }
        public DBContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        DbSet<Product.Domain.Model.Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration["DBConn"]);
            }
        }

        
    }
}
