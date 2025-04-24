Controller-DAL Bağlantılı Silme İşlemi(Sabit)

1. Adım: ICustomerDal Arayüzüne Delete() Metodu Eklendi

public interface ICustomerDal
{
    void Delete(Customer customer);
}

✍️ Açıklama:
Delete() metodu, bir Customer nesnesini veritabanından silmek için tanımlandı.
Interface içinde sadece metodun imzası vardır, içi yazılmaz.
Repository (veri erişim) sınıfı, bu metodu zorunlu olarak tanımlar.

✅ 2. Adım: EfCustomerRepository İçinde Delete() Metodunun Tanımı

public void Delete(Customer customer)
{
    var existingCustomer = GetById(customer.CustomerID);
    if (existingCustomer != null)
    {
        _appDbContext.Customers.Remove(existingCustomer); // Silme işlemi
        _appDbContext.SaveChanges(); // Değişiklikleri kaydet
    }
}

✍️ Açıklama:
Silinmek istenen müşteri önce veritabanından GetById() ile getirilir.
Eğer null değilse, EF Core’un Remove() metodu ile silinir.
Ardından SaveChanges() çağrılarak değişiklikler veritabanına kaydedilir.

📌 Önemli Detaylar:

Nokta | Açıklama
GetById() | Daha önce yazılan metod, belirli bir CustomerID'yi bulur.
Remove() | EF Core’da veritabanından silme işlemi için kullanılır.
SaveChanges() | Gerçekten veritabanına işlenmesini sağlar.

3. Adım: CustomerController İçindeki DeleteStaticCustomer() Metodu

public IActionResult DeleteStaticCustomer(int id)
{
    id = 3; // Sabit bir ID ile test yapılıyor
    var customer = _customerDal.GetById(id);
    _customerDal.Delete(customer); // Silme işlemi yapılır
    return RedirectToAction("Index"); // Liste sayfasına dönülür
}

✍️ Açıklama:

id = 3 olarak sabitlenmiş (test amaçlı).
Belirtilen ID’ye sahip müşteri veritabanından çekilir.
Delete() metodu ile müşteri silinir.
Silme işlemi bittikten sonra Index sayfasına yönlendirilir.

✅ 4. Adım: Razor View – Index Sayfası (Silme Butonu)

@model List<EntityLayer.Concrete.Customer>

@{
    ViewData["Title"] = "Müşteri Listesi";
}

<h1>@ViewData["Title"]</h1>

<a class="btn btn-secondary" href="/Customer/DeleteStaticCustomer">Sabit Müşteri Sil</a>

✍️ Açıklama:

Sayfa, Customer listesini (List<Customer>) model olarak alır.
Bir adet "Sabit Müşteri Sil" butonu vardır.
Tıklanınca /Customer/DeleteStaticCustomer adresine gider ve silme işlemi başlar.

🔁 Akış Şeması

Kullanıcı (butona tıklar)
       ↓
CustomerController.DeleteStaticCustomer()
       ↓
Müşteri GetById ile bulunur
       ↓
EF Core Remove ile müşteri silinir
       ↓
SaveChanges ile veritabanına işlenir
       ↓
Index sayfasına yönlendirilir

🧠 Teknik Notlar

Konu | Açıklama
Remove() | Nesneyi EF tarafından silinmiş olarak işaretler.
SaveChanges() | Silme dahil tüm değişiklikleri veritabanına uygular.
Sabit ID kullanımı | Geliştirme/test sürecinde deneme yapmak için idealdir. Gerçek sistemde bu dinamik olmalıdır.
Null kontrolü | Silinecek veri bulunamazsa null olur, bu yüzden kontrol gereklidir.

✅ Özet

Adım | Açıklama
1 | ICustomerDal arayüzüne Delete() metodu eklendi.
2 | EfCustomerRepository içinde EF Core ile silme işlemi tanımlandı.
3 | CustomerController içinde sabit ID’li bir müşteri silme işlemi yapıldı.
4 | Index sayfasında buton ile silme işlemi tetiklendi.

