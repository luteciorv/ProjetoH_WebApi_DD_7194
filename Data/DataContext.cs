using Microsoft.EntityFrameworkCore;
using ProjetoH_WebApi_DD_7194.Models;

namespace ProjetoH_WebApi_DD_7194.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
