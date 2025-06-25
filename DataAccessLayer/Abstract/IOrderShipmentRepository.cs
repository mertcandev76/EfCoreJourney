using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderShipmentRepository : IRepository<OrderShipment>
    {
        //Özel Metotlar
        Task<List<OrderShipment>> GetAllWithOrderAsync();
        Task<OrderShipment?> GetByIdWithOrderAsync(int id);
    }
}
