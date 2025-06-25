using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IVendorProductService:IGenericService<VendorProduct>
    {
        //Özel Metotlar
        //Özel Metotlar
        Task<List<VendorProduct>> GetAllWithProductandProductVendorAsync();
        Task<VendorProduct?> GetByIdWithProductandProductVendorAsync(int id);
    }
}
