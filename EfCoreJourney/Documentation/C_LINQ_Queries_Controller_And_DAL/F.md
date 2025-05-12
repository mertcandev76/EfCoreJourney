Set Operations
 LINQ'te birden fazla liste (koleksiyon) veya veritabanı sorgusu üzerinde kesişim, birleşim, fark gibi işlemleri yapmamızı sağlar.

Bunlar matematikteki küme işlemlerinin programlamadaki karşılığıdır. LINQ ile koleksiyonlar üzerinde kolayca bu işlemleri gerçekleştirebiliriz.

🔸 Kullanılan Başlıca Set Operasyonları

| Metot         | Anlamı                 | Açıklama                                                   |
| ------------- | ---------------------- | ---------------------------------------------------------- |
| `Distinct()`  | Yinelenenleri kaldırır | Bir koleksiyondaki tekrar eden öğeleri çıkarır.            |
| `Union()`     | Birleşim               | İki koleksiyonu birleştirir, tekrar edenleri 1 kez alır.   |
| `Intersect()` | Kesişim                | İki koleksiyonda da ortak olan öğeleri getirir.            |
| `Except()`    | Fark                   | Bir koleksiyonda olup, diğerinde olmayan öğeleri döndürür. |

18-🔹  Distinct() 

Bir listede aynı değeri birden fazla kez içeren kayıtlar varsa, Distinct() bu tekrarları kaldırır ve her değeri yalnızca bir kez döndürür.

 Distinct() – Eleman Türü ve Dönüş Tipi Tablosu

| Eleman Tipi                         | Örnek Kod                                 |
|------------------------------------|--------------------------------------------|
| `List<string>`                     | `list.Distinct()`                          |
| `List<int>`                        | `list.Distinct()`                          |
| `List<Customer>`                   | `list.Distinct()`                          |
| `Customer -> FirstName`            | `list.Select(c => c.FirstName).Distinct()` |
| `Customer -> {FirstName, LastName}`| `Select(...).Distinct()` (anonim tür)      |
| `Customer` (Email’e göre)          | `list.DistinctBy(c => c.Email)` (.NET 6+)  |

---

### 📘 Açıklamalar

- `List<string>` → Aynı string’leri bir kez alır.
- `List<int>` → Aynı sayılar bir kez alınır.
- `List<Customer>` → `Equals()` ve `GetHashCode()` override edilmediyse işe yaramaz.
- `Select(c => c.FirstName)` → Sadece isimleri seçip tekrar edenleri çıkarır.
- `Select(c => new { ... })` → Aynı isim-soyisim çiftleri bir kez alınır.
- `DistinctBy(c => c.Email)` (.NET 6+) → Email’e göre tekrarları temizler.

---

### 🎯 Dönüş Tipleri

- `Distinct()` → `IEnumerable<T>` döner.
- `ToList()` ile listeye çevrilir: `.ToList()` → `List<T>`


Basit Örnek (String listesiyle)
List<string> names = new List<string>
{
    "Ali", "Ayşe", "Ali", "Mehmet", "Ayşe"
};

var uniqueNames = names.Distinct().ToList();
 Çıktı:
 ["Ali", "Ayşe", "Mehmet"]
 Distinct() aynı ismi tekrar etmeyen bir liste döndürdü.

 🔸 Customer Örneği
 Varsayalım ki veritabanında şu müşteriler var:

 | CustomerID | FirstName | LastName | Email                                       |
| ---------- | --------- | -------- | ------------------------------------------- |
| 1          | Ali       | Yılmaz   | [ali@mail.com](mailto:ali@mail.com)         |
| 2          | Ayşe      | Demir    | [ayse@mail.com](mailto:ayse@mail.com)       |
| 3          | Ali       | Koç      | [ali.koc@mail.com](mailto:ali.koc@mail.com) |
| 4          | Mehmet    | Yıldız   | [mehmet@mail.com](mailto:mehmet@mail.com)   |
Amaç: Aynı isme sahip müşterilerden sadece bir tane getirmek

  public async Task<List<string?>> GetDistinctFirstNamesAsync()
        {

            return await _appDbContext.Customers
                .Select(x => x.FirstName)
                .Distinct()
                .ToListAsync();
        }

📌 Çıktı:
["Ali", "Ayşe", "Mehmet"]
Ali ismi iki müşteride geçiyor, ama Distinct sadece birini getirir (tekil değerler).

🔸 Püf Nokta
❗ Eğer Distinct() ile doğrudan nesne (Customer) üzerinden çalışırsan, tekrarları ayırt edemez. Çünkü Customer sınıfı bir class ve her biri bellekte farklı referansa sahip. Bu durumda Distinct() işe yaramaz.

Yanlış Kullanım:
public async Task<List<string?>> GetDistinctFirstNamesAsync()
        {

            return await _appDbContext.Customers
                .Distinct()
                .ToListAsync();
        }

💡 Bonus: .DistinctBy() (eğer .NET 6 veya üzerindeysen)
DistinctBy() metodu, nesneleri bir property'e göre ayırt ederek tekil hale getirir.

göre tekilleştirerek döndürmek:

    public async Task<List<Customer>> GetDistinctFirstNamesAsync()
        {

            return await _appDbContext.Customers
                  .DistinctBy(x => x.FirstName)
                 .ToListAsync();
        }


















