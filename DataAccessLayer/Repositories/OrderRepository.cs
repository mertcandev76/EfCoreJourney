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
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Tüm siparişleri asenkron olarak getirir.
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            
                throw new NotImplementedException();
        }

        //Belirli bir OrderID'ye sahip siparişi getirir.
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        //Yeni bir siparişi veritabanına ekler.
        public async Task AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        //Mevcut bir sipariş kaydını günceller.
        public async Task UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        //Verilen id'ye sahip siparişi siler.
        public async Task DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
