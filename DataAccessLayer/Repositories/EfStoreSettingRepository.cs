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
    public class EfStoreSettingRepository : GenericRepository<StoreSetting>, IStoreSettingRepository
    {

        public EfStoreSettingRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<StoreSetting>> GetAllWithStoreAsync()
        {
           var storeSetting= await  Table
                .Include(ss=>ss.Store)
                .ToListAsync();
            return storeSetting;
        }

        public async Task<StoreSetting?> GetByIdWithStoreAsync(int id)
        {
            var storeSetting= await Table
                .Include(ss => ss.Store)
                .FirstOrDefaultAsync(ss=>ss.StoreSettingID==id);
            return storeSetting;
        }
    }
}
