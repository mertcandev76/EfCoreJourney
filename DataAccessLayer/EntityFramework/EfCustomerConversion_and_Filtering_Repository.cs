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
    public class EfCustomerConversion_and_Filtering_Repository:ICustomerConversion_and_Filtering_Dal
    {
         private readonly AppDbContext _appDbContext;

        public EfCustomerConversion_and_Filtering_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //Tüm müşterileri liste olarak getir
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _appDbContext.Customers.ToListAsync();
        }
        //Sadece aktif müşterileri getir
        public async Task<List<Customer>> GetActiveCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==true)
                .ToListAsync();
        }
        //Pasif müşterileri listele
        public async Task<List<Customer>> GetInactiveCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==false)
                .ToListAsync();
        }
        //Yaşı 30’dan büyük müşterileri listele
        public async Task<List<Customer>> GetCustomersOlderThan30Async()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>30)
                .ToListAsync();
        }
        //Yaşı 18'den küçük müşteriler
        public async Task<List<Customer>> GetMinorCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age<18)
                .ToListAsync();
        }
        //Telefon numarası boş olmayan müşteriler(yani şu demek telefon numarası olan müşteriler)
        public async Task<List<Customer>> GetCustomersWithPhoneAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Phone))
                .ToListAsync();
            //anlamı
            //"05554443322" → gelir
            //null → gelmez
            //"" → gelmez
        }
        // Telefon numarası boş olan müşteriler
         public async Task<List<Customer>> GetCustomersWithoutPhoneAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>string.IsNullOrEmpty(x.Phone))
                .ToListAsync();
        }

        //Email adresi olan müşteriler
        public async Task<List<Customer>> GetCustomersWithEmailAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Email))
                .ToListAsync();
        }
        //Sadece adı "Ali" olan müşteriler
        public async Task<List<Customer>> GetCustomersNamedAliAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.FirstName=="Ali")
                .ToListAsync();
        }
        //Yaşı 20 ile 40 arasında olan müşteriler
        public async Task<List<Customer>> GetCustomersBetween20And40Async()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>20 && x.Age<40)
                .ToListAsync();
        }
        //Adı "a" harfi ile başlayan müşteriler
        public async Task<List<Customer>> GetCustomersStartsWithAAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.FirstName.StartsWith("a"))
                .ToListAsync();
        }
        //Adı "can" ile biten müşteriler
        public async Task<List<Customer>> GetCustomersEndsWithCanAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.FirstName.EndsWith("can"))
                .ToListAsync();
           
        }
        //Soyadında "z" geçen müşteriler
        public async Task<List<Customer>> GetCustomersWithZInLastNameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.LastName.Contains("z"))
                .ToListAsync();
        }
        //Yaşı null olmayan müşteriler
        public async Task<List<Customer>> GetCustomersWithAgeAsync()
        {
            return await _appDbContext.Customers
                //1.adım .Where(x=>x.Age!=null)
                //2.adım
                .Where(x=>x.Age.HasValue)
                .ToListAsync();
        }
        //Yaşı null olan müşteriler
        public async Task<List<Customer>> GetCustomersWithNullAgeAsync()
        {
            return await _appDbContext.Customers
                //2.adım
                //.Where(x=>!x.Age.HasValue)
                //1.adım
                .Where(x=>x.Age==null)
                .ToListAsync();
        }
        //Yaşı 25'ten küçük ve aktif olanlar
        public async Task<List<Customer>> GetActiveYoungCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age<25 && x.IsActive==true)
                .ToListAsync();
        }
        //Email'i "gmail.com" içeren müşteriler
        public async Task<List<Customer>> GetCustomersWithGmailAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Email.EndsWith("gmail"))
                .ToListAsync();
        }
        //Soyadı 4 karakterden uzun olanlar
        public async Task<List<Customer>> GetCustomersWithLongLastNameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.LastName.Length>4)
                .ToListAsync();
        }
        // Adı ve soyadı boş olmayan müşteriler
        public async Task<List<Customer>> GetCustomersWithFullNameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!String.IsNullOrEmpty(x.FirstName)&&!string.IsNullOrEmpty(x.LastName))
                .ToListAsync();
        }
        // Adı ve soyadı boş olan müşteriler
        public async Task<List<Customer>> GetCustomersWithEmptyFullNameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>string.IsNullOrEmpty(x.FirstName) && string.IsNullOrEmpty(x.LastName))
                .ToListAsync();
        }
        //Adı "a" veya "e" harfi içeren müşteriler
        public async Task<List<Customer>> GetCustomersWithAorEInNameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.FirstName.ToLower().Contains("a")&&x.LastName.ToLower().Contains("e"))
                .ToListAsync();
        }
        //Yaşı 19, 24 veya 31 olan müşteriler
        public async Task<List<Customer>> GetCustomersWithSpecificAgesAsync()
        {
            return await _appDbContext.Customers
                 .Where(x=>x.Age==19 || x.Age==24 || x.Age==31)
                .ToListAsync();
        }
        //Hem email hem telefon bilgisi olanlar
        public async Task<List<Customer>> GetCustomersWithEmailAndPhoneAsync()
        {
            return await _appDbContext.Customers
                 .Where(x=>!string.IsNullOrEmpty(x.Email) && !string.IsNullOrEmpty(x.Phone))
                .ToListAsync();
        }
        //"İstanbul" adresinde oturan müşteriler
        public async Task<List<Customer>> GetCustomersFromIstanbulAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Address=="İstanbul")
                .ToListAsync();
        }
        //Telefon numarası "05" ile başlayanlar
        public async Task<List<Customer>> GetCustomersWithPhoneStartsWith05Async()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Phone.StartsWith("05"))
                .ToListAsync();
        }
        //Email uzantısı "gmail.com" olmayan müşteriler
        public async Task<List<Customer>> GetCustomersWithoutGmailComEmailAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!x.Email.EndsWith("gmail.com"))
                .ToListAsync();
        }
        //Soyadı 5 harften uzun olanlar
        public async Task<List<Customer>> GetCustomersWithLongSurnameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.LastName.Length>5)
                .ToListAsync();
        }
        //Yaşı 40’tan büyük ve pasif olanlar
        public async Task<List<Customer>> GetOldInactiveCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>40 && x.IsActive==false)
                .ToListAsync();
        }
        // Adı "a" harfi içerip soyadı "z" harfi içerenler
        public async Task<List<Customer>> GetCustomersWithAinNameAndZinSurnameAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.FirstName.Contains("a") && x.LastName.Contains("z"))
                .ToListAsync();
        }
        //Adı "Ahmet" veya soyadı "Demir" olan müşteriler
        public async Task<List<Customer>> GetCustomersNamedAhmetOrSurnameDemirAsync()
        {
            return await _appDbContext.Customers
                .Where(x => x.FirstName == "Ahmet" || x.LastName == "Demir")
                .ToListAsync();
        }
        //Yaşı 20’den küçük veya 60’tan büyük olanlar
        public async Task<List<Customer>> GetCustomersUnder20OrOver60Async()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age<20 || x.Age>60)
                .ToListAsync();
        }
        //Telefonu olmayan veya emaili olmayan müşteriler
        public async Task<List<Customer>> GetCustomersMissingPhoneOrEmailAsync()
        {
             return await _appDbContext.Customers
                .Where(x=>string.IsNullOrEmpty(x.Phone) || string.IsNullOrEmpty(x.Email))
                .ToListAsync();
        }
        //Aktif olmayan veya yaşı 30’dan küçük olanlar
        public async Task<List<Customer>> GetInactiveOrYoungCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==false || x.Age>30)
                .ToListAsync();
        }
        // Emaili ".net" veya ".org" içerenler
        public async Task<List<Customer>> GetCustomersWithNetOrOrgEmailsAsync()
        {
           return await _appDbContext.Customers
                .Where(x=>x.Email.Contains(".net") || x.Email.Contains(".org"))
                .ToListAsync();
        }

    }
}
