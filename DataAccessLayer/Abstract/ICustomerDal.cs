using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerDal
    {
        Customer GetById(int id);  // ID ile müşteri al
        Task<List<Customer>> GetAll();  // Asenkron metodun dönüş tipi 
        void Insert(Customer customer);

        void Update(Customer customer);

        void Delete(Customer customer);
    }
}
