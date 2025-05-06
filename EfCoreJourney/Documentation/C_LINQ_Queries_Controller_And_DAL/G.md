11-🔹Min() Nedir?

bir koleksiyondaki en küçük değeri almak için kullanılır. Bu fonksiyon, genellikle sayısal veya sıralanabilir (comparable) veri türleriyle çalışır. Min() fonksiyonu, bir koleksiyon üzerinde çalışırken, sıralanabilir veri türlerinin en küçük değerini döndüren bir işlem gerçekleştirir.

Kullanım Örneği:
Aşağıdaki örnekte, Customer tablosundaki en küçük Age (Yaş) değerini almak için Min() fonksiyonu kullanılmaktadır:

public async Task<int?> GetMinAgeAsync()
{
    var minAge = await _context.Customers
                                .Where(c => c.Age.HasValue) // Null olmayan yaşları al
                                .MinAsync(c => c.Age);      // En küçük yaşı al
    return minAge;
}
Açıklamalar:

Where(c => c.Age.HasValue) filtresi, null olmayan yaş değerlerini almak için kullanılır.
MinAsync(c => c.Age) ise bu değerler arasında en küçük olanı döndürür.

12-🔹 Max() Fonksiyonu
Max() fonksiyonu, bir koleksiyondaki en büyük değeri almak için kullanılır. Min() fonksiyonunun zıttı olarak, en yüksek değeri döndürür. Bu fonksiyon da sıralanabilir veri türleri üzerinde çalışır.

Kullanım Örneği:
Aşağıdaki örnekte, Customer tablosundaki en büyük Age (Yaş) değerini almak için Max() fonksiyonu kullanılmaktadır:

public async Task<int?> GetMaxAgeAsync()
{
    var maxAge = await _context.Customers
                                .Where(c => c.Age.HasValue) // Null olmayan yaşları al
                                .MaxAsync(c => c.Age);      // En büyük yaşı al
    return maxAge;
}
Açıklamalar:

Where(c => c.Age.HasValue) filtresi, null olmayan yaş değerlerini almak için yine kullanılır.
MaxAsync(c => c.Age) fonksiyonu, bu değerler arasında en büyük olanı döndürür. 



Min() ve Max() Kullanırken Dikkat Edilmesi Gerekenler

Min() fonksiyon üzerinden anlatalım:

1. Nullable Değerler ve null Kontrolü
Min() fonksiyonu nullable türlerle çalışırken bazı özel durumlar yaratabilir. Eğer koleksiyon null değeri içeren öğelere sahipse, bu değerler göz ardı edilir, ancak yine de dikkat edilmesi gereken bazı noktalar vardır.

Öneri:
Null Değerlerin Göz Ardı Edilmesi: Min() fonksiyonu, nullable türlerde (örneğin int?, decimal?, DateTime?) null değerleri görmezden gelir. Bu yüzden veritabanındaki bazı değerler null olabilir. Eğer null değerler göz önünde bulundurulmak isteniyorsa, önce Where() ile null değerlerin filtrelenmesi önerilir.

// Nullable olmayanları filtreleyerek min yaş alır
var minAge = _context.Customers
                     .Where(c => c.Age.HasValue) // Null olmayan yaşlar
                     .MinAsync(c => c.Age);

2. Boş Koleksiyonlar ve InvalidOperationException
Eğer koleksiyon boşsa, Min() fonksiyonu bir InvalidOperationException hatası fırlatır. Bu, özellikle veritabanı sorgularında önemli bir noktadır, çünkü bazen veritabanı boş olabilir veya sorgu, hiç veri döndürmeyebilir.


✅ Min() ve Max() – Eleman Türü ve Dönüş Tipi Tablosu

| Koleksiyondaki Eleman Türü | `Min()` Dönüş Tipi | `Max()` Dönüş Tipi | `MinAsync()` Dönüş Tipi | `MaxAsync()` Dönüş Tipi | Açıklama                                       |
| -------------------------- | ------------------ | ------------------ | ----------------------- | ----------------------- | ---------------------------------------------- |
| `int`                      | `int`              | `int`              | `Task<int>`             | `Task<int>`             | Sayısal veriler için                           |
| `long`                     | `long`             | `long`             | `Task<long>`            | `Task<long>`            | Büyük sayılar için                             |
| `float`                    | `float`            | `float`            | `Task<float>`           | `Task<float>`           | Ondalık sayılar                                |
| `double`                   | `double`           | `double`           | `Task<double>`          | `Task<double>`          | Yüksek hassasiyetli ondalıklı veriler          |
| `decimal`                  | `decimal`          | `decimal`          | `Task<decimal>`         | `Task<decimal>`         | Finansal ve hassas veriler için                |
| `DateTime`                 | `DateTime`         | `DateTime`         | `Task<DateTime>`        | `Task<DateTime>`        | Tarih verileri için                            |
| `DateTimeOffset`           | `DateTimeOffset`   | `DateTimeOffset`   | `Task<DateTimeOffset>`  | `Task<DateTimeOffset>`  | Zaman dilimi bilgisiyle tarih                  |
| `TimeSpan`                 | `TimeSpan`         | `TimeSpan`         | `Task<TimeSpan>`        | `Task<TimeSpan>`        | Zaman farkları                                 |
| `Guid`                     | `Guid`             | `Guid`             | `Task<Guid>`            | `Task<Guid>`            | Benzersiz kimlikler                            |
| `string`                   | `string`           | `string`           | `Task<string>`          | `Task<string>`          | Alfabetik sıralama                             |
| `nullable int?`            | `int?`             | `int?`             | `Task<int?>`            | `Task<int?>`            | Nullable sayılar                               |
| `nullable long?`           | `long?`            | `long?`            | `Task<long?>`           | `Task<long?>`           | Nullable büyük sayılar                         |
| `nullable float?`          | `float?`           | `float?`           | `Task<float?>`          | `Task<float?>`          | Nullable ondalıklı sayılar                     |
| `nullable double?`         | `double?`          | `double?`          | `Task<double?>`         | `Task<double?>`         | Nullable yüksek hassasiyetli ondalıklı veriler |
| `nullable decimal?`        | `decimal?`         | `decimal?`         | `Task<decimal?>`        | `Task<decimal?>`        | Nullable finansal veriler                      |






















