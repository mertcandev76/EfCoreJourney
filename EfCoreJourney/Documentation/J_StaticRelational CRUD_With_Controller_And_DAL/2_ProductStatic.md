-->Product

1. Durum: Product Entity Tanımı

public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }

    // Foreign Key
    public int ProductBrandID { get; set; }

    // Navigation Property (Product -> ProductBrand ilişkisi)
    public ProductBrand? ProductBrand { get; set; }

    // Diğer ilişkiler
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public ICollection<VendorProduct>? VendorProducts { get; set; }
}

Açıklama:

ProductBrandID: ProductBrand tablosuna ait Foreign Key'dir.
ProductBrand: Navigation Property'dir, Include() ile erişilebilir.
Bu yapı sayesinde EF Core Product nesnesine karşılık gelen markayı ProductBrand üzerinden bulabilir.

2. Durum: Repository Katmanı

📁 DataAccessLayer.Repositories -> ProductWithProductBrandStaticRepository.cs
✔️ GetAllAsync()
Tüm ürünleri ve ilişkili markaları getirir.

public async Task<List<Product>> GetAllAsync()
{
    var product = await _appDbContext.Products
        .Include(p => p.ProductBrand)
        .ToListAsync();
    return product;
}
Include(p => p.ProductBrand) ile ProductBrand verisi de çekilir.


✔️ GetByIdAsync()

Belirli bir ürün ID’si ile (örnek: 1002) veri getirir.

public async Task<Product?> GetByIdAsync()
{
    int staticID = 1002;
    var product = await _appDbContext.Products
        .Include(p => p.ProductBrand)
        .FirstOrDefaultAsync(p => p.ProductID == staticID);
    return product;
}

✔️ AddAsync()

Yeni bir ürün ekler. ProductBrandID verilmiştir, dolayısıyla marka zaten mevcut varsayılır.

public async Task AddAsync()
{
    var product = new Product
    {
        Name = "Samsung Galaxy S21",
        Description = "Yüksek performanslı, 128GB depolama kapasiteli akıllı telefon.",
        Price = 7499.99m,
        Stock = 50,
        IsActive = true,
        ProductBrandID = 1 // marka zaten var
    };
    await _appDbContext.Products.AddAsync(product);
    await _appDbContext.SaveChangesAsync();
}

✅ Marka daha önce veritabanında tanımlı olmalı.

✔️ UpdateAsync() → İKİ YÖNTEM:
📌 1. Yol: Navigation Property üzerinden güncelleme (product.ProductBrand)

if (product.ProductBrand != null)
{
    product.ProductBrand.Name = "Apple";
    product.ProductBrand.Description = "Dünyaca ünlü teknoloji ve elektronik ürünler markası.";
    product.ProductBrand.Country = "Amerika Birleşik Devletleri";
}

❗ Bu yol, ürünle birlikte marka verisini de değiştirir. Ancak:
Markayı doğrudan değiştirmek yerine, var olan markanın bilgileri güncellenir.
Eğer bu marka birden fazla üründe kullanılıyorsa, tüm ürünler etkilenir!

📌 2. Yol: ProductBrandID alanını değiştirerek başka bir markaya bağlama

product.ProductBrandID = 1;
✅ Bu yöntem sadece markanın ID’sini değiştirir, başka ürünleri etkilemez.



✔️ DeleteAsync()

Ürün ID’si ile (örnek: 1003) ürünü ve ilişkili marka nesnesini de getirip sadece ürünü siler:

public async Task DeleteAsync()
{
    int staticID = 1003;
    var product = await _appDbContext.Products
        .Include(p => p.ProductBrand)
        .FirstOrDefaultAsync(p => p.ProductID == staticID);
    if (product != null)
    {
        _appDbContext.Products.Remove(product);
        await _appDbContext.SaveChangesAsync();
    }
}

⚠ ProductBrand silinmez çünkü EF Core cascade delete tanımı yapılmadıysa sadece Product silinir.

3. Durum: Controller Katmanı

📁 EfCoreJourney.Controllers -> ProductWithProductBrandStaticController.cs

public async Task<IActionResult> Index()
{
    var products = await _productWithProductBrandStaticRepository.GetAllAsync();
    return View(products);
}

Açıklamalar:

AddProduct, UpdateProduct, DeleteProduct gibi actionlar static ID'ler ile çalışıyor.
TempData ile View'a işlem sonucunu mesaj olarak iletiyor.
View'da bu mesajlar alert olarak gösteriliyor.

4. Durum: Razor View (Listeleme)

📁 Views/ProductWithProductBrandStatic/Index.cshtml

@model List<EntityLayer.Concrete.Product>

HTML Yapısı:

ViewData["Title"]: Başlık
TempData["Message"]: Son işlem mesajı
Ürünler tablo olarak listeleniyor

@product.ProductBrand?.Name: Null olabilir, bu yüzden ?. kullanılıyor

<td>@product.ProductBrand?.Name</td>
<td>@product.ProductBrand?.Description</td>
<td>@product.ProductBrand?.Country</td>



