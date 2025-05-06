Aggregate Functions-Tekli Veri Getiren Sorgulama Fonksiyonları

8-🔹Count() Nedir?
Count() metodu, bir koleksiyondaki eleman sayısını döndürmek için kullanılır. Veritabanı sorgularında, koleksiyonlarda ve IEnumerable, IQueryable gibi yapılarda çok yaygın bir şekilde kullanılır.

Açıklama: Koleksiyondaki öğelerin sayısını döndürür.
Dönüş Tipi: int
Asenkron Versiyon: CountAsync()
Dönüş Tipi: Task<int>
Kullanım: Koleksiyondaki öğe sayısını asenkron olarak döndürür.


✅ Count() – Eleman Türü ve Dönüş Tipi Tablosu

| Koleksiyondaki Eleman Türü | `Count()` Dönüş Tipi | `CountAsync()` Dönüş Tipi | Açıklama                             
| -------------------------- | -------------------- | ------------------------- | -------------------------------------
| `int`                      | `int`                | `Task<int>`               | Elemanlar sayılır, tür önemli değil          |
| `string`                   | `int`                | `Task<int>`               | Geçerli                                      |
| `Product` (sınıf)          | `int`                | `Task<int>`               | Entity sayılır                               |
| `decimal`                  | `int`                | `Task<int>`               | Değer sayısı                                 |
| `bool`                     | `int`                | `Task<int>`               | Koşula göre sayım yapılabilir                |
| `nullable` türler (`int?`) | `int`                | `Task<int>`               | `null` olanlar filtrelenmedikçe dahil edilir |
| **Herhangi bir tür         | `int`                | `Task<int>`               | Her koleksiyonda `Count` yapılabilir         

🔹 Temel Kullanım
return await _appDbContext.Customers.CountAsync();
Customers tablosundaki toplam müşteri sayısını verir.

🔹 Şartlı Kullanım
return await _appDbContext.Customers
    .CountAsync(c => c.Address.Contains("Avcılar"));
Adresi “Avcılar” içeren müşterilerin sayısını döndürür.

Örnek
Aynı Soyada Sahip Müşteri Sayısı
return await _appDbContext.Customers.CountAsync(c => c.LastName == "Demir");

View'da Count Göstermek (Razor)

1-@_appDbContext.Customers.Count()
ya da
2-Toplam Müşteri Sayısı: @Model.Count()

Not!!!
CountAsync() int türünde bir değer döndürür.
Eğer amaç sadece müşteri sayısını almaksa:

public async Task<int> GetCustomerCountAsync()
{
    return await _appDbContext.Customers.CountAsync();
}


