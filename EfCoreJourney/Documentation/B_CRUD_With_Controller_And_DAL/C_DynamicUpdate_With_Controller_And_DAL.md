Controller-DAL Bağlantılı Güncelleme İşlemi(Kullanıcıdan Veri Alma)

1. Adım: Veri Katmanı (DAL - Data Access Layer)  Kodunu Yazıyoruz
2. Adım: Controller Üzerinden Müşteri Ekleme İşlemi
3. Adım: View (Görünüm) ile Formdan Veri Alma


1. Adım: Veri Katmanı (DAL - Data Access Layer)  Kodunu Yazıyoruz

Veritabanındaki müşteri bilgilerini güncellemek için Update metodunu kullanıyorsun. Bu metot, güncellenmesi gereken müşteri bilgisini alır, mevcut müşteri bilgileriyle karşılaştırır ve veritabanına kaydeder.


public class EfCustomerRepository : ICustomerDal
{
    private readonly AppDbContext _appDbContext;  // Veritabanı ile iletişim kuracak context

    public EfCustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;  // Constructor üzerinden context nesnesini alıyoruz
    }

public void Update(Customer customer)
{
    var existingCustomer = GetById(customer.CustomerID); // ID'ye göre mevcut müşteri verisini al

    if (existingCustomer != null)
    {
        // Gelen customer objesindeki verilerle mevcut veriyi güncelliyoruz
        existingCustomer.Name = customer.Name;
        existingCustomer.Email = customer.Email;
        existingCustomer.Phone = customer.Phone;

        // Veritabanına kaydediyoruz
        _appDbContext.SaveChanges();
    }
}
}

Açıklama:

GetById: Bu metodun, müşteri ID'sine göre veritabanından ilgili müşteri verisini çektiğini varsayıyoruz.
Eğer müşteri verisi bulunursa, existingCustomer nesnesi üzerinde güncelleme işlemi yapılır. Yani gelen customer nesnesindeki Name, Email ve Phone değerleri ile veritabanındaki mevcut müşteri bilgileri güncellenir.
_appDbContext.SaveChanges(): Güncellenen veriyi veritabanına kaydeder.


2. Adım: Controller Üzerinden Müşteri Güncelleme İşlemi

Controller'da, gelen HTTP isteklerini karşılamak için UpdateCustomer isimli iki metodun var: biri GET (veriyi al), diğeri ise POST (veriyi güncelle).

GET Metodu:
Bu metot, kullanıcıyı güncelleme formuna yönlendirir ve mevcut müşteri bilgisini getirir.

[HttpGet]
public IActionResult UpdateCustomer(int id)
{
    var existingCustomer = _customerDal.GetById(id);
    if (existingCustomer == null)
    {
        return NotFound(); // Müşteri bulunamazsa 404 döner
    }

    return View(existingCustomer); // Formda göstermek için müşteriyi gönderiyoruz
}

Açıklama:

id parametresi, güncellemek istediğimiz müşteri verisinin ID'sini temsil eder.
_customerDal.GetById(id): Veritabanındaki müşteri bilgilerini getirir.
Eğer müşteri bulunmazsa, NotFound() döndürülür.
Müşteri verisi varsa, bu veri View'a (görünüm) aktarılır ve formda kullanıcıya gösterilir.



POST Metodu:
Bu metot, formdan gelen verileri alır ve veritabanında güncelleme işlemi yapar.

[HttpPost]
public IActionResult UpdateCustomer(Customer customer)
{
    var existingCustomer = _customerDal.GetById(customer.CustomerID);
    if (existingCustomer == null)
    {
        return NotFound(); // Yine id yanlışsa veya müşteri yoksa 404
    }

    if (ModelState.IsValid)
    {
        _customerDal.Update(customer); // Veritabanında güncelleme
        return RedirectToAction("Index"); // Güncelleme sonrası listeye dön
    }

    return View(customer); // Hatalıysa formu tekrar göster
}
Açıklama:

Bu metot, kullanıcının formda gönderdiği Customer nesnesini alır.
existingCustomer ile karşılaştırarak veritabanında güncelleme işlemi yapılır.
Eğer ModelState.IsValid doğruysa, _customerDal.Update(customer) metoduyla güncelleme yapılır.
Güncelleme işlemi sonrası RedirectToAction("Index") ile kullanıcı ana sayfaya (veya liste sayfasına) yönlendirilir.
Eğer modelde bir hata varsa, tekrar formu gösterir.


3. Adım: View (Görünüm) ile Formdan Veri Alma

@model EntityLayer.Concrete.Customer
@{
    ViewData["Title"] = "UpdateCustomer";
}

<h1>@ViewData["Title"]</h1>

<form asp-action="UpdateCustomer" method="post">
    <!-- Müşterinin Id'sini gizli bir input ile gönderiyoruz -->
    <input type="hidden" asp-for="CustomerID" />

    <div class="form-group">
        <label asp-for="Name"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Email"></label>
        <input asp-for="Email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Phone"></label>
        <input asp-for="Phone" class="form-control" />
        <span asp-validation-for="Phone" class="text-danger"></span>
    </div>

    <br />

    <button type="submit" class="btn btn-success">Güncelle</button>
</form>

Açıklama:

@model: Formun model türünü belirtiyoruz. Burada Customer modelini kullanıyoruz.
Formda CustomerID, Name, Email, ve Phone bilgileri bulunuyor. Bu alanlar kullanıcı tarafından güncellenebilir.
asp-for: Modeldeki özellikleri form elemanlarına bağlar.
asp-validation-for: Model doğrulama hatalarını kullanıcıya gösterir.
<button type="submit">: Kullanıcı güncellemeyi gönderebilir.
