-->Coupon

 1. Durum: Entity Tanımı (EntityLayer.Concrete)

Amaç:

Veritabanındaki Coupons tablosunu temsil eden veri modelini oluşturmak.

namespace EntityLayer.Concrete
{
    public class Coupon
    {
        [Key]
        public int CouponID { get; set; }

        public string? Code { get; set; }               // Kupon kodu (örn: "WELCOME10")
        public decimal DiscountRate { get; set; }       // İndirim oranı (örn: 10 => %10)
        public DateTime ValidFrom { get; set; }         // Geçerlilik başlangıç tarihi
        public DateTime ValidUntil { get; set; }        // Geçerlilik bitiş tarihi
        public bool IsActive { get; set; }              // Aktif mi?

        public ICollection<CustomerCoupon>? CustomerCoupons { get; set; } // İlişkili kullanıcılar (çoktan çoğa)
    }
}

Bu sınıf, EF Core tarafından Coupons adında bir tabloya dönüştürülür.

2. Durum: Repository Katmanı (DataAccessLayer.Repositories)

Amaç:

Coupon tablosu üzerinde statik CRUD işlemlerini yapmak.

public class CouponStaticRepository : ICouponStaticRepository
{
    private readonly AppDbContext _appDbContext;

    public CouponStaticRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // 1. Tüm kuponları getir
    public async Task<List<Coupon>> GetAllAsync()
    {
        return await _appDbContext.Coupons.ToListAsync();
    }

    // 2. Sabit bir ID ile kupon getir
    public async Task<Coupon?> GetByIdAsync()
    {
        int staticID = 1;
        return await _appDbContext.Coupons.FindAsync(staticID);
    }

    // 3. Sabit verilerle yeni kupon ekle
    public async Task AddAsync()
    {
        var coupon = new Coupon
        {
            Code = "WELCOME10",
            DiscountRate = 10,
            ValidFrom = DateTime.Now,
            ValidUntil = DateTime.Now.AddDays(7),
            IsActive = true
        };
        await _appDbContext.Coupons.AddAsync(coupon);
        await _appDbContext.SaveChangesAsync();
    }

    // 4. Sabit ID'li kuponu güncelle
    public async Task UpdateAsync()
    {
        int staticID = 1;
        var coupon = await _appDbContext.Coupons.FindAsync(staticID);
        if (coupon != null)
        {
            coupon.Code = "SUMMER30";
            coupon.DiscountRate = 30;
            coupon.ValidFrom = DateTime.Now;
            coupon.ValidUntil = DateTime.Now.AddDays(30);
            coupon.IsActive = true;
            await _appDbContext.SaveChangesAsync();
        }
    }

    // 5. Sabit ID'li kuponu sil
    public async Task DeleteAsync()
    {
        int staticID = 1;
        var coupon = await _appDbContext.Coupons.FindAsync(staticID);
        if (coupon != null)
        {
            _appDbContext.Coupons.Remove(coupon);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

AppDbContext üzerinden işlemler yapılır. Kodlar test amaçlı sabit ID (1) üzerinden işlem yapar.

3. Durum: Controller Katmanı (CouponStaticController)

Amaç:

Kullanıcının View üzerinden yaptığı istekleri Repository'e yönlendirmek.

public class CouponStaticController : Controller
{
    private readonly ICouponStaticRepository _couponStaticRepository;

    public CouponStaticController(ICouponStaticRepository couponStaticRepository)
    {
        _couponStaticRepository = couponStaticRepository;
    }

    public async Task<IActionResult> Index()
    {
        var coupons = await _couponStaticRepository.GetAllAsync();
        return View(coupons); // Coupon listesi döner
    }

    public async Task<IActionResult> GetByID()
    {
        var coupon = await _couponStaticRepository.GetByIdAsync();
        if (coupon == null)
            return NotFound("Belirtilen kupon bulunamadı.");
        return View(coupon);
    }

    public async Task<IActionResult> AddCoupon()
    {
        await _couponStaticRepository.AddAsync();
        TempData["Message"] = "Kupon başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateCoupon()
    {
        await _couponStaticRepository.UpdateAsync();
        TempData["Message"] = "Kupon başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteCoupon()
    {
        await _couponStaticRepository.DeleteAsync();
        TempData["Message"] = "Kupon başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}

Controller, kullanıcının tetiklediği eylemleri ilgili repository metotlarına yönlendirir. TempData mesajları ile bilgi verilir.

4. Durum: View Katmanı (Index.cshtml)

Amaç:

Kuponların listesini tabloda göstermek, sabit işlemleri tetiklemek.

@model List<EntityLayer.Concrete.Coupon>

@{
    ViewData["Title"] = "Kupon Listesi";
}

<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<!-- Butonlar -->
<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/CouponStatic/AddCoupon">Ekle</a>
    <a class="btn btn-warning" href="/CouponStatic/UpdateCoupon">Güncelle</a>
    <a class="btn btn-danger" href="/CouponStatic/DeleteCoupon">Sil</a>
</div>

<!-- Kupon Tablosu -->
<table class="table table-bordered">
    <thead>
        <tr>
            <th>CouponID</th>
            <th>Kod</th>
            <th>İndirim Oranı</th>
            <th>Geçerlilik Başlangıç</th>
            <th>Geçerlilik Bitiş</th>
            <th>Aktif mi?</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var coupon in Model)
        {
            <tr>
                <td>@coupon.CouponID</td>
                <td>@coupon.Code</td>
                <td>@coupon.DiscountRate</td>
                <td>@coupon.ValidFrom.ToShortDateString()</td>
                <td>@coupon.ValidUntil.ToShortDateString()</td>
                <td>@(coupon.IsActive ? "Evet" : "Hayır")</td>
            </tr>
        }
    </tbody>
</table>

Veriler Model üzerinden tabloda listelenir. TempData ile mesaj gösterilir. Sabit işlemler için üstteki butonlar kullanılır.


🔚 Sonuç

| Katman                    | Açıklama                                                                    |
| ------------------------- | --------------------------------------------------------------------------- |
| **Entity (1. Durum)**     | Veritabanı tablosunu temsil eden model.                                     |
| **Repository (2. Durum)** | EF Core ile veritabanı işlemleri. Sabit ID ile Add, Update, Delete yapılır. |
| **Controller (3. Durum)** | Kullanıcıdan gelen isteği repository'e yönlendirir.                         |
| **View (4. Durum)**       | Tabloda veri gösterimi ve butonlarla statik CRUD tetiklenir.                |
