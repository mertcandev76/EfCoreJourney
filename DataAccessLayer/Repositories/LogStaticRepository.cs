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

      

        public async Task<List<Log>> GetAllAsync()
        {
            return await _appDbContext.Logs.ToListAsync();
        }

        public async Task<Log?> GetByIdAsync()
        {
            int staticID = 1;
            return await _appDbContext.Logs.FindAsync(staticID);
        }

        public async Task AddAsync()
        {
            var log = new Log
            {
                LogDate = DateTime.Now,
                LogLevel = "INFO",
                Message = "Log temizleme işlemi başarıyla tamamlandı.",
                Details = "Sistemdeki 7 günden eski log kayıtları silindi ve yeni log kayıtları eklendi.",
                Source = "LogCleanerService",
                User = "SystemAdmin"
            }; 
            await _appDbContext.Logs.AddAsync(log);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            int staticID = 1;
            var log = await _appDbContext.Logs.FindAsync(staticID);
            if (log != null)
            {
                log.LogDate = DateTime.Now;
                log.LogLevel = "WARNING";
                log.Message = "Log kaydı güncellendi.";
                log.Details = "Belirli bir statik ID üzerinden log kaydında güncelleme işlemi gerçekleştirildi.";

                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 1; // Silmek istediğin sabit ID
            var log = await _appDbContext.Logs.FindAsync(staticID);
            if (log != null)
            {
                _appDbContext.Logs.Remove(log);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
