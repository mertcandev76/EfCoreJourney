21-🔹 Except()

 Bir koleksiyondan diğer koleksiyonun öğelerini çıkararak iki koleksiyon arasındaki farkı döndürür. Bu, ilk koleksiyondaki ancak ikinci koleksiyondaki olmayan öğeleri alır. Bu işlem, koleksiyonların sırasını dikkate almaz ve benzersiz öğeler döndürür.

1. Except() Fonksiyonunun Temel Kullanımı
Aşağıda Except() fonksiyonunun temelde nasıl kullanıldığına dair bir örnek verilmiştir:
using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var list1 = new List<int> { 1, 2, 3, 4, 5 };
        var list2 = new List<int> { 4, 5, 6, 7, 8 };

        var result = list1.Except(list2); // list1'de olup list2'de olmayan öğeler

        foreach (var item in result)
        {
            Console.WriteLine(item);  // 1, 2, 3 yazdırır
        }
    }
}

Except() Fonksiyonunun Kullanım Şartları
Except() metodu sadece benzersiz elemanları dikkate alır, yani tekrarlanan elemanlar yok sayılır.
Eğer öğeler aynı tipte ve karşılaştırılabilir (yani, doğru bir Equals() ve GetHashCode() implementasyonuna sahiplerse), Except() doğru şekilde çalışır.

public class EfCustomerRepository : ICustomerDal
{
    private readonly AppDbContext _appDbContext;

    public EfCustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Tüm müşterileri getir
    public async Task<List<Customer>> GetAll()
    {
        return await _appDbContext.Customers.ToListAsync();
    }

    // Diğer bir müşteri listesi (örneğin, aktif olmayanlar)
    public async Task<List<Customer>> GetInactiveCustomers()
    {
        return await _appDbContext.Customers
            .Where(c => !c.IsActive)
            .ToListAsync();
    }

    // İki listeyi karşılaştırarak farkı almak için bir metot ekleyelim
    public async Task<List<Customer>> GetUniqueCustomers()
    {
        var allCustomers = await GetAll();  // Tüm müşteriler
        var inactiveCustomers = await GetInactiveCustomers();  // Aktif olmayan müşteriler

        // Except kullanarak sadece aktif olan müşterileri alıyoruz (list1'de olup list2'de olmayanlar)
        var uniqueCustomers = allCustomers.Except(inactiveCustomers, new CustomerComparer()).ToList();

        return uniqueCustomers;
    }
}

CustomerComparer Sınıfı
Except() fonksiyonu, iki koleksiyonu karşılaştırırken her elemanın eşit olup olmadığını kontrol eder. Eğer Customer gibi bir sınıfı karşılaştıracaksak, Equals ve GetHashCode metodlarını düzgün bir şekilde tanımlamamız gerekir. Bu amaçla bir CustomerComparer sınıfı yazalım.

public class CustomerComparer : IEqualityComparer<Customer>
{
    public bool Equals(Customer x, Customer y)
    {
        // Müşteri ID'si eşitse, iki müşteri eşittir
        return x.CustomerID == y.CustomerID;
    }

    public int GetHashCode(Customer obj)
    {
        return obj.CustomerID.GetHashCode();
    }
}

--->CustomerController Sınıfı

public class CustomerController : Controller
{
    private readonly ICustomerDal _customerDal;

    public CustomerController(ICustomerDal customerDal)
    {
        _customerDal = customerDal;
    }

    public async Task<IActionResult> Index()
    {
        // Veritabanından unique müşterileri alıyoruz (aktif olanları)
        var uniqueCustomers = await _customerDal.GetUniqueCustomers();

        // Sonuçları model olarak view'a gönderiyoruz
        var model = new CustomerListViewModel
        {
            Customers = uniqueCustomers
        };

        return View(model);
    }
}





