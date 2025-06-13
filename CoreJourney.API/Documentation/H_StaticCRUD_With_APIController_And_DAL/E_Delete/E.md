-->CONTROLLER(API-TEST ETME)-DAL YAPISI İLE STATİC SİLME İŞLEMİ

🎯 Amaç:
Veritabanındaki Id = 4 olan Log kaydını silmek.

🔧 Repository Arayüzü:

Task DeleteStaticLogAsync();

🔨 Repository Uygulaması:

public async Task DeleteStaticLogAsync()
{
    int staticID = 4;
    var logToDelete = await _appDbContext.Logs.FindAsync(staticID);

    if (logToDelete != null)
    {
        _appDbContext.Logs.Remove(logToDelete);
        await _appDbContext.SaveChangesAsync();
    }
}
🔍 Açıklama:

Remove() → EF Core nesneyi silmeye hazırlar.
SaveChangesAsync() → Silme işlemini veritabanına uygular.

🎯 Controller Endpoint:

[HttpDelete("delete-static-log")]
public async Task<IActionResult> DeleteStaticLog()
{
    try
    {
        await _logStaticRepository.DeleteStaticLogAsync();
        return Ok("Static log başarıyla silindi.");
    }
    catch (Exception ex)
    {
        return BadRequest($"Bir hata oluştu: {ex.Message}");
    }
}

🔍 Açıklama:

DELETE işlemi için HttpDelete kullanılır. Başarılıysa 200 OK, hata varsa 400 BadRequest dönülür.