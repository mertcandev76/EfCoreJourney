using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProductService:IGenericService<Product>
    {

        //Özel Metotlar
        Task<List<Product>> GetAllWithBrandandCategoryAsync();
        Task<Product?> GetByIdWithBrandandCategoryAsync(int id);
    }
}
