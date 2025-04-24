🔗 Migration Zinciri Nedir?
Migration zinciri = Migration'ların sırayla birbirine bağlı olmasıdır.
Her yeni migration, bir öncekinin üzerine inşa edilir.

🔍 Örnek Üzerinden Açıklayalım:
Migration Zinciri:
AddCustomerTable
Veritabanının ilk hali
Örneğin: Users tablosu oluştu

AddOrderTable
Önceki migration AddCustomerTable'e bağlı
Yeni: Orders tablosu eklendi

InitialCreate
AddOrderTable üzerine geldi
Yeni: Users tablosu eklendi

AddCustomerTable
     ↓
AddOrderTable
     ↓
InitialCreate

yani;
🔢 İlk önce oluşturulan tablo, AddCustomerTable içinde tanımlı olanlardır.
💡 Ardından Orders tablosu geldi (AddOrderTable)
💡 En son Users tablosu geldi (InitialCreate)


✅ Zinciri Sağlıklı Yönetmenin Yolu:
Geri almak istiyorsan: en sondan başla, sırayla sil
Migration eklerken: her biri bir öncekini tamamlamalı
Kod ile veritabanı arasında senkron tut!



Migration ve Migrate Nasıl Geri Alınır?
1-🧾 Senaryo:
Birden fazla migration var, örneğin:

AddCustomerTable
AddOrderTable
InitialCreate

InitialCreate gibi son migration'ı geri almak istiyorsun.
Ve veritabanındaki tabloyu da tamamen silmek istiyorsun (örneğin Users tablosu).

Migration'ların sırası şu şekilde:

✅ AddCustomerTable
✅ AddOrderTable 
✅ InitialCreate ← geri almak istiyorsun
InitialCreate

✅ Yöntem 1: Komutlarla Geri Alma (Otomatik)
1️- Veritabanını en son migration'dan önceki bir hale getirme
Öncelikle, InitialCreate migration'ını geri almak için, veritabanını son migration’dan önceki migration’a döndürmelisin. Bu durumda, AddOrderTable migration'ına dönmen gerekiyor. Çünkü InitialCreate, AddOrderTable'dan sonra yapılmış bir migration.

Update-Database AddOrderTable
Bu komut, veritabanını AddOrderTable migration'ına geri döndürecek ve Users tablosu silinecek.

2️- Migration dosyasını sil
Şimdi, InitialCreate migration'ını projenden kaldırman gerekiyor. Bu işlemi şu komutla yapabilirsin:

Remove-Migration
Bu komut, InitialCreate migration dosyasını ve ilgili dosyaları projeden silmiştir.

🧼 Yöntem 2: Elle Silme (Manuel)
Eğer komutları kullanmak yerine elle silmek istersen, işte adımlar:

1️- Veritabanında Users tablosunu sil
SQL Server Management Studio (SSMS) veya Azure Data Studio kullanarak veritabanına bağlan.

Users tablosunu bul.
Sağ tıkla → Delete diyerek tablonun verilerini ve yapısını sil.
Bu, sadece Users tablosunu değil, migration'ların yaptığı diğer tüm değişiklikleri de geri alır.

2️- Migration dosyasını manuel silme
Migrations klasöründe InitialUsersCreate.cs ve InitialUsersCreate.Designer.cs dosyalarını sağ tıkla → Delete.

Ayrıca, ModelSnapshot.cs dosyasını da silebilirsin. Bu dosya da migration bilgilerini tutar.


2-🧾 Senaryo:
Birden fazla migration var:

AddCustomerTable
AddOrderTable
InitialCreate

AddOrderTable migration'ını geri almak ve veritabanındaki tabloyu silmek istiyorsun.


Migration'ların sırası şu şekilde:

✅ AddCustomerTable 
✅ AddOrderTable ← geri almak istiyorsun
✅ InitialCreate

⚠️ Problem:
EF Core'da migration'lar sıralı zincir şeklindedir.
Yani AddOrderTable’ı doğrudan geri alamazsın, çünkü ondan sonra gelen InitialCreate hala aktif durumda.
AddOrderTable ger alıp silmek istiyorsan InitialCreate geri alıp silmen gerekir

ondan dolayı senaryo şuna dönüşüyor
✅ AddCustomerTable
✅ AddOrderTable ← geri alınıcak
✅ InitialCreate ← geri alınıcak 

Yöntem 1'i kullanalım (Otomatik geri alma)

Update-Database AddCustomerTable
Remove-Migration



3-🧾 Senaryo:
Birden fazla migration var:

AddCustomerTable
AddOrderTable
InitialCreate

AddCustomerTable migration'ını geri almak ve veritabanındaki tabloyu silmek istiyorsun.


Migration'ların sırası şu şekilde:

✅ AddCustomerTable ← geri almak istiyorsun
✅ AddOrderTable 
✅ InitialCreate

InitialCreate migration'ını geri almak ve veritabanındaki tabloyu silmek istiyorsun.

⚠️ Problem:
EF Core'da migration'lar sıralı zincir şeklindedir.
Yani AddCustomerTable’ı doğrudan geri alamazsın, çünkü ondan sonra gelen AddOrderTable ve InitialCreate hala aktif durumda.
AddCustomerTable ger alıp silmek istoyorsan AddOrderTable ve InitialCreate geri alıp silmen gerekir

ondan dolayı senaryo şuna dönüşüyor
✅ AddCustomerTable ← geri alınıcak
✅ AddOrderTable ← geri alınıcak
✅ InitialCreate ← geri alınıcak 

Yöntem 1'i kullanalım (Otomatik geri alma)

Update-Database 0
Remove-Migration





