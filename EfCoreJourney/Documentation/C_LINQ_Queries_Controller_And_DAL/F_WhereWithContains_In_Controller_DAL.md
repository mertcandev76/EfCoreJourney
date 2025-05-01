🔹 Where Fonksiyonu ile Containsle Çalışma

🧠 1. TEMEL: Contains Nedir?
Contains, C#'ta özellikle koleksiyonlarda veya string ifadelerde belirli bir değerin içerilip içerilmediğini kontrol etmek için kullanılır.

✅ Basit string örneği:
string name = "Mertcan Adsız";
bool result = name.Contains("can"); // true

🧩 2. ORTA SEVİYE: LINQ İle Contains Kullanımı (Entity Listeleme)

📌 Örnek 1: E-postasında "gmail" geçen müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.Email.Contains("gmail"))
    .ToListAsync();

Bu, SQL'de şu şekilde çalışır:
SELECT * FROM Customers WHERE Email LIKE '%gmail%'

📌 Örnek 2: Adında "can" geçen müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.Name.Contains("can"))
    .ToListAsync();

📌 Örnek 3: Telefon numarasında "0555" geçen müşterileri listele
 return await _appDbContext.Customers
    .Where(c => c.Phone.Contains("0555"))
    .ToListAsync();

📌 Örnek 4: Hem aktif olup hem de e-postasında "hotmail" geçen müşteriler
 return await _appDbContext.Customers
    .Where(c => c.IsActive && c.Email.Contains("hotmail"))
    .ToListAsync();

📌 Örnek 5: Adında "mert" geçen müşterileri  Büyük-küçük harf farkını yok sayarak listele
 return await _appDbContext.Customers
    .Where(c => c.Name.ToLower().Contains("mert"))
    .ToListAsync();
Bu sayede "Mert", "mert" veya "MERT" gibi yazımlar fark etmeyecek.

💡 Dikkat:
Contains ifadesi veritabanına uygun şekilde çevrilir. ToList() gibi komutları sonradan kullanman gerekir, yoksa filtreleme bellekte yapılır, bu da performansı düşürür.



