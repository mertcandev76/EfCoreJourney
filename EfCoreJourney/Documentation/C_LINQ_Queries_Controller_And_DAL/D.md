2-🔹 FirstOrDefault() Nedir?
FirstOrDefault(), bir koleksiyonda (dizi, liste, veritabanı tablosu vb.) belirtilen şarta uyan ilk elemanı döner.
Eğer şarta uyan hiçbir eleman yoksa, default değerini döner (yani null referans tiplerde).

Şartsız Kullanım:
Tüm listeyi getirip ilkini seçer:
 return await _appDbContext.Customers.FirstOrDefaultAsync();

 Şartlı Arama:
 Customers tablosunda isim bilgisi "Hasan" olan ilk müşteri getirilir
  return await _appDbContext.Customers.FirstOrDefaultAsync(e => e.Name == "Hasan");

  🧠 Ne Zaman Kullanılır?
Eğer koleksiyonda veri olmayabileceğini düşünüyorsan,
Eğer "ilk bulduğunu getir ama hiçbiri yoksa null getir" mantığı gerekiyorsa.

⚠️ First() ile Farkı:

| Özellik                   | `First()`              | `FirstOrDefault()` |
| ------------------------- | ---------------------- | ------------------ |
| Veri yoksa                | **Exception fırlatır** | **null döner**     |
| Güvenli mi?               | ❌ Hayır                | ✅ Evet             |
| Null kontrolü gerekir mi? | Hayır                  | ✅ Evet             |
