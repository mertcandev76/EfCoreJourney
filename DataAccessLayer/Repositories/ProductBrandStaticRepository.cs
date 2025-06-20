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
    public class ProductBrandStaticRepository: IProductBrandStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductBrandStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<ProductBrand>> GetAllAsync()
        {
            var productBrand= await _appDbContext.ProductBrands
                .ToListAsync();
            return productBrand;
        }

        public async Task<ProductBrand?> GetByIdAsync()
        {
            int staticID = 2;
            var productBrand = await _appDbContext.ProductBrands
                .FindAsync(staticID);
            return productBrand;
        }
        public async Task AddAsync()
        {
            var productBrand = new ProductBrand
            {
                Name = "Arçelik",
                Description = "Ev elektroniği ve beyaz eşya ürünleri markası.",
                Country = "Türkiye"
            };
            await _appDbContext.ProductBrands.AddAsync(productBrand);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 3;
           var productBrand= await _appDbContext.ProductBrands
                .FindAsync(staticID);
            if (productBrand!=null)
            {
                productBrand.Name = "Bosch";
                productBrand.Description = "Alman menşeli, beyaz eşya ve elektronik ürünler markası.";
                productBrand.Country = "Almanya";
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync()
        {
            int staticID = 4;
            var productBrand= await _appDbContext.ProductBrands
                .FindAsync(staticID);
            if (productBrand!=null)
            {
                _appDbContext.ProductBrands.Remove(productBrand);
                await _appDbContext.SaveChangesAsync();
            }
          
        }      
    }
}
