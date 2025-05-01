2-🔹 Where Fonksiyonu Nedir?
Where, bir koleksiyon veya sorgu üzerinde şart belirterek sadece o şarta uyan elemanları seçmeyi sağlar.

📌 Temel Kullanımı:
var sonuc = koleksiyon.Where(sart);
koleksiyon: Liste, dizi, EF Core sorgusu olabilir.
sart: Lambda ifadeyle (=>) yazılır. Hangi verilerin seçileceğini tanımlar.

🔸 Entity Framework Örneği (Customer tablosu ile)
✅ 1. Aktif müşterileri getirmek:

var aktifMusteriler = await _context.Customers
    .Where(c => c.IsActive)
    .ToListAsync();
Veritabanına şu SQL gider:
SELECT * FROM Customers WHERE IsActive = 1

✅ 2. Belirli bir isimdeki müşteriler:

var ahmetler = await _context.Customers
    .Where(c => c.Name == "Ahmet")
    .ToListAsync();


🎯 Bonus: Liste Üzerinde Where
List<Customer> customers = GetAllCustomers();
var aktifler = customers.Where(c => c.IsActive).ToList();
Bu durumda Where, bellekteki veriler üzerinde çalışır, veritabanı kullanılmaz.
