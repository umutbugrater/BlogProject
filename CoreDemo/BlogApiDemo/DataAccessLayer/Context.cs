using Microsoft.EntityFrameworkCore;

namespace BlogApiDemo.DataAccessLayer
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-JCPO54A\\SQLEXPRESS; database=CoreBlogApiDb; integrated security=true;TrustServerCertificate=True");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
