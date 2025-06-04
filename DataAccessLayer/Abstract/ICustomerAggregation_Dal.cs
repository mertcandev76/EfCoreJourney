using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerAggregation_Dal
    {

        //--->Şartlı Count Örneği
        //Tüm müşterilerin sayısını getir
        Task<int> GetTotalCustomerCountAsync();


        // Aktif müşterilerin sayısını getir
        Task<int> GetActiveCustomerCountAsync();


        //18 yaşından büyük müşteri sayısı
        Task<int> GetAdultCustomerCountAsync();


        //E-posta adresi olan müşteri sayısı
        Task<int> GetCustomerWithEmailCountAsync();


        //Telefon numarası eksik müşteri sayısı
        Task<int> GetCustomerWithoutPhoneCountAsync();


        // Adresi "İstanbul" içeren müşteri sayısı
        Task<int> GetCustomerFromIstanbulCountAsync();


        //Adı null olmayan müşteri sayısı
        Task<int> GetCustomerWithNameCountAsync();


        //Yaşı bilinmeyen (null) müşteri sayısı
        Task<int> GetCustomersWithUnknownAgeAsync();

        //--->Şartlı Sum(Toplama) Örneği

        //18 yaşından büyük müşterilerin yaşlarının toplamı
        Task<int> GetTotalAdultAgeAsync();

        //Adı 'A' harfiyle başlayan müşterilerin yaş toplamı
        Task<int> GetTotalAgeOfCustomersStartingWithAAsync();
        // Email adresi olan müşterilerin yaş toplamı
        Task<int> GetTotalAgeOfCustomersWithEmailAsync();

        //--->Şartlı AverageAsync (Toplama) Örneği

        //Tüm müşterilerin yaş ortalaması
        Task<double> GetAverageAgeAsync();
        //Aktif müşterilerin yaş ortalaması
        Task<double> GetActiveCustomersAverageAgeAsync();
        //Email adresi olanların yaş ortalaması
        Task<double> GetCustomersWithEmailAverageAgeAsync();

        //--->Şartlı MinAsync (Minimum) ve MaxAsync(Maksimum) Örneği

        //Tüm müşterilerin en küçük yaşı
        Task<int> GetMinimumAgeAsync();
        //Adı 'A' ile başlayanların en büyük yaşı
        Task<int> GetAStartingCustomersMaximumAgeAsync();

    }

}
