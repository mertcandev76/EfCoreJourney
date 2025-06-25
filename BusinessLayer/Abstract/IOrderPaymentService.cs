using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderPaymentService:IGenericService<OrderPayment>
    {
        //Özel Metotlar
        Task<List<OrderPayment>> GetAllWithOrderAsync();
        Task<OrderPayment?> GetByIdWithOrderAsync(int id);
    }
}
