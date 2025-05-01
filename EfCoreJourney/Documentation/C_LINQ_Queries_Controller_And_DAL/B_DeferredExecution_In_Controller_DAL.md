🕓 Deferred Execution Nedir?
LINQ gibi sorguların hemen çalışmaması, sadece hazırlanması, ama sonuçlara ihtiyaç duyulana kadar çalıştırılmaması demektir.

🧠 Basit Tanım:
Sorguyu yazarsın ama veri çekilmez. Sorgu sadece bir plan gibidir.
Gerçek çalıştırma, sen ToList(), FirstOrDefault(), Count() gibi bir tetikleyici metot çağırınca olur.

📦 Örnek:

var query = _appDbContext.Customers.Where(c => c.IsActive);
// Bu satırda veritabanına sorgu GİTMEZ

var list = query.ToList(); 
// İşte burada sorgu çalışır, veritabanından veri gelir

🔍 Nerede Kullanılır?
IQueryable ve IEnumerable LINQ sorgularında.
Select, Where, OrderBy gibi sorgu metotları zincirlenir ama çalışmaz.
Çalışma, sonucu aldığın yerde olur.

🚀 Avantajı:
Gereksiz yere veritabanına sorgu atmazsın.
Sorgular zincirleme büyüyebilir, son anda çalışır.
Performans açısından çok iyidir.

🔥 Eager Execution (Karşıtı) Nedir?
Eager execution: Sorgu hemen çalışır, veri hemen gelir.

var list = _appDbContext.Customers.Where(c => c.IsActive).ToList();
// Burada veritabanı sorgusu hemen yapılır — eager execution

🎯 Özet:

| Özellik | Deferred Execution |
|--------|---------------------|
| Ne zaman çalışır? | Sonuç istendiğinde (`ToList()`, `First()`, vs.) |
| Ne işe yarar? | Performans kazandırır, gereksiz sorguları engeller |
| Kimde görülür? | `IQueryable`, `IEnumerable`, LINQ sorgularında |
| Avantajı | Daha az kaynak kullanımı |

