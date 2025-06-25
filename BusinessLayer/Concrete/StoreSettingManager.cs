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
    public class StoreSettingManager : GenericManager<StoreSetting>, IStoreSettingService
    {
        private readonly IStoreSettingRepository _repository;
        public StoreSettingManager(IStoreSettingRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<StoreSetting>> GetAllWithStoreAsync()
      => await _repository.GetAllWithStoreAsync();

        public async Task<StoreSetting?> GetByIdWithStoreAsync(int id)
            => await _repository.GetByIdWithStoreAsync(id);
    }
}
