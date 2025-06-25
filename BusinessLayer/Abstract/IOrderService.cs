using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderService:IGenericService<Order>
    {
        //Özel Metotlar
        Task<List<Order>> GetAllWithCustomerAsync();
        Task<Order?> GetByIdWithCustomerAsync(int id);
    }
}
