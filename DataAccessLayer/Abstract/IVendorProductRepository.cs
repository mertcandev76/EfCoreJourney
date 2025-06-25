using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IVendorProductRepository:IRepository<VendorProduct>
    {
        //Özel Metotlar
        Task<List<VendorProduct>> GetAllWithProductandProductVendorAsync();
        Task<VendorProduct?> GetByIdWithProductandProductVendorAsync(int id);
    }
}
