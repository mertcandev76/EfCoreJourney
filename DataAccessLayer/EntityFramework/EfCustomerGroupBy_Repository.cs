using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DTOsLayer.DTOs.CustomerGroupByDto;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCustomerGroupBy_Repository:ICustomerGroupBy_Dal
    {
        private readonly AppDbContext _appDbContext;

        public EfCustomerGroupBy_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //Tüm liste
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _appDbContext.Customers.ToListAsync();
        }



        // 1. Yaşa göre gruplama
        public async Task<List<GroupByAgeDto>> GetGroupByAgeAsync()
            {
              return await _appDbContext.Customers
                .GroupBy(x=>x.Age)
                .Select(x=>new GroupByAgeDto
                {
                    Age=x.Key,
                    Count=x.Count()
                })
               .ToListAsync();
            //Cast programalama dilinde tip dönüştürme anlamına geliyor 
            }

            // 2. Aktifliğe göre gruplama
            public async Task<List<GroupByIsActiveDto>> GetGroupByIsActiveAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>x.IsActive)
                .Select(x=>new GroupByIsActiveDto
                {
                    IsActive=x.Key,
                    Count=x.Count()
                })
                .ToListAsync();
            }

        // 3. İlk harfe göre ad gruplama
        //-->Not!!! Bu örnek sadece Bellekte çalıştırılıyor(IEnumerable) veritabanında çalıştırılmaz(IQueryable)
        public async Task<List<GroupByFirstNameInitialDto>> GetGroupByFirstNameInitialAsync()
            {
            var customers= await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.FirstName))// null ve boş kontrolü
                .ToListAsync();// veriyi belleğe al
            var result = customers
                .GroupBy(x => x.FirstName![0])// bellekte çalıştığı için hata vermez
                .Select(x => new GroupByFirstNameInitialDto
                {
                    FirstLetter = x.Key,
                    Count = x.Count()
                })
                .ToList();
           
            return result;
             }
       

            // 4. Email boş/dolu durumuna göre gruplama
            public async Task<List<GroupByEmailStatusDto>> GetGroupByEmailStatusAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>string.IsNullOrEmpty(x.Email))
                .Select(x=>new GroupByEmailStatusDto
                {
                    EmailStatus = x.Key,
                    Count=x.Count()
                })
                .ToListAsync();
               
            }

            // 5. Yaş aralığına göre gruplama (Genç, Orta, Yaşlı) listeli
            public async Task<List<GroupByAgeGroupDto>> GetGroupByAgeGroupAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>
                x.Age<=25 ? "Genç":
                x.Age<=50 ? "Orta":"Yaşlı"
                )
                .Select(x=>new GroupByAgeGroupDto
                {
                    Grup=x.Key,
                    Customers=x.ToList()
                })                .ToListAsync();
            }
        // 6. Yaş aralığına göre gruplama (Genç, Orta, Yaşlı) sayılı
        public async Task<List<CustomerCountByAgeGroupDto>> GetCustomerCountByAgeGroupAsync()
        {
            return await _appDbContext.Customers
                .GroupBy(x=>
                x.Age<=25 ? "Genç":
                x.Age<=50 ? "Orta":"Yaşlı"

                )
                .Select(x=>new CustomerCountByAgeGroupDto
                {
                    Grup=x.Key,
                    Count=x.Count()
                })
                .ToListAsync();
        }

        // 7. Ad + Soyad’a göre gruplama
        public async Task<List<object>> GetGroupByFullNameAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x => new
                {
                    x.FirstName,x.LastName
                })
                .Select(x=>new
                {
                    FullName=x.Key,
                    Count=x.Count()
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 8. Telefon var/yok durumuna göre gruplama(listeli,sıralı)
            public async Task<List<object>> GetGroupByPhoneStatusAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>string.IsNullOrEmpty(x.Phone))
                .Select(x => new
                {
                    Telefon=x.Key,
                    Count=x.Count(),
                    List=x.ToList()
                })
                .Cast<object>()
                .ToListAsync();
            }

        // 9. Adresin son kelimesine göre şehir tahminiyle gruplama
        public async Task<List<GroupByCityFromAddressDto>> GetGroupByCityFromAddressAsync()
        {
            var customers= await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Address))
                .ToListAsync();
            var result = customers
                .GroupBy(x =>
                {
                    var city= x.Address.Trim().Split(' ');//Metni ayırmak:Belirtilen karaktere göre metni parçalara ayırır ve bir dizi (array) döner.
                    return city.Last();
                })
                .Select(x => new GroupByCityFromAddressDto
                {
                    City=x.Key,
                    Count=x.Count()
                })
          
                .ToList();
            return result;
        }

            // 10. Aktiflik durumuna göre ortalama yaş
            public async Task<List<object>> GetGroupByIsActiveWithAvgAgeAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>x.IsActive)
                .Select(x=>new
                {
                    IsActive=x.Key,
                    AvgAge=x.Average(x=>x.Age ?? 0)
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 11. Email domainine göre gruplama (örnek: gmail.com)
            public async Task<List<object>> GetGroupByEmailDomainAsync()
            {
            var customers= await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Email))
                .ToListAsync();
            var result = customers
                .GroupBy(x =>
                {
                    var domain = x.Email.Trim().Split('@');
                    return domain.Last();

                })
                .Select(x => new
                {
                    Domain = x.Key,
                    Count = x.Count()
                })
                .Cast<object>()
                .ToList();
            return result;

            }

            // 12. Ad’a göre gruplama
            public async Task<List<object>> GetGroupByFirstNameAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>x.FirstName)
                .Select(x => new
                {
                    FirstName=x.Key,
                    Count=x.Count(),
                    List=x.ToList()
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 13. Aktifliğe göre yaş ortalaması
            public async Task<List<object>> GetGroupByIsActiveWithAvgAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x => x.Age)
                .Select(x => new
                {
                    Age=x.Key,
                    AgeAvg=x.Average(x=>x.Age)

                })
                .Cast<object>()
                .ToListAsync();
            }

            // 14. Belirli yaş aralığına göre (0-19, 20-40, 40+) gruplama
            public async Task<List<object>> GetGroupByAgeRangeAsync()
            {
            return await _appDbContext.Customers
                .GroupBy
                (x=>
                x.Age<=20 ? "0-19":
                x.Age<=40 ? "20-40":"40+"
                )
                .Select(x => new
                {
                    Age=x.Key,
                    Count=x.Count()
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 15. Ad bilgisi boş mu dolu mu diye gruplama
            public async Task<List<object>> GetGroupByEmptyFirstNameAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>!string.IsNullOrEmpty(x.FirstName))
                .Select(x => new
                {
                    FirstName=x.Key,
                    Count=x.Count()
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 16. Adrese göre gruplama(listele ile)
            public async Task<List<object>> GetGroupByAddressAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>x.Address)
                .Select(x => new
                {
                    Adress=x.Key,
                    List=x.ToList()
                    
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 17. Ad + soyad toplam harf sayısına göre gruplama
            public async Task<List<object>> GetGroupByFullNameLengthAsync()
            {
            var customers = await _appDbContext.Customers
                .Where(x => x.FirstName != null && x.LastName != null)
            .ToListAsync();

            var result = customers
                .GroupBy(x => (x.FirstName?.Length ?? 0) + (x.LastName?.Length ?? 0))
                .Select(g => new
                {
                    NumberofLetters = g.Key,
                    Count = g.Count()
                })
                .Cast<object>()
                .ToList();

            return result;
        }

            // 18. Email’in ilk harfine göre gruplama(listele)
            public async Task<List<object>> GetGroupByEmailFirstCharAsync()
            {
            var customers= await _appDbContext.Customers
                .Where(x=>!string.IsNullOrEmpty(x.Email))
                .ToListAsync();
            var result = customers
                .GroupBy(x => x.Email![0])
                .Select(x => new
                {
                    Email = x.Key,
                    List = x.ToList()
                })
                .Cast<object>()
                .ToList();
            return result;

            }

            // 19. Aynı yaşa sahip kullanıcıları listeleme
            public async Task<List<object>> GetGroupByAgeListAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>x.Age)
                .Select(x => new
                {
                   Age=x.Key,
                   List=x.ToList()
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 20. Telefon numarası uzunluğuna göre gruplama
            public async Task<List<object>> GetGroupByPhoneLengthAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x=>x.Phone!.Length)
                .Select(x => new
                {
                    Phone=x.Key,
                    Count=x.Count()
                })
                .Cast<object>()
                .ToListAsync();
            }

            // 21. Hem aktiflik hem yaş bilgisine göre gruplama
            public async Task<List<object>> GetGroupByIsActiveAndAgeAsync()
            {
            return await _appDbContext.Customers
                .GroupBy(x => new {x.IsActive,x.Age})
                .Select(x => new
                {
                    Group=x.Key,
                    Count=x.Count()
                })
                .Cast<object>()            
                .ToListAsync();
            }
        

    }
}
