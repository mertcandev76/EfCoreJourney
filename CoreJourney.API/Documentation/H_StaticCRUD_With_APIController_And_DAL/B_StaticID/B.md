-->CONTROLLER(API-TEST ETME)-DAL YAPISI İLE STATİC ID GETİRME

🎯 Amaç:
Sabit bir ID (örnek: 4) ile Log kaydını çekmek.

🔧 Repository Arayüzü:

Task<Log?> GetStaticLogByIdAsync();

🔍 Açıklama:

Log? → Dönüş tipi nullable çünkü o ID'de veri olmayabilir.

🔨 Repository Uygulaması:

public async Task<Log?> GetStaticLogByIdAsync()
{
    int staticID = 4;
    return await _appDbContext.Logs.FindAsync(staticID);
}
🔍 Açıklama:

FindAsync(ID) → Primary key'e göre arama yapar, çok hızlıdır.
staticID = 4 → Burada sabit ID kullandık, dinamik değil.

🎯 Controller Endpoint:

[HttpGet("static-log")]
public async Task<ActionResult<Log>> GetStaticLogById()
{
    var log = await _logStaticRepository.GetStaticLogByIdAsync();
    if (log == null)
        return NotFound("Static ID ile kayıt bulunamadı.");
    return Ok(log);
}
🔍 Açıklama:

NotFound() → Eğer log null ise, kullanıcıya 404 hatası döner.
Ok(log) → Kayıt varsa HTTP 200 ile birlikte kayıt gönderilir.