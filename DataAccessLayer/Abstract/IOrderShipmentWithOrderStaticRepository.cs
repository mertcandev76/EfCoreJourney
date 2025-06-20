using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderShipmentWithOrderStaticRepository
    {
        Task<List<OrderShipment>> GetAllAsync();
        Task<OrderShipment?> GetByIdAsync();
        Task AddAsync();
        Task UpdateAsync();
        Task DeleteAsync();
    }
}
