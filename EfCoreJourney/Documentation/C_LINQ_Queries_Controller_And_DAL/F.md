10-🔹Average() Nedir?

LINQ’in agregat (toplayıcı) metotlarından biri olan Average, sayısal türdeki bir property'sinin ortalamasını hesaplar ve hem bellek içindeki koleksiyonlarda hem de veritabanı sorgularında kullanılabilir.

Açıklama: Koleksiyondaki öğelerin ortalamasını döndürür.
Dönüş Tipi: double
Asenkron Versiyon: AverageAsync()
Dönüş Tipi: Task<double>
Kullanım: Koleksiyondaki öğelerin ortalamasını asenkron olarak döndürür.

Average() Eleman Türü ve Dönüş Tipi Tablosu

| Eleman Türü | `Average()` Dönüş Tipi | `AverageAsync()` Dönüş Tipi |
| ----------- | ---------------------- | --------------------------- |
| `int`       | `double`               | `Task<double>`              |
| `long`      | `double`               | `Task<double>`              |
| `float`     | `float`                | `Task<float>`               |
| `double`    | `double`               | `Task<double>`              |
| `decimal`   | `decimal`              | `Task<decimal>`             |
| `int?`      | `double?`              | `Task<double?>`             |
| `long?`     | `double?`              | `Task<double?>`             |
| `float?`    | `float?`               | `Task<float?>`              |
| `double?`   | `double?`              | `Task<double?>`             |
| `decimal?`  | `decimal?`             | `Task<decimal?>`            |

✅ 1. Koleksiyon (Memory) Üzerinde Kullanımı

🔹 Basit sayı listesi:
List<int> yaslar = new List<int> { 20, 30, 40 };
double ortalama = yaslar.Average(); // Sonuç: 30.0
🔹 Nesne listesi üzerinden:
var ortalamaYas = customers.Average(c => c.Age);

✅ 2. Entity Framework ile Kullanımı (EF Core)
Veritabanındaki verilerin ortalamasını almak için AverageAsync() kullanılır:
return await _appDbContext.Customers
    .AverageAsync(c => c.Age);

❗ Nullable Tiplerde Kullanım
Eğer ortalaması alınacak alan nullable (int?, decimal?) ise Average() null değerleri otomatik olarak yok sayar.
double ortalamaMaas = await _appDbContext.Customers
    .AverageAsync(c => c.Salary); // Salary decimal? ise null'lar atlanır
Ama yine de kontrol amaçlı c.Salary ?? 0 yazmak güvenli olabilir.

Şartlı Ortalama (Koşullu Kullanım)
 
     public async Task<decimal?> GetValueAsync()
        {
            return (decimal?)await _appDbContext.Customers
             .Where(c => c.City == "İstanbul")
             .AverageAsync(c => c.Age);
        }
Sadece İstanbul’daki müşterilerin yaş ortalaması alınır.