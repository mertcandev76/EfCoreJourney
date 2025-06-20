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
    public class StoreWithStoreSettingStaticRepository: IStoreWithStoreSettingStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public StoreWithStoreSettingStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<List<Store>> GetAllAsync()
        {
            var store= await _appDbContext.Stores 
                .ToListAsync();
            return store;
        }

        public async Task<Store?> GetByIdAsync()
        {
            int staticID = 1;
            var store= await _appDbContext.Stores
                .FindAsync(staticID);
            return store;

        }

        public async Task AddAsync()
        {
            var store = new Store
            {
                Name = "Mertcan's Store",
                OwnerName = "Mertcan Adsız",
                Email = "mertcan.adsiz@example.com"

            };

            // Fluent API sayesinde EF Core burada StoreSetting.Store ilişkisini otomatik olarak kurar.

            await _appDbContext.Stores.AddAsync(store);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            int staticID = 1;
            var store = await _appDbContext.Stores
                .FindAsync(staticID);
            if (store!=null)
            {
                store.Name = "Mertcan's Updated Store";
                store.OwnerName = "Mertcan Adsız";
                store.Email = "mertcan.updated@example.com";

                await _appDbContext.SaveChangesAsync();
            }
           
        }

        public async Task DeleteAsync()
        {
            int staticID = 1;
            var store = await _appDbContext.Stores             
                .FindAsync(staticID);
            if (store!=null)
            {
                _appDbContext.Stores.Remove(store);
                await _appDbContext.SaveChangesAsync();
            }
           
        }

       
    }
}
