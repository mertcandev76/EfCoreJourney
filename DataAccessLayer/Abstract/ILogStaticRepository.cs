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
        Task<List<Log>> GetAllStaticLogsAsync();
        Task<Log?> GetStaticLogByIdAsync(); // Static id
        Task AddStaticLogAsync(); //Static Ekleme
        Task UpdateStaticLogAsync(); //Static Güncelleme
        Task DeleteStaticLogAsync(); //  Statik Silme
    }
}
