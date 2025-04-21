using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class AppDbContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Bağlantı cümlesi buraya yazılır
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=EfOrmJourneyDB; integrated security=true;");
        }
        public DbSet<Customer> Customers { get; set; }
    }

}
