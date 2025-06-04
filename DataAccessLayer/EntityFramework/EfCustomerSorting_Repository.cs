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
    public class EfCustomerSorting_Repository: ICustomerSorting_Dal
    {
        private readonly AppDbContext _appDbContext;

        public EfCustomerSorting_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        //-->OrderBy Kullanımı

        //Ada göre artan sıralama (FirstName)
        public async Task<List<Customer>> GetCustomersOrderedByFirstNameAsync()
        {
            return await _appDbContext.Customers
                .OrderBy(x=>x.FirstName)
                .ToListAsync();
        }
      
        //Aktif müşterileri soyada göre sıralama (LastName)
        public async Task<List<Customer>> GetActiveCustomersOrderedByLastNameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==true)
                .OrderBy(x=>x.LastName)
                .ToListAsync();
        }
        //Yaşı olan (null olmayan) müşterileri yaşa göre sıralama
        public async Task<List<Customer>> GetCustomersWithAgeOrderedAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age!=null)
                .OrderBy(x=>x.Age)
                .ToListAsync();
        }
        // Ad + Soyad birleştirilmiş şekilde sıralama
        public async Task<List<Customer>> GetCustomersOrderedByFullNameAsync()
        {
            return await _appDbContext.Customers
                .OrderBy(x=>x.FirstName+" "+x.LastName)
                .ToListAsync();
        }

        //-->OrderByDescending Kullanımı
        //Yaşa göre azalan sıralama (En yaşlı en önde)
        public async Task<List<Customer>> GetCustomersByAgeDescAsync()
        {
            return await _appDbContext.Customers
                .OrderByDescending(x=>x.Age)
                .ToListAsync();
        }
        // Ada göre azalan sıralama (Z’den A’ya)
        public async Task<List<Customer>> GetCustomersByFirstNameDescAsync()
        {
            return await _appDbContext.Customers
                .OrderByDescending(x=>x.FirstName)
                .ToListAsync();
        }
        // Email’e göre azalan sıralama
        public async Task<List<Customer>> GetCustomersByEmailDescAsync()
        {
            return await _appDbContext.Customers
                .OrderByDescending(x=>x.Email)
                .ToListAsync();
        }
        // Sadece aktif müşterileri LastName'e göre azalan sıralama
        public async Task<List<Customer>> GetActiveCustomersByLastNameDescAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==true)
                .OrderByDescending(x=>x.LastName)
                .ToListAsync();
        }
        //Telefon numarasına göre azalan sıralama (null olmayanlar)
        public async Task<List<Customer>> GetCustomersByPhoneDescAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Phone!=null)
                .OrderByDescending(x=>x.Phone)
                .ToListAsync();
        }

        //-->ThenBy ve ThenByDescending Kullanımı

        //->ThenBy ve ThenByDescending Kuralı
        /*ThenBy ve ThenByDescending, sıralamada ikinci(veya üçüncü, dördüncü...) bir ölçüt belirtmek için kullanılır.
        Örnek
        .OrderBy(c => c.LastName)      // Önce soyada göre sıralar
        .ThenBy(c => c.FirstName)      // Soyadı aynı olanları ada göre sıralar (A-Z)*/

        //Soyada göre, sonra ada göre artan sıralama
        public async Task<List<Customer>> GetCustomersByLastNameThenFirstNameAsync()
        {
            return await _appDbContext.Customers
                .OrderBy(x=>x.LastName)
                .ThenBy(x=>x.FirstName)
                .ToListAsync();
        }
        //Yaşa göre azalan, sonra ada göre artan sıralama
        public async Task<List<Customer>> GetCustomersByAgeThenFirstNameAsync()
        {
            return await _appDbContext.Customers
                .OrderByDescending(x=>x.Age)
                .ThenBy(x=>x.FirstName)
                .ToListAsync();
        }
        //Aktif müşteriler: Ada göre, sonra soyada göre
        public async Task<List<Customer>> GetActiveCustomersByNameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==true)
                .OrderBy(x=>x.FirstName)
                .ThenBy(x=>x.LastName)
                .ToListAsync();
        }

        //Önce ada göre artan, sonra soyada göre azalan sıralama
        public async Task<List<Customer>> GetCustomersByFirstNameThenLastNameDescAsync()
        {
            return await _appDbContext.Customers
                .OrderBy(x=>x.FirstName)
                .ThenByDescending(x=>x.LastName)
                .ToListAsync();
        }
        //Yaşa göre azalan, sonra e-postaya göre azalan sıralama
        public async Task<List<Customer>> GetCustomersByAgeThenEmailDescAsync()
        {
            return await _appDbContext.Customers
                .OrderByDescending(x=>x.Age)
                .ThenByDescending(x=>x.Email)
                .ToListAsync();
        }
    }
}
