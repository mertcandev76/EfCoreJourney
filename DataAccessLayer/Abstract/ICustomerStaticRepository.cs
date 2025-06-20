using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerStaticRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync();
        Task AddAsync();
        Task UpdateAsync();
        Task DeleteAsync();
    }
}
