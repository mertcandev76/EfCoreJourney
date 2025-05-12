using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerConversion_and_Filtering_Dal
    {
        //Tüm müşterileri liste olarak getir
        Task<List<Customer>> GetAllCustomersAsync();
        //Sadece aktif müşterileri ToList() ile getir
        Task<List<Customer>> GetActiveCustomersAsync();
        //Pasif müşterileri listele
        Task<List<Customer>> GetInactiveCustomersAsync();
        //Yaşı 30’dan büyük müşterileri listele
        Task<List<Customer>> GetCustomersOlderThan30Async();
        //Yaşı 18'den küçük müşteriler
        Task<List<Customer>> GetMinorCustomersAsync();
        //Telefon numarası boş olmayan müşteriler
        Task<List<Customer>> GetCustomersWithPhoneAsync();
        // Telefon numarası boş olan müşteriler
        Task<List<Customer>> GetCustomersWithoutPhoneAsync();
        //Email adresi olan müşteriler
        Task<List<Customer>> GetCustomersWithEmailAsync();
        //Sadece adı "Ali" olan müşteriler
        Task<List<Customer>> GetCustomersNamedAliAsync();
        //Yaşı 20 ile 40 arasında olan müşteriler
        Task<List<Customer>> GetCustomersBetween20And40Async();
        //Adı "a" harfi ile başlayan müşteriler
        Task<List<Customer>> GetCustomersStartsWithAAsync();
        //Adı "can" ile biten müşteriler
        Task<List<Customer>> GetCustomersEndsWithCanAsync();
        //Soyadında "z" geçen müşteriler
        Task<List<Customer>> GetCustomersWithZInLastNameAsync();
        //Yaşı null olmayan müşteriler
        Task<List<Customer>> GetCustomersWithAgeAsync();
        //Yaşı null olan müşteriler
        Task<List<Customer>> GetCustomersWithNullAgeAsync();
        //Yaşı 25'ten küçük ve aktif olanlar
        Task<List<Customer>> GetActiveYoungCustomersAsync();
        //Email'i "gmail.com" içeren müşteriler
        Task<List<Customer>> GetCustomersWithGmailAsync();
        //Soyadı 4 karakterden uzun olanlar
        Task<List<Customer>> GetCustomersWithLongLastNameAsync();
        // Adı ve soyadı boş olmayan müşteriler
        Task<List<Customer>> GetCustomersWithFullNameAsync();
        // Adı ve soyadı boş olan müşteriler
        Task<List<Customer>> GetCustomersWithEmptyFullNameAsync();
        //Adı "a" veya "e" harfi içeren müşteriler
        Task<List<Customer>> GetCustomersWithAorEInNameAsync();
        //Yaşı 19, 24 veya 31 olan müşteriler
        Task<List<Customer>> GetCustomersWithSpecificAgesAsync();
        //Hem email hem telefon bilgisi olanlar
        Task<List<Customer>> GetCustomersWithEmailAndPhoneAsync();
        //"İstanbul" adresinde oturan müşteriler
        Task<List<Customer>> GetCustomersFromIstanbulAsync();
        //Telefon numarası "05" ile başlayanlar
        Task<List<Customer>> GetCustomersWithPhoneStartsWith05Async();
        //Email uzantısı "gmail.com" olmayan müşteriler
        Task<List<Customer>> GetCustomersWithoutGmailComEmailAsync();
        //Soyadı 5 harften uzun olanlar
        Task<List<Customer>> GetCustomersWithLongSurnameAsync();
        //Yaşı 40’tan büyük ve pasif olanlar
        Task<List<Customer>> GetOldInactiveCustomersAsync();
        // Adı "a" harfi içerip soyadı "z" harfi içerenler
        Task<List<Customer>> GetCustomersWithAinNameAndZinSurnameAsync();
        //Adı "Ahmet" veya soyadı "Demir" olan müşteriler
        Task<List<Customer>> GetCustomersNamedAhmetOrSurnameDemirAsync();
        //Yaşı 20’den küçük veya 60’tan büyük olanlar
        Task<List<Customer>> GetCustomersUnder20OrOver60Async();
        //Telefonu olmayan veya emaili olmayan müşteriler
        Task<List<Customer>> GetCustomersMissingPhoneOrEmailAsync();
        //Aktif olmayan veya yaşı 30’dan küçük olanlar
        Task<List<Customer>> GetInactiveOrYoungCustomersAsync();
        // Emaili ".net" veya ".org" içerenler
        Task<List<Customer>> GetCustomersWithNetOrOrgEmailsAsync();



    }
}
