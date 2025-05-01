1.3.Mantıksal VEYA (||) Kullanımı

|| → VEYA (OR) Operatörü
Koşullardan en az biri doğruysa, sonuç doğru olur.
İkisi de yanlışsa, sonuç yanlıştır.

🧠 Akılda Kalması İçin:
|| → Biri doğruysa yeter (Esnek bir filtre)



 ÖRNEKLER: || (Mantıksal VEYA) ile Birlikte Karşılaştırma Operatörlerini (==, !=, <, >, <=, >=)

 📌 Örnek 1:
 Adı ya "Ali" ya da "Veli" olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => x.Name == "Ali" || x.Name == "Veli")
              .ToListAsync();

 📌 Örnek 2:
E-posta ya da telefon bilgisi eksik olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => x.Email == null || x.Phone == null)
              .ToListAsync();


 📌 Örnek 3:
Aktif olanlar veya ID’si 0 olan olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => x.IsActive || x.CustomerID == 0)
              .ToListAsync();


 📌 Örnek 4:
ID'si 1 veya 2   olan müşterileri listele
 return await _appDbContext.Customers
             .Where(x => x.CustomerID == 1 || x.CustomerID == 2)
              .ToListAsync();


 📌 Örnek 5:
Türk numarası veya +90 ile başlayan numaraları olan müşterileri listele
 return await _appDbContext.Customers
             .Where(x => x.Phone.StartsWith("05") || x.Phone.StartsWith("+90"))
              .ToListAsync();


 📌 Örnek 6:
Adında "can" geçen veya Gmail olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => x.Name.Contains("can") || x.Email.Contains("gmail.com"))
              .ToListAsync();

 📌 Örnek 7:
ID’si 10'dan büyük  eşit olan veya ismi 5 karakterden uzun  olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => x.CustomerID >= 10 || x.Name.Length > 5)
              .ToListAsync();


 📌 Örnek 8:
 Aktif olmayan veya çift sayılı ID'ye sahip olan müşterileri listele
 return await _appDbContext.Customers
             .Where(x => !x.IsActive || x.CustomerID % 2 == 0)
              .ToListAsync();

 📌 Örnek 9:
 İsim veya e-posta boş olmayan müşterileri listele
 return await _appDbContext.Customers
             .Where(x => x.Email != null || x.Name != null)
              .ToListAsync();

 📌 Örnek 10:
Adı "ahmet" veya "mehmet" (büyük-küçük harf duyarsız)  olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => x.Name.ToLower() == "ahmet" || x.Name.ToLower() == "mehmet")
              .ToListAsync();


 📌 Örnek 11:
Aktif olanlardan ID’si 5’ten büyük veya pasif olup ID’si 3’ten küçük olan müşterileri listele
 return await _appDbContext.Customers
            .Where(x => (x.IsActive && x.CustomerID > 5) || (!x.IsActive && x.CustomerID < 3))
              .ToListAsync();



 📌 Örnek 12:
Telefonu 10 haneli veya e-posta .com  olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => x.Phone?.Length == 10 || x.Email?.EndsWith(".com") == true)
              .ToListAsync();

 📌 Örnek 13:
 Adında "a" geçen ve hotmail olanlar veya "e" geçip gmail  olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => (x.Name.Contains("a") && x.Email.Contains("hotmail")) || (x.Name.Contains("e") && x.Email.Contains("gmail")))
              .ToListAsync();


 📌 Örnek 14:
 E-posta veya telefon boş bırakılmış olan müşterileri listele
 return await _appDbContext.Customers
              .Where(x => string.IsNullOrWhiteSpace(x.Email) || string.IsNullOrWhiteSpace(x.Phone))
              .ToListAsync();


 📌 Örnek 15:
 ID’si 6–9 arasında olanlar veya 20’den büyük ve aktif  olan müşterileri listele
 return await _appDbContext.Customers
               .Where(x => (x.CustomerID > 5 && x.CustomerID < 10) || (x.CustomerID > 20 && x.IsActive))
              .ToListAsync();

