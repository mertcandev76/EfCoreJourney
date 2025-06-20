-->ProductVendor

1. Durum: ProductVendor Entity Sınıfı

namespace EntityLayer.Concrete
{
    public class ProductVendor
    {
        [Key]
        public int ProductVendorID { get; set; }

        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}

✔ Açıklama:

ProductVendor: Tedarikçi tablosunu temsil eden sınıf.
[Key]: ProductVendorID birincil anahtar olduğunu belirtir.
Diğer alanlar (CompanyName, ContactPerson, Email, Phone) nullable string türündedir ve tedarikçiye ait temel bilgileri tutar.
Bu sınıf, EF Core ile bir tabloya dönüşür.

 2. Durum: ProductVendorStaticRepository Sınıfı (DataAccessLayer)

public class ProductVendorStaticRepository : IProductVendorStaticRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductVendorStaticRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<ProductVendor>> GetAllAsync()
    {
        return await _appDbContext.ProductVendors.ToListAsync();
    }

    public async Task<ProductVendor?> GetByIdAsync()
    {
        int staticID = 2;
        return await _appDbContext.ProductVendors.FindAsync(staticID);
    }

    public async Task AddAsync()
    {
        var productVendor = new ProductVendor
        {
            CompanyName = "GlobalTech Solutions",
            ContactPerson = "Ayşe Yılmaz",
            Email = "ayse.yilmaz@globaltech.com",
            Phone = "+90 212 555 1234"
        };
        await _appDbContext.ProductVendors.AddAsync(productVendor);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        int staticID = 1;
        var productVendor = await _appDbContext.ProductVendors.FindAsync(staticID);
        if (productVendor != null)
        {
            productVendor.CompanyName = "TechSolutions Ltd.";
            productVendor.ContactPerson = "Ahmet Demir";
            productVendor.Email = "ahmet.demir@techsolutions.com";
            productVendor.Phone = "+90 532 987 6543";
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync()
    {
        int staticID = 1;
        var productVendor = await _appDbContext.ProductVendors.FindAsync(staticID);
        if (productVendor != null)
        {
            _appDbContext.ProductVendors.Remove(productVendor);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

Açıklama:

Bu sınıf, IProductVendorStaticRepository arayüzünü implement eder ve veritabanı işlemlerini yapar.

Metot Adı	Açıklama
GetAllAsync()	Tüm tedarikçi verilerini listeler.
GetByIdAsync()	Sabit ID'ye göre (örnek: staticID = 2) tek bir tedarikçi getirir.
AddAsync()	Yeni bir sabit tedarikçi ekler.
UpdateAsync()	ID’si 1 olan tedarikçinin bilgilerini günceller.
DeleteAsync()	ID’si 1 olan tedarikçiyi siler.

⚠ Bu işlemler “statik” ID’lerle çalıştığı için kullanıcıdan veri alınmaz. Test ve gösterim amaçlıdır.

🔹 3. Durum: ProductVendorStaticController Sınıfı (Controller)

public class ProductVendorStaticController : Controller
{
    private readonly IProductVendorStaticRepository _productsVendorStaticRepository;

    public ProductVendorStaticController(IProductVendorStaticRepository productsVendorStaticRepository)
    {
        _productsVendorStaticRepository = productsVendorStaticRepository;
    }

    public async Task<IActionResult> Index()
    {
        var productVendors = await _productsVendorStaticRepository.GetAllAsync();
        return View(productVendors);
    }

    public async Task<IActionResult> GetByID()
    {
        var productVendor = await _productsVendorStaticRepository.GetByIdAsync();
        if (productVendor == null)
            return NotFound("Belirtilen tedarikçi bulunamadı.");
        return View(productVendor);
    }

    public async Task<IActionResult> AddProductVendor()
    {
        await _productsVendorStaticRepository.AddAsync();
        TempData["Message"] = "Tedarikçi başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateProductVendor()
    {
        await _productsVendorStaticRepository.UpdateAsync();
        TempData["Message"] = "Tedarikçi başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteProductVendor()
    {
        await _productsVendorStaticRepository.DeleteAsync();
        TempData["Message"] = "Tedarikçi başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}

Açıklama:

Controller, ilgili repository üzerinden çağrı yapar.
İşlemler sırasıyla çağrılır:
Index(): Listeleme işlemi
GetByID(): Sabit ID ile tek veri gösterimi
AddProductVendor(): Ekleme işlemi
UpdateProductVendor(): Güncelleme
DeleteProductVendor(): Silme
TempData["Message"] ile işlem başarı mesajı kullanıcıya View'da gösterilir.

4. Durum: Razor View Sayfası (Index.cshtml)

@model List<EntityLayer.Concrete.ProductVendor>
@{
    ViewData["Title"] = "Tedarikçi Listesi";
}

<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/ProductVendorStatic/AddProductVendor">Ekle</a>
    <a class="btn btn-warning" href="/ProductVendorStatic/UpdateProductVendor">Güncelle</a>
    <a class="btn btn-danger" href="/ProductVendorStatic/DeleteProductVendor">Sil</a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Firma Adı</th>
            <th>İlgili Kişi</th>
            <th>Email</th>
            <th>Telefon</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var productVendor in Model)
        {
            <tr>
                <td>@productVendor.ProductVendorID</td>
                <td>@productVendor.CompanyName</td>
                <td>@productVendor.ContactPerson</td>
                <td>@productVendor.Email</td>
                <td>@productVendor.Phone</td>
            </tr>
        }
    </tbody>
</table>

Açıklama:

Model: List<ProductVendor> — yani birden fazla tedarikçiyi temsil eder.
TempData mesajı varsa başarılı işlem mesajı gösterilir.
Üstteki butonlar ilgili controller action’larına gider (Ekle, Güncelle, Sil).
Alt kısımda foreach ile tüm tedarikçiler tabloya yazdırılır.

🔚 SONUÇ

| Katman         | Açıklama                                                                                         |
| -------------- | ------------------------------------------------------------------------------------------------ |
| **Entity**     | `ProductVendor` sınıfı ile veri modeli oluşturulmuş.                                             |
| **Repository** | Veri işlemleri statik olarak gerçekleştirilmiş (`ID = 1`, `ID = 2`).                             |
| **Controller** | Repository üzerinden View’a veri taşınmış.                                                       |
| **View**       | Tüm işlemler sonucu elde edilen veriler tabloda listelenmiş, işlemler butonlarla yönlendirilmiş. |


