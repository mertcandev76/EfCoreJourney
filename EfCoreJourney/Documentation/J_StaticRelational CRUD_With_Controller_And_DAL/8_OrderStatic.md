-->Order

1. Durum – Order Entity Sınıfı (EntityLayer.Concrete)

public class Order
{
    [Key]
    public int OrderID { get; set; }

    public DateTime OrderDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public string? Status { get; set; } // Örn: Pending, Shipped, Delivered, Cancelled
    public string? Notes { get; set; }

    public int CustomerID { get; set; } // Foreign Key
    public Customer? Customer { get; set; } // Navigation Property

    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public OrderPayment? OrderPayment { get; set; }
    public OrderShipment? OrderShipment { get; set; }
}

Açıklama:

Order sınıfı bir siparişi temsil eder.
CustomerID: Bu siparişin hangi müşteriye ait olduğunu belirtir.
Customer: Navigation property'dir. Include() kullanarak ilişkili Customer bilgilerine erişmemizi sağlar.
OrderDetail, OrderPayment, OrderShipment: Bu alanlar siparişin detaylarıyla ilgili alt ilişkileri tutar ama bu örnekte henüz işlenmemiştir.

2. Durum – Statik Repository Sınıfı (DataAccessLayer.Repositories)

public class OrderWithCustomerStaticRepository : IOrderWithCustomerStaticRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderWithCustomerStaticRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _appDbContext.Orders
            .Include(o => o.Customer)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync()
    {
        int staticID = 1;
        return await _appDbContext.Orders
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.OrderID == staticID);
    }

    public async Task AddAsync()
    {
        var order = new Order
        {
            OrderDate = DateTime.Now,
            TotalAmount = 150.75m,
            Status = "Onaylandı",
            Notes = "Hızlı teslimat istendi.",
            CustomerID = 1
        };
        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        int staticID = 1;
        var order = await _appDbContext.Orders.Include(o => o.Customer).FirstOrDefaultAsync(o => o.OrderID == staticID);
        if (order != null)
        {
            order.OrderDate = DateTime.Now.AddDays(1);
            order.TotalAmount = 250.75m;
            order.Status = "Hazırlanıyor";
            order.Notes = "Müşteri tarafından hızlı teslimat istendi.";
            order.CustomerID = 2;
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync()
    {
        int staticID = 1;
        var order = await _appDbContext.Orders.Include(o => o.Customer).FirstOrDefaultAsync(o => o.OrderID == staticID);
        if (order != null)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

Açıklama:

Repository sınıfı, veritabanı işlemlerinin merkezi yeridir.
Include(o => o.Customer): Sipariş ile birlikte müşteri bilgilerini de getirir.
AddAsync, UpdateAsync, DeleteAsync: Statik olarak ID’si 1 olan kayıtlar üzerinde işlem yapılır.
Her işlem await ile çağrıldığından asenkron yapılara uygundur.

3. Durum – MVC Controller Sınıfı (EfCoreJourney.Controllers)

public class OrderWithCustomerStaticController : Controller
{
    private readonly IOrderWithCustomerStaticRepository _orderStaticRepository;

    public OrderWithCustomerStaticController(IOrderWithCustomerStaticRepository orderStaticRepository)
    {
        _orderStaticRepository = orderStaticRepository;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderStaticRepository.GetAllAsync();
        return View(orders);
    }

    public async Task<IActionResult> GetByID()
    {
        var orders = await _orderStaticRepository.GetByIdAsync();
        if (orders == null) return NotFound("Belirtilen ID ile eşleşen sipariş bulunamadı.");
        return View(orders);
    }

    public async Task<IActionResult> AddOrder()
    {
        await _orderStaticRepository.AddAsync();
        TempData["Message"] = "Sipariş başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateOrder()
    {
        await _orderStaticRepository.UpdateAsync();
        TempData["Message"] = "Sipariş başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteOrder()
    {
        await _orderStaticRepository.DeleteAsync();
        TempData["Message"] = "Sipariş başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}

Açıklama:

Controller sınıfı, kullanıcıdan gelen isteklere göre repository çağrılarını yapar.
TempData["Message"]: İşlem sonucu mesajları kullanıcıya gösterilir.
Statik işlem butonları /OrderWithCustomerStatic/AddOrder gibi URL’lerle çağrılır.

4. Durum – Razor View Sayfası (Order Listesi)

@model List<EntityLayer.Concrete.Order>
@{
    ViewData["Title"] = "Sipariş Listesi";
}

<h1 class="mb-4">@ViewData["Title"]</h1>

@if (TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<div class="d-flex gap-2 mb-3">
    <a class="btn btn-success" href="/OrderWithCustomerStatic/AddOrder">Ekle</a>
    <a class="btn btn-warning" href="/OrderWithCustomerStatic/UpdateOrder">Güncelle</a>
    <a class="btn btn-danger" href="/OrderWithCustomerStatic/DeleteOrder">Sil</a>
</div>

<table class="table table-bordered">
    <thead>
        <tr>
            <th>ID</th>
            <th>Ürün Tarihi</th>
            <th>Toplam Tutar</th>
            <th>Durum</th>
            <th>Ürün Not</th>
            <th>Ad-Soyad</th>
            <th>Yaş</th>
            <th>Email</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var order in Model)
        {
            <tr>
                <td>@order.OrderID</td>
                <td>@order.OrderDate</td>
                <td>@order.TotalAmount</td>
                <td>@order.Status</td>
                <td>@order.Notes</td>
                <td>@order.Customer?.FullName</td>
                <td>@order.Customer?.Age</td>
                <td>@order.Customer?.Email</td>
            </tr>
        }
    </tbody>
</table>

Açıklama:

Model: Controller’dan gelen List<Order> listesidir.
@order.Customer?.FullName: Navigation property üzerinden ilişkilendirilmiş müşteri bilgisine erişilir.
Bootstrap sınıflarıyla buton ve tablo düzeni sağlanmıştır.
View, CRUD işlemleri sonrası gelen TempData mesajını kullanıcıya gösterir.

🔚 Özetle:

Bu yapı sayesinde:

Order ve Customer arasında güçlü bir ilişki kurulmuş,
Repository deseniyle veritabanı işlemleri katmanlara ayrılmış,
Controller üzerinden işlemler yönlendirilmiş,
Razor View ile sonuçlar kullanıcıya sunulmuştur.

