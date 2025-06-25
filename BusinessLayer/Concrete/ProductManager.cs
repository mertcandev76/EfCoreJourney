using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : GenericManager<Product>, IProductService
    {
        private readonly IProductRepository _repository;

        public ProductManager(IProductRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetAllWithBrandandCategoryAsync()=>await _repository.GetAllWithBrandandCategoryAsync();

        public async Task<Product?> GetByIdWithBrandandCategoryAsync(int id)=>await _repository.GetByIdWithBrandandCategoryAsync(id);
    }
}
