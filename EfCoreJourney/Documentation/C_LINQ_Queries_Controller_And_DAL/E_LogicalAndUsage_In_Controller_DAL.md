🔹 Where Fonksiyonu ile Operatörlerle Çalışma

 1. Mantıksal Operatör Nedir?
Mantıksal operatörler, birden fazla koşulu birleştirmek, koşulu tersine çevirmek veya bir koşulun sağlanıp sağlanmadığını kontrol etmek için kullanılır.

1.1. Basit Sorgular (Tek Koşul)

📌 Örnek 1:
Aktif müşterileri listele (IsActive == true)
var sonuc = db.Customers.Where(c => c.IsActive).ToList();

📌Örnek 2:
İsmi "hasan" olan müşterileri listele
var sonuc = db.Customers
    .Where(c => c.Name == "hasan")
    .ToList();

1.2.Mantıksal VE (&&) Kullanımı

&& → VE (AND) Operatörü
Her iki koşul doğruysa, sonuç doğru olur.
Biri bile yanlışsa, sonuç yanlıştır.
🧠 Akılda Kalması İçin:
&& → Her ikisi de doğru olmalı (Sıkı bir filtre)


 ÖRNEKLER: && (Mantıksal VE) ile Birlikte Karşılaştırma Operatörlerini (==, !=, <, >, <=, >=)

📌 Örnek 1:
Aktif olan ve e-posta adresi loremipsum@gmail.com olan müşterilerileri listele
 return await _appDbContext.Customers
              .Where(c => c.IsActive && c.Email == "loremipsum@gmail.com")
              .ToListAsync();
SQL karşılığı:
SELECT * FROM Customers WHERE IsActive = 1 AND Email="loremipsum@gmail.com";

📌Örnek 2:
Aktif olan ve e-posta adresi olmayan müşterileri listele
 return await _appDbContext.Customers
              .Where(c => c.IsActive && c.Email != null)
              .ToListAsync();
SQL karşılığı:
SELECT * FROM Customers WHERE IsActive = 1 AND Email IS NOT NULL;


📌Örnek 3:
ID’si 10’dan büyük ve aktif olan müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.CustomerID > 10 && c.IsActive)
    .ToListAsync();

📌Örnek 4:
İsmi "mertcan" olan ve ID’si 5’ten küçük olan müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.Name == "mertcan" && c.CustomerID < 5)
   .ToListAsync();


📌Örnek 5:
Email'i olmayan ve aktif olmayan müşterileri listele
 return await _appDbContext.Customers
    .Where(c => string.IsNullOrEmpty(c.Email) && !c.IsActive)
    .ToListAsync();


📌Örnek 6:
 İsmi “Mehmet” değil ve ID’si 20’den küçük olan müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.Name != "Mehmet" && c.CustomerID < 20)
    .ToListAsync();

📌Örnek 7:
ismi 5 harf olan müşterelire listele(yada örneklerde 5 karakterli diye)
 return await _appDbContext.Customers
    .Where(c => c.Name.Length == 3)
    .ToListAsync();

📌Örnek 8:
Telefonu boş olmayan ve ismi 3 harften uzun olan müşterileri listele
 return await _appDbContext.Customers
    .Where(c => !string.IsNullOrEmpty(c.Phone) && c.Name.Length > 3)
    .ToListAsync();

📌Örnek 9:
ID’si çift sayı olan(mod alma işlemiyle) ve aktif olan müşteriyi listele
 return await _appDbContext.Customers
    .Where(c => c.CustomerID % 2 == 0 && c.IsActive)
    .ToListAsync();

📌Örnek 10:
ID’si tek sayı olan(mod alma işlemiyle)  müşteriyi listele
 return await _appDbContext.Customers
    .Where(c => c.CustomerID % 2 == 1 )
    .ToListAsync();

📌Örnek 11:
ID’si tek sayı olan(mod alma işlemiyle)  müşteriyi listele
 return await _appDbContext.Customers
    .Where(c => c.CustomerID % 2 == 1 )
   .ToListAsync();

📌Örnek 12:
ismi büyük harfle başlayan müşteriyi listele
 return await _appDbContext.Customers
                .Where(x => char.IsUpper(x.Name[0]))
                .ToListAsync();


📌Örnek 13:
ismi küçük harfle başlayan müşteriyi listele
 return await _appDbContext.Customers
                .Where(x => char.IsLower(x.Name[0]))
                .ToListAsync();

📌Örnek 14:
İsmi küçük harfe çevrilince "Hasan" olan müşteriyi
 return await _appDbContext.Customers
                .Where(x => x.Name.ToLower() == "Hasan").
                .ToListAsync();

📌Örnek 15:
İsmi büyük harfe çevrilince "Mertcan" olan müşteriyi
 return await _appDbContext.Customers
                .Where(x => x.Name.ToUpper() == "Mertcan").
                .ToListAsync();
