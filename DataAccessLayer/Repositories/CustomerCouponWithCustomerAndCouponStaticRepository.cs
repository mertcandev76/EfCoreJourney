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
    public class CustomerCouponWithCustomerAndCouponStaticRepository: ICustomerCouponWithCustomerAndCouponStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerCouponWithCustomerAndCouponStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<CustomerCoupon>> GetAllAsync()
        {
            var customerCoupon= await _appDbContext.CustomerCoupons
                .Include(cc=>cc.Customer)
                .Include(cc=>cc.Coupon)
                .ToListAsync();
            return customerCoupon;
        }

        public async Task<CustomerCoupon?> GetByIdAsync()
        {
            int staticID = 3;
            var customerCoupon= await _appDbContext.CustomerCoupons
                .Include(cc => cc.Customer)
                .Include(cc=>cc.Coupon)
                .FirstOrDefaultAsync(cc=>cc.CustomerCouponID== staticID);
            return customerCoupon;
        }

        public async Task AddAsync()
        {
            var customerCoupon = new CustomerCoupon
            {
                CustomerID = 1,
                CouponID = 3,
                RedeemedAt = DateTime.Now
            };
            await _appDbContext.CustomerCoupons.AddAsync(customerCoupon);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 1;
            var customerCoupon= await _appDbContext.CustomerCoupons
                .Include(cc=>cc.Customer)
                .Include(cc=>cc.Coupon)
                .FirstOrDefaultAsync(cc=>cc.CustomerCouponID== staticID);
            if (customerCoupon!=null)
            {
                if (customerCoupon.Customer != null)
                {
                    customerCoupon.Customer.FullName = "Ahmet Yılmaz";
                    customerCoupon.Customer.Age = 30;
                    customerCoupon.Customer.Address = "İstanbul, Beşiktaş, Barbaros Bulvarı No:12";
                    //...
                }

                if (customerCoupon.Coupon != null)
                {
                    customerCoupon.Coupon.Code = "WELCOME2025";
                    customerCoupon.Coupon.ValidFrom = DateTime.Now;
                    customerCoupon.Coupon.ValidUntil = DateTime.Now.AddMonths(1);
                    //...
                }

                customerCoupon.RedeemedAt = DateTime.Now.AddMonths(2);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 2;
            var customerCoupon = await _appDbContext.CustomerCoupons
                .Include(cc => cc.Customer)
                .Include(cc => cc.Coupon)
                .FirstOrDefaultAsync(cc=>cc.CustomerCouponID== staticID);
            if (customerCoupon!=null)
            {
                _appDbContext.CustomerCoupons.Remove(customerCoupon);
                await _appDbContext.SaveChangesAsync();
            }
        }

    
      
    }
}
