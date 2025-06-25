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
    public class EfCustomerCouponRepository : GenericRepository<CustomerCoupon>, ICustomerCouponRepository
    {
        public EfCustomerCouponRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<CustomerCoupon>> GetAllWithCustomerandCouponAsync()
        {
            return await Table
                .Include(cc=>cc.Customer)
                .Include(cc => cc.Coupon)
                .ToListAsync();
        }

        public async Task<CustomerCoupon?> GetByIdWithCustomerandCouponAsync(int id)
        {
            return await Table
             .Include(cc => cc.Customer)
             .Include(cc => cc.Coupon)
             .FirstOrDefaultAsync(cc=>cc.CustomerCouponID==id);
        }
    }
}
