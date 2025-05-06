15-🔹 Contains()
Koleksiyonda belirli bir değerin var olup olmadığını kontrol eder.

✅ Contains() – Eleman Türü ve Dönüş Tipi Tablosu
|  Eleman Türü       | `All()` Dönüş Tipi | `AllAsync()`| Açıklama    

| Koleksiyondaki Eleman Türü | Aranan Değer Türü | `Contains()` Dönüş Tipi | `ContainsAsync()` Dönüş Tipi | Açıklama --------------------------| ----------------- | ----------------------- | ----------------------------  | -----------------|
| `int`                      | `int`             | `bool`                  | `Task<bool>`                 | Koleksiyonda belirli bir tam sayı var mı?                          |
| `long`                     | `long`            | `bool`                  | `Task<bool>`                 | Büyük sayılarda varlık kontrolü                                    |
| `float`                    | `float`           | `bool`                  | `Task<bool>`                 | Ondalık sayılarda varlık kontrolü                                  |
| `double`                   | `double`          | `bool`                  | `Task<bool>`                 | Hassas ondalık değer arama                                         |
| `decimal`                  | `decimal`         | `bool`                  | `Task<bool>`                 | Finansal verilerde değer arama                                     |
| `string`                   | `string`          | `bool`                  | `Task<bool>`                 | E-posta, kullanıcı adı gibi string verilerde eşleşme kontrolü      |
| `DateTime`                 | `DateTime`        | `bool`                  | `Task<bool>`                 | Koleksiyonda belirli bir tarih var mı?                             |
| `DateTimeOffset`           | `DateTimeOffset`  | `bool`                  | `Task<bool>`                 | Zaman dilimi bilgili tarihlerde kontrol                            |
| `TimeSpan`                 | `TimeSpan`        | `bool`                  | `Task<bool>`                 | Süre veri tiplerinde eşleşme                                       |
| `Guid`                     | `Guid`            | `bool`                  | `Task<bool>`                 | Benzersiz kimlik eşleşmesi                                         |
| `bool`                     | `bool`            | `bool`                  | `Task<bool>`                 | Koleksiyonda `true` veya `false` değeri var mı?                    |
| `nullable int?`            | `int?`            | `bool`                  | `Task<bool>`                 | Null olabilen değerlerde eşleşme                                   |
| `nullable decimal?`        | `decimal?`        | `bool`                  | `Task<bool>`                 | Null olabilen finansal değerlerde arama                            |
| `Customer` (veya class)    | `Customer`        | `bool`                  | `Task<bool>`                 | Nesne referansı eşleşmesi (birebir `Equals()` ile karşılaştırılır) |

❌ Şartsız Kullanım – Hatalı Örnek:
var result = customers.Contains(); // ❌ Derleme hatası: Eksik argüman

Şartlı Kullanım 
 E-mail listesinde belirli bir adres var mı?

❌ Geçersiz Kullanım (HATA VERİR):
public async Task<bool> IsEmailExistsAsync(string email)
{
    return await _appDbContext.Customers
        .Select(c => c.Email)
        .ContainsAsync(email);
}
Ondan dolayı ContainsAsync() desteklenmediğinden ToListAsync ile çalışmak gerekir:
public async Task<bool> IsEmailExistsAsync(string email)
{
    var emails = await _appDbContext.Customers
        .Select(c => c.Email)
        .ToListAsync();

    return emails.Contains(email);
}
Açıklama:
Select ile sadece e-posta adreslerini alıyoruz.
Ardından liste içinde email değeri var mı diye kontrol ediyoruz.



-->ViewModel de Nasıl Çağırırız
public class CustomerListViewModel
{
    public bool EmailExists { get; set; }
}

-->Controllerda Nasıl Çağırırız.
public async Task<IActionResult> Index()
{
 var containsEmail = await ((EfCustomerRepository)_customerDal).IsEmailExistsAsync("example@example.com"); // Contains
 var model = new CustomerListViewModel
    {
        EmailExists = containsEmail
    };

    return View(model);
}

-->Viewdeki Görünüm Halinden  Nasıl Çağırırız.

@model CustomerListViewModel
@{
    ViewData["Title"] = "Müşteri Listesi";
}
<h2 class="mb-3">E-posta Kontrol Sonucu</h2>

@if (Model.EmailExists)
{
    <div class="alert alert-success" role="alert">
        <strong>✓</strong> Bu e-posta adresi sistemde <strong>mevcut</strong>.
    </div>
}
else
{
    <div class="alert alert-danger" role="alert">
        <strong>✗</strong> Bu e-posta adresi <strong>bulunamadı</strong>.
    </div>
}

<!-- Diğer müşteri listesi veya veriler buraya gelebilir -->
