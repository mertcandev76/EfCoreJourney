🔸 Controller-DAL Bağlantılı Yapı Nedir?
Bu yapı, controller katmanının doğrudan veri erişim katmanına (DAL) eriştiği, arada iş kurallarını yöneten bir business layer (iş katmanı) bulunmayan daha basit ve hızlı kurulabilen bir yapıdır.

Katmanlar:

ICustomerDal (Data Access Interface – veri erişim arayüzü)

EfCustomerRepository (Data Access Implementation – EF ile DAL'ın gerçekleştirilmesi)

CustomerController (Presentation katmanında Controller)

Index.cshtml (Razor View – kullanıcıya gösterilen kısım)

Katmanlar arası geçiş:
Controller → doğrudan DAL’a (EfCustomerRepository) erişir.

Bu yapı küçük projeler için yeterli olabilir ama iş kuralları (business logic) ayrı bir katmanda değil.


Özellik                     | Yapı 
Business katmanı var mı?    | ❌ Yok 
Controller'ın DAL'a erişimi | ✅ Doğrudan 
SOLID'e uygunluk            | ⚠️ Düşük 
Proje büyüklüğüne uygunluk  | Küçük projeler  
