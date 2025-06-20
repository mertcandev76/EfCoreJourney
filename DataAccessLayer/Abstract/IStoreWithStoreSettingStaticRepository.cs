using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStoreWithStoreSettingStaticRepository
    {
        Task<List<Store>> GetAllAsync(); //Listeleme
        Task<Store?> GetByIdAsync(); //Static ID ile Getirme
        Task AddAsync(); //Static İlişkisel Ekleme
        Task UpdateAsync(); //Static İlişkisel Güncelleme
        Task DeleteAsync(); //Static İlişkisel Silme
    }
}
