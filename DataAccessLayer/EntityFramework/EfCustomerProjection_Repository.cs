using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DTOsLayer.DTOs;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerProjection_Repository: ICustomerProjection_Dal
    {
        private readonly AppDbContext _appDbContext;

        public EfCustomerProjection_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Tüm müşterileri liste olarak getir
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _appDbContext.Customers.ToListAsync();
        }



        // Tüm müşterilerin ad ve soyadını almak
         public async Task<List<CustomerNameDto>> GetCustomerFullNamesAsync()
        {
            return await _appDbContext.Customers
                .Select(x=> new CustomerNameDto
                {
                    FirstName=x.FirstName,
                    LastName=x.LastName
                })
               .ToListAsync();
        }

        //Aktif Müşterilerin Ad,Soyad ve Yaşını Almak
        public async Task<List<ActiveCustomerDto>> GetActiveCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==true)
                .Select(x=>new ActiveCustomerDto
                {
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                     Age=x.Age
                })
                .ToListAsync();
        }

        //Yaşı 30'dan Büyük Olan Müşterilerin Ad ve Soyadını Almak
        public async Task<List<OlderThan30Dto>> GetCustomersOlderThan30Async()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>30)
                .Select(x=>new OlderThan30Dto
                {
                    FirstName=x.FirstName,
                    LastName=x.LastName

                })
                .ToListAsync();
        }

        //Pasif Müşterilerin Yalnızca emailini almak Almak
        public async Task<List<InactiveCustomerDto>> GetInactiveCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==false)
                .Select(x=>new InactiveCustomerDto
                {
                    Email=x.Email
                })
                .ToListAsync();
        }

        //Yaşı 25'ten Büyük Olan Müşterilerin İsim,Soyisim ve Telefon Numaralarını Almak
        public async Task<List<OlderThan25Dto>> GetOlderThan25CustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>25)
                .Select(x=>new OlderThan25Dto
                {
                    FullName=x.FirstName+" "+x.LastName,
                    Phone=x.Phone
                })
                .ToListAsync();
        }

        // Adı "John" Olan Müşterilerin Soyadlarını Almak
        public async Task<List<JohnCustomerDto>> GetJohnCustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.FirstName=="John")
                .Select(x=>new JohnCustomerDto
                {
                    LastName=x.LastName
                })
                .ToListAsync();
        }

        //Aktif Olmayan Müşterilerin E-posta ve Adres Bilgilerini Almak
        public async Task<List<InactiveCustomerDetailsDto>> GetInactiveCustomerDetailsAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.IsActive==false)
                .Select(x=>new InactiveCustomerDetailsDto
                {
                    Email=x.Email,
                    Address=x.Address
                })
                .ToListAsync();
        }

        //Yaşı 25 ile 35 Arasındaki Müşterilerin İsimlerini Almak
        public async Task<List<CustomersBetween25And35Dto>> GetCustomersBetween25And35Async()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>25 && x.Age<35)
                .Select(x=>new CustomersBetween25And35Dto
                {
                    FirstName=x.FirstName
                })
                .ToListAsync();
        }

        //Müşteri Adı ve Soyadı ile Birlikte Telefon Bilgisini Almak
        public async Task<List<CustomerDetailsDto>> GetCustomerDetailsAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Email))
                .Select(x=>new CustomerDetailsDto
                {
                    FullName=x.FirstName+" "+x.LastName,
                    Phone=x.Phone
                })
                .ToListAsync();
        }

        //Aktif Olmayan Müşteri Sayısını Almak
        public async Task<List<InactiveCustomerCountDto>> GetInactiveCustomerCountAsync()
        {
            throw new NotImplementedException();
        }

        //Müşterilerin Yalnızca E-posta Adreslerini Almak
        public async Task<List<CustomerEmailDto>> GetCustomerEmailAsync()
        {
           return await _appDbContext.Customers
                .Select(x=> new CustomerEmailDto
                {
                    Email=x.Email
                })
                .ToListAsync();
        }

        //Yaşı 50'den Büyük Olan Müşteri Adları ve Yaşlarını Almak
        public async Task<List<OlderThan50Dto>> GetOlderThan50CustomersAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>x.Age>50)
                .Select(x=>new OlderThan50Dto
                {
                    FirstName=x.FirstName,
                    Age=x.Age
                })
                .ToListAsync();
        }

        // Müşterilerin İsim ve Soyadlarını Birleştirerek Tam Ad Almak
        public async Task<List<FullNameDto>> GetFullNamesAsync()
        {
            return await _appDbContext.Customers
                .Select(x=>new FullNameDto
                {
                    FullName=x.FirstName+" "+x.LastName
                })
                .ToListAsync();
        }

        //Her Müşterinin Telefon Numarasının Uzunluğunu Alarak telefon numarasını getirme
        public async Task<List<CustomerPhoneLengthDto>> GetCustomerPhoneLengthAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Phone))
                .Select(x=>new CustomerPhoneLengthDto
                {
                    Phone=x.Phone,
                    Length=x.Phone.Length
                })
                .ToListAsync();
        }

     

        // Müşterilerin Yalnızca Adlarını Büyük Harflerle Almak
        public async Task<List<CustomerFirstNameUpperCaseDto>> GetCustomerFirstNameUpperCaseAsync()
        {
            return await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.FirstName))
                .Select(x=>new CustomerFirstNameUpperCaseDto
                {
                    FirstNameUpper=x.FirstName.ToUpper()
                })
                .ToListAsync();
        }

        //Müşterilerin Adlarını ve Yaşlarını Kategorilere Ayırmak
        public Task<List<CustomerNameAndAgeCategoryDto>> GetCustomerNameAndAgeCategoryAsync()
        {
            throw new NotImplementedException();
        }

        //Müşterilerin Yaşlarının Ortalama Değerini Almak
        public Task<double> GetAverageCustomerAgeAsync()
        {
            throw new NotImplementedException();
        }

        //Müşterilerin İsimlerini ve Soyadlarını Büyük Harflerle Almak
        public Task<List<CustomerNameUpperCaseDto>> GetCustomerNameUpperCaseAsync()
        {
            throw new NotImplementedException();
        }

    }
}
