using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        //Özel Metotlar
        Task<List<Product>> GetAllWithBrandandCategoryAsync();
        Task<Product?> GetByIdWithBrandandCategoryAsync(int id);
    }
}
