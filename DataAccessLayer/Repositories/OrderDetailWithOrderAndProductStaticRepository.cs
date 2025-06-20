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
    public class OrderDetailWithOrderAndProductStaticRepository: IOrderDetailWithOrderAndProductStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderDetailWithOrderAndProductStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<OrderDetail>> GetAllAsync()
        {
            var orderDetail=  await _appDbContext.OrderDetails
          .Include(od => od.Order)
              .ThenInclude(o => o.Customer)
          .Include(od => od.Product)
              .ThenInclude(p => p.ProductBrand)
          .ToListAsync();
            return orderDetail;
        }

        public async Task<OrderDetail?> GetByIdAsync()
        {
            int staticID = 1;

            var orderDetail = await _appDbContext.OrderDetails
                .Include(od => od.Order)
                    .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                    .ThenInclude(p => p.ProductBrand)
                .FirstOrDefaultAsync(od => od.OrderDetailID == staticID);
            return orderDetail;
        }
        public async Task AddAsync()
        {
            var orderDetail = new OrderDetail
            {
                Quantity = 3,
                UnitPrice = 25,
                Discount = 8,
                Notes = "Ürün detayı notu eklendi - 1",
                OrderId = 2,
                ProductID = 1006
            };

            await _appDbContext.OrderDetails.AddAsync(orderDetail);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 1;

            var orderDetail = await _appDbContext.OrderDetails
                .Include(od => od.Order)
                    .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                    .ThenInclude(p => p.ProductBrand)
                .FirstOrDefaultAsync(od => od.OrderDetailID == staticID);

            if (orderDetail != null)
            {
                // OrderDetail güncelleme
                orderDetail.Quantity = 2;
                orderDetail.UnitPrice = 24;
                orderDetail.Discount = 8;
                orderDetail.Notes = "Ürün detayı notu güncellendi - 1";

                // Order güncelleme
                if (orderDetail.Order != null)
                {
                    orderDetail.Order.OrderDate = new DateTime(2025, 6, 20); // Örnek tarih
                    orderDetail.Order.TotalAmount = 120;                    // Örnek tutar
                    orderDetail.Order.Status = "Hazırlanıyor";              // Örnek durum
                    orderDetail.Order.Notes = "Sipariş güncellendi.";       // Örnek not

                    // Customer güncelleme
                    if (orderDetail.Order.Customer != null)
                    {
                        orderDetail.Order.Customer.FullName = "Mertcan Adsız"; // Örnek isim
                                                                               // Diğer müşteri bilgileri gerekiyorsa buraya eklenebilir
                    }
                }

                // Product güncelleme
                if (orderDetail.Product != null)
                {
                    orderDetail.Product.Name = "Güncellenmiş Ürün Adı";
                    orderDetail.Product.Description = "Ürün açıklaması güncellendi.";
                    orderDetail.Product.Price = 45; // Yeni fiyat
                    orderDetail.Product.Stock = 150;
                    orderDetail.Product.IsActive = true;

                    // ProductBrand güncelleme
                    if (orderDetail.Product.ProductBrand != null)
                    {
                        orderDetail.Product.ProductBrand.Name = "Yeni Marka";
                        orderDetail.Product.ProductBrand.Description = "Marka açıklaması güncellendi.";
                        orderDetail.Product.ProductBrand.Country = "Türkiye";
                    }
                }

                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync()
        {
            int staticID = 1;

            var orderDetail = await _appDbContext.OrderDetails
                .Include(od => od.Order)
                    .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                    .ThenInclude(p => p.ProductBrand)
                .FirstOrDefaultAsync(od => od.OrderDetailID == staticID);

            if (orderDetail != null)
            {
                // İlişkili Order silinsin mi?
                if (orderDetail.Order != null)
                {
                    // İlişkili Customer silinsin mi?
                    if (orderDetail.Order.Customer != null)
                    {
                        _appDbContext.Customers.Remove(orderDetail.Order.Customer);
                    }

                    _appDbContext.Orders.Remove(orderDetail.Order);
                }

                // İlişkili Product silinsin mi?
                if (orderDetail.Product != null)
                {
                    // İlişkili ProductBrand silinsin mi?
                    if (orderDetail.Product.ProductBrand != null)
                    {
                        _appDbContext.ProductBrands.Remove(orderDetail.Product.ProductBrand);
                    }

                    _appDbContext.Products.Remove(orderDetail.Product);
                }

                // Son olarak OrderDetail'i sil
                _appDbContext.OrderDetails.Remove(orderDetail);

                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
