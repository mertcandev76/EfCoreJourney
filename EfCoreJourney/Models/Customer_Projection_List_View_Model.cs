using DTOsLayer.DTOs;
using EntityLayer.Concrete;

namespace EfCoreJourney.Models
{
    public class Customer_Projection_List_View_Model
    {
        //Tüm müşterileri liste olarak getir
        public List<OrderCustomer> Customers { get; set; }

        // Tüm müşterilerin ad ve soyadını almak
        public List<CustomerNameDto> CustomerFullNames { get; set; }
        
        //Aktif Müşterilerin Ad,Soyad ve Yaşını Almak
        public List<ActiveCustomerDto> ActiveCustomers { get; set; }
        

        //Yaşı 30'dan Büyük Olan Müşterilerin Ad ve Soyadını Almak
        public List<OlderThan30Dto> CustomersOlderThan { get; set; }

        //Pasif Müşterilerin Yalnızca İsimlerini Almak
        public List<InactiveCustomerDto>InactiveCustomers { get; set; }


        //Yaşı 25'ten Büyük Olan Müşterilerin İsim ve Telefon Numaralarını Almak
        public List<OlderThan25Dto> OlderThan25Customers { get; set; }


        // Adı "John" Olan Müşterilerin Soyadlarını Almak
        public List<JohnCustomerDto> JohnCustomers { get; set; }


        //Aktif Olmayan Müşterilerin E-posta ve Adres Bilgilerini Almak
        public List<InactiveCustomerDetailsDto> InactiveCustomerDetails { get; set; }


        //Yaşı 25 ile 35 Arasındaki Müşterilerin İsimlerini Almak
        public List<CustomersBetween25And35Dto> CustomersBetween25And35 { get; set; }


        //Müşteri Adı ve Soyadı ile Birlikte Telefon Bilgisini Almak
        public List<CustomerDetailsDto> CustomerDetails { get; set; }


        //Aktif Olmayan Müşteri Sayısını Almak
        public List<InactiveCustomerCountDto> InactiveCustomerCount { get; set; }


        //Müşterilerin Yalnızca E-posta Adreslerini Almak
        public List<CustomerEmailDto> CustomerEmail { get; set; }


        //Yaşı 50'den Büyük Olan Müşteri Adları ve Yaşlarını Almak
        public List<OlderThan50Dto> OlderThan50Customers { get; set; }


        // Müşterilerin İsim ve Soyadlarını Birleştirerek Tam Ad Almak
        public List<FullNameDto> FullNames { get; set; }


        //Her Müşterinin Telefon Numarasının Uzunluğunu Almak
        public List<CustomerPhoneLengthDto> CustomerPhoneLength { get; set; }

        // Müşterilerin Yalnızca Adlarını Büyük Harflerle Almak
        public List<CustomerFirstNameUpperCaseDto> CustomerFirstNameUpperCase { get; set; }


        //Müşterilerin Adlarını ve Yaşlarını Kategorilere Ayırmak
        public List<CustomerNameAndAgeCategoryDto> CustomerNameAndAgeCategory { get; set; }


        //Müşterilerin Yaşlarının Ortalama Değerini Almak
        public double AverageCustomerAge { get; set; }


        //Müşterilerin İsimlerini ve Soyadlarını Büyük Harflerle Almak
        public List<CustomerNameUpperCaseDto> CustomerNameUpperCase { get; set; }


    }
}
