-->CONTROLLER-DAL YAPISI İLE STATİC GÜNCELLEME İŞLEMİ

1.ADIM

ILogsStaticRepository Arayüzüne Metot Güncelleme

Task UpdateStaticLogAsync();
2.ADIM 
LogStaticRepository Sınıfına Metodu Güncelleme

public async Task UpdateStaticLogAsync()
{
    int staticID = 4; // Güncellenecek sabit ID
    var log = await _appDbContext.Logs.FindAsync(staticID);
    if (log != null)
    {
        log.LogDate = DateTime.Now;
        log.LogLevel = "WARNING";
        log.Message = "Log güncellendi.";
        log.Details = "Statik ID ile güncelleme yapıldı.";

        await _appDbContext.SaveChangesAsync();
    }
}
Bu metot, veritabanındaki ID’si 4 olan log kaydını günceller.

3.ADIM
MVC Controller Üzerinden Güncelleme İşlemi

LogController içerisinde bu metodu çağıran bir Action tanımlanır:

public class LogController : Controller
{
    private readonly ILogRepository _logRepository;

    public LogController(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }


    public async Task<IActionResult> UpdateStaticLog()
    {
        await _logRepository.UpdateStaticLogAsync();
        TempData["Message"] = "Statik log başarıyla güncellendi.";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index()
    {
        var logs = await _logRepository.GetAllLogsAsync();
        return View(logs);
    }
}

4.ADIM
View (Index.cshtml) İçinde Tabloya Buton Ekle

@model List<EntityLayer.Concrete.Log>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<h2>Log Listesi</h2>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Tarih</th>
            <th>Seviye</th>
            <th>Mesaj</th>
            <th>İşlemler</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var log in Model)
        {
            <tr>
                <td>@log.LogID</td>
                <td>@log.LogDate</td>
                <td>@log.LogLevel</td>
                <td>@log.Message</td>
                <td>
                    <a href="/Log/UpdateStaticLog" class="btn btn-sm btn-warning">
                        Statik Güncelle
                    </a>
                </td>
            </tr>
        }
    </tbody>
</table>

Her satırdaki buton aynı action'a (/Log/UpdateStaticLog) yönlendiği için hep sabit ID (4) güncellenir.

🔁 Alternatif: Sadece ID’si 4 olan satıra buton eklemek
@if (log.LogID == 4)
{
    <a href="/Log/UpdateStaticLog" class="btn btn-sm btn-warning">
        Statik Güncelle
    </a>
}

📌 Özet

| Adım | Açıklama                                                                                 |
| ---- | ---------------------------------------------------------------------------------------- |
| 1️⃣  | Arayüz (`ILogRepository`) içinde static güncelleme metodu tanımlandı                     |
| 2️⃣  | `LogRepository` içinde sabit ID ile güncelleme işlemi yazıldı                            |
| 3️⃣  | `LogController` içinde metodu çağıran Action oluşturuldu                                 |
| 4️⃣  | View içinde tabloya buton eklendi; her satırdan static güncelleme yapılabilir hale geldi |
