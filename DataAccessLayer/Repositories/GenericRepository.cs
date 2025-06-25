using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T>: IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public DbSet<T> Table { get => _appDbContext.Set<T>(); }
        public async Task<List<T>> GetAllAsync() => await Table.ToListAsync();
        

        public async Task<T?> GetByIdAsync(int id) => await Table.FindAsync(id);
        
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            Table.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
