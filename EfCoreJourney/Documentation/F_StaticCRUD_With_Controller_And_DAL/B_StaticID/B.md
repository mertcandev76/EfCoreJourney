-->CONTROLLER-DAL YAPISI İLE STATİC ID GETİRME

Statik ID getirme, veritabanındaki belirli bir kaydın (örneğin ID'si 4 olan bir satır) kod içerisinde sabit bir şekilde çağrılmasıdır.
Yani ID değeri kullanıcıdan gelmez, veritabanından dinamik olarak seçilmez; doğrudan kod içinde yazılır.

✅ Kullanım Amaçları
Belirli bir sistem mesajını göstermek (örn. ID = 1 olan sistem hatası logu)
Hakkımızda, Gizlilik Politikası gibi sabit içerikleri çekmek
Yönetici profili gibi sabit kullanıcı bilgilerini almak

1.ADIM 
Arayüz (Interface) Oluşturma

namespace DataAccessLayer.Abstract
{
    public interface ILogStaticRepository
    {
        Task<Log?> GetStaticLogByIdAsync(); // Static id
    }
}

🔍 Açıklama:
Bu, DataAccessLayer (veri erişim katmanı) içinde bir interface’tir.
Amaç: Log tablosundan sabit bir ID ile veri getirme fonksiyonunun imzasını tanımlamaktır.
ILogStaticRepository, bir sözleşmedir: "Bu interface'i kullanan sınıf, bu fonksiyonu yazmalıdır."

2.ADIM
Arayüzü (Interface’i) Uygulama – Repository Sınıfı

namespace DataAccessLayer.Repositories
{
    public class LogStaticRepository : ILogStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public LogStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Log?> GetStaticLogByIdAsync()
        {
            int staticID = 4;
            return await _appDbContext.Logs.FindAsync(staticID);
        }
    }
}

🔍 Açıklama:
Bu sınıf, yukarıdaki ILogStaticRepository interface’ini gerçekleştiriyor.
AppDbContext üzerinden veritabanına erişiyor.
FindAsync(staticID) ile Log tablosundaki ID’si 4 olan kaydı getiriyor.
Yani, veritabanında sabit olarak ID’si 4 olan bir log kaydını çağıran metot.


3. Adım
Controller Katmanı

namespace EfCoreJourney.Controllers
{
    public class LogStaticController : Controller
    {
        private readonly ILogStaticRepository _logStaticRepository;

        public LogStaticController(ILogStaticRepository logStaticRepository)
        {
            _logStaticRepository = logStaticRepository;
        }

        public async Task<IActionResult> StaticLog()
        {
            var logs = await _logStaticRepository.GetStaticLogByIdAsync();
            if (logs == null)
            {
                return NotFound();
            }
            return View(logs);
        }
    }
}

🔍 Açıklama:
MVC'nin Controller katmanındasın.
ILogStaticRepository DI (Dependency Injection) yoluyla controller’a aktarılmış.

StaticLog() adlı action:
GetStaticLogByIdAsync() metodunu çağırır.

Geriye dönen log kaydını view’a gönderir.
Eğer veri yoksa NotFound() döner.

Bu noktada, kullanıcı bir URL'ye girdiğinde, sistem otomatik olarak ID’si 4 olan log kaydını veritabanından alıp view’a yönlendirir.

4. Adım
View (Razor) Sayfası

@model EntityLayer.Concrete.Log
@{
    ViewData["Title"] = "Static Log Detayı";
}

<h2>Statik Log Detayı</h2>

<div>
    <p><strong>ID:</strong> @Model.LogID</p>
    <p><strong>Mesaj:</strong> @Model.Message</p>
    <p><strong>Tarih:</strong> @Model.LogDate</p>
</div>

🔍 Açıklama:
Bu kısım görsel arayüz yani kullanıcıya gösterilecek sayfadır.
Sayfa, Log modelini alır (@model).
Gelen log nesnesindeki bilgileri (LogID, Message, LogDate) kullanıcıya listeler.
Kullanıcı, bu sayfayı ziyaret ettiğinde ID’si 4 olan log bilgilerini ekranda görür.

🔚 Sonuç: Bu Yapı Ne Yapar?
🔗 Kullanıcı /LogStatic/StaticLog adresine gider.
🔁 Controller, Repository’ye sorar: “ID’si 4 olan log var mı?”
🛢 Repository, veritabanından bu log'u getirir.
👀 View, bu log’un bilgilerini ekrana yazdırır.