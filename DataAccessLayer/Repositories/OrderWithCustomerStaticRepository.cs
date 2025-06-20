using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace DataAccessLayer.Repositories
{
    public class OrderWithCustomerStaticRepository : IOrderWithCustomerStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderWithCustomerStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var order= await _appDbContext.Orders
                .Include(o=>o.Customer)
                .ToListAsync();
            return order;
        }

        public async Task<Order?> GetByIdAsync()
        {
            int staticID = 1;
            var order= await _appDbContext.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o=>o.OrderID== staticID);
            return order;
        }


        public async Task AddAsync()
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = 150.75m,
                Status = "Onaylandı",
                Notes = "Hızlı teslimat istendi.",
                CustomerID = 1

            };
            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            int staticID = 1;
            var order = await _appDbContext.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.OrderID == staticID);
            if (order!=null)
            {
                order.OrderDate = DateTime.Now.AddDays(1); // Sipariş tarihi yarına ayarlandı
                order.TotalAmount = 250.75m;                // Gerçekçi tutar
                order.Status = "Hazırlanıyor";               // Anlamlı durum bilgisi
                order.Notes = "Müşteri tarafından hızlı teslimat istendi."; // Anlamlı not
                order.CustomerID = 2;// Müşteri ID'si değişmediği varsayılıyor
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 1;
            var order = await _appDbContext.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.OrderID == staticID);
            if (order != null)
            {
                _appDbContext.Orders.Remove(order);
                await _appDbContext.SaveChangesAsync();
            }
        }

  
    }
}
