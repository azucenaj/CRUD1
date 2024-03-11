using Microsoft.EntityFrameworkCore;

namespace CRUD1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<CRUD1> CRUD1 { get; set; }
        public DbSet<Model.Department> departments { get; set; }
    }
}
