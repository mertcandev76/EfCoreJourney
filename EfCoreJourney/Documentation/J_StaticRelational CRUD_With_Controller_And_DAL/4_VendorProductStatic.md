-->VendorProduct

1. Durum – VendorProduct Entity Sınıfı (EntityLayer.Concrete)

public class VendorProduct
{
    [Key]
    public int VendorProductID { get; set; }

    public int ProductID { get; set; } // foreign key
    public Product? Product { get; set; }

    public int ProductVendorID { get; set; } // foreign key
    public ProductVendor? ProductVendor { get; set; }
}

Açıklama:

Bu sınıf, Product (ürün) ve ProductVendor (tedarikçi) arasındaki çoktan çoğa ilişkiyi temsil eder.
Foreign key'ler: ProductID, ProductVendorID
Navigation Property'ler: Product, ProductVendor
Entity Framework, bu yapıyı okurken VendorProduct tablosunun içinde ProductID ve ProductVendorID üzerinden ilişkili verileri Include edebilmeni sağlar.

2. Durum – VendorProductWithProductAndProductVendorStaticRepository (DataAccessLayer.Repositories)

public class VendorProductWithProductAndProductVendorStaticRepository : IVendorProductWithProductAndProductVendorStaticRepository

📌 Metotlar:
🔹 GetAllAsync()

var vendorProduct = await _appDbContext.VendorProducts
    .Include(vp => vp.Product)
    .Include(vp => vp.ProductVendor)
    .ToListAsync();

Tüm VendorProduct kayıtlarını, Product ve ProductVendor verileriyle birlikte getirir.
Include() sayesinde navigation property’leri yükler.

🔹 GetByIdAsync()

int staticID = 3;
var vendorProduct = await _appDbContext.VendorProducts
    .Include(vp => vp.Product)
    .Include(vp => vp.ProductVendor)
    .FirstOrDefaultAsync(vp => vp.VendorProductID == staticID);

ID’si sabit (örnek: 3) olan kayıt getirilir.
Navigation property’ler de birlikte yüklenir.

🔹 AddAsync()

var vendorProduct = new VendorProduct
{
    ProductVendorID = 1,
    ProductID = 1004
};

Sadece ProductID ve ProductVendorID atayarak yeni bir kayıt oluşturulur.
Navigation property set edilmez — sadece ID üzerinden ilişki kurulmuş olur (EF bunu anlar ve bağlar).

🔹 UpdateAsync()

var vendorProduct = await _appDbContext.VendorProducts
    .Include(vp => vp.Product)
    .Include(vp => vp.ProductVendor)
    .FirstOrDefaultAsync(vp => vp.VendorProductID == staticID);
VendorProduct kaydı, ilişkili Product ve ProductVendor ile birlikte çekilir.

Bu navigation property’ler üzerinden doğrudan güncelleme yapılır:
Örn: Product.Name = "Samsung Galaxy S22";

📌 Not: Bu yapı navigation üzerinden güncelleme yapar, yani Product ve ProductVendor nesneleri doğrudan değiştirilmiş olur.

🔹 DeleteAsync()

var vendorProduct = await _appDbContext.VendorProducts
    .Include(vp => vp.Product)
    .Include(vp => vp.ProductVendor)
    .FirstOrDefaultAsync(vp => vp.VendorProductID == staticID);

Belirli bir VendorProduct silinir.
Include() kullanılmış ama silme sadece VendorProduct tablosunda yapılır.
Cascade Delete ayarlıysa Product veya ProductVendor da silinebilir — bu durumda dikkatli olmak gerekir.

3. Durum – Controller Sınıfı

namespace EfCoreJourney.Controllers
{
    public class VendorProductWithProductAndProductVendorStaticController : Controller
    {
        private readonly IVendorProductWithProductAndProductVendorStaticRepository _vendorProductRepo;

        public VendorProductWithProductAndProductVendorStaticController(IVendorProductWithProductAndProductVendorStaticRepository vendorProductRepo)
        {
            _vendorProductRepo = vendorProductRepo;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _vendorProductRepo.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> GetById()
        {
            var entity = await _vendorProductRepo.GetByIdAsync();
            if (entity == null) return NotFound("Kayıt bulunamadı.");
            return View(entity);
        }

        public async Task<IActionResult> AddVendorProduct()
        {
            await _vendorProductRepo.AddAsync();
            TempData["Message"] = "Tedarikçi ürünü eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateVendorProduct()
        {
            await _vendorProductRepo.UpdateAsync();
            TempData["Message"] = "Tedarikçi ürünü güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteVendorProduct()
        {
            await _vendorProductRepo.DeleteAsync();
            TempData["Message"] = "Tedarikçi ürünü silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}

Açıklama:

Controller, sadece Repository katmanına bağımlıdır. DbContext'e doğrudan erişmez.
İşlemler sonrası TempData ile kullanıcıya mesaj verilir.
Index: Listeleme
GetById: Sabit ID ile detay
Add, Update, Delete: Statik işlemler (test amaçlı)

4. Durum – View (Index.cshtml)

@model List<EntityLayer.Concrete.VendorProduct>
@{
    ViewData["Title"] = "Tedarikçi Ürünleri Listesi";
}

<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/VendorProductWithProductAndProductVendorStatic/AddVendorProduct">Ekle</a>
    <a class="btn btn-danger" href="/VendorProductWithProductAndProductVendorStatic/DeleteVendorProduct">Sil</a>
    <a class="btn btn-warning" href="/VendorProductWithProductAndProductVendorStatic/UpdateVendorProduct">Güncelle</a>
</div>

<table class="table table-bordered">
    <thead>
        <tr>
            <th>ID</th>
            <th>Ürün İsmi</th>
            <th>Ürün Açıklaması</th>
            <th>Firma Adı</th>
            <th>İlgili Kişi</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.VendorProductID</td>
                <td>@item.Product?.Name</td>
                <td>@item.Product?.Description</td>
                <td>@item.ProductVendor?.CompanyName</td>
                <td>@item.ProductVendor?.ContactPerson</td>
            </tr>
        }
    </tbody>
</table>

Açıklama:

Model, VendorProduct listesidir.
Product ve ProductVendor üzerinden ilgili veriler Navigation ile çekilir.
Kullanıcı işlemlerinden sonra bilgi mesajı TempData ile gösterilir.
Basit ve işlevsel bir listeleme ekranı sunar.

✅ SONUÇ

| Bileşen        | Açıklama                                                                              |
| -------------- | ------------------------------------------------------------------------------------- |
| **Entity**     | `VendorProduct`, iki tablo arasında köprü görevindedir.                               |
| **Repository** | Sadece static verilerle işlem yapar. `Include()` ile ilişkili veriler alınır.         |
| **Controller** | Repository’e bağlı, temiz bir yapıdadır. İşlem sonrası yönlendirme ve mesaj verir.    |
| **View**       | Listeleme ve aksiyon butonları içerir. Navigation property'lerle detaylar gösterilir. |


