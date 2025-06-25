using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStoreSettingRepository:IRepository<StoreSetting>
    {
        //Özel Metotlar
        Task<List<StoreSetting>> GetAllWithStoreAsync();
        Task<StoreSetting?> GetByIdWithStoreAsync(int id);

    }
}
