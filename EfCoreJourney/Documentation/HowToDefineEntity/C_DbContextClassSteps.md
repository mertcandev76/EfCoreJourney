2.2.İleri Düzeyde DbContext(Bağlantı cümlesi dışarıda)

2.2.1.👉 "Manual Instantiation" Yöntemi (Elle Nesne Oluşturma ya da Manuel Bağımlılık Yönetimi)



DbContext Sınıfı — Parametreli Bağlantı Alacak

1-public class AppDbContext : DbContext
{
    private readonly string _connectionString;

    public AppDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

💡 Açıklaması:

🧠 public class AppDbContext : DbContext

Bu sınıf, Entity Framework Core’un sağladığı DbContext sınıfından türetilir.
DbContext yazılımda veritabanı ile kod arasındaki köprüdür.

📌 Görevi:
Veritabanındaki tabloları temsil eder.
Veriyi sorgulamak, kaydetmek, güncellemek için kullanılır.


🔐 private readonly string _connectionString;
Bu bir private field’dır.
Dışarıdan bağlantı cümlesi alır ve saklar.

readonly demek:
Sadece constructor'da set edilir.
Sonradan değiştirilmez.

Bunun amacı:
Bağlantı cümlesini dışarıdan almak, sabit yazmamak!
(örn: appsettings.json ya da kullanıcıdan alınan string)



🔧 public AppDbContext(string connectionString)

Bu bir constructor (yapıcı metot).
AppDbContext sınıfından nesne oluşturulurken çalışır.
Dışarıdan gelen bağlantı cümlesini parametre olarak alır.

_connectionString değişkenine atar.

📌 Amaç:
Veritabanı bağlantısını dışarıdan esnek bir şekilde almak.


🗃️ public DbSet<Customer> Customers { get; set; }
Entity Framework'teki Customer tablosunu temsil eder.
Customer sınıfı bir tablo modeli.
DbSet<Customer> → Veritabanındaki Customers tablosuyla çalışmanı sağlar.


⚙️ protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
Entity Framework’ün bir fonksiyonudur.
DbContext açıldığında bağlantı ayarlarını yapılandırır.


💡 if (!optionsBuilder.IsConfigured)
Eğer optionsBuilder daha önce ayarlanmamışsa (UseSqlServer çağrılmamışsa),
aşağıdaki satırı çalıştırır.


💾 optionsBuilder.UseSqlServer(_connectionString);
Burada bağlantı cümlesini kullanarak:
Entity Framework’e "SQL Server kullanacaksın" der.
_connectionString ile hangi sunucuya bağlanacağını belirtir.



✅ Kısacası:

Bu yapı sayesinde;
Bağlantı cümlesi sabit kodlanmaz.
Dışarıdan gelen connectionString kullanılır.
DbContext her açıldığında doğru sunucuya bağlanır.

🎯 Neden Bu Yapı Kullanılır?
Projeyi farklı ortamlarda çalıştırmak kolay olsun diye. (development, test, production fark etmez.)
Kodun başka yere taşınması daha esnek olur.
Bağlantı cümlesi config dosyası, environment, ya da parametreyle değiştirilebilir.




2-appsettings.json Kullanacaksan

appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=YourDbName;Trusted_Connection=True;"
  }
}


3-Program.cs içinde:

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json") // appsettings.json dosyasını ekliyoruz.
    .Build();


string connectionString = configuration.GetConnectionString("DefaultConnection"); // ConnectionStrings içinden DefaultConnection'ı alıyoruz.

using (var context = new AppDbContext(connectionString)) // DbContext'e bağlantı bilgisini veriyoruz.
{
    var customers = context.Customers.ToList();  // Customers tablosundaki tüm verileri çekiyoruz.
    foreach (var customer in customers)
    {
        Console.WriteLine($"Name: {customer.Name}, Email: {customer.Email}");
    }
}

Not:
🔍 foreach Döngüsünün Mantığı:

foreach (var customer in customers)
{
    Console.WriteLine($"Name: {customer.Name}, Email: {customer.Email}");
}
Burada foreach listesindeki her bir Customer nesnesi üzerinde işlem yapmak için kullanılır.
Ama yazdığın {} bloğu tamamen senin kontrolünde.

💡 Boş bırakabilirsin:
Eğer şimdilik işlem yapmayacaksan, şu şekilde de yazabilirsin:

foreach (var customer in customers)
{
    // Şimdilik işlem yok.
}
Kod hata vermez, çalışır.
Ama mantıklı bir şey yapmaz — çünkü döngü içi boş.

💡 Alternatif:
Hiç kullanmayacaksan, foreach’e bile gerek yok: