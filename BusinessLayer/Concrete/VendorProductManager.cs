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
    public class VendorProductManager:GenericManager<VendorProduct>,IVendorProductService
    {
        private readonly IVendorProductRepository _repository;

        public VendorProductManager(IVendorProductRepository repository):base(repository)
        {
            _repository = repository;
        }

        public async Task<List<VendorProduct>> GetAllWithProductandProductVendorAsync() => await _repository.GetAllWithProductandProductVendorAsync();

        public async Task<VendorProduct?> GetByIdWithProductandProductVendorAsync(int id) => await _repository.GetByIdWithProductandProductVendorAsync(id);
    }
}
