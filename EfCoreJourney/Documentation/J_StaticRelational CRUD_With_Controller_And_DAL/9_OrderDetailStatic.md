-->OrderDetail

1. Durum: OrderDetail Entity Tanımı (EntityLayer)

namespace EntityLayer.Concrete
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; } // Birincil anahtar

        public int Quantity { get; set; }      // Sipariş edilen ürün adedi
        public decimal UnitPrice { get; set; } // Ürünün birim fiyatı
        public decimal? Discount { get; set; } // Varsa uygulanan indirim
        public string? Notes { get; set; }     // Ek açıklamalar

        public int OrderId { get; set; }       // Yabancı anahtar (FK) - Order
        public Order? Order { get; set; }      // Navigasyon - Sipariş ilişkisi

        public int ProductID { get; set; }     // Yabancı anahtar (FK) - Product
        public Product? Product { get; set; }  // Navigasyon - Ürün ilişkisi
    }
}

Bu sınıf, bir ürünün bir sipariş içerisindeki detaylarını tutar. Yani bir siparişe ait kaç adet ürün alındı, birim fiyatı nedir, ürün bilgisi nedir gibi verileri içerir.
Ayrıca Order ve Product ile bire çok ilişki kurulmuştur.

2. Durum: OrderDetailWithOrderAndProductStaticRepository (Repository Layer)

namespace DataAccessLayer.Repositories
{
    public class OrderDetailWithOrderAndProductStaticRepository : IOrderDetailWithOrderAndProductStaticRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderDetailWithOrderAndProductStaticRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

📌 GetAllAsync()

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            var orderDetails = await _appDbContext.OrderDetails
                .Include(od => od.Order)
                    .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                    .ThenInclude(p => p.ProductBrand)
                .ToListAsync();

            return orderDetails;
        }

Bütün sipariş detaylarını, sipariş, müşteri, ürün ve ürün markasıyla birlikte getirir.

📌 GetByIdAsync()

        public async Task<OrderDetail?> GetByIdAsync()
        {
            int staticID = 1;

            var orderDetail = await _appDbContext.OrderDetails
                .Include(od => od.Order)
                    .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                    .ThenInclude(p => p.ProductBrand)
                .FirstOrDefaultAsync(od => od.OrderDetailID == staticID);

            return orderDetail;
        }

Belirli bir OrderDetailID'ye sahip olan sipariş detayını tüm ilişkili verilerle birlikte getirir.

📌 AddAsync()

        public async Task AddAsync()
        {
            var orderDetail = new OrderDetail
            {
                Quantity = 3,
                UnitPrice = 25,
                Discount = 8,
                Notes = "Ürün detayı notu eklendi - 1",
                OrderId = 2,
                ProductID = 1006
            };

            await _appDbContext.OrderDetails.AddAsync(orderDetail);
            await _appDbContext.SaveChangesAsync();
        }
Statik verilerle yeni bir sipariş detayı ekler.

📌 UpdateAsync()

        public async Task UpdateAsync()
        {
            int staticID = 1;
            var orderDetail = await _appDbContext.OrderDetails
                .Include(od => od.Order).ThenInclude(o => o.Customer)
                .Include(od => od.Product).ThenInclude(p => p.ProductBrand)
                .FirstOrDefaultAsync(od => od.OrderDetailID == staticID);

            if (orderDetail != null)
            {
                orderDetail.Quantity = 2;
                orderDetail.UnitPrice = 24;
                orderDetail.Notes = "Ürün detayı notu güncellendi - 1";

                // İlişkili nesneleri de günceller
                orderDetail.Order!.Status = "Hazırlanıyor";
                orderDetail.Order.Customer!.FullName = "Mertcan Adsız";
                orderDetail.Product!.Name = "Güncellenmiş Ürün Adı";
                orderDetail.Product.ProductBrand!.Name = "Yeni Marka";

                await _appDbContext.SaveChangesAsync();
            }
        }

Hem OrderDetail, hem de ilişkili Order, Customer, Product ve ProductBrand bilgileri güncellenebilir.

📌 DeleteAsync()

        public async Task DeleteAsync()
        {
            int staticID = 1;
            var orderDetail = await _appDbContext.OrderDetails
                .Include(od => od.Order).ThenInclude(o => o.Customer)
                .Include(od => od.Product).ThenInclude(p => p.ProductBrand)
                .FirstOrDefaultAsync(od => od.OrderDetailID == staticID);

            if (orderDetail != null)
            {
                // Zincirleme silme işlemi
                _appDbContext.Customers.Remove(orderDetail.Order!.Customer!);
                _appDbContext.Orders.Remove(orderDetail.Order);
                _appDbContext.ProductBrands.Remove(orderDetail.Product!.ProductBrand!);
                _appDbContext.Products.Remove(orderDetail.Product);
                _appDbContext.OrderDetails.Remove(orderDetail);

                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
Bu örnekte, OrderDetail silindiğinde ona bağlı tüm ilişkili veriler de isteğe bağlı olarak silinmektedir.

3. Durum: Controller Katmanı (UI → Controller)

namespace EfCoreJourney.Controllers
{
    public class OrderDetailWithOrderAndProductStaticController : Controller
    {
        private readonly IOrderDetailWithOrderAndProductStaticRepository _orderDetailsStaticRepository;

        public OrderDetailWithOrderAndProductStaticController(IOrderDetailWithOrderAndProductStaticRepository repo)
        {
            _orderDetailsStaticRepository = repo;
        }

📌 Index (Listeleme)

        public async Task<IActionResult> Index()
        {
            var orderDetails = await _orderDetailsStaticRepository.GetAllAsync();
            return View(orderDetails);
        }

📌 Diğer CRUD işlemleri

        public async Task<IActionResult> GetByID()
        {
            var orderDetail = await _orderDetailsStaticRepository.GetByIdAsync();
            if (orderDetail == null) return NotFound("Sabit ID'li sipariş bulunamadı.");
            return View(orderDetail);
        }

        public async Task<IActionResult> AddOrderDetail()
        {
            await _orderDetailsStaticRepository.AddAsync();
            TempData["Message"] = "Yeni sipariş detayı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateOrderDetail()
        {
            await _orderDetailsStaticRepository.UpdateAsync();
            TempData["Message"] = "Sipariş detayı başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteOrderDetail()
        {
            await _orderDetailsStaticRepository.DeleteAsync();
            TempData["Message"] = "Sipariş detayı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
4. Durum: View (Razor Sayfası - Index.cshtml)

@model List<EntityLayer.Concrete.OrderDetail>
@{
    ViewData["Title"] = "Sipariş Detayları Listesi";
}

<h1>@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/OrderDetailWithOrderAndProductStatic/AddOrderDetail">Ekle</a>
    <a class="btn btn-warning" href="/OrderDetailWithOrderAndProductStatic/UpdateOrderDetail">Güncelle</a>
    <a class="btn btn-danger" href="/OrderDetailWithOrderAndProductStatic/DeleteOrderDetail">Sil</a>
</div>

<table class="table table-bordered">
    <thead>
        <tr>
            <th>ID</th>
            <th>Miktar</th>
            <th>Birim Fiyat</th>
            <th>İndirim</th>
            <th>Notlar</th>
            <th>Sipariş Tarihi</th>
            <th>Toplam Tutar</th>
            <th>Durum</th>
            <th>Sipariş Notu</th>
            <th>Müşteri Adı</th>
            <th>Ürün Adı</th>
            <th>Açıklama</th>
            <th>Fiyat</th>
            <th>Stok</th>
            <th>Aktif mi?</th>
            <th>Marka</th>
            <th>Marka Açıklaması</th>
            <th>Ülke</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var orderDetail in Model)
        {
            <tr>
                <td>@orderDetail.OrderDetailID</td>
                <td>@orderDetail.Quantity</td>
                <td>@orderDetail.UnitPrice</td>
                <td>@orderDetail.Discount</td>
                <td>@orderDetail.Notes</td>
                <td>@orderDetail.Order?.OrderDate</td>
                <td>@orderDetail.Order?.TotalAmount</td>
                <td>@orderDetail.Order?.Status</td>
                <td>@orderDetail.Order?.Notes</td>
                <td>@orderDetail.Order?.Customer?.FullName</td>
                <td>@orderDetail.Product?.Name</td>
                <td>@orderDetail.Product?.Description</td>
                <td>@orderDetail.Product?.Price</td>
                <td>@orderDetail.Product?.Stock</td>
                <td>@(orderDetail.Product?.IsActive == true ? "Evet" : "Hayır")</td>
                <td>@orderDetail.Product?.ProductBrand?.Name</td>
                <td>@orderDetail.Product?.ProductBrand?.Description</td>
                <td>@orderDetail.Product?.ProductBrand?.Country</td>
            </tr>
        }
    </tbody>
</table>

📌 Özet

Bu yapı sayesinde:
OrderDetail üzerinden ilişkili tüm veriler okunabilir.
CRUD işlemleri statik ID ile yapılabilir.
Tüm ilişkili entity’lerin bilgileri güncellenebilir veya silinebilir.
View’da tüm detaylar tablo şeklinde sunulur.