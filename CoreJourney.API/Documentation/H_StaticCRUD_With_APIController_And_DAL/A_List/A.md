-->CONTROLLER(API-TEST ETME)-DAL YAPISI İLE LİSTELEME 

🔧 Repository Arayüzü:

🎯 Amaç:

Veritabanındaki tüm Log kayıtlarını çekip listelemek.
Task<List<Log>> GetAllStaticLogsAsync();

🔍 Açıklama:

Bu satır, interface'te bir görev tanımıdır. Bize, tüm Log kayıtlarını asenkron şekilde getirecek bir metot sunar.
Task<List<Log>> → Asenkron olarak Log listesini döndürür.

🔨 Repository Uygulaması:

public async Task<List<Log>> GetAllStaticLogsAsync()
{
    return await _appDbContext.Logs.ToListAsync();
}

🔍 Açıklama:

Burada Entity Framework kullanarak Logs tablosundaki tüm kayıtları alıyoruz.
_appDbContext.Logs → Logs tablosuna erişir.
.ToListAsync() → Tüm kayıtları liste halinde getirir, asenkron çalışır.
await → Asenkron işlemin tamamlanmasını bekler.

🎯 Controller Endpoint:

[HttpGet]
public async Task<ActionResult<List<Log>>> GetAllStaticLogs()
{
    var logs = await _logStaticRepository.GetAllStaticLogsAsync();
    return Ok(logs);
}

🔍 Açıklama:

[HttpGet] → Bu endpoint'e GET isteği gönderileceğini belirtir.
ActionResult<List<Log>> → HTTP yanıtında Log listesini döndürürüz.
await _logStaticRepository... → Repository katmanını kullanarak veri çekeriz (katmanlı mimari mantığı!).

