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
    public class CouponStaticRepository: ICouponStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public CouponStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Coupon>> GetAllAsync()
        {
            var coupon= await _appDbContext.Coupons
                .ToListAsync();
            return coupon;
        }

        public async Task<Coupon?> GetByIdAsync()
        {
            int staticID = 1;
            var coupon = await _appDbContext.Coupons
                .FindAsync(staticID);
            return coupon;
        }
        public async Task AddAsync()
        {
            var coupon = new Coupon
            {
                Code = "WELCOME10",
                DiscountRate = 10, // yüzde 10 indirim
                ValidFrom = DateTime.Now,
                ValidUntil = DateTime.Now.AddDays(7), // 7 gün geçerli
                IsActive = true
            };
            await _appDbContext.Coupons.AddAsync(coupon);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 1;
           var coupon= await _appDbContext.Coupons
                .FindAsync(staticID);
            if (coupon!=null)
            {
                coupon.Code = "SUMMER30";
                coupon.DiscountRate = 30; // %30 indirim
                coupon.ValidFrom = DateTime.Now;
                coupon.ValidUntil = DateTime.Now.AddDays(30); // 30 gün geçerli
                coupon.IsActive = true;
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync()
        {
            int staticID = 1;
            var coupon = await _appDbContext.Coupons
                .FindAsync(staticID);
            if (coupon!=null)
            {
                _appDbContext.Coupons.Remove(coupon);
                await _appDbContext.SaveChangesAsync();
            }
        }

      
    }
}
