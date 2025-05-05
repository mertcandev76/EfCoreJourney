6-🔹LastAsync() Nedir?
LastAsync(), bir koleksiyon ya da veritabanı sorgusunda, şarta uyan son öğeyi asenkron şekilde getirir.

Eğer:
Hiç öğe yoksa → InvalidOperationException
Birden fazla öğe varsa → InvalidOperationException
fırlatır.

⚠️ Dikkat!
1. LastAsync() için veri sıralı olmalıdır
EF Core LastAsync()'i ancak bir sıralama (OrderBy) varsa verimli şekilde çalıştırabilir.

Aksi halde şu hatayı alırsın:

Şartsız Kullanım:
Tüm listede son kayıt varmı?:
 return await _appDbContext.Customers
 .OrderBy(c => c.Id) // sıralama şart
 .LastAsync();

 Şartlı Arama:
 Customers tablosunda customerID olan 1son kayıt müşteriyi getir
  return await _appDbContext.Customers
  .OrderBy(c => c.Id) // sıralama şart
  .LastAsync();

 Hatalı Kullanım:
var musteri = await _context.Customers.LastAsync(); // sıralama YOK → hata

🆚 FirstAsync() ile farkı

| Özellik             | `FirstAsync()`               | `LastAsync()`                  |
| ------------------- | ---------------------------- | ------------------------------ |
| Dönen veri          | Tekil (ilk eşleşen)          | Tekil (son eşleşen)            |
| Varsayılan sıralama | Varsayılan veritabanı sırası | Ama sıralama verilmesi gerekir |
| Veri yoksa          | Hata fırlatır                | Hata fırlatır                  |  
| Filtre destekli     | Evet                         | Evet                           |
