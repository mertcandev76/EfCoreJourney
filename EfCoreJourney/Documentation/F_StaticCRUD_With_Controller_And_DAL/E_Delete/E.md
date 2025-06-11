-->CONTROLLER-DAL YAPISI İLE STATİC SİLME İŞLEMİ

1.ADIM
ILogsStaticRepository Arayüzüne Metot Silme

Task DeleteStaticLogAsync();

2.ADIM 
LogStaticRepository Sınıfına Metodu Silme

 public async Task DeleteStaticLogAsync()
    {
        int staticID = 11; // Sabit ID
        var log = await _context.Logs.FindAsync(staticID);
        if (log != null)
        {
            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();
        }
    }

3.ADIM
MVC Controller Üzerinden Silme İşlemi


    public async Task<IActionResult> DeleteStaticLog()
    {
        await _logStaticRepository.DeleteStaticLogAsync();
        TempData["Message"] = "Statik log başarıyla silindi.";
        return RedirectToAction("Index");
    }

4.ADIM
View (Index.cshtml) İçinde Tabloya Buton Ekle(Tablonun Dışında)

<a href="/LogStatic/DeleteStaticLog" class="btn btn-danger" onclick="return confirm('Statik log silinsin mi?');">
    Statik Log Sil
</a>

Bu buton, hep aynı statik ID'yi siler.
Tablo ile ilişkili değildir.


NOT!!
🔁 Peki yine de tabloya bıraksaydık ne olurdu?(Static)
Eğer silme butonunu her satıra koysaydık:
<a href="/LogStatic/DeleteStaticLog" class="btn btn-sm btn-danger">Sil</a>
Bu durumda hangi ID'yi sileceğini sistem bilmez. Çünkü tüm satırlarda aynı URL var (/LogStatic/DeleteStaticLog) ama log ID'si gönderilmiyor.

❌ Neden Uygun Değil?
Senin DeleteStaticLogAsync() metodun şöyle:
public async Task DeleteStaticLogAsync()
{
    int staticID = 11; // sabit
    ...
}
Burada hangi satıra tıklanırsa tıklansın sadece ID = 11 olan log silinir.
Bu yüzden tablonun içine koymak yanıltıcı olur. Kullanıcı "10 numaralı satıra bastım" der ama aslında 11 numara silinir.

Dinamik Silme İşlemi yapmış olsaydık (Kullanıcıdan) Tabloya bırakırdık.