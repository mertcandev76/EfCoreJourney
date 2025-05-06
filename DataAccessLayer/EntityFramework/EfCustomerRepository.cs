using Microsoft.EntityFrameworkCore;  // Gerekli using direktifi
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerRepository:ICustomerDal
    {
        private readonly AppDbContext _appDbContext;

        public EfCustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        async Task<List<Customer>> ICustomerDal.GetAll()
        {
            return await _appDbContext.Customers.ToListAsync();
        }
        public async Task<Customer> GetSingleCustomerOperationAsync()
        {
            return await _appDbContext.Customers.FirstOrDefaultAsync();
        }
        public async Task<int> GetCustomerStatisticsAsync()
        {
            return await _appDbContext.Customers.CountAsync();
        }

        public async Task<bool> CustomerExistsAsync()
        {
            return await _appDbContext.Customers.AnyAsync(x=>x.FirstName=="Cabbar");
        }
        public async Task<decimal?> GetValueAsync()
        {
            return await _appDbContext.Customers.SumAsync(c => c.Age ?? 0);
        }

        public async Task InsertAsync(Customer customer)
        {
            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }

       
    }
}
