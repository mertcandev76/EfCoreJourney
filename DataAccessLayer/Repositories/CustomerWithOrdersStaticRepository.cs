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
    public class CustomerWithOrdersStaticRepository:ICustomerWithOrdersStaticRepository
    {
        private readonly AppDbContext _appDbContext;
        public CustomerWithOrdersStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            var customer= await _appDbContext.Customers
                .Include(c=>c.Orders)
                .ToListAsync();
            return customer;
        }

        public async Task<Customer?> GetByIdAsync()
        {
            int StaticID = 1;
            var customer = await _appDbContext.Customers
                 .Include(c => c.Orders)
                 .FirstOrDefaultAsync(c=>c.CustomerID==StaticID);
            return customer;
        }

        public async Task AddAsync()
        {
            var customer = new Customer
            {
                FullName = "Hasan Adsız",
                Age = 34,
                Email = "Hasan@gmail.com",
                Phone = "05336584574",
                Address = "Avcılar,İstanbul",
                IsActive = true,
                RegisteredDate = DateTime.Now,
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderDate = DateTime.Now,
                        TotalAmount = 1200,
                        Status = "Pending",
                        Notes = "İlk sipariş"
                    } //Bundan sonra ekleme yapabilirz liste olduğu için devamını ekleyebiliriz(bu bize kalmış)
                }
                

            };

            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int StaticID = 1;
             var customer = await _appDbContext.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c=>c.CustomerID==StaticID);
            if (customer !=null)
            {
                customer.FullName = "Mertcan Adsız";
                customer.Age = 24;
                customer.Email = "mertcanadsiz@gmail.com";
                customer.Phone = "05536841076";
                customer.Address = "Avcılar,İstanbul";
                customer.IsActive = true;
                customer.RegisteredDate = DateTime.Now;
                foreach (var order in customer.Orders ?? Enumerable.Empty<Order>())
                {
                    order.Notes = "Notlar Güncellendi";
                    order.OrderDate= DateTime.Now;
                    order.Status = "Updated";
                }
                await _appDbContext.SaveChangesAsync();
            }
            
        }

        public async Task DeleteAsync()
        {
            int StaticID = 1;
            var customer= await _appDbContext.Customers
                .Include(c=>c.Orders)
                .FirstOrDefaultAsync(c=>c.CustomerID==StaticID);
            if (customer!=null)
            {
                _appDbContext.Orders.RemoveRange(customer.Orders!);
                _appDbContext.Customers.Remove(customer);
                await _appDbContext.SaveChangesAsync();
            }
        
        
        }

      

     
    }
}
