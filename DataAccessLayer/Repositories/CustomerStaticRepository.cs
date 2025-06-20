using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CustomerStaticRepository:ICustomerStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            var customer= await _appDbContext.Customers.ToListAsync();
            return customer;

        }

        public async Task<Customer?> GetByIdAsync()
        {
            int staticID = 3;
            var customer= await _appDbContext.Customers
                .FindAsync(staticID);
            return customer;
        }
        public async Task AddAsync()
        {
            var customer = new Customer
            {
                FullName = "Celil Süleyman",
                Age = 23,
                Email = "celil.suleyman@example.com",
                Phone = "+90 553 984 5176",
                Address = "İstanbul, Kadıköy, Bahariye Caddesi No:45",
                IsActive = false,
                RegisteredDate = DateTime.Now

            };
            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 2;
            var customer= await _appDbContext.Customers
                .FindAsync(staticID);
            if (customer!=null)
            {
                customer.FullName = "Yasin Alakurt";
                customer.Age = 36; 
                customer.Email = "yasin.alakurt@gmail.com";
                customer.Phone = "+90 542 859 63 25";
                customer.Address = "Ankara, Çankaya, Atatürk Bulvarı No:120";
                customer.IsActive = true;
                customer.RegisteredDate = DateTime.Now.AddDays(8); // (örneğin ileri tarihli planlı kayıt)
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 2;
            var customer= await _appDbContext.Customers
                .FindAsync(staticID);
            if (customer!=null)
            {
                _appDbContext.Customers.Remove(customer);
                await _appDbContext.SaveChangesAsync();
            }
        }

    }
}
