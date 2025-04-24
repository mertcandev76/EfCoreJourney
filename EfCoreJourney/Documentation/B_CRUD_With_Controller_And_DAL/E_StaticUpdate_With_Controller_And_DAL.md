Controller-DAL Bağlantılı Güncelleme İşlemi(Sabit)

1. Adım - ICustomerDal Interface(Data Access Interface)

public interface ICustomerDal
{
    void Update(Customer customer);
}


✍️ Açıklama:
Bu arayüz metodu, bir Customer nesnesini güncellemek için tanımlandı.
Arayüzde sadece metod imzası olur, nasıl çalışacağı yazılmaz.
Bu sayede bu arayüzü kullanan sınıf, Update metodunu zorunlu olarak implement etmek zorundadır.

🎯 Amaç:
Güncelleme işleminin tanımı yapılır ama işleyişi soyutlanır.
Kodun esnekliği ve test edilebilirliği artar.

2. Adım: EfCustomerRepository (EF Core ile Gerçek Güncelleme İşlemi)

public void Update(Customer customer)
{
    var existingCustomer = GetById(customer.CustomerID);

    if (existingCustomer != null)
    {
        existingCustomer.Name = "updated lorem ipsum 2";
        existingCustomer.Email = "updatedloremipsum@gmail.com 2";
        existingCustomer.Phone = "987654321";

        _appDbContext.SaveChanges();  // Veritabanına kaydet
    }
}

✍️ Açıklama:
Önce GetById(customer.CustomerID) ile güncellenecek müşteri veritabanından çekilir.
Eğer müşteri varsa (null değilse), alanları sabit değerlerle güncellenir.
Son olarak SaveChanges() ile yapılan değişiklikler veritabanına kaydedilir.

📌 Önemli Noktalar:

Nokta | Açıklama
GetById | Bu metod daha önce yazdığın ID ile müşteri getirme fonksiyonudur.
SaveChanges() | Değişikliklerin fiziksel olarak veritabanına yansımasını sağlar.
if (existingCustomer != null) | Müşteri bulunamazsa hata vermemek için kontrol yapılır.

3. Adım: CustomerController içinde (MVC Controller)

public IActionResult UpdateStaticCustomer(int id)
{
    id = 2; // örnek olarak sabit bir ID atanmış

    var customer = _customerDal.GetById(id);

    if (customer == null)
    {
        return NotFound();  // Müşteri bulunmazsa 404 döndür
    }

    _customerDal.Update(customer); // Sabit verilerle güncelle

    return RedirectToAction("Index"); // Listeleme sayfasına yönlen
}

✍️ Açıklama:

id parametresi sabit olarak 2 yapılmış (örnek test için).
O ID'ye sahip müşteri veritabanından alınır.
Eğer müşteri varsa, _customerDal.Update() çağrılarak sabit bilgilerle güncellenir.
İşlem sonrası Index sayfasına (listeleme) yönlendirilir.

@model List<EntityLayer.Concrete.Customer>

@{
    ViewData["Title"] = "Müşteri Listesi";
}

<h1>@ViewData["Title"]</h1>

<a class="btn btn-secondary" href="/Customer/UpdateStaticCustomer">Sabit Müşteri Düzenle</a>


✍️ Açıklama:
View Customer listesini (List<Customer>) model olarak alır.
Başlık dinamik olarak ViewData üzerinden atanır.
Bir buton vardır: /Customer/UpdateStaticCustomer linkine tıklandığında güncelleme işlemi yapılır.

🔁 Genel Akış

Kullanıcı (butona tıklar)
       ↓
CustomerController.UpdateStaticCustomer()
       ↓
Repository üzerinden veritabanından müşteri alınır
       ↓
Müşteri bilgileri sabit değerlerle güncellenir
       ↓
EF Core üzerinden SaveChanges() yapılır
       ↓
Index (listeleme) sayfasına yönlendirilir


🧠 Ekstra Bilgiler

Konu | Açıklama
Veri güncelleme | EF Core'da nesne izleniyorsa, sadece property'leri değiştirip SaveChanges() demek yeterlidir.
GetById kullanımı | Kodun tekrar kullanılabilirliğini artırır.
Sabit veri kullanımı | Eğitim/test için uygundur. Gerçek hayatta formdan gelen verilerle güncellenir.




