using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderPaymentRepository:IRepository<OrderPayment>
    {
        //Özel Metotlar
        Task<List<OrderPayment>> GetAllWithOrderAsync();
        Task<OrderPayment?> GetByIdWithOrderAsync(int id);
    }
}
