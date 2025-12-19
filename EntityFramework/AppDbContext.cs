using customer_request_accounting_system.Models;
using Microsoft.EntityFrameworkCore;

namespace customer_request_accounting_system.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=customerRequestDatabase.db");
        }

    }
}
