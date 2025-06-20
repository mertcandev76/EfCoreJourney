using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Repositories
{
    public class StoreSettingWithStoreStaticRepository: IStoreSettingWithStoreStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public StoreSettingWithStoreStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<StoreSetting>> GetAllAsync()
        {
            var storeSetting=await _appDbContext.StoreSettings
                .Include(ss=>ss.Store)
                .ToListAsync();
            return storeSetting;
        }

        public async Task<StoreSetting?> GetByIdAsync()
        {
            int staticID = 2;
            var storeSetting= await _appDbContext.StoreSettings
                .Include(ss=>ss.Store)
                .FirstOrDefaultAsync(ss=>ss.StoreSettingID== staticID);
            return storeSetting;
        }
        public async Task AddAsync()
        {
            var storeSetting = new StoreSetting
            {
                Currency = "USD",             
                Language = "English",           
                EnableNotifications = false,   
                StoreId = 2                    
            };
            await _appDbContext.StoreSettings.AddAsync(storeSetting);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            int staticID = 2;

            var storeSetting = await _appDbContext.StoreSettings
                .Include(ss => ss.Store)
                .FirstOrDefaultAsync(ss => ss.StoreSettingID == staticID);

            if (storeSetting != null)
            {
                // StoreSetting tablosunu güncelle
                storeSetting.Currency = "USD";                
                storeSetting.Language = "English";            
                storeSetting.EnableNotifications = true;      

                // İlişkili Store tablosunu da güncelle
                if (storeSetting.Store != null)
                {
                    storeSetting.Store.Name = "Mertcan's Store";         
                    storeSetting.Store.OwnerName = "Mertcan Adsız";      
                    storeSetting.Store.Email = "mertcan@example.com";     
                }

                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync()
        {
            int staticID = 2;
            var storeSetting = await _appDbContext.StoreSettings
            .Include(ss => ss.Store)
            .FirstOrDefaultAsync(ss => ss.StoreSettingID == staticID);
            if (storeSetting != null)
            {
                _appDbContext.StoreSettings.Remove(storeSetting);
                await _appDbContext.SaveChangesAsync();
            }
        }

       

       
    }
}
