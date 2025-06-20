-->StoreSetting

1. Durum: StoreSetting Entity’si (Model Katmaný)

namespace EntityLayer.Concrete
{
    public class StoreSetting
    {
        [Key]
        public int StoreSettingID { get; set; }

        public string? Currency { get; set; }
        public string? Language { get; set; }
        public bool EnableNotifications { get; set; }

        public int StoreId { get; set; }
        public Store? Store { get; set; }
    }
}

Açýklama:

Bu sýnýf StoreSetting adýnda bir tabloyu temsil eder.
StoreSettingID: Birincil anahtar.
Currency, Language, EnableNotifications: Maðazaya özel ayarlar.
StoreId: Ýliþkili Store tablosunun Foreign Key'i.
Store: Navigation Property, bir StoreSetting sadece bir Store ile iliþkilidir. (One-to-One ya da One-to-Many senaryosu olabilir ama burada birebir varsayýlmýþ).

2. Durum: StoreSettingWithStoreStaticRepository (Repository Katmaný)

public class StoreSettingWithStoreStaticRepository : IStoreSettingWithStoreStaticRepository
{
    private readonly AppDbContext _appDbContext;

    public StoreSettingWithStoreStaticRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<StoreSetting>> GetAllAsync()
    {
        return await _appDbContext.StoreSettings
            .Include(ss => ss.Store) // Store bilgilerini de dahil et
            .ToListAsync();
    }

    public async Task<StoreSetting?> GetByIdAsync()
    {
        int staticID = 2;
        return await _appDbContext.StoreSettings
            .Include(ss => ss.Store)
            .FirstOrDefaultAsync(ss => ss.StoreSettingID == staticID);
    }

    public async Task AddAsync()
    {
        var storeSetting = new StoreSetting
        {
            Currency = "USD",
            Language = "English",
            EnableNotifications = false,
            StoreId = 2 // mevcut bir Store ile eþleþtirildi
        };

        await _appDbContext.StoreSettings.AddAsync(storeSetting);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        int staticID = 2;
        var storeSetting = await _appDbContext.StoreSettings
            .Include(ss => ss.Store)
            .FirstOrDefaultAsync(ss => ss.StoreSettingID == staticID);

        if (storeSetting != null)
        {
            storeSetting.Currency = "USD";
            storeSetting.Language = "English";
            storeSetting.EnableNotifications = true;

            if (storeSetting.Store != null)
            {
                storeSetting.Store.Name = "Mertcan's Store";
                storeSetting.Store.OwnerName = "Mertcan Adsýz";
                storeSetting.Store.Email = "mertcan@example.com";
            }

            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync()
    {
        int staticID = 2;
        var storeSetting = await _appDbContext.StoreSettings
            .Include(ss => ss.Store)
            .FirstOrDefaultAsync(ss => ss.StoreSettingID == staticID);

        if (storeSetting != null)
        {
            _appDbContext.StoreSettings.Remove(storeSetting);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

Açýklama:

Bu Repository, veri eriþimini soyutlar ve StoreSetting üzerinde CRUD iþlemlerini tanýmlar.
Her metod statik ID=2 üzerinden iþlem yapar (bu sabit ID öðrenme amaçlýdýr, dinamik yapýlarda kullanýcýdan alýnmalýdýr).
Include(ss => ss.Store): StoreSetting ile iliþkili Store verisi de çekilir. Bu bir Eager Loading örneðidir.

3. Durum: StoreSettingWithStoreStaticController (Controller Katmaný)

public class StoreSettingWithStoreStaticController : Controller
{
    private readonly IStoreSettingWithStoreStaticRepository _settingWithStoreStaticRepository;

    public StoreSettingWithStoreStaticController(IStoreSettingWithStoreStaticRepository settingWithStoreStaticRepository)
    {
        _settingWithStoreStaticRepository = settingWithStoreStaticRepository;
    }

    public async Task<IActionResult> Index()
    {
        var storeSettings = await _settingWithStoreStaticRepository.GetAllAsync();
        return View(storeSettings);
    }

    public async Task<IActionResult> GetByID()
    {
        var storeSetting = await _settingWithStoreStaticRepository.GetByIdAsync();
        if (storeSetting == null)
            return NotFound("Belirtilen ID'ye ait maðaza ayarý bulunamadý.");
        return View(storeSetting);
    }

    public async Task<IActionResult> AddStoreSetting()
    {
        await _settingWithStoreStaticRepository.AddAsync();
        TempData["Message"] = "Maðaza ayarý baþarýyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateStoreSetting()
    {
        await _settingWithStoreStaticRepository.UpdateAsync();
        TempData["Message"] = "Maðaza ayarý baþarýyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteStoreSetting()
    {
        await _settingWithStoreStaticRepository.DeleteAsync();
        TempData["Message"] = "Maðaza ayarý baþarýyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}

Açýklama:

Controller sýnýfý MVC yapýsýnýn Controller katmanýný temsil eder.
Repository’den gelen verileri iþler ve View'e aktarýr.
TempData["Message"]: View'da geçici mesaj göstermek için kullanýlýr.
Her iþlemden sonra RedirectToAction ile Index sayfasýna yönlendirme yapýlýr.
Bu yapý sade, anlaþýlýr ve öðreticidir.

4. Durum: Razor View (Listeleme Ekraný)

@model List<EntityLayer.Concrete.StoreSetting>
@{
    ViewData["Title"] = "Maðaza Ayarlar Listesi";
}
<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/StoreSettingWithStoreStatic/AddStoreSetting">Ekle</a>
    <a class="btn btn-warning" href="/StoreSettingWithStoreStatic/UpdateStoreSetting">Güncelle</a>
    <a class="btn btn-danger" href="/StoreSettingWithStoreStatic/DeleteStoreSetting">Sil</a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Para Birimi</th>
            <th>Dil</th>
            <th>Bildirimleri Etkinleþtir</th>
            <th>Maðaza Adý</th>
            <th>Maðaza Sahibinin Adý</th>
            <th>Email</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var storeSetting in Model)
        {
            <tr>
                <td>@storeSetting.StoreSettingID</td>
                <td>@storeSetting.Currency</td>
                <td>@storeSetting.Language</td>
                <td>@storeSetting.EnableNotifications</td>
                <td>@storeSetting.Store?.Name</td>
                <td>@storeSetting.Store?.OwnerName</td>
                <td>@storeSetting.Store?.Email</td>
            </tr>
        }
    </tbody>
</table>

Açýklama:

Bu Razor View dosyasý StoreSetting listesi görüntüler.
@model List<StoreSetting>: View, Controller'dan liste þeklinde veri bekler.
TempData["Message"]: Ekleme, silme, güncelleme iþlemlerinden sonra kullanýcýya mesaj verir.
@storeSetting.Store?.Name gibi alanlar sayesinde iliþkili Store bilgisini de gösterebiliriz.
Bootstrap class’larý ile butonlar ve tablo þýk hale getirilmiþ.