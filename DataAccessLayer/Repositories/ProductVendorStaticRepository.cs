using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductVendorStaticRepository : IProductVendorStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductVendorStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<ProductVendor>> GetAllAsync()
        {
            var productVendor = await _appDbContext.ProductVendors
                .ToListAsync();
            return productVendor;
        }

        public async Task<ProductVendor?> GetByIdAsync()
        {
            int staticID = 2;
            var productVendor = await _appDbContext.ProductVendors
                .FindAsync(staticID);
            return productVendor;
        }

        public async Task AddAsync()
        {
            var productVendor = new ProductVendor
            {
                CompanyName = "GlobalTech Solutions",
                ContactPerson = "Ayşe Yılmaz",
                Email = "ayse.yilmaz@globaltech.com",
                Phone = "+90 212 555 1234"
            };
            await _appDbContext.ProductVendors.AddAsync(productVendor);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            int staticID = 1;
            var productVendor = await _appDbContext.ProductVendors.FindAsync(staticID);
            if (productVendor != null)
            {
                productVendor.CompanyName = "TechSolutions Ltd.";
                productVendor.ContactPerson = "Ahmet Demir";
                productVendor.Email = "ahmet.demir@techsolutions.com";
                productVendor.Phone = "+90 532 987 6543";
                await _appDbContext.SaveChangesAsync();
            }

        }


        public async Task DeleteAsync()
        {
            int staticID = 1;
            var productVendor = await _appDbContext.ProductVendors
                .FindAsync(staticID);
            if (productVendor != null)
            {
                _appDbContext.ProductVendors.Remove(productVendor);
                await _appDbContext.SaveChangesAsync();
            }
        }

    }


}
