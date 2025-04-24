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
            return _appDbContext.Customers.ToList();
        }
        // Yeni müşteri ekleyen metod
        public void Insert(Customer customer)
        {
            // Varsayılan değerler atandı
            customer.Name = "lorem ipsum";
            customer.Email = "loremipsum@gmail.com";
            customer.Phone = "123456789";
            _appDbContext.Customers.Add(customer);
            // Değişiklikleri kaydediyoruz
            _appDbContext.SaveChanges();
        }



        // Müşteri güncelleyen metod
        public void Update(Customer customer)
        {
            var existingCustomer = GetById(customer.CustomerID);

            if (existingCustomer != null)
            {
                // Sabit verilerle güncelleme
                existingCustomer.Name = "updated lorem ipsum 2";  // Sabit yeni değer
                existingCustomer.Email = "updatedloremipsum@gmail.com 2";  // Sabit yeni değer
                existingCustomer.Phone = "987654321";  // Sabit yeni değer

                _appDbContext.SaveChanges();  // Güncellenen müşteri bilgilerini kaydet
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
