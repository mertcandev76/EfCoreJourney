using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerQuantifiers_Repository:ICustomerQuantifiers_Dal
    {
        private readonly AppDbContext _appDbContext;

        public EfCustomerQuantifiers_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //-->AnyAsenkron Kullanımı

        //Veritabanında hiç müşteri var mı?
        public async Task<bool> AnyCustomerExistsAsync()
        {
            return await _appDbContext.Customers.AnyAsync();
        }
        //Aktif durumda olan en az bir müşteri var mı?
        public async Task<bool> AnyActiveCustomerAsync()
        {
            //1.yol
            /*  return await _appDbContext.Customers
                  .Where(x=>x.IsActive==true)
                  .AnyAsync();*/
            return await _appDbContext.Customers
                .AnyAsync(x=>x.IsActive==true);
        }
        //18 yaşından küçük müşteri var mı?
        public async Task<bool> AnyUnderageCustomerAsync()
        {
            return await _appDbContext.Customers
               .AnyAsync(x => x.Age < 18);
                
        }
        // E-posta adresi tanımlı olmayan müşteri var mı?
        public async Task<bool> AnyCustomerWithoutEmailAsync()
        {
            return await _appDbContext.Customers
                .AnyAsync(x => x.Email == null);
               
        }
        //Belirli bir telefon numarasına sahip müşteri var mı?
        public async Task<bool> AnyCustomerWithPhoneAsync(string phone)
        {
            return await _appDbContext.Customers
                .AnyAsync(x=>x.Phone==phone);
        }
        //-->AllAsenkron Kullanımı

        //Tüm müşteriler aktif mi?
        public async Task<bool> AreAllCustomersActiveAsync()
        {
          
            return await _appDbContext.Customers.AllAsync(x=>x.IsActive==true);
        }

        //Tüm müşterilerin e-posta adresi dolu mu?
        public async Task<bool> DoAllCustomersHaveEmailAsync()
        {
            return await _appDbContext.Customers.AllAsync(x=>string.IsNullOrEmpty(x.Email));
        }
        //Tüm müşteriler 18 yaşından büyük mü?
        public async Task<bool> AreAllCustomersAdultsAsync()
        {
            return await _appDbContext.Customers.AllAsync(x=>x.Age>18);
        }
        // Tüm müşterilerin telefon numarası girilmiş mi?
        public async Task<bool> DoAllCustomersHavePhoneAsync()
        {
             return await _appDbContext.Customers.AllAsync(x => !string.IsNullOrEmpty(x.Phone));
        }
        //Tüm müşteriler aynı şehirde mi yaşıyor? (örneğin "Ankara")
        public async Task<bool> AreAllCustomersFromAnkaraAsync()
        {
            return await _appDbContext.Customers.AllAsync(x => x.Address!=null && x.Address.Contains("Ankara"));
        }

    }
}
