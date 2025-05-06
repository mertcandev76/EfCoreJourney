Projeksiyon (Projection) Çoklu Veri Getiren Sorgulama Fonksiyonu
Veritabanından çekilen verilerin tamamını değil, sadece istenilen alanlarını seçerek veya başka bir yapıya dönüştürerek kullanmaktır. LINQ’de bu amaçla genellikle Select() ve SelectMany() metodları kullanılır.

| Operatör    | Kavram Olarak | Uygulandığı Veri | Sonuç |
| ----------- | ------------- | ---------------- | ----- |
| Distinct()  | Çoğul İşlem   | Çoğul            | Çoğul |
| Union()     | Çoğul İşlem   | Çoğul + Çoğul    | Çoğul |
| Intersect() | Çoğul İşlem   | Çoğul + Çoğul    | Çoğul |
| Except()    | Çoğul İşlem   | Çoğul + Çoğul    | Çoğul |


Projeksiyon (Projection)-Tekli Veri Getiren Sorgulama Fonksiyonları

16-🔹 Select() 
Select(), koleksiyon üzerindeki her öğeyi dönüştürmek için kullanılır. Genellikle bir nesnenin tüm özellikleri yerine sadece ihtiyacımız olan özelliklerini seçmek veya yeni bir anonim/DTO nesne oluşturmak için kullanılır.

🔧 Günlük Hayattan Basit Bir Benzetme
Diyelim ki elinde müşteri dosyaları var. Her dosyada bu bilgiler var:

Ad
Soyad
Yaş
E-posta
Telefon

Ama sen sadece ad ve soyad istiyorsun. Gidip tüm dosyaları taşımazsın. Sadece ad ve soyadları kopyalarsın → işte bu Select().
📄 Müşteri Sınıfın (Customer)
public class Customer
{
    public int CustomerID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
}

🧠 Senaryo:
Tüm müşterilerin sadece ad ve soyadlarını al.
✅ 1. Adım: Kod EfCustomerRepository'ye Yazılacak
public class EfCustomerRepository : ICustomerDal

✅ 2. Adım: DTO Oluştur
Projenin mimarisine göre DTO sınıflarını ayrı bir klasörde tutmak en doğrusudur.


├── DTOsLayer/
│    └── DTOs/
│        └── CustomerNameDto.cs  ✅ BURAYA
│── YourProjectName
│
├── EntityLayer/
├── DataAccessLayer/
├── BusinessLayer/
├── Controllers/
└── Views/

public class CustomerNameDto
{
    public string FullName { get; set; }
}

✅ 3. Adım: EfCustomerRepository İçine Yeni Metot Ekle

public async Task<List<CustomerNameDto>> GetCustomerFullNamesAsync()
{
    return await _appDbContext.Customers
        .Select(c => new CustomerNameDto
        {
            FullName = c.FirstName + " " + c.LastName
            /*
            FirstName ve LastName veritabanından okunuyor,
            ama sadece birleştirilip FullName adlı tek bir string'e dönüştürülüyor.
            */

        })
        .ToListAsync();
}

CustomerNameDto sınıfında sadece FullName (yani "Ad + Soyad") yazdık çünkü:
Select() ile hem adı hem soyadı birleştirip tek bir string haline getiriyoruz.
Artık ayrı ayrı FirstName ve LastName alanlarına ihtiyaç kalmıyor çünkü zaten FullName = "Ahmet Yılmaz" gibi tek bir alan olarak dönüyor.

📝 Peki ayrı ayrı almak isteseydik?
2.adımdaki 
O zaman DTO şöyle olurdu:
public class CustomerNameDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
Ve Select() böyle yazılırdı:
.Select(c => new CustomerNameDto
{
    FirstName = c.FirstName,
    LastName = c.LastName
})


✅ 4. Adım: ICustomerDal Arayüzüne Tanımla
Task<List<CustomerNameDto>> GetCustomerFullNamesAsync();

----->1-Eğer Ayrı Bir Controllerda Çalışmak İstiyorsan-CustomerNames()

✅ 5. Adım: Controller'da Çağır
public async Task<IActionResult> CustomerNames()
{
    var names = await _customerDal.GetCustomerFullNamesAsync();
    return View(names); // veya return Json(names);
}

✅ 6.  View Oluşturma
İlk olarak, CustomerNames adlı bir View oluşturacağız. Bu View, CustomerNameDto listesini alacak ve ekranda gösterecek.

CustomerNames.cshtml view sayfası
@model List<DTOsLayer.DTOs.CustomerNameDto>

@{
    ViewData["Title"] = "Müşteri Adı ve Soyadı";
}

<h2>@ViewData["Title"]</h2>

<table class="table table-bordered table-striped">
    <thead>
        <tr>
            <th>Full Name</th>
           
        </tr>
    </thead>
    <tbody>
        @foreach (var customer in Model)
        {
            <tr>
                <td>@customer.FullName</td>
            </tr>
        }
    </tbody>
</table>

