-->CustomerCoupon

1. Durum – Entity (Varlık) Tanımı: CustomerCoupon.cs

namespace EntityLayer.Concrete
{
    public class CustomerCoupon
    {
        public int CustomerCouponID { get; set; }

        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }

        public int CouponID { get; set; }
        public Coupon? Coupon { get; set; }

        public DateTime? RedeemedAt { get; set; } // Kuponun kullanıldığı tarih
    }
}

Açıklama:

CustomerCoupon sınıfı, Customer ve Coupon arasında çoktan-çoka ilişkiyi temsil eden ara tablodur.
CustomerID ve CouponID Foreign Key'dir.
Customer ve Coupon navigation property olarak tanımlanmıştır.
RedeemedAt, müşterinin bu kuponu hangi tarihte kullandığını tutar (nullable yapılmış).

2. Durum – Repository Sınıfı: CustomerCouponWithCustomerAndCouponStaticRepository.cs

public class CustomerCouponWithCustomerAndCouponStaticRepository : ICustomerCouponWithCustomerAndCouponStaticRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerCouponWithCustomerAndCouponStaticRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

CRUD İşlemleri:

🔹 Tüm Verileri Getirme (Include ile Navigation)

public async Task<List<CustomerCoupon>> GetAllAsync()
{
    return await _appDbContext.CustomerCoupons
        .Include(cc => cc.Customer)
        .Include(cc => cc.Coupon)
        .ToListAsync();
}
Include ile ilişkili Customer ve Coupon bilgileri de çekilir.

🔹 Tekil Veri Getirme (ID sabit)

public async Task<CustomerCoupon?> GetByIdAsync()
{
    int staticID = 3;
    return await _appDbContext.CustomerCoupons
        .Include(cc => cc.Customer)
        .Include(cc => cc.Coupon)
        .FirstOrDefaultAsync(cc => cc.CustomerCouponID == staticID);
}

🔹 Ekleme İşlemi (Statik Veri ile)

public async Task AddAsync()
{
    var customerCoupon = new CustomerCoupon
    {
        CustomerID = 1,
        CouponID = 3,
        RedeemedAt = DateTime.Now
    };
    await _appDbContext.CustomerCoupons.AddAsync(customerCoupon);
    await _appDbContext.SaveChangesAsync();
}

🔹 Güncelleme İşlemi (İlişkili Tablolarla Birlikte)

public async Task UpdateAsync()
{
    int staticID = 1;
    var customerCoupon = await _appDbContext.CustomerCoupons
        .Include(cc => cc.Customer)
        .Include(cc => cc.Coupon)
        .FirstOrDefaultAsync(cc => cc.CustomerCouponID == staticID);

    if (customerCoupon != null)
    {
        customerCoupon.Customer!.FullName = "Ahmet Yılmaz";
        customerCoupon.Customer.Age = 30;
        customerCoupon.Customer.Address = "İstanbul, Beşiktaş";

        customerCoupon.Coupon!.Code = "WELCOME2025";
        customerCoupon.Coupon.ValidFrom = DateTime.Now;
        customerCoupon.Coupon.ValidUntil = DateTime.Now.AddMonths(1);

        customerCoupon.RedeemedAt = DateTime.Now.AddMonths(2);

        await _appDbContext.SaveChangesAsync();
    }
}

🔹 Silme İşlemi (Include ile Tablolarla Beraber)

public async Task DeleteAsync()
{
    int staticID = 2;
    var customerCoupon = await _appDbContext.CustomerCoupons
        .Include(cc => cc.Customer)
        .Include(cc => cc.Coupon)
        .FirstOrDefaultAsync(cc => cc.CustomerCouponID == staticID);

    if (customerCoupon != null)
    {
        _appDbContext.CustomerCoupons.Remove(customerCoupon);
        await _appDbContext.SaveChangesAsync();
    }
}

 3. Durum – Controller: CustomerCouponWithCustomerAndCouponStaticController.cs

public class CustomerCouponWithCustomerAndCouponStaticController : Controller
{
    private readonly ICustomerCouponWithCustomerAndCouponStaticRepository _repo;

    public CustomerCouponWithCustomerAndCouponStaticController(ICustomerCouponWithCustomerAndCouponStaticRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _repo.GetAllAsync();
        return View(list);
    }

    public async Task<IActionResult> GetByID()
    {
        var item = await _repo.GetByIdAsync();
        if (item == null) return NotFound("Bulunamadı");
        return View(item);
    }

    public async Task<IActionResult> AddCustomerCoupon()
    {
        await _repo.AddAsync();
        TempData["Message"] = "Eklendi";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateCustomerCoupon()
    {
        await _repo.UpdateAsync();
        TempData["Message"] = "Güncellendi";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteCustomerCoupon()
    {
        await _repo.DeleteAsync();
        TempData["Message"] = "Silindi";
        return RedirectToAction(nameof(Index));
    }
}

Açıklama:

Controller içinde statik veri ile Add, Update, Delete, GetAll, GetByID gibi işlemler yapılır.
View’a yönlendirme yapılırken kullanıcıya bilgi mesajı (TempData) verilir.

 4. Durum – View (Listeleme Sayfası): Index.cshtml

@model List<CustomerCoupon>

@{
    ViewData["Title"] = "Müşteri Kuponları Listesi";
}

<h1>@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/CustomerCouponWithCustomerAndCouponStatic/AddCustomerCoupon">Ekle</a>
    <a class="btn btn-warning" href="/CustomerCouponWithCustomerAndCouponStatic/UpdateCustomerCoupon">Güncelle</a>
    <a class="btn btn-danger" href="/CustomerCouponWithCustomerAndCouponStatic/DeleteCustomerCoupon">Sil</a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Ad-Soyad</th>
            <th>Yaş</th>
            <th>Adres</th>
            <th>Kod</th>
            <th>Başlangıç</th>
            <th>Bitiş</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.CustomerCouponID</td>
                <td>@item.Customer?.FullName</td>
                <td>@item.Customer?.Age</td>
                <td>@item.Customer?.Address</td>
                <td>@item.Coupon?.Code</td>
                <td>@item.Coupon?.ValidFrom</td>
                <td>@item.Coupon?.ValidUntil</td>
            </tr>
        }
    </tbody>
</table>

Açıklama:

@model tanımı ile List<CustomerCoupon> tipi karşılanır.
İlişkili Customer ve Coupon bilgileri kolayca erişilir (Include ile geldiği için).
Bootstrap class’ları ile tablo ve butonlar görsel olarak düzenlenmiştir.

🎯 GENEL SONUÇ

Bu yapı ile EF Core'da:
Many-to-Many ilişkili tablolar oluşturmayı,
Bu yapılar üzerinden Include ile veri çekmeyi,
Statik olarak CRUD işlemleri gerçekleştirmeyi,
Tüm işlemleri controller ve view katmanlarında yönetmeyi öğrendin.
