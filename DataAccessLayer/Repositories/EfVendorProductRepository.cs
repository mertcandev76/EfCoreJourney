using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EfVendorProductRepository : GenericRepository<VendorProduct>, IVendorProductRepository
    {
        public EfVendorProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<VendorProduct>> GetAllWithProductandProductVendorAsync()
        {
            return await Table
                .Include(vp=>vp.Product)
                 .Include(vp => vp.ProductVendor)
                .ToListAsync();
        }

        public async Task<VendorProduct?> GetByIdWithProductandProductVendorAsync(int id)
        {
            return await Table
                 .Include(vp => vp.Product)
                  .Include(vp => vp.ProductVendor)
                 .FirstOrDefaultAsync(vp=>vp.VendorProductID==id);
        }
    }
}
