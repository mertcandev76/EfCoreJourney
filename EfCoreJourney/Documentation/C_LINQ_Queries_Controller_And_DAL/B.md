14-🔹 All()
Koleksiyondaki tüm elemanların belirli bir koşulu sağlayıp sağlamadığını kontrol eder.

✅ All() – Eleman Türü ve Dönüş Tipi Tablosu

|  Eleman Türü       | `All()` Dönüş Tipi | `AllAsync()`| Açıklama                                                   |
| -------------------| ------------------ | ------------|------------------------------------------------------------|
| `int`              | `bool`             | `Task<bool>`| Tüm elemanlar belirli bir sayısal koşulu sağlıyor mu?      |
| `long`             | `bool`             | `Task<bool>`| Büyük sayı koleksiyonlarında tüm elemanlar kontrolü        |
| `float`            | `bool`             | `Task<bool>`| Tüm ondalık veriler belli bir aralıktamı?                  |
| `double`           | `bool`             | `Task<bool>`| Daha hassas karşılaştırmalar için                          |
| `decimal`          | `bool`             | `Task<bool>`| Finansal verilerde tüm değerler belirli eşiklerigeçiyor mu?|
| `string`           | `bool`             | `Task<bool>`| Tüm string’ler boş değil mi? Belirli kurala uygun mu?      |
| `DateTime`         | `bool`             | `Task<bool>`| Tüm tarihler belli bir zaman aralığında mı?                |
| `DateTimeOffset`   | `bool`             | `Task<bool>`| Tüm zaman dilimli tarihler bir koşulu sağlıyor mu?         |
| `TimeSpan`         | `bool`             | `Task<bool>`| Tüm süreler belirli bir uzunluğun üstünde mi?              |
| `Guid`             | `bool`             | `Task<bool>`| Tüm kimlikler belirli bir yapıya uygun mu?                 |
| `bool`             | `bool`             | `Task<bool>`| Tüm değerler `true` mu?                                    |
| `nullable int?`    | `bool`             | `Task<bool>`| Tüm nullable değerler null değil mi?                       |
| `nullable decimal?`| `bool`             | `Task<bool>`| Tüm finansal nullable değerler geçerli mi?                 |
| `Customer`(class)  | `bool`             | `Task<bool>`| Tüm müşteriler aktif mi? Tüm nesneler  kurala uyuyor mu?   |

Kullanım Senaryosu:
Tüm müşteriler e-posta adresine sahip mi?

All() metodu şartsız olarak doğrudan kullanılamaz, çünkü All() mutlaka bir şart (predicate) ister. Any() gibi şartsız hali yoktur.
❌ Geçersiz Kullanım (HATA VERİR):
var result = customers.All(); // Derleme hatası: Predicate eksik

Şartlı Kullanım
public async Task<bool> AllCustomersHaveEmailAsync()
{
    return await _appDbContext.Customers.AllAsync(c => c.Email != null && c.Email != "");
}
Açıklama:
Eğer tüm müşterilerin Email bilgisi varsa true döner.
Tek bir müşteri bile null ya da boş e-posta adresine sahipse false döner.

-->ViewModel de Nasıl Çağırırız
public class CustomerListViewModel
{
    public bool AllHaveEmail { get; set; }
    // Diğer özellikler...
}

-->Controllerda Nasıl Çağırırız.
public async Task<IActionResult> Index()
{
var allHaveEmail = await ((EfCustomerRepository)_customerDal).AllCustomersHaveEmailAsync(); // All
 var model = new CustomerListViewModel
    {
        AllHaveEmail = allHaveEmail,
    };

    return View(model);
}

-->Viewdeki Görünüm Halinden  Nasıl Çağırırız.
@model CustomerListViewModel

<h2>Müşteri E-posta Kontrolü</h2>

@if (Model.AllHaveEmail)
{
    <div class="alert alert-success">
        Tüm müşterilerin e-posta adresi mevcuttur.
    </div>
}
else
{
    <div class="alert alert-danger">
        Bazı müşterilerin e-posta adresi eksik!
    </div>
}
