-->Generic Repository Pattern + Generic Service (Manager) Pattern ile Katmanl� Mimari

1. Repository Pattern (Generic Repository)
Ama�: Veritaban� eri�im kodunu bir katmanda toplamak, her entity i�in CRUD i�lemlerini tekrar yazmamak.

// IRepository<T> � veri eri�im i�in soyut aray�z (interface)
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

// GenericRepository<T> � IRepository'nin somut uygulamas� (Entity Framework Core kullan�yor)
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

Ne i�e yar�yor?
Herhangi bir entity tipi i�in bu s�n�f CRUD i�lemlerini haz�r olarak sunuyor. �rne�in Product entity'si i�in ayr�ca repository yazmana gerek yok. GenericRepository<Product> kullanabilirsin.

2. Service Layer (Generic Service)
Ama�: �� kurallar�n� (business logic) burada yapaca��z, repository ile UI aras�ndaki katman. Ayr�ca test ve bak�m kolayl��� sa�lar.

// IGenericService<T> � servis katman� i�in interface
public interface IGenericService<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

// GenericManager<T> � servis katman�n�n somut uygulamas�
public class GenericManager<T> : IGenericService<T> where T : class
{
    private readonly IRepository<T> _repository;

    public GenericManager(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<List<T>> GetAllAsync()
    {
        // �� kurallar� varsa buraya eklenir, �imdilik direkt repository �a�r�s�
        return await _repository.GetAllAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        // �rne�in, eklemeden �nce validasyon veya log eklenebilir
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

3. Controller Katman� � Generic Service'i Kullanmak

LogController �zerinden �rnek verelim.

Genel

IActionResult ASP.NET Core MVC�de controller metotlar�n�n d�n�� tipi.
HTTP isteklerine kar�� �e�itli cevap t�rlerini temsil eder.

�rne�in: bir View d�nd�rebilir, ba�ka bir sayfaya y�nlendirebilir (Redirect), hata (NotFound) d�nd�rebilir

Metot Metot �nceleme

1. Index (GET) � Listeleme

public async Task<IActionResult> Index()
{
    var logs = await _logService.GetAllAsync();
    return View(logs);
}

G�rev: Veritaban�ndaki t�m Log kay�tlar�n� getirip listeleme sayfas�na g�nderir.
IActionResult olarak View d�nd�r�r.
View(logs) => logs listesi View�e model olarak gider, HTML sayfas� olarak d�ner.

2. Create (GET) � Yeni kay�t ekleme formu g�sterme

[HttpGet]
public IActionResult Create()
{
    return View();
}

G�rev: Bo� bir form g�r�n�m� (View()) d�nd�r�r.
Kullan�c� buradan yeni bir Log giri�i yapacak.
IActionResult olarak sadece View d�nd�r�r, model g�ndermez (bo� form).

3. Create (POST) � Yeni kay�t ekleme i�lemi

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

G�rev: Formdan gelen log nesnesini al�r, model do�rulamas� ba�ar�l�ysa veritaban�na ekler.

E�er ekleme ba�ar�l�ysa:
RedirectToAction(nameof(Index)) ile Index action��na y�nlendirir (listeleme sayfas�na).

E�er model do�rulama ba�ar�s�zsa:
Ayn� formu (View(log)) kullan�c�ya, doldurdu�u bilgilerle tekrar g�sterir (hatalar� g�stermek i�in).
IActionResult burada hem RedirectResult (y�nlendirme) hem de ViewResult (form sayfas�) d�nd�rebilir.

4. Edit (GET) � Var olan kay�t i�in g�ncelleme formu g�sterme

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

G�rev: G�ncellenecek Log kayd�n� ID ile getirip kullan�c�ya g�sterir.
E�er id parametresi gelmemi�se (null ise):
RedirectToAction("Edit", new { id = 1 }) ile id=1 olarak Edit sayfas�na y�nlendirir.
E�er id var ama b�yle bir kay�t yoksa:
NotFound() d�ner, yani HTTP 404 sayfas�.

E�er kay�t varsa:
View(logs) ile formu mevcut kay�t bilgisiyle doldurup g�sterir.

IActionResult burada:
RedirectToActionResult, NotFoundResult, veya ViewResult d�nebilir.

5. Edit (POST) � G�ncelleme i�lemi

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

G�rev: Kullan�c�n�n formda g�nderdi�i log nesnesini al�r ve g�nceller.
Model do�rulamas� ba�ar�l� ise:
UpdateAsync ile g�nceller ve listeleme sayfas�na y�nlendirir.

Ba�ar�s�z ise:
G�ncelleme formunu ayn� veri ile tekrar g�sterir.

IActionResult:
RedirectToActionResult veya ViewResult d�ner.

6. Delete (GET) � Silme onay� sayfas� g�sterme

[HttpGet]
public async Task<IActionResult> Delete(int id)
{
    var log = await _logService.GetByIdAsync(id);
    if (log == null) return NotFound();
    return View(log);
}

G�rev: Silinecek kay�t varsa onu getirir, kullan�c�ya onay sayfas� g�sterir.
Kay�t yoksa 404 Not Found d�ner.
Varsa ilgili kayd� modeliyle View(log) ile silme onay ekran� a��l�r.

IActionResult: NotFoundResult veya ViewResult.

7. Delete (POST) � Silme i�lemini ger�ekle�tirme

[HttpPost]
public async Task<IActionResult> Delete(Log log)
{
    await _logService.DeleteAsync(log);
    return RedirectToAction("Index");
}

G�rev: Silme onay formundan gelen log nesnesi ile veritaban�ndan siler.
��lem sonras� listeleme sayfas�na (Index) y�nlendirir.
D�n�� tipi: RedirectToActionResult (listeleme sayfas�na y�nlendirme).

8. Details (GET) � Kay�t detaylar�n� g�sterme

public async Task<IActionResult> Details(int id)
{
    var log = await _logService.GetByIdAsync(id);
    if (log == null) return NotFound();
    return View(log);
}

G�rev: Belirli bir log kayd�n� getirip detay sayfas�nda g�sterir.
Kay�t yoksa 404 d�ner.
Kay�t varsa model ile birlikte detay view d�ner.
IActionResult: NotFoundResult veya ViewResult.

�zet Tablo

| Action  | HTTP Method | Girdi (Parametre) | Yapt��� ��lem                            | D�nen IActionResult T�rleri                          |
| ------- | ----------- | ----------------- | ---------------------------------------- | ---------------------------------------------------- |
| Index   | GET         | Yok               | T�m kay�tlar� listeler                   | ViewResult                                           |
| Create  | GET         | Yok               | Bo� kay�t ekleme formu g�sterir          | ViewResult                                           |
| Create  | POST        | Log nesnesi       | Yeni kay�t ekler                         | RedirectToActionResult / ViewResult                  |
| Edit    | GET         | int? id           | Kay�t getirir, g�ncelleme formu g�sterir | RedirectToActionResult / NotFoundResult / ViewResult |
| Edit    | POST        | Log nesnesi       | Kay�t g�nceller                          | RedirectToActionResult / ViewResult                  |
| Delete  | GET         | int id            | Silme onay� i�in kay�t g�sterir          | NotFoundResult / ViewResult                          |
| Delete  | POST        | Log nesnesi       | Kayd� siler                              | RedirectToActionResult                               |
| Details | GET         | int id            | Kay�t detaylar�n� g�sterir               | NotFoundResult / ViewResult                          |
View �rnekelerin� incelersin.

->ProductBrand
->ProductVendor
->ProductCategory
->Customer
->Coupon
->Store

Bu mant�kta yap�l�yor 
Di�erler S�n�flar �zel s�n�fta ili�kisel ba�lant� metotlar� ile controller taraf�ndan i�leniyor

