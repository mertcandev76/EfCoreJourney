using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderShipmentService : IGenericService<OrderShipment>
    {
        //Özel Metotlar
        Task<List<OrderShipment>> GetAllWithOrderAsync();
        Task<OrderShipment?> GetByIdWithOrderAsync(int id);
    }
}
