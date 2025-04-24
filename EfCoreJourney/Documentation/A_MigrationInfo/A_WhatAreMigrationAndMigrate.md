🔧 Migration Nedir?
Migration, bir veritabanı şemasındaki (tablolar, kolonlar, ilişkiler vb.) değişiklikleri kod üzerinden tanımlayıp bunları versiyon kontrolü ile yönetmemizi sağlayan bir yapıdır.

Örneğin:

Yeni bir tablo oluşturmak
Mevcut bir kolona yeni bir özellik eklemek
Bir tabloyu silmek gibi işlemleri migration dosyalarıyla tanımlarız.

1.adım
Visual Studio → Tools → NuGet Package Manager → Package Manager Console
Ve aşağıdaki komutu gir:
Add-Migration AddCustomerTable
✅ Bu komut, Migrations klasöründe bir migration dosyası oluşturur. Bu dosya, EF Core’un veritabanında ne yapması gerektiğini bilir.
2.adım
dotnet ef migrations add IlkMigration


🚀 Migrate Nedir?
Migrate, oluşturulan migration'ların veritabanına uygulanması işlemidir.
Yani migration dosyası hazırlandıktan sonra bu değişiklikleri gerçekten veritabanında gerçekleştirmek için kullanılır.
1.adım
Package Manager Console ile aşağıdaki komutu gir:
Update-Database
✅ Bu komut, AddCustomerTable migration’ını çalıştırır ve Customer tablosunu veritabanına ekler.

2.adım
dotnet ef database update

🔁 Ekstra: Geri Alma (Remove-Migration)
Eğer migration’ı yanlış oluşturduysan ve henüz Update-Database yapmadıysan, geri almak için:
Remove-Migration

Bonus 🎁: Migration Adlarını Görmek
Hangi migration’lar tanımlı görmek istersen:
Get-Migrations
Bu komut tüm migration listesini gösterir.















