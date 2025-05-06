9-🔹Sum() Nedir?

LINQ’in agregat (toplayıcı) metotlarından biri olan Sum, belirli bir sayısal özelliğin toplamını verir
ve hem koleksiyonlar hem de veritabanı sorguları üzerinde kullanılabilir.

Açıklama: Koleksiyondaki öğelerin toplamını döndürür.
Dönüş Tipi: Koleksiyondaki elemanların türüne bağlı olarak (int, double, decimal, vb.)
Asenkron Versiyon: SumAsync()
Dönüş Tipi: Task<decimal>, Task<int>, Task<double> vb.
Kullanım: Koleksiyondaki öğelerin toplamını asenkron olarak döndürür.

 Sum() Eleman Türü ve Dönüş Tipi Tablosu

| Eleman Türü | `Sum()` Dönüş Tipi     | `SumAsync()` Dönüş Tipi     |
| ----------- | ---------------------- | --------------------------- |
| `int`       | `int `                 | `Task<int>`                 |
| `long`      | `long `                | `Task<long>`                |
| `float`     | `float`                | `Task<float>`               |
| `double`    | `double`               | `Task<double>`              |
| `decimal`   | `decimal`              | `Task<decimal>`             |
| `int?`      | `int?`                 | `Task<int?>`                |
| `long?`     | `long?`                | `Task<long?>`               |
| `float?`    | `float?`               | `Task<float?>`              |
| `double?`   | `double?`              | `Task<double?>`             |
| `decimal?`  | `decimal?`             | `Task<decimal?>`            |


✅ Basit Örnek (Bellekte Koleksiyon Üzerinde)

List<int> sayilar = new List<int> { 5, 10, 15 };
int toplam = sayilar.Sum();  // Sonuç: 30

✅ Nesne Koleksiyonunda Sum()
var toplamYas = customers.Sum(c => c.Age);
customers listesinde tüm müşterilerin yaşlarını toplar.

✅ Entity Framework ile Kullanım

return await _appDbContext.Customers.SumAsync(c => c.Age);
Veritabanındaki tüm müşteri yaşlarını toplar.
SumAsync sayesinde veritabanına async istek atılır.

Sum() ile Null Değerler Ne Olur?

Nullable alan varsa Sum() null dönebilir.
Nullable ile çalışırken Sum(c => c.Salary ?? 0) şeklinde kullanılabilir.

      public async Task<decimal?> GetValueAsync()
        {
            return await _appDbContext.Customers.SumAsync(c => c.Age ?? 0);
        }


🧠 İpucu: Koşullu(Şartlı) Toplama
Sadece belirli koşuldaki müşterilerin yaş toplamı:
return await customers
    .Where(c => c.City == "İstanbul")
    .SumAsync(c => c.Age);