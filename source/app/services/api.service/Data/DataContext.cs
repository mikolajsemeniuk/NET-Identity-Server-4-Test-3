using api.service.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}