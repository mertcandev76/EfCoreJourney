using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderShipmentWithOrderStaticRepository: IOrderShipmentWithOrderStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderShipmentWithOrderStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<OrderShipment>> GetAllAsync()
        {
            var orderShipment= await _appDbContext.OrderShipments
                .ToListAsync();
            return orderShipment;
        }

        public async Task<OrderShipment?> GetByIdAsync()
        {
            int staticID = 2;
             var orderShipment = await _appDbContext.OrderShipments.FindAsync(staticID);
            return orderShipment;
        }
        public async Task AddAsync()
        {
            var orderShipment = new OrderShipment
            {
                Carrier = "Yurtiçi Kargo",                    // Kargo firması örneği
                TrackingNumber = "YT123456789TR",              // Takip numarası örneği
                ShippedDate = DateTime.Now,
                DeliveredDate = DateTime.Now.AddDays(1),
                OrderID = 2

            };
             await _appDbContext.OrderShipments.AddAsync(orderShipment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            int staticID = 2;
            var orderShipment = await _appDbContext.OrderShipments.FindAsync(staticID);
            if (orderShipment!=null)
            {
                orderShipment.Carrier = "Aras Kargo";
                orderShipment.TrackingNumber = "AR123456789TR";
                // Gönderim tarihi güncelleniyor
                orderShipment.ShippedDate = DateTime.UtcNow;
                // Teslim tarihi güncelleniyor (2 gün sonrası)
                orderShipment.DeliveredDate = DateTime.UtcNow.AddDays(2);
                orderShipment.OrderID = 2;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 2;
            var orderShipment = await _appDbContext.OrderShipments.FindAsync(staticID);
            if (orderShipment != null)
            {
                _appDbContext.OrderShipments.Remove(orderShipment);
                await _appDbContext.SaveChangesAsync();
            }
        }

      

    }
}
