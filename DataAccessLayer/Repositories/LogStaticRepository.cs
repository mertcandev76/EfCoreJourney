using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class LogStaticRepository:ILogStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public LogStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

      

        public async Task<List<Log>> GetAllStaticLogsAsync()
        {
            return await _appDbContext.Logs.ToListAsync();
        }

        public async Task<Log?> GetStaticLogByIdAsync()
        {
            int staticID = 4;
            return await _appDbContext.Logs.FindAsync(staticID);
        }

        public async Task AddStaticLogAsync()
        {
            var log = new Log
            {
                LogDate = DateTime.Now,
                LogLevel = "INFO",
                Message = "Günlük Temizlenme Tamamlandı",
                Details = "7 günden yeni log kayıtları eklendi."
            }; 
            await _appDbContext.Logs.AddAsync(log);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateStaticLogAsync()
        {
            int staticID = 11;
            var log = await _appDbContext.Logs.FindAsync(staticID);
            if (log != null)
            {
                log.LogDate = DateTime.Now;
                log.LogLevel = "WARNING";
                log.Message = "Log güncellendi.";
                log.Details = "Statik ID ile güncelleme yapıldı.";

                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteStaticLogAsync()
        {
            int staticID = 11; // Silmek istediğin sabit ID
            var log = await _appDbContext.Logs.FindAsync(staticID);
            if (log != null)
            {
                _appDbContext.Logs.Remove(log);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
