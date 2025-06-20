-->ProductBrand

1. Durum: Entity Sınıfı (ProductBrand)

namespace EntityLayer.Concrete
{
    public class ProductBrand
    {
        [Key] // Bu alanın birincil anahtar (Primary Key) olduğunu belirtir.
        public int ProductBrandID { get; set; }

        public string? Name { get; set; } // Marka adı (örnek: Arçelik, Bosch)
        public string? Description { get; set; } // Açıklama (örnek: Elektronik ürünler üreticisi)
        public string? Country { get; set; } // Ülke bilgisi (örnek: Türkiye, Almanya)

        // Bire-çok (One-to-Many) ilişkiyi temsil eder.
        public ICollection<Product>? Products { get; set; }
    }
}

Açıklama:

ICollection<Product>? Products navigasyon property’si, bir ürün markasının birden çok ürünü olabileceğini belirtir (1-N ilişkisi).
Bu sayede Entity Framework, markayla ilişkili ürünleri de sorgulayabilir.

 2. Durum: ProductBrandStaticRepository (Statik Repository Katmanı)
namespace DataAccessLayer.Repositories
{
    public class ProductBrandStaticRepository : IProductBrandStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductBrandStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Tüm marka kayıtlarını getirir
        public async Task<List<ProductBrand>> GetAllAsync()
        {
            var productBrand = await _appDbContext.ProductBrands.ToListAsync();
            return productBrand;
        }

        // Belirli (sabit) bir ID'ye göre marka getirir
        public async Task<ProductBrand?> GetByIdAsync()
        {
            int staticID = 2;
            var productBrand = await _appDbContext.ProductBrands.FindAsync(staticID);
            return productBrand;
        }

        // Yeni bir marka ekler
        public async Task AddAsync()
        {
            var productBrand = new ProductBrand
            {
                Name = "Arçelik",
                Description = "Ev elektroniği ve beyaz eşya ürünleri markası.",
                Country = "Türkiye"
            };
            await _appDbContext.ProductBrands.AddAsync(productBrand);
            await _appDbContext.SaveChangesAsync();
        }

        // Sabit ID'li markayı günceller
        public async Task UpdateAsync()
        {
            int staticID = 3;
            var productBrand = await _appDbContext.ProductBrands.FindAsync(staticID);
            if (productBrand != null)
            {
                productBrand.Name = "Bosch";
                productBrand.Description = "Alman menşeli, beyaz eşya ve elektronik ürünler markası.";
                productBrand.Country = "Almanya";
                await _appDbContext.SaveChangesAsync();
            }
        }

        // Sabit ID'li markayı siler
        public async Task DeleteAsync()
        {
            int staticID = 4;
            var productBrand = await _appDbContext.ProductBrands.FindAsync(staticID);
            if (productBrand != null)
            {
                _appDbContext.ProductBrands.Remove(productBrand);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}

Özet:

GetAllAsync → Tüm markaları getirir.
GetByIdAsync → Sabit ID ile bir marka bulur (ID = 2).
AddAsync → Arçelik markasını ekler.
UpdateAsync → ID’si 3 olan markayı Bosch olarak günceller.
DeleteAsync → ID’si 4 olan markayı siler.
Repository yapısı ile veriye erişim katmanı soyutlanır.

3. Durum: ProductBrandStaticController (Controller Katmanı)

namespace EfCoreJourney.Controllers
{
    public class ProductBrandStaticController : Controller
    {
        private readonly IProductBrandStaticRepository _productsBrandStaticRepository;

        public ProductBrandStaticController(IProductBrandStaticRepository productsBrandWithProductsStaticRepository)
        {
            _productsBrandStaticRepository = productsBrandWithProductsStaticRepository;
        }

        // Marka listesi sayfası
        public async Task<IActionResult> Index()
        {
            var productBrands = await _productsBrandStaticRepository.GetAllAsync();
            return View(productBrands);
        }

        // ID'ye göre bir marka getirir (detay sayfası olabilir)
        public async Task<IActionResult> GetByID()
        {
            var productBrands = await _productsBrandStaticRepository.GetByIdAsync();
            if (productBrands == null)
            {
                return NotFound("Belirtilen ürün markası bulunamadı.");
            }
            return View(productBrands);
        }

        // Sabit veriyi veritabanına ekler
        public async Task<IActionResult> AddProductBrand()
        {
            await _productsBrandStaticRepository.AddAsync();
            TempData["Message"] = "Ürün markası başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        // Sabit ID’li veriyi günceller
        public async Task<IActionResult> UpdateProductBrand()
        {
            await _productsBrandStaticRepository.UpdateAsync();
            TempData["Message"] = "Ürün markası başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        // Sabit ID’li veriyi siler
        public async Task<IActionResult> DeleteProductBrand()
        {
            await _productsBrandStaticRepository.DeleteAsync();
            TempData["Message"] = "Ürün markası başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}

Özet:

Index() → View'e tüm markaları gönderir.
AddProductBrand() → Sabit veriyi ekler.
UpdateProductBrand() → Sabit ID ile veriyi günceller.
DeleteProductBrand() → Sabit ID ile veriyi siler.
TempData["Message"] → Kullanıcıya işlem sonrası mesaj gösterir.

 4. Durum: Index.cshtml - Razor View Sayfası (UI / Arayüz)

@model List<EntityLayer.Concrete.ProductBrand>
@{
    ViewData["Title"] = "Ürün Markaları Listesi";
}

<h1 class="mb-4">@ViewData["Title"]</h1>

<br />

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/ProductBrandStatic/AddProductBrand">Ekle</a>
    <a class="btn btn-warning" href="/ProductBrandStatic/UpdateProductBrand">Güncelle</a>
    <a class="btn btn-danger" href="/ProductBrandStatic/DeleteProductBrand">Sil</a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Marka Adı</th>
            <th>Açıklama</th>
            <th>Ülke Markası</th>
            <th>Ürün Sayısı</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var brand in Model)
        {
            <tr>
                <td>@brand.ProductBrandID</td>
                <td>@brand.Name</td>
                <td>@brand.Description</td>
                <td>@brand.Country</td>
                <td>@(brand.Products?.Count ?? 0)</td>
            </tr>
        }
    </tbody>
</table>

Özet:

View, List<ProductBrand> türünde bir model bekler.
Sayfa başlığı dinamik olarak ayarlanır.
TempData sayesinde kullanıcıya bilgi mesajı gösterilir.
"Ekle", "Güncelle", "Sil" butonları, ilgili Action'lara yönlendirme yapar.
Tablo ile markaların detayları (ID, isim, açıklama, ülke, ürün sayısı) listelenir.

🔚 SONUÇ:

| Katman                           | Görev             | Açıklama                                                |
| -------------------------------- | ----------------- | ------------------------------------------------------- |
| **EntityLayer**                  | Veri Modeli       | EF Core ile veritabanı tablosunu temsil eder.           |
| **DataAccessLayer (Repository)** | Veri erişimi      | CRUD işlemleri burada tanımlanır.                       |
| **Controller**                   | Yönetim           | UI ile veri işlemleri arasında köprü kurar.             |
| **View (Razor)**                 | Kullanıcı Arayüzü | Veriyi kullanıcıya gösterir, butonlarla işlem yaptırır. |

