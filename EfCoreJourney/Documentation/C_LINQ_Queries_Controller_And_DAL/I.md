7-🔹LastOrDefaultAsync() Nedir?
LastOrDefaultAsync(), bir koleksiyon veya veritabanı sorgusunda şarta uyan son öğeyi asenkron olarak getirir.
Eğer hiç öğe yoksa, hata fırlatmaz — onun yerine null döner.

⚠️ Önemli Not: Sıralama Gerekli!

 Ne İşe Yarar?
Koleksiyonda veya veritabanında son öğeyi almak istediğinde kullanılır.
Hiç eşleşme yoksa, uygulaman çökmez; güvenli şekilde null döner.
LastAsync()'in daha güvenli bir versiyonudur.


Şartsız Kullanım:
Tüm listede son kayıt varmı?:
return await _appDbContext.Customers
    .OrderBy(c => c.Id)
    .LastOrDefaultAsync();

 Şartlı Arama:
 Customers tablosunda customerID olan 1son kayıt müşteriyi getir
return await _appDbContext.Customers
    .OrderBy(c => c.Id)
    .LastOrDefaultAsync(c => c.City == "Ankara");

🆚 LastAsync() ile Farkı

| Özellik                 | `LastAsync()`                        | `LastOrDefaultAsync()`                     |
| ----------------------- | ------------------------------------ | ------------------------------------------ |
| Veri bulunmazsa         | ❌ Hata fırlatır (`InvalidOperation`) | ✅ `null` döner                             |
| Dönen öğe               | Tekil                                | Tekil                                      |
| Sıralama (`OrderBy`)    | ✅ Gerekir                            | ✅ Gerekir                                  |
| Filtre uygulanabilir mi | ✅ Evet                               | ✅ Evet                                     |
| Kullanım amacı          | Mutlaka veri olduğunda               | Veri olabilir ya da olmayabilir durumlarda |

