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

        public void Delete(Customer customer)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<Customer>> GetAll()
        {

            return await _appDbContext.Customers
                .Where(x=>x.CustomerID>5)
                .OrderBy(x=>x.Name)
               .ToListAsync();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
