Çoklu Veri Getiren Sorgulama Fonksiyonları


🔸 Örnek Sınıf

public class Customer 
{
    [Key]
    public int CustomerID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }
}


1-🔹 ToListAsync() Nedir?
ToListAsync() metodu, Entity Framework Core ile gelen asenkron bir LINQ uzantı metodudur.

Kullanıldığında:
Veritabanı sorgusu hemen çalıştırılır.
Sonuçlar List<T> şeklinde döner.
await ile kullanılır.

🧠 Kısaca:
await context.Customers.ToListAsync(); → "Veritabanındaki tüm müşteri kayıtlarını listeye çevirerek getir."

Not!!!

Gerekli using Direktifini Kontrol Edin
ToListAsync metodunu kullanabilmek için Microsoft.EntityFrameworkCore namespace'ini dosyanıza dahil etmeniz gerekmektedir. Bu direktifi kontrol etmek için, sınıfın üst kısmına şu using satırını eklediğinizden emin olun:

using Microsoft.EntityFrameworkCore;
Bu, ToListAsync ve diğer EF Core metodlarının çalışabilmesi için gereklidir.


