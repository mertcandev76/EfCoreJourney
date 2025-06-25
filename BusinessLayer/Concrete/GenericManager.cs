using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public GenericManager(IRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task<List<T>> GetAllAsync()=>await _repository.GetAllAsync();

        public async Task<T?> GetByIdAsync(int id)=> await _repository.GetByIdAsync(id);
        public async Task AddAsync(T entity)=>await _repository.AddAsync(entity);

        public async Task UpdateAsync(T entity)=>await _repository.UpdateAsync(entity);
        public async Task DeleteAsync(T entity)=>await _repository.DeleteAsync(entity);
       
    }
}
