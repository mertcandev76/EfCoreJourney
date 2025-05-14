using DTOsLayer.DTOs;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerProjection_Dal
    {
        //Tüm müşterileri liste olarak getir
        Task<List<Customer>> GetAllCustomersAsync();

        // Tüm müşterilerin ad ve soyadını almak
        Task<List<CustomerNameDto>> GetCustomerFullNamesAsync();

        //Aktif Müşterilerin Ad,Soyad ve Yaşını Almak
        Task<List<ActiveCustomerDto>> GetActiveCustomersAsync();

        //Yaşı 30'dan Büyük Olan Müşterilerin Ad ve Soyadını Almak
        Task<List<OlderThan30Dto>> GetCustomersOlderThan30Async();

        //Pasif Müşterilerin Yalnızca İsimlerini Almak
        Task<List<InactiveCustomerDto>> GetInactiveCustomersAsync();

        //Yaşı 25'ten Büyük Olan Müşterilerin İsim ve Telefon Numaralarını Almak
        Task<List<OlderThan25Dto>> GetOlderThan25CustomersAsync();

        // Adı "John" Olan Müşterilerin Soyadlarını Almak
        Task<List<JohnCustomerDto>> GetJohnCustomersAsync();

        //Aktif Olmayan Müşterilerin E-posta ve Adres Bilgilerini Almak
        Task<List<InactiveCustomerDetailsDto>> GetInactiveCustomerDetailsAsync();

        //Yaşı 25 ile 35 Arasındaki Müşterilerin İsimlerini Almak
        Task<List<CustomersBetween25And35Dto>> GetCustomersBetween25And35Async();

        //Müşteri Adı ve Soyadı ile Birlikte Telefon Bilgisini Almak
        Task<List<CustomerDetailsDto>> GetCustomerDetailsAsync();

        //Aktif Olmayan Müşteri Sayısını Almak
        Task<List<InactiveCustomerCountDto>> GetInactiveCustomerCountAsync();

        //Müşterilerin Yalnızca E-posta Adreslerini Almak
        Task<List<CustomerEmailDto>> GetCustomerEmailAsync();

        //Yaşı 50'den Büyük Olan Müşteri Adları ve Yaşlarını Almak
        Task<List<OlderThan50Dto>> GetOlderThan50CustomersAsync();

        // Müşterilerin İsim ve Soyadlarını Birleştirerek Tam Ad Almak
        Task<List<FullNameDto>> GetFullNamesAsync();

        //Her Müşterinin Telefon Numarasının Uzunluğunu Almak
        Task<List<CustomerPhoneLengthDto>> GetCustomerPhoneLengthAsync();

       

        // Müşterilerin Yalnızca Adlarını Büyük Harflerle Almak
        Task<List<CustomerFirstNameUpperCaseDto>> GetCustomerFirstNameUpperCaseAsync();

        //Müşterilerin Adlarını ve Yaşlarını Kategorilere Ayırmak
        Task<List<CustomerNameAndAgeCategoryDto>> GetCustomerNameAndAgeCategoryAsync();

        //Müşterilerin Yaşlarının Ortalama Değerini Almak
        Task<double> GetAverageCustomerAgeAsync();

        //Müşterilerin İsimlerini ve Soyadlarını Büyük Harflerle Almak
        Task<List<CustomerNameUpperCaseDto>> GetCustomerNameUpperCaseAsync();
    }
}
