Controller-DAL Bağlantılı Ekleme İşlemi(Sabit)

1. Adım - ICustomerDal Interface(Data Access Interface)

public interface ICustomerDal 
{
    void Insert(Customer customer);
}

✍️ Açıklama:
Bu bir interface (arayüz).

Insert() metodu, bir Customer (müşteri) nesnesini veritabanına eklemeyi hedefler.

Henüz metodun içeriği (gövdesi) yoktur. Bu sadece bir sözleşmedir: "Bu interface'i uygulayan sınıf, Insert metodunu içermek zorundadır."

💡 Neden Kullanılır?
Bağımlılıkların azaltılması için: Controller doğrudan EF Core’a bağlı kalmaz.

Test edilebilirlik artar.

SOLID prensiplerinden Dependency Inversion ilkesini destekler.

2. Adım - EfCustomerRepository (EF Core ile Gerçek Ekleme İşlemi)
public class EfCustomerRepository : ICustomerDal
{
    private readonly AppDbContext _appDbContext;

    public EfCustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Insert(Customer customer)
    {
        customer.Name = "lorem ipsum";
        customer.Email = "loremipsum@gmail.com";
        customer.Phone = "123456789";
        
        _appDbContext.Customers.Add(customer);
        _appDbContext.SaveChanges();
    }
}
✍️ Açıklama:
EfCustomerRepository, ICustomerDal arayüzünü implement eder (uygular).
AppDbContext, EF Core veritabanı bağlantısını temsil eder. Dependency Injection ile alınır.

Insert() metodunda:

Müşteriye sabit veriler atanır.
Add() metodu ile nesne veritabanına eklenir.
SaveChanges() ile değişiklikler kaydedilir.

🔧 Teknik Detaylar:
AppDbContext sınıfı, genellikle DbContext sınıfından türetilir ve içinde DbSet<Customer> gibi tablolar yer alır.

Bu yapı, Repository Pattern'in temel bir örneğidir.

✅ 3 - Adım: CustomerController (MVC Controller)
public class CustomerController : Controller
{
    private readonly ICustomerDal _customerDal;

    public CustomerController(ICustomerDal customerDal)
    {
        _customerDal = customerDal;
    }

    public IActionResult AddStaticCustomer()
    {
        Customer customer = new Customer(); // Boş müşteri nesnesi
        _customerDal.Insert(customer);      // DAL katmanı üzerinden ekleme işlemi
        return RedirectToAction("Index");   // Ekleme sonrası listeye yönlendirme
    }
}
✍️ Açıklama:
Bu bir ASP.NET Core MVC Controller'ıdır.

ICustomerDal parametresi sayesinde Repository dış dünyaya soyutlanmıştır.
AddStaticCustomer metodu, sabit verilerle bir müşteri oluşturur ve ekler.

🧠 Neden Önemli?
Controller, veri erişim işini kendi içinde yapmaz, bunun yerine ICustomerDal aracılığıyla işi başkasına devreder (separation of concerns).

✅ 4 - Adım: Index.cshtml (View – Razor Sayfası)
@model List<EntityLayer.Concrete.Customer>

@{
    ViewData["Title"] = "Müşteri Listesi";
}

<h1>@ViewData["Title"]</h1>

<a class="btn btn-primary" href="/Customer/AddStaticCustomer">Sabit Müşteri Ekle</a>
✍️ Açıklama:
Bu, Razor View Engine ile yazılmış bir View dosyasıdır.
Sayfa bir Customer listesi modelini bekler.
ViewData["Title"] ile başlık dinamik olarak ayarlanır.

Alt tarafta bir buton/link var:
Bu link tıklandığında /Customer/AddStaticCustomer URL'sine yönlendirir.
Böylece sabit müşteri ekleme işlemi başlatılır.

🖼 Görsel Akış:
Kullanıcı butona tıklar → Controller’a istek gider → Repository üzerinden müşteri eklenir → Index sayfasına geri dönülür.

🔁 Genel İşleyiş Akışı
KULLANICI (Butona Tıklar)
       ↓
CustomerController (AddStaticCustomer metodu çağrılır)
       ↓
EfCustomerRepository (Insert ile müşteri eklenir)
       ↓
AppDbContext.SaveChanges() (Veri veritabanına yazılır)
       ↓
Index View (Listeleme sayfasına yönlendirilir)

