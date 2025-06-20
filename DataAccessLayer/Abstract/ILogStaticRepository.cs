using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ILogStaticRepository
    {
        Task<List<Log>> GetAllAsync();
        Task<Log?> GetByIdAsync(); // Static id
        Task AddAsync(); //Static Ekleme
        Task UpdateAsync(); //Static Güncelleme
        Task DeleteAsync(); //  Statik Silme
    }
}
