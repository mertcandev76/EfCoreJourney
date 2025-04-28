Controller-DAL Bağlantılı Ekleme İşlemi(Kullanıcıdan Veri Alma)

1. Adım: Veri Katmanı (DAL - Data Access Layer)  Kodunu Yazıyoruz

public class EfCustomerRepository : ICustomerDal
{
    private readonly AppDbContext _appDbContext;  // Veritabanı ile iletişim kuracak context

    public EfCustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;  // Constructor üzerinden context nesnesini alıyoruz
    }

    public void Insert(Customer customer)
    {
        _appDbContext.Customers.Add(customer);  // Gelen müşteri nesnesini ekliyoruz
        _appDbContext.SaveChanges();  // Değişiklikleri veritabanına kaydediyoruz
    }
}

Burada Ne Yapılıyor?
EfCustomerRepository, ICustomerDal arayüzünü (interface) implemente ediyor.
AppDbContext nesnesiyle veritabanına erişim sağlıyoruz.
Insert metodu ile dışarıdan gelen Customer nesnesi veritabanına ekleniyor.

2. Adım: Controller Üzerinden Müşteri Ekleme İşlemi
[HttpGet]
public IActionResult AddCustomer()
{
    return View();  // Sadece boş formu göstermek için GET isteği
}

[HttpPost]
public IActionResult AddCustomer(Customer customer)
{
    if (ModelState.IsValid)  // Formdan gelen veri geçerli mi?
    {
        _customerDal.Insert(customer);  // Veri geçerliyse DAL'a gönderip kaydediyoruz
        return RedirectToAction("Index");  // Kayıt sonrası Index sayfasına yönlendiriyoruz
    }

    return View(customer);  // Hatalıysa formu hatalarla tekrar gösteriyoruz
}

Burada Ne Yapılıyor?
GET isteği geldiğinde kullanıcıya boş bir form sunuyoruz.
POST isteği geldiğinde kullanıcıdan gelen Customer verisini alıyoruz.
ModelState.IsValid ile modelin doğruluğunu kontrol ediyoruz (örneğin zorunlu alanlar dolu mu? gibi).
Geçerli ise _customerDal.Insert(customer) diyerek DAL katmanına kaydetme işlemini iletiyoruz.
Başarılı kayıttan sonra kullanıcıyı Index sayfasına yönlendiriyoruz.

3. Adım: View (Görünüm) ile Formdan Veri Alma
@model EntityLayer.Concrete.Customer

@{
    ViewData["Title"] = "Müşteri Ekleme Alanı";
}

<h1>@ViewData["Title"]</h1>

<form asp-action="AddCustomer" method="post">
    <div class="form-group">
        <label asp-for="Name" class="control-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Email" class="control-label"></label>
        <input asp-for="Email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Phone" class="control-label"></label>
        <input asp-for="Phone" class="form-control" />
        <span asp-validation-for="Phone" class="text-danger"></span>
    </div>

    <br />

    <button type="submit" class="btn btn-primary">Müşteri Ekle</button>
</form>

Burada Ne Yapılıyor?
Form, Customer modeline bağlı (@model EntityLayer.Concrete.Customer).
Form elemanları (<input>) asp-for etiketi ile modelin ilgili alanlarına (Name, Email, Phone) bağlanıyor.
asp-validation-for alanları ile doğrulama (validation) mesajları gösteriliyor.
Form POST yöntemiyle AddCustomer action'ına veri gönderiyor.

Genel Akış:
Kullanıcı "Müşteri Ekle" sayfasına gider.
Boş form gösterilir (GET methodu çalışır).
Kullanıcı formu doldurup gönderir (POST methodu çalışır).
Controller'da ModelState kontrol edilir:
Eğer geçerliyse: DAL katmanındaki Insert metodu çağrılır ➔ Müşteri verisi veritabanına kaydedilir ➔ "Index" sayfasına yönlendirilir.
Eğer hatalıysa: Aynı form hatalarla birlikte tekrar gösterilir.
DAL, veriyi AppDbContext üzerinden veritabanına kaydeder.

