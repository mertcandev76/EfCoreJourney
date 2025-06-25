using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderRepository:IRepository<Order>
    {
        //Özel Metotlar
        Task<List<Order>> GetAllWithCustomerAsync();
        Task<Order?> GetByIdWithCustomerAsync(int id);
    }
}
