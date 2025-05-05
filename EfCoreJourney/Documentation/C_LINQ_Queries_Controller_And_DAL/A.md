🔍 Veritabanı İşlemleri (EF Core):

🔧 Metodun İmzası:

🧩 1. Task<List<Customer>> GetAll();
✅ Ne yapar?
Tüm müşteri listesini getirir.

💡 Dönüş Tipi:
Task<List<Customer>> → Asenkron olarak List<Customer> döner.
Task burada, metodun asenkron olduğunu gösterir. await ile çalışır.



🧩 2. Task<Customer> GetSingleCustomerOperationAsync();
✅ Ne yapar?
Tek bir müşteri getirir. Genellikle FirstOrDefault() ya da SingleOrDefault() gibi sorgularla kullanılır.

💡 Ne zaman kullanılır?
Temsilci müşteri,
Son eklenen müşteri,
Belirli bir kurala uyan tek müşteri gerekiyorsa.

💡 Dönüş Tipi:
Task<Customer> → Asenkron olarak bir Customer nesnesi döner.



🧩 3. Task<int> GetCustomerStatisticsAsync();
✅ Ne yapar?
Toplam müşteri sayısını getirir.

💡 Ne zaman kullanılır?
Dashboard istatistikleri,
Raporlar,Sayfa üstü bilgi panelleri gibi yerlerde.

💡 Dönüş Tipi:
Task<int> → Asenkron olarak bir sayı döner.

🧩 4. Task<bool> CustomerExistsAsync();
✅ Ne yapar?
Belirli bir müşterinin veritabanında olup olmadığını kontrol eder.

💡 Ne zaman kullanılır?
Belirli bir ada sahip müşteri var mı?
Kayıt öncesi kontrol (aynı müşteri zaten kayıtlı mı?)
Butonlar, uyarılar, yönlendirmeler

💡 Dönüş Tipi:
Task<bool> → Asenkron olarak true ya da false döner.


🧩 4. Task<string>, Task<decimal>,Task<double>,Task<Dictionary<TKey, TValue>>,Task<IEnumerable<T>>,Task<IQueryable<T>>
asenkron şekilde veri türünde sonuç dönen işlemler ilerleyen derste işlenecek
