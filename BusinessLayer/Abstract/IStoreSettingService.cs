using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStoreSettingService : IGenericService<StoreSetting>
    {
        Task<List<StoreSetting>> GetAllWithStoreAsync();
        Task<StoreSetting?> GetByIdWithStoreAsync(int id);
    }
}
