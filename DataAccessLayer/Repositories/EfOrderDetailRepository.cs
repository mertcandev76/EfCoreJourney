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
    public class EfOrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public EfOrderDetailRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<OrderDetail>> GetAllWithOrderandProductAsync()
        {
            return await Table
                .Include(oD=>oD.Order)
                 .Include(oD => oD.Product)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetByIdWithOrderandProductAsync(int id)
        {
            return await Table
                .Include(oD => oD.Order)
                 .Include(oD => oD.Product)
                .FirstOrDefaultAsync(oD=>oD.OrderDetailID==id);
        }
    }
}
