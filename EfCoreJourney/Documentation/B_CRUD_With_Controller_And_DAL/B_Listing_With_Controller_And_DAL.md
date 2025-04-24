Controller-DAL Bağlantılı Listeleme İşlemi

1. Adım - ICustomerDal Interface
İlk olarak, ICustomerDal adlı bir interface oluşturuyoruz. Bu interface, müşteri verilerine erişim için kullanılacak metotları tanımlar.

public interface ICustomerDal
{
    List<Customer> GetAll();  // Tüm müşterileri alacak metodu tanımlıyoruz.
}
Notlar:
Interface: ICustomerDal bir arayüzdür. Yani, bu interface, veri erişimi yapacak sınıfın hangi metotları implement edeceğini belirtir. Bu sınıf, veritabanı işlemlerini yapacak repository sınıfı olacaktır.

GetAll(): GetAll metodu, tüm müşteri listesini döndürecek şekilde tasarlanmıştır. Bu, veritabanındaki tüm müşteri verilerini almayı sağlayacaktır.

2. Adım - EfCustomerRepository Class
Bu adımda, EfCustomerRepository sınıfını oluşturuyoruz. Bu sınıf, ICustomerDal arayüzünü implement eder ve veritabanı işlemlerini gerçekleştirir.

public class EfCustomerRepository : ICustomerDal
{
    private readonly AppDbContext _appDbContext;

    public EfCustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public List<Customer> GetAll()
    {
        return _appDbContext.Customers.ToList();
    }
}
Notlar:
EfCustomerRepository Sınıfı: Bu sınıf, ICustomerDal interface'ini implement eder ve veritabanına erişim sağlar. Burada Entity Framework kullanıyoruz (EF) ve AppDbContext sınıfını kullanarak veritabanıyla iletişime geçiyoruz.

_appDbContext: Bu, uygulamanın veritabanı ile etkileşimde bulunmasını sağlayan bir nesnedir. AppDbContext, veritabanındaki tabloları temsil eden sınıfları içerir.

GetAll Metodu: Bu metod, AppDbContext.Customers.ToList() koduyla tüm müşteri verilerini veritabanından alır ve bir liste olarak döndürür.

3. Adım - CustomerController
Controller, HTTP isteklerini işleyen ve kullanıcı etkileşimi sağlamak için kullanılan bir bileşendir. CustomerController, müşteri verilerini görüntülemek için EfCustomerRepository sınıfından yararlanır.

public class CustomerController : Controller
{
    private readonly ICustomerDal _customerDal;

    public CustomerController(ICustomerDal customerDal)
    {
        _customerDal = customerDal;
    }

    public IActionResult Index()
    {
        var customers = _customerDal.GetAll();
        return View(customers);
    }
}
Notlar:
Controller: CustomerController sınıfı, müşteri verileriyle ilgili işlemleri yönetir. Burada, ICustomerDal interface'i üzerinden veritabanı işlemleri yapılır.

Dependency Injection: CustomerController sınıfı, ICustomerDal tipinde bir bağımlılık alır (constructor'da). Bu, dependency injection prensibine uygun şekilde yapılır ve repository sınıfı (örneğin EfCustomerRepository) ICustomerDal implementasyonu ile sağlanır.

Index Metodu: Index metodu, tüm müşteri listesini almak için GetAll() metodunu çağırır. Ardından, bu listeyi View'a (görünüm) gönderir. View(customers) kısmı, bu listeyi kullanıcıya gösterecek olan View'a yönlendirme yapar.

4. Adım - Müşteri Listesi Görüntüleme (Index.cshtml)
Bu adımda, veritabanından alınan müşteri listesini HTML tablo olarak görüntüleyeceğiz.

@model List<EntityLayer.Concrete.Customer>

@{
    ViewData["Title"] = "Müşteri Listesi";
}

<h1>@ViewData["Title"]</h1>

<table class="table">
    <thead>
        <tr>
            <th>Ad</th>
            <th>Email</th>
            <th>Telefon</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.Name</td>
                <td>@item.Email</td>
                <td>@item.Phone</td>
            </tr>
        }
    </tbody>
</table>

Notlar:
@model Direktifi: Bu satır, bu view'ın List<Customer> modelini kullandığını belirtir. Bu, CustomerController sınıfından gelen veriyi alır ve görünümde kullanır.

ViewData["Title"]: Bu, sayfanın başlığını dinamik olarak belirler.

HTML Tablo: Müşteri verilerini görüntülemek için bir HTML tablo yapısı kullanıyoruz. Tabloda müşteri adı, e-posta ve telefon bilgileri sırasıyla listeleniyor.

foreach Döngüsü: Model içinde döngü başlatılır ve her müşteri için bir satır eklenir. Model burada, controller’dan gönderilen tüm müşteri listesidir.

Genel Akış
Veri Erişimi (Repository): EfCustomerRepository, ICustomerDal arayüzünü implement eder ve veritabanı işlemleri için GetAll gibi metodları içerir.

Controller: CustomerController, ICustomerDal bağımlılığını alır ve GetAll() metodunu çağırarak tüm müşteri verilerini alır. Bu veriler, view’a (Index.cshtml) gönderilir.

View: Index.cshtml, controller’dan gelen müşteri verilerini HTML tablosu olarak görüntüler.

Özet:

ICustomerDal: CRUD işlemleri için metodları tanımlar.
EfCustomerRepository: Veritabanı ile etkileşimde bulunur, ICustomerDal metodlarını implement eder.
CustomerController: EfCustomerRepository'den veri alır ve veriyi View'a ileterek kullanıcıya gösterir.
View (Index.cshtml): Kullanıcıya müşteri listesini HTML formatında sunar.