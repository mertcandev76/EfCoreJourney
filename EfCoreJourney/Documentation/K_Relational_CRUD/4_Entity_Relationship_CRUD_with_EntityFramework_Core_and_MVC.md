-->Repository-pattern (Depo Deseni) ve Service-layer (Servis Katmanı) ile Asenkron CRUD İşlemlerine Özel Metotlar Ekleyerek Çalışma

DataAccessLayer Abstract (Interface)

namespace DataAccessLayer.Abstract 
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        // Özel metotlar
        Task<List<OrderDetail>> GetAllWithOrderandProductAsync();
        Task<OrderDetail?> GetByIdWithOrderandProductAsync(int id);
    }
}

Ne yapıyor?
OrderDetail için generic repository IRepository<T>'den türetilmiş özel repository arayüzü.

İçerisinde OrderDetail nesnesiyle ilişkili Order ve Product nesnelerini dahil ederek (include) getiren iki asenkron özel metot tanımlanmış.

Burada sadece metod imzaları var, implementasyon yok.

DataAccessLayer Repositories (Implementation)

namespace DataAccessLayer.Repositories
{
    public class EfOrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public EfOrderDetailRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<OrderDetail>> GetAllWithOrderandProductAsync()
        {
            return await Table
                .Include(oD => oD.Order)
                .Include(oD => oD.Product)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetByIdWithOrderandProductAsync(int id)
        {
            return await Table
                .Include(oD => oD.Order)
                .Include(oD => oD.Product)
                .FirstOrDefaultAsync(oD => oD.OrderDetailID == id);
        }
    }
}

Ne yapıyor?
IOrderDetailRepository arayüzünü Entity Framework Core tabanlı somut repository olarak implemente ediyor.

GenericRepository<OrderDetail>'den kalıtım alıyor (CRUD metotları orada var) ve sadece ekstra ilişkisel Include içeren metotları burada yazıyor.

Table muhtemelen DbSet<OrderDetail>'in alias'ı.Include metodu, EF Core ile ilişkili tabloların yüklenmesini sağlar (Eager Loading).
Böylece, OrderDetail ile birlikte ilişkili Order ve Product nesneleri de çekiliyor.

BusinessLayer Abstract (Interface)

namespace BusinessLayer.Abstract
{
    public interface IOrderDetailService : IGenericService<OrderDetail>
    {
        // Özel metotlar
        Task<List<OrderDetail>> GetAllWithOrderandProductAsync();
        Task<OrderDetail?> GetByIdWithOrderandProductAsync(int id);
    }
}

Ne yapıyor?

İş katmanında OrderDetail için generic servis arayüzünden türetilmiş özel servis arayüzü.
Repository'deki özel metotlar burada da aynen servis arayüzüne eklenmiş.
Bu katmanda genelde iş kuralları (business logic) uygulanır. Burada sadece repository'den gelen metotlar aynen aktarılmış.

BusinessLayer Concrete (Implementation)

namespace BusinessLayer.Concrete
{
    public class OrderDetailManager : GenericManager<OrderDetail>, IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository;

        public OrderDetailManager(IOrderDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderDetail>> GetAllWithOrderandProductAsync() 
            => await _repository.GetAllWithOrderandProductAsync();

        public async Task<OrderDetail?> GetByIdWithOrderandProductAsync(int id) 
            => await _repository.GetByIdWithOrderandProductAsync(id);
    }
}

Ne yapıyor?

IOrderDetailService'i implemente eden iş katmanı somut sınıfı.
IOrderDetailRepository nesnesini dependency injection ile alıyor (injection) ve generic manager (muhtemelen CRUD işlemleri içerir) sınıfından kalıtım alıyor.
Repository'deki özel metotları burada servis metodu olarak kapsülleyip çağırıyor.
Bu yapı, iş katmanında repository işlemlerini servisler üzerinden kullanmayı sağlar.



OrderDetailController’ın İşleyişi

Controller, MVC modelinde “Sipariş Detayları” üzerinde CRUD işlemlerini (Listeleme, Ekleme, Güncelleme, Silme, Detay Görüntüleme) yapıyor.

Önemli noktalar:
Controller’da 3 servis var:

_orderDetailService: Sipariş detaylarıyla ilgili işlemler.
_orderService: Siparişlerle ilgili işlemler.
_productService: Ürünlerle ilgili işlemler.

Servisler genellikle Entity Framework Core veya Repository Pattern altında asenkron çalışır.

View’larda dropdown (açılır liste) göstermek için SelectList kullanılıyor.

Metodlar ve Çalışma Şekilleri:

a) Index (Listeleme)

public async Task<IActionResult> Index()
{
    var orderDetails = await _orderDetailService.GetAllWithOrderandProductAsync();
    return View(orderDetails);
}

Sipariş detaylarını hem sipariş bilgisi hem ürün bilgisi ile birlikte çekiyor.

Sonra View’a gönderiyor.
Kullanıcıya tabloda tüm sipariş detayları listeleniyor.

b) Create (Yeni Sipariş Detayı Ekleme)

[HttpGet] Create(): Formu gösterir.
await LoadSelectListsAsync(); // Sipariş ve ürün dropdownlarını hazırla
return View();

[HttpPost] Create(OrderDetail orderDetail): Formdan gelen veriyi alır.

if (ModelState.IsValid)
{
    await _orderDetailService.AddAsync(orderDetail); // Veritabanına ekle
    return RedirectToAction(nameof(Index));
}
await LoadSelectListsAsync(); // Eğer hata varsa dropdownları tekrar yükle
return View(orderDetail);

Kullanıcı yeni bir sipariş detayı ekler.
Siparişler ve ürünler dropdown’dan seçilir.
Geçerli ise veri kaydedilir, değilse form tekrar gösterilir.

c) Edit (Düzenleme)

[HttpGet] Edit(int id): İlgili sipariş detayını bul ve formu doldur.

var orderDetails = await _orderDetailService.GetByIdWithOrderandProductAsync(id);
if (orderDetails == null) return NotFound();
await LoadSelectListsAsync(orderDetails.OrderId, orderDetails.ProductID);
return View(orderDetails);

[HttpPost] Edit(OrderDetail orderDetail): Formdan gelen güncelleme verisini al ve kaydet.


if (ModelState.IsValid)
{
    await _orderDetailService.UpdateAsync(orderDetail);
    return RedirectToAction(nameof(Index));
}
await LoadSelectListsAsync(orderDetail.OrderId, orderDetail.ProductID);
return View(orderDetail);

d) Delete (Silme)

[HttpGet] Delete(int id): Silinecek veriyi göster, onay al.

var orderDetails = await _orderDetailService.GetByIdWithOrderandProductAsync(id);
if (orderDetails == null) return NotFound();
return View(orderDetails);

[HttpPost, ActionName("Delete")] DeleteConfirmed(int id): Silme işlemini yap.

var orderDetails = await _orderDetailService.GetByIdAsync(id);
if (orderDetails != null)
{
    await _orderDetailService.DeleteAsync(orderDetails);
}
return RedirectToAction(nameof(Index));

e) Details (Detay Görüntüleme)

public async Task<IActionResult> Details(int id)
{
    var orderDetails = await _orderDetailService.GetByIdWithOrderandProductAsync(id);
    if (orderDetails == null) return NotFound();
    return View(orderDetails);
}

f) Yardımcı Metod: LoadSelectListsAsync

private async Task LoadSelectListsAsync(int? selectedOrderId = null, int? selectedProductId = null)
{
    var orders = await _orderService.GetAllAsync();
    var products = await _productService.GetAllAsync();

    ViewBag.Orders = orders?.Select(o => new SelectListItem
    {
        Value = o.OrderID.ToString(),
        Text = o.OrderDate.ToString("dd.MM.yyyy") // Tarih formatı
    }).ToList();

    ViewBag.Products = products?.Select(p => new SelectListItem
    {
        Value = p.ProductID.ToString(),
        Text = $"{p.Name} - {p.Description}"
    }).ToList();
}

Sipariş ve ürünlerin dropdown listelerini hazırlar.
View’da kolay seçim yapılmasını sağlar.

Özet ve Sonuç

Model tarafında Order, Product ve OrderDetail arasındaki ilişkiler çok iyi tanımlanmış (bire çok, birden çoğa, bire bire).
Controller’da CRUD işlemleri async servisler ile temiz ve profesyonel şekilde uygulanmış.
Sipariş detayları listeleniyor, yeni detay eklenebiliyor, var olan detay güncellenip silinebiliyor.
Dropdown listelerle kullanıcı seçim kolaylığı sağlanmış.
Her işlem sonrası kullanıcı uygun sayfaya yönlendirilip işlem durumu kontrol ediliyor.
Bu yapı ASP.NET Core MVC + Entity Framework Core için güzel, standart, pratik ve sürdürülebilir bir çözüm.

View Örnekelerinİ incelersin.


Product gibi aşağıdaki sınıflarda ilişkisel mzel metotlarda controller tarafında aynı mantıkta işleniyor.
->VendorProduct
->CustomerCoupon
->Order
->OrderShipment
->OrderPayment
->OrderDetail
->StoreSetting


