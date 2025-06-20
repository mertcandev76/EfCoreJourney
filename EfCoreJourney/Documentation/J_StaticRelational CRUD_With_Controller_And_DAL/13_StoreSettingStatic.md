-->StoreSetting

1. Durum: StoreSetting Entity�si (Model Katman�)

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

A��klama:

Bu s�n�f StoreSetting ad�nda bir tabloyu temsil eder.
StoreSettingID: Birincil anahtar.
Currency, Language, EnableNotifications: Ma�azaya �zel ayarlar.
StoreId: �li�kili Store tablosunun Foreign Key'i.
Store: Navigation Property, bir StoreSetting sadece bir Store ile ili�kilidir. (One-to-One ya da One-to-Many senaryosu olabilir ama burada birebir varsay�lm��).

2. Durum: StoreSettingWithStoreStaticRepository (Repository Katman�)

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
            StoreId = 2 // mevcut bir Store ile e�le�tirildi
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
                storeSetting.Store.OwnerName = "Mertcan Ads�z";
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

A��klama:

Bu Repository, veri eri�imini soyutlar ve StoreSetting �zerinde CRUD i�lemlerini tan�mlar.
Her metod statik ID=2 �zerinden i�lem yapar (bu sabit ID ��renme ama�l�d�r, dinamik yap�larda kullan�c�dan al�nmal�d�r).
Include(ss => ss.Store): StoreSetting ile ili�kili Store verisi de �ekilir. Bu bir Eager Loading �rne�idir.

3. Durum: StoreSettingWithStoreStaticController (Controller Katman�)

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
            return NotFound("Belirtilen ID'ye ait ma�aza ayar� bulunamad�.");
        return View(storeSetting);
    }

    public async Task<IActionResult> AddStoreSetting()
    {
        await _settingWithStoreStaticRepository.AddAsync();
        TempData["Message"] = "Ma�aza ayar� ba�ar�yla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateStoreSetting()
    {
        await _settingWithStoreStaticRepository.UpdateAsync();
        TempData["Message"] = "Ma�aza ayar� ba�ar�yla g�ncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteStoreSetting()
    {
        await _settingWithStoreStaticRepository.DeleteAsync();
        TempData["Message"] = "Ma�aza ayar� ba�ar�yla silindi.";
        return RedirectToAction(nameof(Index));
    }
}

A��klama:

Controller s�n�f� MVC yap�s�n�n Controller katman�n� temsil eder.
Repository�den gelen verileri i�ler ve View'e aktar�r.
TempData["Message"]: View'da ge�ici mesaj g�stermek i�in kullan�l�r.
Her i�lemden sonra RedirectToAction ile Index sayfas�na y�nlendirme yap�l�r.
Bu yap� sade, anla��l�r ve ��reticidir.

4. Durum: Razor View (Listeleme Ekran�)

@model List<EntityLayer.Concrete.StoreSetting>
@{
    ViewData["Title"] = "Ma�aza Ayarlar Listesi";
}
<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/StoreSettingWithStoreStatic/AddStoreSetting">Ekle</a>
    <a class="btn btn-warning" href="/StoreSettingWithStoreStatic/UpdateStoreSetting">G�ncelle</a>
    <a class="btn btn-danger" href="/StoreSettingWithStoreStatic/DeleteStoreSetting">Sil</a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Para Birimi</th>
            <th>Dil</th>
            <th>Bildirimleri Etkinle�tir</th>
            <th>Ma�aza Ad�</th>
            <th>Ma�aza Sahibinin Ad�</th>
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

A��klama:

Bu Razor View dosyas� StoreSetting listesi g�r�nt�ler.
@model List<StoreSetting>: View, Controller'dan liste �eklinde veri bekler.
TempData["Message"]: Ekleme, silme, g�ncelleme i�lemlerinden sonra kullan�c�ya mesaj verir.
@storeSetting.Store?.Name gibi alanlar sayesinde ili�kili Store bilgisini de g�sterebiliriz.
Bootstrap class�lar� ile butonlar ve tablo ��k hale getirilmi�.