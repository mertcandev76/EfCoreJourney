using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICustomerCouponService:IGenericService<CustomerCoupon>
    {
        //Özel Metotlar
        Task<List<CustomerCoupon>> GetAllWithCustomerandCouponAsync();
        Task<CustomerCoupon?> GetByIdWithCustomerandCouponAsync(int id);
    }
}
