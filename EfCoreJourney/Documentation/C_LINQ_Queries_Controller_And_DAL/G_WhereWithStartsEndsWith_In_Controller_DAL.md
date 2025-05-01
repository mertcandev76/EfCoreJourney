🔹 Where Fonksiyonu ile StartsWith ve EndWithle Çalışma

🔹 Temel Seviye: StartsWith Nedir?

❓ Amaç:
Bir string alanın belirli bir harf veya kelimeyle başlayıp başlamadığını kontrol eder.

string name = "Mertcan";
bool result = name.StartsWith("Mer"); // true
Ama senin asıl ilgin LINQ içinde kullanımı.

📌 Örnek 1:  Adı "A" harfiyle başlayan müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.Name.StartsWith("A"))
    .ToListAsync();

Not!!!
StartsWith + ToLower (Büyük küçük harf duyarlılığına dikkat)
StartsWith büyük/küçük harf duyarlıdır. Eğer "ali", "Ali" olarak kayıtlıysa StartsWith("a") eşleşmez. Bu yüzden:

 return await _appDbContext.Customers
    .Where(c => c.Name.ToLower().StartsWith("a"))
    .ToListAsync();
Ama EF Core 6+ kullanılırsa EF.Functions.Like daha performanslı olur.

📌 Örnek 2: Hem aktif hem de adı "S" ile başlayan müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.IsActive && c.Name.StartsWith("S"))
    .ToListAsync();

📌 Örnek 3:Telefonu 0 ile başlamayan müşterileri listele
return await _appDbContext.Customers
                .Where(x => !x.Phone.StartsWith("0"))
                .ToListAsync();

🔹 Temel Seviye: EndsWith Nedir?
❓ Amaç:
Bir string'in belirli bir harf/grup ile bitip bitmediğini kontrol eder.

string email = "mertcan@gmail.com";
bool result = email.EndsWith("gmail.com"); // true
Ama bizim amacımız, bunu LINQ içinde EF Core üzerinde kullanmak.

📌 Örnek 1: e-posta adresi gmail.com ile biten müşterileri listele

 return await _appDbContext.Customers
    .Where(c => c.Email.EndsWith("gmail.com"))
   .ToListAsync();

Not!!!
EndsWith + ToLower() (Harf duyarlılığına dikkat)
 return await _appDbContext.Customers
    .Where(c => c.Email.ToLower().EndsWith("gmail.com"))
   .ToListAsync();
Çünkü "MERTCAN@GMAIL.COM" gibi büyük harfli veriler olabilir.


