using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerRepository:ICustomerDal
    {

        //AppDbContext sınfı manuel olsaydı aşağıdaki kodu yapıyoruz.
        //AppDbContext appDbContext = new AppDbContext();

        private readonly AppDbContext _appDbContext;

        public EfCustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // ID ile müşteri getiren metod
        public Customer GetById(int id)
        {
            return _appDbContext.Customers.FirstOrDefault(c => c.CustomerID == id);
        }


        // Tüm müşterileri listeleyen metod
        public List<Customer> GetAll()
        {
            var customer = _appDbContext.Customers.ToList();
            return customer;
        }


        // Yeni müşteri ekleyen metod(Dynamic)
        public void Insert(Customer customer)
        {
            _appDbContext.Customers.Add(customer);
            _appDbContext.SaveChanges();
        }



        // Müşteri güncelleyen metod(Dynamic)
        public void Update(Customer customer)
        {
            var existingCustomer = GetById(customer.CustomerID); // ID'ye göre mevcut müşteri verisini al

            if (existingCustomer != null)
            {
                // Gelen customer objesindeki verilerle mevcut veriyi güncelliyoruz
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;

                // Veritabanına kaydediyoruz
                _appDbContext.SaveChanges();
            }
        }
        // Müşteri silme metod
        public void Delete(Customer customer)
        {
            var existingCustomer = GetById(customer.CustomerID);
            if (existingCustomer != null)
            {

                _appDbContext.Customers.Remove(existingCustomer); // Müşteri silinir
                _appDbContext.SaveChanges(); // Değişiklik veritabanına kaydedilir
            }
        }










    }
}
