using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderDetailService:IGenericService<OrderDetail>
    {
        //Özel Metotlar
        Task<List<OrderDetail>> GetAllWithOrderandProductAsync();
        Task<OrderDetail?> GetByIdWithOrderandProductAsync(int id);
    }
}
