2.2.İleri Düzeyde DbContext(Bağlantı cümlesi dışarıda)

2.2.2.👉 Dependency injection Yöntemi 



DbContext Sınıfı — Parametreli Bağlantı Alacak

1-public class AppDbContext : DbContext
{
           public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
      
        public DbSet<Customer> Customers { get; set; }
}

💡 Açıklaması:

1. public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
DbContextOptions<AppDbContext>: Bu, DbContext'in yapılandırmasını (configuration) tutan bir nesnedir. İçerisinde bağlantı dizesi, veritabanı türü (örneğin, SQL Server) gibi ayarlar yer alır.

base(options): Bu ifade, DbContext sınıfının taban sınıfı olan DbContext'in yapılandırıcısını çağırır. Yani AppDbContext sınıfı, DbContext'in tüm özelliklerini ve işlevselliğini miras alır. base(options)'in amacı, verilen options parametresini üst sınıfa (DbContext) iletmek ve böylece Entity Framework'ün doğru şekilde yapılandırılmasını sağlamak.

2. public DbSet<Customer> Customers { get; set; }
DbSet<Customer>: Bu, DbContext'in içinde veritabanı tablosu olarak temsil edilen bir koleksiyondur. DbSet<T>, T türündeki nesnelerin (burada Customer sınıfı) veritabanındaki karşılıklarıyla etkileşime girmeye olanak sağlar.

Bu örnekte Customer sınıfı bir varlık (entity) olarak veritabanında bir tabloyu temsil eder.

{ get; set; }: Bu, Customers özelliğinin getter (okuma) ve setter (yazma) metodlarını tanımlar. Bu özellik, DbContext üzerinden Customer nesnelerine erişim sağlamak için kullanılır.

Ne işe yarar?
DbContext: Veritabanı ile etkileşimde bulunmamıza yarayan bir sınıftır. DbContext nesnesi üzerinden, veritabanına sorgular gönderilebilir ve veritabanı işlemleri (CRUD işlemleri gibi) yapılabilir.

DbSet<Customer>: Bu özellik, Customer tablosuyla etkileşime geçmek için kullanılır. Örneğin, veritabanına yeni müşteri eklemek, müşteri bilgilerini güncellemek veya silmek için bu özellik üzerinden işlem yapılabilir.




2-appsettings.json Kullanacaksan

appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=YourDbName;Trusted_Connection=True;"
  }
}


3-Program.cs içinde:

// Connection String'i oku
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// AppDbContext'i servise ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


