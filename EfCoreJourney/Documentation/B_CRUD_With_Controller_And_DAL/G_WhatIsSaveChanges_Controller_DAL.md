🔧 SaveChanges() NEDİR?
SaveChanges() metodu, DbContext nesnesi aracılığıyla veritabanına yapılan değişiklikleri kalıcı hâle getirir.

📌 GÖREVİ NEDİR?
Uygulamada yapılan şu değişiklikleri veritabanına yansıtır:

Yeni bir kayıt eklediysen (Insert)
Var olan bir kaydı güncellediysen (Update)
Var olan bir kaydı sildiysen (Delete)

Detaylı Açıklama:
Kodda şu işlem yapılmış:

_appDbContext.Customers.Add(customer);
_appDbContext.SaveChanges();
Bu işlemlerin anlamı:

Add(customer) –> Veritabanına eklenmek üzere bir Customer nesnesi bellekte EF tarafından izlenmeye (track edilmeye) başlanır.

SaveChanges() –> EF, bellekte izlenen tüm değişiklikleri (ekleme, silme, güncelleme gibi) veritabanına SQL komutları olarak gönderir ve uygular.

Yani, sadece Add(customer) yaparsan veri henüz veritabanına gitmez, sadece bellekte bekletilir. Gerçek ekleme işlemi, SaveChanges() çağrıldığında olur.
