using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IVendorProductWithProductAndProductVendorStaticRepository
    {
        Task<List<VendorProduct>> GetAllAsync();
        Task<VendorProduct?> GetByIdAsync();
        Task AddAsync();
        Task UpdateAsync();
        Task DeleteAsync();
    }
}
