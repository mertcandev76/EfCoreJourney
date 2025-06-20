-->Store

1. Durum – Store Entity Sınıfı

📌 Kod:

namespace EntityLayer.Concrete
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        public string? Name { get; set; }
        public string? OwnerName { get; set; }
        public string? Email { get; set; }

        public StoreSetting? StoreSetting { get; set; }
    }
}

Açıklama:

Bu sınıf, Entity katmanında yer alır ve veritabanındaki Stores tablosunu temsil eder.
StoreID: Primary Key olarak tanımlanmıştır.
Name, OwnerName, Email: Mağazaya ait temel verileri tutar.
StoreSetting: Mağaza ayarlarıyla bire-bir ilişki kurmak için kullanılan navigation property’dir.
EF Core'da bu ilişkiyi Fluent API ile destekleyebilirsin.

2. Durum – StoreWithStoreSettingStaticRepository (Repository Sınıfı)

📌 Kod:

namespace DataAccessLayer.Repositories
{
    public class StoreWithStoreSettingStaticRepository : IStoreWithStoreSettingStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public StoreWithStoreSettingStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Store>> GetAllAsync()
        {
            var store = await _appDbContext.Stores.ToListAsync();
            return store;
        }

        public async Task<Store?> GetByIdAsync()
        {
            int staticID = 1;
            var store = await _appDbContext.Stores.FindAsync(staticID);
            return store;
        }

        public async Task AddAsync()
        {
            var store = new Store
            {
                Name = "Mertcan's Store",
                OwnerName = "Mertcan Adsız",
                Email = "mertcan.adsiz@example.com"
            };

            await _appDbContext.Stores.AddAsync(store);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            int staticID = 1;
            var store = await _appDbContext.Stores.FindAsync(staticID);
            if (store != null)
            {
                store.Name = "Mertcan's Updated Store";
                store.OwnerName = "Mertcan Adsız";
                store.Email = "mertcan.updated@example.com";

                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync()
        {
            int staticID = 1;
            var store = await _appDbContext.Stores.FindAsync(staticID);
            if (store != null)
            {
                _appDbContext.Stores.Remove(store);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}

 Açıklama:

Repository sınıfı, veritabanı işlemlerini (CRUD) gerçekleştiren katmandır.
AppDbContext üzerinden Store tablosuna erişilir.
Her işlemde sabit ID (staticID = 1) kullanılmıştır, öğrenme amaçlı idealdir.
AddAsync, UpdateAsync, DeleteAsync metotlarında veriler manuel olarak girilir.
Bu yapı daha sonra dinamikleştirilebilir (formdan gelen veriyle).

3. Durum – StoreWithStoreSettingStaticController (Controller Sınıfı)

📌 Kod:

namespace EfCoreJourney.Controllers
{
    public class StoreWithStoreSettingStaticController : Controller
    {
        private readonly IStoreWithStoreSettingStaticRepository _storeWithStoreSettingStaticRepository;

        public StoreWithStoreSettingStaticController(IStoreWithStoreSettingStaticRepository storeWithStoreSettingStaticRepository)
        {
            _storeWithStoreSettingStaticRepository = storeWithStoreSettingStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _storeWithStoreSettingStaticRepository.GetAllAsync();
            return View(stores);
        }

        public async Task<IActionResult> GetByID()
        {
            var store = await _storeWithStoreSettingStaticRepository.GetByIdAsync();
            if (store == null)
            {
                return NotFound("Belirtilen ID'ye ait mağaza bulunamadı.");
            }
            return View(store);
        }

        public async Task<IActionResult> AddStore()
        {
            await _storeWithStoreSettingStaticRepository.AddAsync();
            TempData["Message"] = "Mağaza başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateStore()
        {
            await _storeWithStoreSettingStaticRepository.UpdateAsync();
            TempData["Message"] = "Mağaza başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteStore()
        {
            await _storeWithStoreSettingStaticRepository.DeleteAsync();
            TempData["Message"] = "Mağaza başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}

Açıklama:

Controller sınıfı, kullanıcıdan gelen istekleri karşılar ve gerekli işlemleri başlatır.
Repository sınıfı Dependency Injection ile alınır.
TempData sayesinde işlemler sonrası kullanıcıya bildirim gönderilir.

Her metot belirli bir işlevi yerine getirir:

Index() → Listeleme
GetByID() → Belirli bir mağazayı getirir
AddStore() → Mağaza ekler
UpdateStore() → Günceller
DeleteStore() → Siler

4. Durum – View (Razor Sayfası)

📌 Kod:

@model List<EntityLayer.Concrete.Store>
@{
    ViewData["Title"] = "Mağaza Listesi";
}

<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a href="/StoreWithStoreSettingStatic/AddStore" class="btn btn-success">Ekle</a>
    <a href="/StoreWithStoreSettingStatic/UpdateStore" class="btn btn-warning">Güncelle</a>
    <a href="/StoreWithStoreSettingStatic/DeleteStore" class="btn btn-danger">Sil</a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Mağaza Adı</th>
            <th>Sahibi</th>
            <th>Email</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var store in Model)
        {
            <tr>
                <th>@store.StoreID</th>
                <td>@store.Name</td>
                <td>@store.OwnerName</td>
                <td>@store.Email</td>
            </tr>
        }
    </tbody>
</table>

Açıklama:

Razor sayfası, Controller'dan gelen Store listesini kullanıcıya gösterir.
@model List<Store> ile gelen veri tipi belirtilir.
TempData["Message"] işlemler sonrası kullanıcıya bilgi verir.
<a href="..."> linkleri Controller’daki action’lara yönlendirir.

🎯 Özet:

| Katman              | Amaç                                                        |
| ------------------- | ----------------------------------------------------------- |
| **EntityLayer**     | Veritabanı tablosunu temsil eden `Store` sınıfı tanımlanır. |
| **DataAccessLayer** | EF Core ile CRUD işlemlerini yapan `Repository` yazılır.    |
| **Controller**      | İstekleri karşılar, veriyi işleyip View’a gönderir.         |
| **View (Razor)**    | Kullanıcıya listeleme ve işlem butonları sunar.             |


