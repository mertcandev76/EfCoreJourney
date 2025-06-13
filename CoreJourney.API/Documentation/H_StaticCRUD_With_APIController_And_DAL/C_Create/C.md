-->CONTROLLER(API-TEST ETME)-DAL YAPISI İLE STATİC EKLEME İŞLEMİ

🎯 Amaç:
ID’si 4 olan bir Log kaydını veritabanına eklemek.

🔧 Repository Arayüzü:

Task AddStaticLogAsync();

🔍 Açıklama:

Yeni bir kayıt eklemek için bir görev tanımı yapıyoruz. Parametre almıyor çünkü veriyi içeride sabit tanımlıyoruz.

🔨 Repository Uygulaması:

public async Task AddStaticLogAsync()
{
    var newLog = new Log
    {
        Id = 4,
        Message = "Static log eklendi",
        CreatedDate = DateTime.Now
    };

    _appDbContext.Logs.Add(newLog);
    await _appDbContext.SaveChangesAsync();
}
🔍 Açıklama:

new Log { ... } → Yeni bir nesne oluşturduk.
Id = 4 → Sabit ID verdik. Bu ID veritabanında varsa hata verir.
Add(newLog) → EF Core ile tabloya ekleme işlemi.
SaveChangesAsync() → Değişiklikleri veritabanına kalıcı olarak yazar.

🎯 Controller Endpoint:

[HttpPost("add-static-log")]
public async Task<IActionResult> AddStaticLog()
{
    try
    {
        await _logStaticRepository.AddStaticLogAsync();
        return Ok("Static log başarıyla eklendi.");
    }
    catch (Exception ex)
    {
        return BadRequest($"Bir hata oluştu: {ex.Message}");
    }
}

🔍 Açıklama:

HttpPost → Veri eklemek için HTTP POST kullanılır.
try-catch → Hata olursa kullanıcıya mesaj verilir.
IActionResult → İşlem başarılı mı, hata mı dönülecek onu belirtiriz.