-->Customer

 1. Durum – Customer Entity Tanımı (EntityLayer)

namespace EntityLayer.Concrete
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = null!;

        public int? Age { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        public ICollection<Order>? Orders { get; set; }
        public ICollection<CustomerCoupon>? CustomerCoupons { get; set; }
    }
}

Açıklama:

Customer sınıfı, bir müşteriye ait tüm temel bilgileri tutar.
ICollection<Order> → Bu müşterinin verdiği siparişleri temsil eder. (Bire-çok ilişki)
ICollection<CustomerCoupon> → Müşterinin sahip olduğu kuponları tutar. (Çoktan-çoğa ilişki)

2. Durum – CustomerStaticRepository Sınıfı (DataAccessLayer)

public class CustomerStaticRepository : ICustomerStaticRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerStaticRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _appDbContext.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync()
    {
        int staticID = 3;
        return await _appDbContext.Customers.FindAsync(staticID);
    }

    public async Task AddAsync()
    {
        var customer = new Customer
        {
            FullName = "Celil Süleyman",
            Age = 23,
            Email = "celil.suleyman@example.com",
            Phone = "+90 553 984 5176",
            Address = "İstanbul, Kadıköy, Bahariye Caddesi No:45",
            IsActive = false,
            RegisteredDate = DateTime.Now
        };
        await _appDbContext.Customers.AddAsync(customer);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        int staticID = 2;
        var customer = await _appDbContext.Customers.FindAsync(staticID);
        if (customer != null)
        {
            customer.FullName = "Yasin Alakurt";
            customer.Age = 36;
            customer.Email = "yasin.alakurt@gmail.com";
            customer.Phone = "+90 542 859 63 25";
            customer.Address = "Ankara, Çankaya, Atatürk Bulvarı No:120";
            customer.IsActive = true;
            customer.RegisteredDate = DateTime.Now.AddDays(8);
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync()
    {
        int staticID = 2;
        var customer = await _appDbContext.Customers.FindAsync(staticID);
        if (customer != null)
        {
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

Açıklama:

Bu repository sınıfı, Customer nesnesi için statik ID üzerinden CRUD işlemlerini gerçekleştirir.
AddAsync, sabit verilerle bir müşteri ekler.
UpdateAsync, ID’si 2 olan müşteri bilgilerini günceller.
DeleteAsync, ID’si 2 olan müşteriyi siler.
Bu işlemler, demo/test amaçlı statik verilerle yapılmıştır. Dinamik veri ile yapılması için form inputları kullanılabilir.

 3. Durum – CustomerStaticController Sınıfı (MVC Controller)

public class CustomerStaticController : Controller
{
    private readonly ICustomerStaticRepository _customerStaticRepository;

    public CustomerStaticController(ICustomerStaticRepository customerStaticRepository)
    {
        _customerStaticRepository = customerStaticRepository;
    }

    public async Task<IActionResult> Index()
    {
        var customers = await _customerStaticRepository.GetAllAsync();
        return View(customers);
    }

    public async Task<IActionResult> GetByID()
    {
        var customer = await _customerStaticRepository.GetByIdAsync();
        if (customer == null) return NotFound("Belirtilen müşteri bulunamadı.");
        return View(customer);
    }

    public async Task<IActionResult> AddCustomer()
    {
        await _customerStaticRepository.AddAsync();
        TempData["Message"] = "Müşteri başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateCustomer()
    {
        await _customerStaticRepository.UpdateAsync();
        TempData["Message"] = "Müşteri bilgileri başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteCustomer()
    {
        await _customerStaticRepository.DeleteAsync();
        TempData["Message"] = "Müşteri başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}

Açıklama:

MVC Controller, Repository üzerinden işlemleri tetikler.
Index → Tüm müşterileri listeleme işlemi.
GetByID → Belirli bir ID'deki müşteriyi View’a gönderir.
AddCustomer, UpdateCustomer, DeleteCustomer → Sırasıyla ekleme, güncelleme ve silme işlemi yapar.
TempData["Message"], işlemler sonrasında kullanıcıya mesaj göstermek için kullanılır.

4. Durum – View (Customer Listesi Razor Sayfası)

@model List<EntityLayer.Concrete.Customer>
@{
    ViewData["Title"] = "Müşteri Listesi";
}

<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/CustomerStatic/AddCustomer">Ekle</a>
    <a class="btn btn-warning" href="/CustomerStatic/UpdateCustomer">Güncelle</a>
    <a class="btn btn-danger" href="/CustomerStatic/DeleteCustomer">Sil</a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Ad-Soyad</th>
            <th>Yaş</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Adres</th>
            <th>Aktivlik</th>
            <th>Kayıt Tarihi</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var customer in Model)
        {
            <tr>
                <td>@customer.CustomerID</td>
                <td>@customer.FullName</td>
                <td>@customer.Age</td>
                <td>@customer.Email</td>
                <td>@customer.Phone</td>
                <td>@customer.Address</td>
                <td>@customer.IsActive</td>
                <td>@customer.RegisteredDate</td>
            </tr>
        }
    </tbody>
</table>

Açıklama:

Bu sayfa, tüm müşterilerin listelendiği ana sayfadır (/CustomerStatic/Index).
@model List<Customer> → Sayfaya gelen modelin türünü belirtir.
TempData mesajları kullanıcıya başarılı işlem uyarısı verir.
Üstteki 3 buton, sırasıyla statik olarak müşteri ekleme, güncelleme ve silme işlemlerini tetikler.



🔚 SONUÇ

| Konu                      | Açıklama                                                              |
| ------------------------- | --------------------------------------------------------------------- |
| **Entity Tanımı**         | `Customer` sınıfı, veri tabanındaki tabloyu temsil eder.              |
| **Repository Pattern**    | Veri erişimi merkezi bir yapıdan yönetilir.                           |
| **Static CRUD İşlemleri** | ID sabit verilerek ekleme, güncelleme, silme işlemleri yapılır.       |
| **MVC Controller**        | Repository sınıflarını kullanarak iş mantığını yönlendirir.           |
| **View ile Listeleme**    | Müşteri verileri HTML tablosunda listelenir, işlem butonları eklenir. |
| **TempData Kullanımı**    | Başarılı işlemler için kullanıcıya mesaj gösterilir.                  |
