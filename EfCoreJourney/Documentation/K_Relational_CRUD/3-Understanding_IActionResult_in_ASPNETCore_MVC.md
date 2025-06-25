-->Generic Repository Pattern + Generic Service (Manager) Pattern ile Katmanlý Mimari

1. Repository Pattern (Generic Repository)
Amaç: Veritabaný eriþim kodunu bir katmanda toplamak, her entity için CRUD iþlemlerini tekrar yazmamak.

// IRepository<T> — veri eriþim için soyut arayüz (interface)
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

// GenericRepository<T> — IRepository'nin somut uygulamasý (Entity Framework Core kullanýyor)
public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<List<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Table.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Table.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

Ne iþe yarýyor?
Herhangi bir entity tipi için bu sýnýf CRUD iþlemlerini hazýr olarak sunuyor. Örneðin Product entity'si için ayrýca repository yazmana gerek yok. GenericRepository<Product> kullanabilirsin.

2. Service Layer (Generic Service)
Amaç: Ýþ kurallarýný (business logic) burada yapacaðýz, repository ile UI arasýndaki katman. Ayrýca test ve bakým kolaylýðý saðlar.

// IGenericService<T> — servis katmaný için interface
public interface IGenericService<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

// GenericManager<T> — servis katmanýnýn somut uygulamasý
public class GenericManager<T> : IGenericService<T> where T : class
{
    private readonly IRepository<T> _repository;

    public GenericManager(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<List<T>> GetAllAsync()
    {
        // Ýþ kurallarý varsa buraya eklenir, þimdilik direkt repository çaðrýsý
        return await _repository.GetAllAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        // Örneðin, eklemeden önce validasyon veya log eklenebilir
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        await _repository.DeleteAsync(entity);
    }
}

3. Controller Katmaný — Generic Service'i Kullanmak

LogController üzerinden örnek verelim.

Genel

IActionResult ASP.NET Core MVC’de controller metotlarýnýn dönüþ tipi.
HTTP isteklerine karþý çeþitli cevap türlerini temsil eder.

Örneðin: bir View döndürebilir, baþka bir sayfaya yönlendirebilir (Redirect), hata (NotFound) döndürebilir

Metot Metot Ýnceleme

1. Index (GET) — Listeleme

public async Task<IActionResult> Index()
{
    var logs = await _logService.GetAllAsync();
    return View(logs);
}

Görev: Veritabanýndaki tüm Log kayýtlarýný getirip listeleme sayfasýna gönderir.
IActionResult olarak View döndürür.
View(logs) => logs listesi View’e model olarak gider, HTML sayfasý olarak döner.

2. Create (GET) — Yeni kayýt ekleme formu gösterme

[HttpGet]
public IActionResult Create()
{
    return View();
}

Görev: Boþ bir form görünümü (View()) döndürür.
Kullanýcý buradan yeni bir Log giriþi yapacak.
IActionResult olarak sadece View döndürür, model göndermez (boþ form).

3. Create (POST) — Yeni kayýt ekleme iþlemi

[HttpPost]
public async Task<IActionResult> Create(Log log)
{
    if (ModelState.IsValid)
    {
        await _logService.AddAsync(log);
        return RedirectToAction(nameof(Index));
    }
    return View(log);
}

Görev: Formdan gelen log nesnesini alýr, model doðrulamasý baþarýlýysa veritabanýna ekler.

Eðer ekleme baþarýlýysa:
RedirectToAction(nameof(Index)) ile Index action’ýna yönlendirir (listeleme sayfasýna).

Eðer model doðrulama baþarýsýzsa:
Ayný formu (View(log)) kullanýcýya, doldurduðu bilgilerle tekrar gösterir (hatalarý göstermek için).
IActionResult burada hem RedirectResult (yönlendirme) hem de ViewResult (form sayfasý) döndürebilir.

4. Edit (GET) — Var olan kayýt için güncelleme formu gösterme

[HttpGet]
public async Task<IActionResult> Edit(int? id)
{
    if (!id.HasValue)
    {
        return RedirectToAction("Edit", new { id = 1 });
    }

    var logs = await _logService.GetByIdAsync(id.Value);
    if (logs == null)
        return NotFound();

    return View(logs);
}

Görev: Güncellenecek Log kaydýný ID ile getirip kullanýcýya gösterir.
Eðer id parametresi gelmemiþse (null ise):
RedirectToAction("Edit", new { id = 1 }) ile id=1 olarak Edit sayfasýna yönlendirir.
Eðer id var ama böyle bir kayýt yoksa:
NotFound() döner, yani HTTP 404 sayfasý.

Eðer kayýt varsa:
View(logs) ile formu mevcut kayýt bilgisiyle doldurup gösterir.

IActionResult burada:
RedirectToActionResult, NotFoundResult, veya ViewResult dönebilir.

5. Edit (POST) — Güncelleme iþlemi

[HttpPost]
public async Task<IActionResult> Edit(Log log)
{
    if (ModelState.IsValid)
    {
        await _logService.UpdateAsync(log);
        return RedirectToAction(nameof(Index));
    }
    return View(log);
}

Görev: Kullanýcýnýn formda gönderdiði log nesnesini alýr ve günceller.
Model doðrulamasý baþarýlý ise:
UpdateAsync ile günceller ve listeleme sayfasýna yönlendirir.

Baþarýsýz ise:
Güncelleme formunu ayný veri ile tekrar gösterir.

IActionResult:
RedirectToActionResult veya ViewResult döner.

6. Delete (GET) — Silme onayý sayfasý gösterme

[HttpGet]
public async Task<IActionResult> Delete(int id)
{
    var log = await _logService.GetByIdAsync(id);
    if (log == null) return NotFound();
    return View(log);
}

Görev: Silinecek kayýt varsa onu getirir, kullanýcýya onay sayfasý gösterir.
Kayýt yoksa 404 Not Found döner.
Varsa ilgili kaydý modeliyle View(log) ile silme onay ekraný açýlýr.

IActionResult: NotFoundResult veya ViewResult.

7. Delete (POST) — Silme iþlemini gerçekleþtirme

[HttpPost]
public async Task<IActionResult> Delete(Log log)
{
    await _logService.DeleteAsync(log);
    return RedirectToAction("Index");
}

Görev: Silme onay formundan gelen log nesnesi ile veritabanýndan siler.
Ýþlem sonrasý listeleme sayfasýna (Index) yönlendirir.
Dönüþ tipi: RedirectToActionResult (listeleme sayfasýna yönlendirme).

8. Details (GET) — Kayýt detaylarýný gösterme

public async Task<IActionResult> Details(int id)
{
    var log = await _logService.GetByIdAsync(id);
    if (log == null) return NotFound();
    return View(log);
}

Görev: Belirli bir log kaydýný getirip detay sayfasýnda gösterir.
Kayýt yoksa 404 döner.
Kayýt varsa model ile birlikte detay view döner.
IActionResult: NotFoundResult veya ViewResult.

Özet Tablo

| Action  | HTTP Method | Girdi (Parametre) | Yaptýðý Ýþlem                            | Dönen IActionResult Türleri                          |
| ------- | ----------- | ----------------- | ---------------------------------------- | ---------------------------------------------------- |
| Index   | GET         | Yok               | Tüm kayýtlarý listeler                   | ViewResult                                           |
| Create  | GET         | Yok               | Boþ kayýt ekleme formu gösterir          | ViewResult                                           |
| Create  | POST        | Log nesnesi       | Yeni kayýt ekler                         | RedirectToActionResult / ViewResult                  |
| Edit    | GET         | int? id           | Kayýt getirir, güncelleme formu gösterir | RedirectToActionResult / NotFoundResult / ViewResult |
| Edit    | POST        | Log nesnesi       | Kayýt günceller                          | RedirectToActionResult / ViewResult                  |
| Delete  | GET         | int id            | Silme onayý için kayýt gösterir          | NotFoundResult / ViewResult                          |
| Delete  | POST        | Log nesnesi       | Kaydý siler                              | RedirectToActionResult                               |
| Details | GET         | int id            | Kayýt detaylarýný gösterir               | NotFoundResult / ViewResult                          |
View ÖrnekelerinÝ incelersin.

->ProductBrand
->ProductVendor
->ProductCategory
->Customer
->Coupon
->Store

Bu mantýkta yapýlýyor 
Diðerler Sýnýflar özel sýnýfta iliþkisel baðlantý metotlarý ile controller tarafýndan iþleniyor

