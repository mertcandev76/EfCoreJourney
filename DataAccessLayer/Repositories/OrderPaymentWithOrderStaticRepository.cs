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
    public class OrderPaymentWithOrderStaticRepository: IOrderPaymentWithOrderStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderPaymentWithOrderStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<OrderPayment>> GetAllAsync()
        {
            var orderPayment = await _appDbContext.OrderPayments
              .ToListAsync();
            return orderPayment;
        }

        public async Task<OrderPayment?> GetByIdAsync()
        {
            int staticID = 2;
            var orderPayment = await _appDbContext.OrderPayments
                .FindAsync(staticID);
            return orderPayment;
        }

        public async Task AddAsync()
        {
            var orderPayment = new OrderPayment
            {
                Amount = 8,
                PaymentMethod = "Kredi Kartı", // veya "Havale", "Nakit", vb.
                PaymentDate = DateTime.Now,
                OrderID = 2

            };
            await _appDbContext.OrderPayments.AddAsync(orderPayment);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 2;
            var orderPayment = await _appDbContext.OrderPayments.FindAsync(staticID);
            if (orderPayment != null)
            {
                orderPayment.Amount = 5;
                orderPayment.PaymentMethod = "Takip Numarası Güncellendi";
                orderPayment.PaymentDate = DateTime.UtcNow;
                orderPayment.OrderID = 2;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 2;
            var orderPayment = await _appDbContext.OrderPayments.FindAsync(staticID);
            if (orderPayment != null)
            {
                _appDbContext.OrderPayments.Remove(orderPayment);
                await _appDbContext.SaveChangesAsync();
            }
        }


        
    }
}
