-->CONTROLLER-DAL YAPISI İLE LİSTELEME


Abstract (Arayüz - Interface)

📌 Amaç:
Veriye erişim için hangi işlemler yapılacağını tanımlar, ama nasıl yapılacağını söylemez.

✅ Örnek: ILogRepository
namespace DataAccessLayer.Abstract 
{
    public interface ILogRepository
    {
        Task<List<Log>> GetAllLogsAsync();
    }
}

🧠 Açıklama:

Bu arayüz sayesinde Log verisini çekmek isteyen sınıflar, bu metodu kullanmak zorundadır.
Ama veriyi nasıl çekeceğini, yani SQL sorgusunu bu katmanda yazmıyoruz.

Repositories (Somut Sınıf)

📌 Amaç:
Veritabanıyla doğrudan konuşan katmandır. DbContext üzerinden sorgular burada yazılır.

✅ Örnek: LogRepository
namespace DataAccessLayer.Repositories 
{
    public class LogRepository : ILogRepository
    {
        private readonly AppDbContext _appDbContext;

        public LogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Log>> GetAllLogsAsync()
        {
            return await _appDbContext.Logs.ToListAsync();
        }
    }
}
🧠 Açıklama:

AppDbContext, EF Core'un veritabanı bağlantısı kurduğu sınıftır.
Logs tablosundan tüm verileri ToListAsync() ile çeker.
Bu sınıf artık ILogRepository arayüzündeki metodu gerçek hale getirir (implements eder).




MVC Controller (Kullanıcı arayüzü ile çalışan Controller)

📌 Amaç:
Veriyi çekip View’a (HTML sayfasına) gönderir. Kullanıcıların görebileceği arayüz buradan yönetilir.

namespace EfCoreJourney.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogRepository _logRepository;

        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _logRepository.GetAllLogsAsync();
            return View(logs);
        }
    }
}
🧠 Açıklama:

Controller sınıfı View ile birlikte çalışır.
Index() metodu Log listesini çeker ve View’a gönderir.
View(logs): logs listesini .cshtml dosyasına yollar.

 View (Görünüm – Razor Sayfası)

 📌 Amaç:
Kullanıcının göreceği HTML sayfasıdır. Razor ile C# kodları HTML ile birleşir.

@model List<EntityLayer.Concrete.Log>

@{
    ViewData["Title"] = "Log Listesi";
}

<h2>Log Listesi</h2>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Tarih</th>
            <th>Seviye</th>
            <th>Mesaj</th>
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
            </tr>
        }
    </tbody>
</table>

🧠 Açıklama:
@model ile C# veri modeli belirtilir.
@foreach ile her log için tabloya satır eklenir.
Bu sayfa Views/Log/Index.cshtml gibi bir yerde olmalıdır.

🔗 Akış Şeması
Tarayıcı Log/Index sayfasına istek atar.
LogController → ILogRepository.GetAllLogsAsync() metodunu çağırır.
LogRepository, DbContext ile veritabanından verileri çeker.
Geri dönen logs verisi View(logs) ile .cshtml sayfasına gönderilir.
View dosyası bu veriyi tablo halinde gösterir.