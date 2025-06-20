using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICouponStaticRepository
    {
        Task<List<Coupon>> GetAllAsync();
        Task<Coupon?> GetByIdAsync();
        Task AddAsync();
        Task UpdateAsync();
        Task DeleteAsync();
    }
}
