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
    public class VendorProductWithProductAndProductVendorStaticRepository : IVendorProductWithProductAndProductVendorStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public VendorProductWithProductAndProductVendorStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<VendorProduct>> GetAllAsync()
        {
            var vendorProduct= await _appDbContext.VendorProducts
                .Include(vp=>vp.Product)
                .Include(vp=>vp.ProductVendor)
                .ToListAsync();
            return vendorProduct;
        }

        public async Task<VendorProduct?> GetByIdAsync()
        {
            int staticID = 3;
            var vendorProduct= await _appDbContext.VendorProducts
                .Include(vp=>vp.Product)
                .Include(vp=>vp.ProductVendor)
                .FirstOrDefaultAsync(vp=>vp.VendorProductID== staticID);
            return vendorProduct;
        }

        public async Task AddAsync()
        {
            var vendorProduct = new VendorProduct
            {
                ProductVendorID = 1,
                ProductID=1004,
              
            };

            await _appDbContext.VendorProducts.AddAsync(vendorProduct);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 2;
            var vendorProduct= await _appDbContext.VendorProducts
                .Include(vp => vp.Product)
                .Include(vp => vp.ProductVendor)
                .FirstOrDefaultAsync(vp=>vp.VendorProductID== staticID);
            if (vendorProduct!=null)
            {
                if (vendorProduct.Product != null)
                {
                    vendorProduct.Product.Name = "Samsung Galaxy S22";
                    vendorProduct.Product.Description = "Yüksek performanslı akıllı telefon, 128GB depolama.";
                    //...

                }
                if (vendorProduct.ProductVendor!=null)
                {
                    vendorProduct.ProductVendor.CompanyName = "GlobalTech A.Ş.";
                    vendorProduct.ProductVendor.ContactPerson = "Mehmet Yılmaz";
                    //...
                }

                await _appDbContext.SaveChangesAsync();
                  
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 2;
            var vendorProduct = await _appDbContext.VendorProducts
                .Include(vp => vp.Product)
                .Include(vp => vp.ProductVendor)
                .FirstOrDefaultAsync(vp=>vp.VendorProductID== staticID);
            if (vendorProduct!=null)
            {
                 _appDbContext.VendorProducts.Remove(vendorProduct);
                await _appDbContext.SaveChangesAsync();
            }
        }

       
     
    }
}
