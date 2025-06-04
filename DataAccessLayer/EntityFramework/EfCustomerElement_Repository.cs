using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerElement_Repository: ICustomerElement_Dal
    {
        private readonly AppDbContext _appDbContext;

        public EfCustomerElement_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //-->FirstAsync() Kullanımı
        //ID’si 1 olan ilk müşteriyi getir
        public async Task<Customer?> GetByIdAsync()
        {
            return await _appDbContext.Customers
                .FirstAsync(x=>x.CustomerID==1);
        }
        //Adı "Ali" olan ilk müşteriyi getir
        public async Task<Customer?> GetByFirstNameAsync()
        {
             return await _appDbContext.Customers
                .FirstAsync(x=>x.FirstName=="Ali");
        }

        //-->FirstOrDefaultAsync() Kullaımı
        //Aktif olmayan ilk müşteriyi getir (yoksa null)
        public async Task<Customer?> GetFirstInactiveOrNullAsync()
        {
            return await _appDbContext.Customers
               .FirstOrDefaultAsync(x=>x.IsActive==false);
        }
        //Adı null olmayan ve 18 yaşından küçük ilk müşteriyi getir
        public async Task<Customer?> GetMinorCustomerWithNameAsync()
        {
            return await _appDbContext.Customers
                .FirstOrDefaultAsync(x=>x.Age<18 && x.FirstName !=null);
        }
        //Telefon numarası "0530" ile başlayan ilk müşteriyi getir
        public async Task<Customer?> GetByPhonePrefixOrNullAsync()
        {
            return await _appDbContext.Customers
                .FirstOrDefaultAsync(x=>x.Phone.StartsWith("0530"));
        }
        //Adı "Ayşe" olan ve e-postası null olmayan ilk müşteriyi getir
        public async Task<Customer?> GetAyseWithEmailAsync()
        {
            return await _appDbContext.Customers.
                FirstOrDefaultAsync(x=>x.FirstName=="Ayşe" && x.Email!=null);
        }
        //Belirli bir e-posta adresine sahip ilk müşteriyi getir (yoksa null döner)
        public async Task<Customer?> GetByEmailOrNullAsync(string email)
        {
            return await _appDbContext.Customers
                .FirstOrDefaultAsync(x=>x.Email==email);
        }

        //-->LastAsync() ve LastOrDefaultAsync() Kullanımı
        //En son eklenen müşteriyi getir (ID’ye göre sıralanmış)
        public async Task<Customer> GetLastCustomerAsync()
        {
            return await _appDbContext.Customers
                .OrderBy(x=>x.CustomerID)
                .LastAsync();
        }
        //Adı "Mehmet" olan son müşteriyi getir
        public async Task<Customer> GetLastMehmetAsync()
        {
            //1.yol
            /*
            return await _appDbContext.Customers
                .OrderBy(x=>x.CustomerID)
                .Where(x=>x.FirstName=="Mehmet")
                .LastOrDefaultAsync();*/
            //2.yol
            return await _appDbContext.Customers
                .OrderBy(x=>x.CustomerID)
                .LastOrDefaultAsync(x=>x.FirstName=="Mehmet");
        }
        //Email adresi null olmayan son müşteriyi getir
        public async Task<Customer> GetLastWithEmailAsync()
        {
            return await _appDbContext.Customers
                .OrderBy(x=>x.CustomerID)
                .LastOrDefaultAsync(x=>x.Email!=null);
        }
        //Yaşı 30’dan büyük olan son müşteriyi getir
        public async Task<Customer> GetLastAdultOver30Async()
        {
            return await _appDbContext.Customers
                .OrderBy(x=>x.CustomerID)
                .LastAsync(x=>x.Age>30);
        }

        //-->SingleAsync() ve SingleOrDefaultAsync() Kullanımı

        //-->SingleAsync: Şarta uyan tam olarak bir kayıt varsa sonucu döner,yoksa veya birden fazla varsa hata fırlatır(InvalidOperationException).

        //E-posta adresi eşleşen tek kullanıcıyı getir
        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _appDbContext.Customers
                .SingleAsync(x=>x.Email==email);
        }
        // Belirli ID'ye sahip tek kullanıcıyı getir
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _appDbContext.Customers
                .SingleAsync(x=>x.CustomerID==id);
        }
        //Tek bir telefon numarasına sahip müşteri getir
        public async Task<Customer> GetByPhoneAsync(string phone)
        {
            return await _appDbContext.Customers
                .SingleAsync(x=>x.Phone==phone);
        }

        //-->SingleOrDefaultAsync: Şarta uyan 0 veya 1 kayıt varsa sonucu döner,birden fazla varsa hata verir,hiç kayıt yoksa null döner.

        //E-posta ile kullanıcıyı getir (yoksa null)
        public async Task<Customer?> GetBySingleEmailOrNullAsync(string email)
        {
            return await _appDbContext.Customers
                .SingleOrDefaultAsync(x=>x.Email==email);
        }
        //Yaşı 99 olan kullanıcıyı getir (istisnai bir yaş)
        public async Task<Customer?> GetOldestCustomerAsync()
        {
            return await _appDbContext.Customers
                .SingleOrDefaultAsync(x=>x.Age==99);
        }
        //Adı "Admin" olan müşteri varsa getir
        public async Task<Customer?> GetAdminCustomerAsync()
        {
            return await _appDbContext.Customers
                .SingleOrDefaultAsync(x=>x.FirstName=="Admin");
        }


        //-->FindAsync Kullanımı
        //FindAsync, yalnızca birincil anahtar (primary key) üzerinden arama yapar.EF Core, önce bellekte(DbContext’in içinde) arar, sonra gerekirse veritabanına sorgu gönderir.Genellikle performanslıdır.

        //CustomerID ile müşteri getir
        public async Task<Customer?> GetByFindIdAsync(int id)
        {
            return await _appDbContext.Customers.FindAsync(id);
        }
        //ID’si 10 olan müşteriyi getir (sabit değer)
        public async Task<Customer?> GetCustomer10Async()
        {
            return await _appDbContext.Customers.FindAsync(10);
        }
        // Formdan gelen ID’ye göre müşteri getir
        public async Task<Customer?> GetCustomerFromFormAsync(Customer customerForm)
        {
            return await _appDbContext.Customers.FindAsync(customerForm);
        }
        //Giriş yapan kullanıcı ID’si ile müşteri getir
        public async Task<Customer?> GetLoggedInCustomerAsync(int loggedInUserId)
        {
            return await _appDbContext.Customers.FindAsync(loggedInUserId);
        }



    }
}
