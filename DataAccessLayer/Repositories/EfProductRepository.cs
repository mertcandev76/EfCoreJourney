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
    public class EfProductRepository : GenericRepository<Product>, IProductRepository
    {
        public EfProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Product>> GetAllWithBrandandCategoryAsync()
        {
            return await Table
                .Include(p=>p.ProductBrand)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdWithBrandandCategoryAsync(int id)
        {
            return await Table
                .Include(p=>p.ProductBrand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p=>p.ProductID==id);
        }
    }
}
