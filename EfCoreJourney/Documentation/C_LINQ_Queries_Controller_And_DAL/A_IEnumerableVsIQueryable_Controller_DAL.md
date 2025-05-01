IEnumerable ve IQueryable Karşılaştırması

Diyelim ki bir müşteri listesi var:
List<Customer> customers = new List<Customer>
{
    new Customer { Name = "Ali", IsActive = true },
    new Customer { Name = "Veli", IsActive = false },
    new Customer { Name = "Ayşe", IsActive = true }
};
Sen bu listeyi süzmek (aktif müşterileri bulmak) istiyorsun.

🧩 IEnumerable Nedir?
Elinde bir liste (veri) varsa ve onun üstünde işlem yapmak istiyorsan IEnumerable devreye girer.

Yani:

Veri zaten bellekte (örneğin List<Customer> gibi).
Sen bu veriyle çalışırsın.
Filtreleme, sıralama gibi işlemler bilgisayarında (bellekte) yapılır.

🧪 Örnek:

IEnumerable<Customer> aktifMusteriler = customers.Where(c => c.IsActive);
Bu örnekte:

customers: Zaten bellekte.
.Where(...): Filtreleme senin bilgisayarında yapılır.

🔌 IQueryable Nedir?
Veri veritabanındaysa ve sen henüz çekmediysen, IQueryable kullanılır.

Yani:

Veri veritabanında duruyor.
Sen diyorsun ki: "Veritabanına sor, sadece aktif olanları getir."

🧪 Örnek:

IQueryable<Customer> query = _appDbContext.Customers.Where(c => c.IsActive);
Bu örnekte:
Customers: Veritabanındaki tablo.
.Where(...): Bu sorgu SQL olarak veritabanına gider.
Veritabanı sadece aktif olanları gönderir → daha hızlı, daha az veri gelir.

🔁 En Basit Karşılaştırma:

| Ne zaman? | `IEnumerable` | `IQueryable` |
|----------|----------------|--------------|
| Veri nerede? | Bellekte | Veritabanında |
| Nerede çalışır? | Bilgisayarında | Veritabanında |
| Performans | Küçük veri için iyi | Büyük veri için iyi |
| Örnek | `List<Customer>` | `DbSet<Customer>` |

🎯 Özet:
🔸 IEnumerable: Verin zaten varsa, üzerinde işlem yapmak için.
🔸 IQueryable: Veriyi veritabanından süzerek getirmek için.

