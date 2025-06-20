-->OrderShipment

1. Durum: OrderShipment Entity Tanımı

namespace EntityLayer.Concrete
{
    public class OrderShipment
    {
        [Key]
        public int OrderShipmentID { get; set; }

        public string? Carrier { get; set; } // Taşıyıcı firma (Yurtiçi, Aras, MNG vs.)
        public string? TrackingNumber { get; set; } // Kargo takip numarası
        public DateTime? ShippedDate { get; set; } // Gönderim tarihi
        public DateTime? DeliveredDate { get; set; } // Teslim tarihi
        public int OrderID { get; set; } // Hangi siparişe ait olduğunu gösterir (FK olabilir)
    }
}

📌 Açıklama:

Bu sınıf OrderShipment adında bir gönderi kaydını temsil eder.
Her gönderim bir OrderID (sipariş) ile ilişkilidir.
Teslimat süreçlerini takip etmek için Carrier, TrackingNumber, ShippedDate, DeliveredDate gibi alanlar mevcuttur.
Veritabanına yansıtılacak bir tablo şemasıdır.

2. Durum: OrderShipmentWithOrderStaticController - Statik CRUD Controller

public class OrderShipmentWithOrderStaticController : Controller
{
    private readonly IOrderShipmentWithOrderStaticRepository _orderShipmentWithOrderStaticRepository;

    public OrderShipmentWithOrderStaticController(IOrderShipmentWithOrderStaticRepository orderShipmentWithOrderStaticRepository)
    {
        _orderShipmentWithOrderStaticRepository = orderShipmentWithOrderStaticRepository;
    }

    public async Task<IActionResult> Index()
    {
        var orderShipments = await _orderShipmentWithOrderStaticRepository.GetAllAsync();
        return View(orderShipments);
    }

    public async Task<IActionResult> GetByID()
    {
        var orderShipments = await _orderShipmentWithOrderStaticRepository.GetByIdAsync();
        if (orderShipments == null) return NotFound("Belirtilen ID'ye ait gönderim kaydı bulunamadı.");
        return View(orderShipments);
    }

    public async Task<IActionResult> AddOrderShipment()
    {
        await _orderShipmentWithOrderStaticRepository.AddAsync();
        TempData["Message"] = "Gönderim kaydı başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateOrderShipment()
    {
        await _orderShipmentWithOrderStaticRepository.UpdateAsync();
        TempData["Message"] = "Gönderim kaydı başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteOrderShipment()
    {
        await _orderShipmentWithOrderStaticRepository.DeleteAsync();
        TempData["Message"] = "Gönderim kaydı başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}

📌 Açıklama:

Bu Controller sınıfı tamamen statik veri işlemleri için tasarlanmıştır.
CRUD metotları, bir Repository üzerinden çalışır.
TempData ile işlem mesajları kullanıcıya iletilir.
Statik işlemler, ID’ler veya parametreler View'dan alınmaz, doğrudan Repository’ye yansıtılır.

3. Durum: (Yinelenen controller, 2. durum ile birebir aynı)

Bu durum, 2. durumla aynıdır. Yani, tekrar eden bir OrderShipmentWithOrderStaticController tanımıdır. Burada önemli olan nokta, Controller’ın sabit değerlerle çalışan bir yapı olmasıdır.

4. Durum: Index.cshtml – View Sayfası (Listeleme)

@model List<EntityLayer.Concrete.OrderShipment>
@{
    ViewData["Title"] = "Gönderim Kayıtları Listesi";
}
<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/OrderShipmentWithOrderStatic/AddOrderShipment">Ekle</a>
    <a class="btn btn-warning" href="/OrderShipmentWithOrderStatic/UpdateOrderShipment">Güncelle</a>
    <a class="btn btn-danger" href="/OrderShipmentWithOrderStatic/DeleteOrderShipment">Sil</a>
</div>

<table class="table table-bordered">
    <thead>
        <tr>
            <th>ID</th>
            <th>Taşıyıcı</th>
            <th>Takip Numarası</th>
            <th>Gönderim Tarihi</th>
            <th>Teslim Tarihi</th>
            <th>Sipariş-ID</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var orderShipment in Model)
        {
            <tr>
                <td>@orderShipment.OrderShipmentID</td>
                <td>@orderShipment.Carrier</td>
                <td>@orderShipment.TrackingNumber</td>
                <td>@orderShipment.ShippedDate</td>
                <td>@orderShipment.DeliveredDate</td>
                <td>@orderShipment.OrderID</td>
            </tr>
        }
    </tbody>
</table>

📌 Açıklama:

@model direktifi ile Controller’dan gelen List<OrderShipment> verisi View’a aktarılır.
TempData["Message"] ile başarılı işlemler sonrası mesaj gösterimi yapılır.
3 buton, statik olarak CRUD işlemlerini tetikler: Ekle, Güncelle, Sil.
Tablo yapısında veriler sıralı biçimde ekrana yansıtılır.

🔚 GENEL DEĞERLENDİRME

| Katman          | Açıklama                                                                                |
| --------------- | --------------------------------------------------------------------------------------- |
| **EntityLayer** | `OrderShipment` sınıfı, gönderi bilgilerini taşıyan bir modeldir.                       |
| **Controller**  | MVC yapısında işlemleri yöneten sınıftır. Repository’den veri çeker, View’a aktarır.    |
| **Repository**  | (Kod verilmedi ama) bu yapı veritabanı işlemlerini gerçekleştiren sınıf olmalıdır.      |
| **View**        | Kullanıcı arayüzüdür, listeleme ve TempData mesajları içerir. Bootstrap kullanılmıştır. |
