4-🔹SingleOrDefault() Nedir?
SingleOrDefault() metodu, koleksiyonda tam olarak bir (ve yalnızca bir) öğe varsa onu döner.

Eğer:
Hiç öğe yoksa → default değerini döner (null referans tiplerde).
Birden fazla öğe varsa → InvalidOperationException fırlatır.

Şartsız Kullanım:
Tüm listede sadece bir kayıt varmı?:
 return await _appDbContext.Customers.SingleAsync();

 Şartlı Arama:
 Customers tablosunda isim bilgisi sadece "Hasan" olan 1 müşteri varmı?
  return await _appDbContext.Customers.SingleAsync(e => e.Name == "Hasan");

🧠 Ne Zaman Kullanılır?
Koleksiyonda tam olarak 1 öğe olmasını garanti edemiyorsan ama eğer yoksa güvenli bir şekilde işlem yapmak istiyorsan, SingleOrDefault() kullanılır.

Eğer 0 veya 1 öğe dönecekse, ancak 2+ öğe olması beklenmeyen durum ise.