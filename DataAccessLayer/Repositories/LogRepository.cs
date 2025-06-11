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
    public class LogRepository:ILogRepository
    {
        private readonly AppDbContext _appDbContext;

        public LogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Log>> GetAllLogsAsync()
        {
            return await _appDbContext.Logs.ToListAsync();
        }
        public async Task<Log?> GetLogByIdAsync(int id)
        {
            return await _appDbContext.Logs.FindAsync(id);
        }
        public async Task DeleteLogAsync(int id)
        {
            var logs= await _appDbContext.Logs.FindAsync(id);
            if (logs != null)
            {
                _appDbContext.Logs.Remove(logs);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task AddLogAsync(Log log)
        {
             await _appDbContext.Logs.AddAsync(log);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateLogAsync(Log log)
        {
            _appDbContext.Logs.Update(log);
            await _appDbContext.SaveChangesAsync();
        }

       

    }
}
