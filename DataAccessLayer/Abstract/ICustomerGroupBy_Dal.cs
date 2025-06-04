using DTOsLayer.DTOs.CustomerGroupByDto;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerGroupBy_Dal
    {
            //Tüm liste
            Task<List<Customer>> GetAllCustomersAsync();

            //-->GroupBy Kullanımı

            // 1. Yaşa göre gruplama
            Task<List<GroupByAgeDto>> GetGroupByAgeAsync();

            // 2. Aktifliğe göre gruplama
            Task<List<GroupByIsActiveDto>> GetGroupByIsActiveAsync();

            // 3. İlk harfe göre ad gruplama
            Task<List<GroupByFirstNameInitialDto>> GetGroupByFirstNameInitialAsync();

            // 4. Email boş/dolu durumuna göre gruplama
            Task<List<GroupByEmailStatusDto>> GetGroupByEmailStatusAsync();

            // 5. Yaş aralığına göre gruplama (Genç, Orta, Yaşlı)
            Task<List<GroupByAgeGroupDto>> GetGroupByAgeGroupAsync();
        // 6. Yaş aralığına göre gruplama (Genç, Orta, Yaşlı) sayılı
        Task<List<CustomerCountByAgeGroupDto>> GetCustomerCountByAgeGroupAsync();
        // 7. Ad + Soyad’a göre gruplama
        Task<List<object>> GetGroupByFullNameAsync();

            // 8. Telefon var/yok durumuna göre gruplama
            Task<List<object>> GetGroupByPhoneStatusAsync();

            // 9. Adresin son kelimesine göre şehir tahminiyle gruplama
            Task<List<GroupByCityFromAddressDto>> GetGroupByCityFromAddressAsync();

            // 10. Aktiflik durumuna göre ortalama yaş
            Task<List<object>> GetGroupByIsActiveWithAvgAgeAsync();

            // 11. Email domainine göre gruplama (örnek: gmail.com)
            Task<List<object>> GetGroupByEmailDomainAsync();

            // 12. Ad’a göre gruplama
            Task<List<object>> GetGroupByFirstNameAsync();

            // 13. Aktifliğe göre yaş ortalaması
            Task<List<object>> GetGroupByIsActiveWithAvgAsync();

            // 14. Belirli yaş aralığına göre (0-19, 20-40, 40+) gruplama
            Task<List<object>> GetGroupByAgeRangeAsync();

            // 15. Ad bilgisi boş mu dolu mu diye gruplama
            Task<List<object>> GetGroupByEmptyFirstNameAsync();

            // 16. Adrese göre gruplama
            Task<List<object>> GetGroupByAddressAsync();

            // 17. Ad + soyad toplam harf sayısına göre gruplama
            Task<List<object>> GetGroupByFullNameLengthAsync();

            // 18. Email’in ilk harfine göre gruplama
            Task<List<object>> GetGroupByEmailFirstCharAsync();

            // 19. Aynı yaşa sahip kullanıcıları listeleme
            Task<List<object>> GetGroupByAgeListAsync();

            // 20. Telefon numarası uzunluğuna göre gruplama
            Task<List<object>> GetGroupByPhoneLengthAsync();

            // 21. Hem aktiflik hem yaş bilgisine göre gruplama
            Task<List<object>> GetGroupByIsActiveAndAgeAsync();
        }


    }

