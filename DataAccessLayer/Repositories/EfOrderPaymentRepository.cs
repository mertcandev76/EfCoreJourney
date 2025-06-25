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
    public class EfOrderPaymentRepository : GenericRepository<OrderPayment>, IOrderPaymentRepository
    {
        public EfOrderPaymentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<OrderPayment>> GetAllWithOrderAsync()
        {
            return await Table
                .Include(oP=>oP.Order)
                .ToListAsync();
        }

        public async Task<OrderPayment?> GetByIdWithOrderAsync(int id)
        {
            return await Table
               .Include(oP => oP.Order)
               .FirstOrDefaultAsync(oP=>oP.OrderPaymentID==id);
        }
    }
}
