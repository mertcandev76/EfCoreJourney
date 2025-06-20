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
    public class ProductWithProductBrandStaticRepository: IProductWithProductBrandStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductWithProductBrandStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var product= await _appDbContext.Products
                .Include(p=>p.ProductBrand)
                .ToListAsync();
            return product;
        }

        public async Task<Product?> GetByIdAsync()
        {
            int staticID = 1002;
            var product= await _appDbContext.Products
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.ProductID==staticID);
            return product;
        }
        public async Task AddAsync()
        {
            var product = new Product
            {
                Name = "Samsung Galaxy S21",
                Description = "Yüksek performanslı, 128GB depolama kapasiteli akıllı telefon.",
                Price = 7499.99m,
                Stock = 50,
                IsActive = true,
                ProductBrandID = 1

            };
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 1004;
            var product= await _appDbContext.Products
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.ProductID==staticID);
            if (product!=null)
            {
                product.Name = "Apple iPhone 14 Pro";
                product.Description = "Gelişmiş kamera sistemi ve yüksek performanslı akıllı telefon.";
                product.Price = 15999.99m;
                product.Stock = 17;
                product.IsActive = false;
            

                if (product.ProductBrand != null)
                {
                    product.ProductBrand.Name = "Apple";
                    product.ProductBrand.Description = "Dünyaca ünlü teknoloji ve elektronik ürünler markası.";
                    product.ProductBrand.Country = "Amerika Birleşik Devletleri";
                }
                await _appDbContext.SaveChangesAsync();
                
            }

        }
        public async Task DeleteAsync()
        {
            int staticID = 1003;
            var product= await _appDbContext.Products
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.ProductID== staticID);
            if (product!=null)
            {
                _appDbContext.Products.Remove(product);
                await _appDbContext.SaveChangesAsync();
            }
        }

    }
}
