using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerElement_Dal
    {
        //-->FirstAsync() Kullanımı
        //ID’si 1 olan ilk müşteriyi getir
        Task<Customer?> GetByIdAsync();
        //Adı "Ali" olan ilk müşteriyi getir
        Task<Customer?> GetByFirstNameAsync();

        //-->FisrtOrDefaultAsync() Kullaımı
        //Aktif olmayan ilk müşteriyi getir (yoksa null)
        Task<Customer?> GetFirstInactiveOrNullAsync();
        //Adı null olmayan ve 18 yaşından küçük ilk müşteriyi getir
        Task<Customer?> GetMinorCustomerWithNameAsync();
        //Telefon numarası "0530" ile başlayan ilk müşteriyi getir
        Task<Customer?> GetByPhonePrefixOrNullAsync();
        //Adı "Ayşe" olan ve e-postası null olmayan ilk müşteriyi getir
        Task<Customer?> GetAyseWithEmailAsync();
        //Belirli bir e-posta adresine sahip ilk müşteriyi getir (yoksa null döner)
        Task<Customer?> GetByEmailOrNullAsync(string email);

        //-->LastAsync() ve LastOrDefaultAsync() Kullanımı
        //En son eklenen müşteriyi getir (ID’ye göre sıralanmış)
        Task<Customer> GetLastCustomerAsync();
        //Adı "Mehmet" olan son müşteriyi getir
        Task<Customer> GetLastMehmetAsync();
        //Email adresi null olmayan son müşteriyi getir
        Task<Customer> GetLastWithEmailAsync();
        //Yaşı 30’dan büyük olan son müşteriyi getir
        Task<Customer> GetLastAdultOver30Async();

        //-->SingleAsync() ve SingleOrDefaultAsync() Kullanımı

        //-->SingleAsync: Şarta uyan tam olarak bir kayıt varsa sonucu döner,yoksa veya birden fazla varsa hata fırlatır(InvalidOperationException).

        //E-posta adresi eşleşen tek kullanıcıyı getir
        Task<Customer> GetByEmailAsync(string email);
        // Belirli ID'ye sahip tek kullanıcıyı getir
        Task<Customer> GetByIdAsync(int id);
        //Tek bir telefon numarasına sahip müşteri getir
        Task<Customer> GetByPhoneAsync(string phone);

        //-->SingleOrDefaultAsync: Şarta uyan 0 veya 1 kayıt varsa sonucu döner,birden fazla varsa hata verir,hiç kayıt yoksa null döner.

        //E-posta ile kullanıcıyı getir (yoksa null)
        Task<Customer?> GetBySingleEmailOrNullAsync(string email);
        //Yaşı 99 olan kullanıcıyı getir (istisnai bir yaş)
        Task<Customer?> GetOldestCustomerAsync();
        //Adı "Admin" olan müşteri varsa getir
        Task<Customer?> GetAdminCustomerAsync();


        //-->FindAsync Kullanımı
        //FindAsync, yalnızca birincil anahtar (primary key) üzerinden arama yapar.EF Core, önce bellekte(DbContext’in içinde) arar, sonra gerekirse veritabanına sorgu gönderir.Genellikle performanslıdır.

        //CustomerID ile müşteri getir
        Task<Customer?> GetByFindIdAsync(int id);
        //ID’si 10 olan müşteriyi getir (sabit değer)
        Task<Customer?> GetCustomer10Async();
        // Formdan gelen ID’ye göre müşteri getir
        Task<Customer?> GetCustomerFromFormAsync(Customer customerForm);
        //Giriş yapan kullanıcı ID’si ile müşteri getir
        Task<Customer?> GetLoggedInCustomerAsync(int loggedInUserId);


    }
}
