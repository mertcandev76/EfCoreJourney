-->OrderPayment

1. Durum: OrderPayment Entity Tanımı

namespace EntityLayer.Concrete
{
    public class OrderPayment
    {
        [Key]
        public int OrderPaymentID { get; set; }         // Ödeme ID'si (Primary Key)
        public decimal Amount { get; set; }              // Ödenen Tutar
        public string? PaymentMethod { get; set; }       // Ödeme Yöntemi (Kredi Kartı, Havale vb.)
        public DateTime? PaymentDate { get; set; }       // Ödeme Tarihi
        public int OrderID { get; set; }                 // İlgili Sipariş ID (Foreign Key)
    }
}

Bu entity, her bir ödeme işlemini temsil eder. OrderID alanı ile ilgili siparişe bağlanır (İlişki kurulabilir).

2. Durum: Repository Sınıfı ile Statik CRUD İşlemleri

namespace DataAccessLayer.Repositories
{
    public class OrderPaymentWithOrderStaticRepository : IOrderPaymentWithOrderStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderPaymentWithOrderStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

a) Tüm Verileri Listeleme (Read)

public async Task<List<OrderPayment>> GetAllAsync()
{
    return await _appDbContext.OrderPayments.ToListAsync();
}

b) ID'ye Göre Veri Getirme (Read)

public async Task<OrderPayment?> GetByIdAsync()
{
    int staticID = 2; // Statik ID ile örnek veri getirme
    return await _appDbContext.OrderPayments.FindAsync(staticID);
}

c) Yeni Veri Ekleme (Create)

public async Task AddAsync()
{
    var orderPayment = new OrderPayment
    {
        Amount = 8,
        PaymentMethod = "Kredi Kartı",
        PaymentDate = DateTime.Now,
        OrderID = 2
    };
    await _appDbContext.OrderPayments.AddAsync(orderPayment);
    await _appDbContext.SaveChangesAsync();
}

d) Mevcut Veriyi Güncelleme (Update)

public async Task UpdateAsync()
{
    int staticID = 2;
    var orderPayment = await _appDbContext.OrderPayments.FindAsync(staticID);
    if (orderPayment != null)
    {
        orderPayment.Amount = 5;
        orderPayment.PaymentMethod = "Takip Numarası Güncellendi";
        orderPayment.PaymentDate = DateTime.UtcNow;
        orderPayment.OrderID = 2;
        await _appDbContext.SaveChangesAsync();
    }
}

e) Veri Silme (Delete)

public async Task DeleteAsync()
{
    int staticID = 2;
    var orderPayment = await _appDbContext.OrderPayments.FindAsync(staticID);
    if (orderPayment != null)
    {
        _appDbContext.OrderPayments.Remove(orderPayment);
        await _appDbContext.SaveChangesAsync();
    }
}

3. Durum: Controller ile View’a Veri Taşıma

namespace EfCoreJourney.Controllers
{
    public class OrderPaymentWithOrderStaticController : Controller
    {
        private readonly IOrderPaymentWithOrderStaticRepository _orderPaymentWithOrderStaticRepository;

        public OrderPaymentWithOrderStaticController(IOrderPaymentWithOrderStaticRepository repo)
        {
            _orderPaymentWithOrderStaticRepository = repo;
        }

a) Listeleme

public async Task<IActionResult> Index()
{
    var orderPayments = await _orderPaymentWithOrderStaticRepository.GetAllAsync();
    return View(orderPayments);
}

b) Belirli ID ile Getirme

public async Task<IActionResult> GetByID()
{
    var orderPayment = await _orderPaymentWithOrderStaticRepository.GetByIdAsync();
    if (orderPayment == null) return NotFound("Belirtilen ID'ye sahip ödeme kaydı bulunamadı.");
    return View(orderPayment);
}

c) Ekleme

public async Task<IActionResult> AddOrderPayment()
{
    await _orderPaymentWithOrderStaticRepository.AddAsync();
    TempData["Message"] = "Ödeme kaydı başarıyla eklendi.";
    return RedirectToAction(nameof(Index));
}

d) Güncelleme

public async Task<IActionResult> UpdateOrderPayment()
{
    await _orderPaymentWithOrderStaticRepository.UpdateAsync();
    TempData["Message"] = "✏️ Ödeme kaydı başarıyla güncellendi.";
    return RedirectToAction(nameof(Index));
}

e) Silme

public async Task<IActionResult> DeleteOrderPayment()
{
    await _orderPaymentWithOrderStaticRepository.DeleteAsync();
    TempData["Message"] = "🗑️ Ödeme kaydı başarıyla silindi.";
    return RedirectToAction(nameof(Index));
}
4. Durum: Razor View ile Ödeme Kayıtlarının Gösterimi

@model List<EntityLayer.Concrete.OrderPayment>
@{
    ViewData["Title"] = "Ödeme Kayıtları Listesi";
}
Bilgilendirme ve İşlem Butonları:

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/OrderPaymentWithOrderStatic/AddOrderPayment">Ekle</a>
    <a class="btn btn-warning" href="/OrderPaymentWithOrderStatic/UpdateOrderPayment">Güncelle</a>
    <a class="btn btn-danger" href="/OrderPaymentWithOrderStatic/DeleteOrderPayment">Sil</a>
</div>

Listeleme Tablosu:

<table class="table table-bordered">
    <thead>
        <tr>
            <th>ID</th>
            <th>Miktar</th>
            <th>Ödeme yöntemi</th>
            <th>ÖdemeTarihi</th>
            <th>Sipariş-ID</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var orderPayment in Model)
        {
            <tr>
                <td>@orderPayment.OrderPaymentID</td>
                <td>@orderPayment.Amount</td>
                <td>@orderPayment.PaymentMethod</td>
                <td>@orderPayment.PaymentDate</td>
                <td>@orderPayment.OrderID</td>
            </tr>
        }
    </tbody>
</table>

🎯 Özet

| Katman           | Görevi                                                      |
| ---------------- | ----------------------------------------------------------- |
| **EntityLayer**  | OrderPayment tablosunun yapısı tanımlandı                   |
| **Repository**   | Statik CRUD işlemleri yazıldı                               |
| **Controller**   | View ile Repository arasındaki bağlantı kuruldu             |
| **View (Razor)** | Ödeme kayıtları listelendi ve işlem butonları yerleştirildi |
