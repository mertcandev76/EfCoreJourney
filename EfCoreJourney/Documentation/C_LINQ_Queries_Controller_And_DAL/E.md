3-🔹 Single() Nedir?
Single() metodu, koleksiyonda tam olarak bir (ve yalnızca bir) öğe varsa onu döner.

Eğer:
Hiç öğe yoksa → InvalidOperationException
Birden fazla öğe varsa → InvalidOperationException
fırlatır.

Şartsız Kullanım:
Tüm listede sadece bir kayıt varmı?:
 return await _appDbContext.Customers.SingleAsync();

 Şartlı Arama:
 Customers tablosunda isim bilgisi sadece "Hasan" olan 1 müşteri varmı?
  return await _appDbContext.Customers.SingleAsync(e => e.Name == "Hasan");


🧠 Ne Zaman Kullanılır?
Koleksiyonda tam olarak 1 tane eleman olduğunu garanti edebiliyorsan kullanılır.
Özellikle benzersiz (unique) verilerde: örneğin bir e-posta, ID veya kullanıcı adı gibi.



