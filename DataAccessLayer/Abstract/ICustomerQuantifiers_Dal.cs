using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerQuantifiers_Dal
    {
        //-->AnyAsenkron Kullanımı

        //Veritabanında hiç müşteri var mı?
        Task<bool> AnyCustomerExistsAsync();
        //Aktif durumda olan en az bir müşteri var mı?
        Task<bool> AnyActiveCustomerAsync();
        //18 yaşından küçük müşteri var mı?
        Task<bool> AnyUnderageCustomerAsync();
        // E-posta adresi tanımlı olmayan müşteri var mı?
        Task<bool> AnyCustomerWithoutEmailAsync();
        //Belirli bir telefon numarasına sahip müşteri var mı?
        Task<bool> AnyCustomerWithPhoneAsync(string phone);

        //-->AllAsenkron Kullanımı

        //Tüm müşteriler aktif mi?
        Task<bool> AreAllCustomersActiveAsync();

        //Tüm müşterilerin e-posta adresi dolu mu?
        Task<bool> DoAllCustomersHaveEmailAsync();
        //Tüm müşteriler 18 yaşından büyük mü?
        Task<bool> AreAllCustomersAdultsAsync();
        // Tüm müşterilerin telefon numarası girilmiş mi?
        Task<bool> DoAllCustomersHavePhoneAsync();
        //Tüm müşteriler aynı şehirde mi yaşıyor? (örneğin "Ankara")
        Task<bool> AreAllCustomersFromAnkaraAsync();


    }
}
