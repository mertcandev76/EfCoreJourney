Kuantlama (quantifiers)-Çoklu Veri Getiren Sorgulama Fonksiyonları

13-🔹 Any()
Koleksiyonda, belirli bir koşulu sağlayan en az bir eleman olup olmadığını kontrol eder.

✅ Any() – Eleman Türü ve Dönüş Tipi Tablosu

| Koleksiyondaki Eleman Türü | `Any()` Dönüş Tipi | `AnyAsync()` Dönüş Tipi | Açıklama                           |
| -------------------------- | ------------------ | ------------ | ----------------------------------------------|
| `int`                      | `bool`             | `Task<bool>` | Sayısal veri içeren koleksiyon                |
| `long`                     | `bool`             | `Task<bool>` | Büyük sayılar içeren koleksiyon               |
| `float`                    | `bool`             | `Task<bool>` | Ondalıklı veri içeren koleksiyon              |
| `double`                   | `bool`             | `Task<bool>` | Daha hassas ondalıklı veriler için            |
| `decimal`                  | `bool`             | `Task<bool>` | Finansal verilerde minimum değer kontrolünde  |
| `string`                   | `bool`             | `Task<bool>` | En az bir metinsel ifade içeriyor mu?         |
| `DateTime`                 | `bool`             | `Task<bool>` | Zaman verisi içeren koleksiyonlarda           |
| `DateTimeOffset`           | `bool`             | `Task<bool>` | Zaman dilimli tarih verilerinde               |
| `TimeSpan`                 | `bool`             | `Task<bool>` | Süre içeren veri koleksiyonlarında            |
| `Guid`                     | `bool`             | `Task<bool>` | Benzersiz ID’ler bulunduğu koleksiyonlarda    |
| `bool`                     | `bool`             | `Task<bool>` | Doğruluk değerleri için (true/false)          |
| `nullable int?`            | `bool`             | `Task<bool>` | Null olabilen sayılar içeren koleksiyonda     |
| `nullable decimal?`        | `bool`             | `Task<bool>` | Null olabilen finansal veriler                |
| `Customer` (veya class)    | `bool`             | `Task<bool>` | Varlık sınıfı (Entity) içeren koleksiyonlarda |


Kullanım Senaryosu:
Aktif olan en az bir müşteri var mı?
Şartsız
    return await _appDbContext.Customers.AnyAsync();
Şartlı
public async Task<bool> CustomerExistsAsync()
{
    return await _appDbContext.Customers.AnyAsync(c => c.IsActive == true);
}
Açıklama:
AnyAsync: Koleksiyonda IsActive == true olan en az bir müşteri varsa true döner, yoksa false.

