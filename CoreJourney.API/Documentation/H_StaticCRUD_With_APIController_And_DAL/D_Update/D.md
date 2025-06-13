-->CONTROLLER(API-TEST ETME)-DAL YAPISI İLE STATİC GÜNCELLEME İŞLEMİ

🎯 Amaç:

Id = 4 olan Log kaydının mesaj veya tarih gibi bilgilerini değiştirmek.

🔧 Repository Arayüzü:

Task UpdateStaticLogAsync();

🔨 Repository Uygulaması:

public async Task UpdateStaticLogAsync()
{
    int staticID = 4;
    var existingLog = await _appDbContext.Logs.FindAsync(staticID);

    if (existingLog != null)
    {
        existingLog.Message = "Static log güncellendi";
        existingLog.CreatedDate = DateTime.Now;

        _appDbContext.Logs.Update(existingLog);
        await _appDbContext.SaveChangesAsync();
    }
}
🔍 Açıklama:

FindAsync ile veriyi bulduk.
Veriyi güncelledik.
Update → EF Core’a "bu nesne güncellendi" dedik.
SaveChangesAsync → Veritabanına yaz.

🎯 Controller Endpoint:

[HttpPut("update-static-log")]
public async Task<IActionResult> UpdateStaticLog()
{
    try
    {
        await _logStaticRepository.UpdateStaticLogAsync();
        return Ok("Static log başarıyla güncellendi.");
    }
    catch (Exception ex)
    {
        return BadRequest($"Bir hata oluştu: {ex.Message}");
    }
}
🔍 Açıklama:
PUT metodu, güncelleme işlemlerinde kullanılır. try-catch bloğu ile hata yönetimi yapılır.