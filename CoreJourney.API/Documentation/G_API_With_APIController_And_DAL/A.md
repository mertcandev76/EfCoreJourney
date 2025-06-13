🔹 API Katmanı Nedir?

EfCoreJourney.API adındaki katman bir Web API projesidir. Genellikle:
Controller sınıfları burada yer alır.
İstemciler (UI, mobil uygulama, postman, javascript vs.) bu katmandaki endpoint’lere istek gönderir.
BusinessLayer üzerinden veri çeker ve DTO'ya dönüştürüp dış dünyaya verir.
UI veya başka bir uygulamaya veri JSON formatında sağlar.
API Katmanı sunucunun dış dünya ile konuştuğu kapıdır.

🔹 API Katmanı ile Ana Katman (UI) Arasındaki Fark Nedir?

| Özellik                      | API Katmanı (EfCoreJourney.API)             | UI Katmanı (EfCoreJourney)                           |
| ---------------------------- | ------------------------------------------- | ---------------------------------------------------- |
| Görev                        | Veri sunar (HTTP üzerinden)                 | Kullanıcıya arayüz sağlar                            |
| Veri iletişimi               | JSON, XML gibi formatlarla veri verir/alır  | Görsel arayüz üzerinden kullanıcıyla etkileşir       |
| Kiminle çalışır?             | UI, mobil app, 3rd party uygulamalar        | Kullanıcıyla (formlar, butonlar, textboxlar)         |
| Teknik olarak                | ASP.NET Core Web API projesi                | ASP.NET Core MVC, Blazor, WPF, WinForms vb. olabilir |
| Kendi başına çalışabilir mi? | Evet (örneğin Postman ile test edebilirsin) | Hayır, veri için API'ye veya BL/DAL'a ihtiyaç duyar  |

🔸 Bir Örnekle Açıklama
Farz edelim bir müşteri listesini göstermek istiyorsun.
EntityLayer → Customer sınıfı

DAL → CustomerRepository içinde GetAllCustomers()
BL → CustomerManager içinde iş kuralı uygulanır (örneğin aktif müşteriler)
DTOsLayer → CustomerDto sadece isim, email taşıyor
API Katmanı → /api/customers endpoint'ine istek gelince CustomerDto listesi döner
UI Katmanı → Bu API endpoint’ine istek atar, dönen veriyi sayfada listeler