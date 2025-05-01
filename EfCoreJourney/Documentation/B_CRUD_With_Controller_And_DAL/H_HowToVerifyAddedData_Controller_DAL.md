Eklenen Veri Nasıl Anlaşılır?

Bir verinin eklenmesi gerektiğini anlamanın birkaç yolu vardır. Entity Framework Core, nesnelerin durumlarını "tracking" (izleme) yoluyla takip eder. Bu sayede, bir nesne üzerinde yapılan değişikliklerin ne olduğunu belirler. İşte eklenmesi gereken verinin nasıl anlaşıldığına dair birkaç açıklama:

1. Yeni Nesnelerin Eklenmesi:
Eğer bir nesne (örneğin Customer) yeni oluşturulmuşsa ve henüz veritabanında karşılık gelen bir kaydı yoksa, bu nesne "Added" olarak işaretlenir.
Yani, EF Core nesnenin veritabanına eklenmesi gerektiğini anlar, çünkü bu nesne daha önce veritabanında bulunmaz.

Nasıl Anlaşılır?

Yeni bir nesne oluşturduğunda ve Add() metodunu çağırdığında, EF Core bu nesneyi Added olarak işaretler. Bu nesnenin ID’si (genellikle Guid ya da int gibi) boş olmalıdır, çünkü veritabanında henüz yer almamaktadır.

// Yeni bir nesne oluşturuluyor ve Add metodu çağrıldığında, EF Core bunu ekleme olarak işaretler.

_appDbContext.Customers.Add(customer);
// SaveChanges çağrıldığında, eklenmesi gereken veri veritabanına kaydedilir.
_appDbContext.SaveChanges();

Yeni Nesnelerin Durumları:
EF Core, her nesnenin durumunu takip eder. Bir nesne oluşturulup Add() metodu çağrıldığında, nesnenin durumu şu şekilde değişir:

Added: Bu nesne, veritabanına eklenmek üzere hazırlanan yeni bir nesnedir.

Durumların Genel Durumu:
Detached: Nesne EF Core tarafından izlenmiyor, yani veritabanına hiç bağlanmamış.

Added: Nesne yeni oluşturulmuş ve eklenmek üzere veritabanına eklenmeye hazır.
Modified: Nesne veritabanında var, ancak üzerinde değişiklik yapılmış.
Unchanged: Nesne veritabanında var ve değişiklik yapılmamış.
Deleted: Nesne silinmek üzere işaretlenmiş.

Durum Değişikliklerinin Takibi:
Entity Framework Core, bir nesne oluşturulup eklenmeye çalışıldığında bu nesneyi Added olarak işaretler. Bu nedenle, bir nesne eklenmeye karar verildiğinde, ID’si null veya 0 gibi henüz veritabanından alınmamış bir değer olmalıdır. Eğer ID zaten mevcutsa, EF Core bu nesneyi Modified (değiştirilmiş) olarak işaretler ve bir ekleme işlemi yapmaz.

Örnek:
Eğer veritabanında zaten var olan bir müşteri nesnesi üzerinde değişiklik yapılıyorsa:

Customer existingCustomer = _appDbContext.Customers.FirstOrDefault(c => c.Id == 1);
if (existingCustomer != null)
{
    existingCustomer.Name = "Yeni Ad";
    // Burada Add() değil, direkt SaveChanges() ile güncelleme yapılır.
    _appDbContext.SaveChanges();  // Bu, güncelleme işlemi yapar, ekleme değil.
}
Özet:

Yeni veriler (ID null veya henüz veritabanında olmayanlar) eklenmesi gereken veriler olarak anlaşılır.
Add() metodu çağrıldığında, EF Core nesneyi Added durumu ile işaretler.
SaveChanges() çağrıldığında, bu nesne veritabanına eklenir.

Bir nesnenin eklenip eklenmeyeceğini anlamak için nesnenin ID'sinin kontrol edilmesi yaygın bir yöntemdir. Eğer ID boşsa (yeni bir nesne), bu nesne eklenmesi gereken bir nesnedir.