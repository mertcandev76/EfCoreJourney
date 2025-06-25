using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CustomerCouponManager:GenericManager<CustomerCoupon>,ICustomerCouponService
    {
        private readonly ICustomerCouponRepository _repository;

        public CustomerCouponManager(ICustomerCouponRepository repository):base(repository) 
        {
            _repository = repository;
        }

        public async Task<List<CustomerCoupon>> GetAllWithCustomerandCouponAsync()=>await _repository.GetAllWithCustomerandCouponAsync();

        public async Task<CustomerCoupon?> GetByIdWithCustomerandCouponAsync(int id) => await _repository.GetByIdWithCustomerandCouponAsync(id);
    }
}
