-->CONTROLLER-DAL YAPISI İLE STATİC EKLEME İŞLEMİ

1.ADIM 
ILogsStaticRepository Arayüzüne Metot Ekleme

Task AddStaticLogAsync();

2.ADIM
LogStaticRepository Sınıfına Metodu Ekleme

public async Task AddStaticLogAsync()
{
    var log = new Log
    {
        Title = "Static Başlık",
        Description = "Bu sabit bir log girişidir.",
        CreatedDate = DateTime.Now
    };

    await _appDbContext.Logs.AddAsync(log);
    await _appDbContext.SaveChangesAsync();
}

Açıklama:
AddStaticLogAsync() adında bir metot tanımlıyoruz.
İçeride new Log { ... } ile sabit bir log nesnesi oluşturuyoruz.
Bu log verisi kullanıcıdan alınmaz, sabit olarak kod içerisinde tanımlanır.
AddAsync metodu ile bu log, veritabanına eklenmek üzere kuyruklanır.
SaveChangesAsync() ile veritabanına kalıcı olarak kaydedilir.

3.ADIM
MVC Controller Üzerinden Ekleme İşlemi

public class LogController : Controller
{
    private readonly ILogRepository _logRepository;

    public LogController(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<IActionResult> AddStaticLog()
    {
        await _logRepository.AddStaticLogAsync();
        TempData["Message"] = "Static log başarıyla eklendi.";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index()
    {
        var logs = await _logRepository.GetAllLogsAsync();
        return View(logs);
    }
}

Açıklama:
Bu sınıf bir MVC Controller’dır. Razor View ile çalışır.
AddStaticLog() metodu tetiklendiğinde, repository’e gidip sabit log verisini ekler.
TempData["Message"] ile kullanıcıya bilgi mesajı taşır (örneğin “başarıyla eklendi”).
Son olarak RedirectToAction("Index") diyerek log listesini gösteren sayfaya geri döner.
Index() metodu tüm log verilerini alır ve Index.cshtml sayfasına gönderir.


4.ADIM
View (Index.cshtml) İçinde Tabloya Buton Ekle
@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<a href="/LogStatic/AddStaticLog" class="btn btn-primary">Static Log Ekle</a>

<table class="table">
    <thead>
        <tr>
            <th>Başlık</th>
            <th>Açıklama</th>
            <th>Tarih</th>
        </tr>
    </thead>
    <tbody>
    @foreach (var item in Model)
    {
        <tr>
            <td>@item.Title</td>
            <td>@item.Description</td>
            <td>@item.CreatedDate</td>
        </tr>
    }
    </tbody>
</table>

Açıklama:
Sayfa yüklendiğinde, LogController.Index() metodundan gelen logs listesini kullanır.
Eğer TempData["Message"] doluysa, kullanıcıya bilgi mesajı gösterilir.
“Static Log Ekle” butonuna basıldığında /LogStatic/AddStaticLog URL'sine gider.
Bu da LogController.AddStaticLog() metodunu tetikler.
Sonrasında kullanıcı tekrar Index sayfasına yönlendirilir ve eklenen log görünür.


📌 Ekstra Bilgiler
TempData: Controller’dan View’a geçici mesaj taşımak için kullanılır (tek seferlik).
RedirectToAction: Başka bir controller metoduna yönlendirme yapar.
View(Model): Controller’dan View’a veri taşıma yöntemidir (listeler, modeller vs).
@foreach (var item in Model): MVC View sayfasında gelen listeyi döner.
