using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderPaymentWithOrderStaticRepository
    {
        Task<List<OrderPayment>> GetAllAsync();
        Task<OrderPayment?> GetByIdAsync();
        Task AddAsync();
        Task UpdateAsync();
        Task DeleteAsync();
    }
}
