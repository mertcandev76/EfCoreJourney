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
    public class EfOrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public EfOrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Order>> GetAllWithCustomerAsync()
        {
           return await Table
                .Include(o=>o.Customer)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdWithCustomerAsync(int id)
        {
            return await Table
           .Include(o => o.Customer)
           .FirstOrDefaultAsync(o=>o.OrderID==id);
        }
    }
}
