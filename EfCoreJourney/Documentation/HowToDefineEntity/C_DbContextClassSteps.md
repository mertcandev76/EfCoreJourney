2-DbContext Sınıfı Tanımlama Adımları 

🔥 DbContext Nedir?
DbContext sınıfı, Entity Framework Core’un veritabanıyla iletişim kurmasını sağlayan bir köprüdür.
Senin için:

Veritabanındaki tabloların C# sınıflarına bağlanmasını,

CRUD işlemlerini (Create, Read, Update, Delete) yönetmeyi,

Migration (veritabanı güncellemesi) gibi işleri yapar.

🧠 DbContext Sınıfı Tanımlama Adımları
1️. Entity Framework Core Paketlerini Ekle
Projende önce EF Core kullanabilmek için NuGet paketlerini yüklersin:

Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
⚠️ SqlServer kullanıyoruz. Eğer başka bir veritabanı kullanıyorsan farklı paket gerekir.

2.DbContext Sınıfını Oluştur

OnConfiguring Metodu Nedir?
OnConfiguring metodu, Entity Framework Core’da DbContext sınıfının bir üyesidir.
Bu metodun amacı, veritabanı bağlantı ayarlarını yapmak ve DbContext’in nasıl davranacağını tanımlamaktır.

Nerede Kullanılır?
Eğer appsettings.json veya Dependency Injection (DI) üzerinden bağlantı cümlesi geçmiyorsan,
direkt kodun içinde veritabanı bağlantısını tanımlamak için OnConfiguring kullanırsın.

2.1.Temel DbContext(Bağlantı cümlesi içeride sabit)

Senin Customer sınıfın bir Entity (Varlık) — yani bu sınıf veritabanındaki bir tablonun model hali.
Ama bu sınıfın veritabanına bağlanıp işlem yapabilmesi için bir DbContext sınıfına ihtiyacı var. 
İşte OnConfiguring metodu bu DbContext içinde kullanılır ve veritabanı bağlantı bilgisini tanımlar.


Örnek:

public class AppDbContext : DbContext
{
 
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Bağlantı cümlesi buraya yazılır
         optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=OrmJourneyDB; integrated security=true;");
    }
}

Burada:

💡 Anlamı:
->OnConfiguring metodu, DbContext çalışırken hangi veritabanına bağlanacağını söyler.
->optionsBuilder.UseSqlServer(...) Entity Framework’e hangi veritabanını kullanacağını ve bağlantı cümlesini söyler.
Bu metod override edilmezse DbContext neye nasıl bağlanacağını bilmez.(Yani Sql server kullanıcağımızı belirtiyoruz)
->İçerideki bağlantı cümlesi (Connection String) veritabanının adresi ve adı gibi bilgileri içerir.
->Senin verdiğin Customer sınıfı, bu AppDbContext içindeki Customers DbSet’ine bağlı olur. Böylece Add, Update, Delete, List gibi işlemleri AppDbContext üzerinden yapabilirsin.


Ne Zaman Kullanılmaz?
Gerçek, katmanlı projelerde.
Startup.cs ya da .NET 6+ için Program.cs içindeki builder.Services.AddDbContext kullanılıyorsa,
Bağlantı ayarlarını appsettings.json dosyasına koyduysan.



2.2.İleri Düzeyde DbContext(Bağlantı cümlesi dışarıda)

--NOT!! 
Diğer commit işleminde anlatılıyor

