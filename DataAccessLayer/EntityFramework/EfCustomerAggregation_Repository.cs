using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerAggregation_Repository : ICustomerAggregation_Dal
    {
        private readonly AppDbContext _appDbContext;

        public EfCustomerAggregation_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //--->Şartlı CountAsync Örneği
        //Tüm müşterilerin sayısını getir
        public async Task<int> GetTotalCustomerCountAsync()
        {
            return await _appDbContext.Customers.CountAsync();
        }

        // Aktif müşterilerin sayısını getir
        public async Task<int> GetActiveCustomerCountAsync()
        {
            //1.yol
            /*return await _appDbContext.Customers
                .Where(x=>x.IsActive==true)
                .CountAsync();*/
            //2.yol
            return await _appDbContext.Customers
                .CountAsync(x=>x.IsActive==true);
        }


        //18 yaşından büyük müşteri sayısı
        public async Task<int> GetAdultCustomerCountAsync()
        {
            return await _appDbContext.Customers
                .CountAsync(x=>x.Age>18);
        }


        //E-posta adresi olan müşteri sayısı
        public async Task<int> GetCustomerWithEmailCountAsync()
        {
            return await _appDbContext.Customers
                .CountAsync(x=>!string.IsNullOrEmpty(x.Email)); 
        }


        //Telefon numarası eksik müşteri sayısı
        public async Task<int> GetCustomerWithoutPhoneCountAsync()
        {
            return await _appDbContext.Customers
                .CountAsync(x=>x.Phone==null);
        }


        // Adresi "İstanbul" içeren müşteri sayısı
        public async Task<int> GetCustomerFromIstanbulCountAsync()
        {
            return await _appDbContext.Customers
                .CountAsync(x=>x.Address=="İstanbul");
        }


        //Adı null olmayan müşteri sayısı
        public async Task<int> GetCustomerWithNameCountAsync()
        {
            return await _appDbContext.Customers
                .CountAsync(x=>x.FirstName!=null);
        }


        //Yaşı bilinmeyen (null) müşteri sayısı
        public async Task<int> GetCustomersWithUnknownAgeAsync()
        {
            return await _appDbContext.Customers
                .CountAsync(x=>x.Age==null);
        }

        //-->not!! Nullable alanlarda dikkatli olunmalı, null değerler önce filtrelenmelidir.(SumAsync,AverageAsync,MinAsync ve MaxAsync)

        //--->Şartlı SumAsync(Toplama) Örneği

        //18 yaşından büyük müşterilerin yaşlarının toplamı
        public async Task<int> GetTotalAdultAgeAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>18 && x.Age != null)
                .SumAsync(x=>x.Age.Value);
        }

        //Adı 'A' harfiyle başlayan müşterilerin yaş toplamı
        public async Task<int> GetTotalAgeOfCustomersStartingWithAAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.FirstName.ToUpper().StartsWith("a") && x.Age != null)
                .SumAsync(x=>x.Age.Value);
        }
        // Email adresi olan müşterilerin yaş toplamı
        public async Task<int> GetTotalAgeOfCustomersWithEmailAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Email) && x.Age != null)
                .SumAsync(x=>x.Age.Value);
        }

        //--->Şartlı AverageAsync (Toplama) Örneği

        //Tüm müşterilerin yaş ortalaması
        public async Task<double> GetAverageAgeAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age!=null)
                .AverageAsync(x=>x.Age.Value);
        }
        //Aktif müşterilerin yaş ortalaması
        public async Task<double> GetActiveCustomersAverageAgeAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==true && x.Age != null)
                .AverageAsync(x=>x.Age.Value);
        }
        //Email adresi olanların yaş ortalaması
        public async Task<double> GetCustomersWithEmailAverageAgeAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Email) &&x.Age!=null)
                .AverageAsync(x=>x.Age.Value);
        }

        //--->Şartlı MinAsync (Minimum) ve MaxAsync(Maksimum) Örneği

        //Tüm müşterilerin en küçük yaşı
        public async Task<int> GetMinimumAgeAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age!=null)
                .MinAsync(x=>x.Age.Value);
        }
        //Adı 'A' ile başlayanların en büyük yaşı
        public async Task<int> GetAStartingCustomersMaximumAgeAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age!=null && x.FirstName.ToUpper().StartsWith("a"))
                .MaxAsync(x=>x.Age.Value);
        }



    }
}
