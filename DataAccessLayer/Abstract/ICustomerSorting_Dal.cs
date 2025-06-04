using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerSorting_Dal
    {
        //-->OrderBy Kullanımı

        //Ad Soyada göre artan sıralama (FirstName)
        Task<List<Customer>> GetCustomersOrderedByFirstNameAsync();
      
        //Aktif müşterileri soyada göre sıralama (LastName)
        Task<List<Customer>> GetActiveCustomersOrderedByLastNameAsync();
        //Yaşı olan (null olmayan) müşterileri yaşa göre sıralama
        Task<List<Customer>> GetCustomersWithAgeOrderedAsync();
        // Ad + Soyad birleştirilmiş şekilde sıralama
        Task<List<Customer>> GetCustomersOrderedByFullNameAsync();

        //-->OrderByDescending Kullanımı
        //Yaşa göre azalan sıralama (En yaşlı en önde)
        Task<List<Customer>> GetCustomersByAgeDescAsync();
        // Ada göre azalan sıralama (Z’den A’ya)
        Task<List<Customer>> GetCustomersByFirstNameDescAsync();
        // Email’e göre azalan sıralama
        Task<List<Customer>> GetCustomersByEmailDescAsync();
        // Sadece aktif müşterileri LastName'e göre azalan sıralama
        Task<List<Customer>> GetActiveCustomersByLastNameDescAsync();
        //Telefon numarasına göre azalan sıralama (null olmayanlar)
        Task<List<Customer>> GetCustomersByPhoneDescAsync();

        //-->ThenBy ve ThenByDescending Kullanımı

        //->ThenBy ve ThenByDescending Kuralı
        /*ThenBy ve ThenByDescending, sıralamada ikinci(veya üçüncü, dördüncü...) bir ölçüt belirtmek için kullanılır.
        Örnek
        .OrderBy(c => c.LastName)      // Önce soyada göre sıralar
        .ThenBy(c => c.FirstName)      // Soyadı aynı olanları ada göre sıralar (A-Z)*/

        //Soyada göre, sonra ada göre artan sıralama
        Task<List<Customer>> GetCustomersByLastNameThenFirstNameAsync();
        //Yaşa göre azalan, sonra ada göre artan sıralama
        Task<List<Customer>> GetCustomersByAgeThenFirstNameAsync();
        //Aktif müşteriler: Ada göre, sonra soyada göre
        Task<List<Customer>> GetActiveCustomersByNameAsync();

        //Önce ada göre artan, sonra soyada göre azalan sıralama
        Task<List<Customer>> GetCustomersByFirstNameThenLastNameDescAsync();
        //Yaşa göre azalan, sonra e-postaya göre azalan sıralama
        Task<List<Customer>> GetCustomersByAgeThenEmailDescAsync();

    }
}
