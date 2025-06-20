✅ 5. One-to-Many (1-N) — Tek Yönlü İlişki Nedir?

One-to-Many (1-N) ilişkisi, bir varlığın (entity), birden fazla başka varlıkla ilişkili olduğunu ifade eder.
"One" taraf: Bir adet kayıt barındırır (örnek: bir marka).
"Many" taraf: Birden çok kayıt barındırır (örnek: bir markaya ait birçok ürün olabilir).

👈 Tek Yönlü Ne Demek?

"Tek yönlü ilişki", sadece bir sınıfın diğerini bildiği ama karşı tarafın onu bilmediği ilişki tipidir.

Yani:

Brand sınıfı, Product listesini içeriyor (⟶ Products navigation property’si var). Ancak Product sınıfı Brand’i tanımıyor (⛔ navigation property yok).

🧱 Kod Üzerinden Anlatım
✅ Brand Sınıfı (1 tarafı)

public class Brand
{
    [Key]
    public int BrandId { get; set; }

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}

BrandId: Primary Key.
Products: Bu markaya ait birden çok Product olabilir. Navigation property’dir.

✅ Product Sınıfı (N tarafı)

public class Product
{
    [Key]
    public int ProductId { get; set; }

    public string Name { get; set; }

    public int BrandId { get; set; }
}

BrandId: Foreign Key — Hangi markaya ait olduğunu gösterir.
Brand isminde navigation property yok — bu yüzden tek yönlü diyoruz.

🎯 Örnek Senaryo

Brand: Apple
Product'lar: iPhone, MacBook, iPad
Apple markasına ait 3 ürün olacak. Ama Product sınıfı üzerinden Apple bilgisine ulaşamayız (çünkü navigation property yok).

🔧 Veritabanı İlişkisi
EF Core bu yapıyı otomatik olarak şöyle anlar:

Brand (1) ─────< (N) Product

Yani:

Brand.BrandId ⟶ Product.BrandId

Bir marka birden çok ürüne sahip olabilir.
Ancak bir ürün sadece bir markaya aittir.

📌 Avantajlar ve Dezavantajlar
✅ Avantajlar
Daha basit yapı.

Sadece bir yönden ilişki kurulması gerekiyorsa yeterlidir.
Performans açısından daha hafiftir (tek taraflı takip).

❌ Dezavantajlar

Product tarafında Brand bilgisi gerektiğinde Include() ile navigation sağlanamaz.
Okunabilirlik ve veri erişimi tek taraflıdır.

🔍 Genişletmek İstersek (Çift Yönlü Yapmak)
Product içine şu şekilde navigation property eklersek ilişki çift yönlü olur:

public Brand Brand { get; set; }

🧠 Özet

| Özellik             | Açıklama                           |
| ------------------- | ---------------------------------- |
| İlişki Türü         | One-to-Many (1-N)                  |
| Yön                 | Tek Yönlü                          |
| Ana Sınıf           | `Brand` (1 tarafı)                 |
| Alt Sınıf           | `Product` (N tarafı)               |
| Foreign Key         | `Product.BrandId`                  |
| Navigation Property | Sadece `Brand.Products` içinde var |



Semantik Kurallandırma


public class ProductBrand
{
    [Key]
    public int BrandId { get; set; }

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    [Key]
    public int ProductId { get; set; }

    public string Name { get; set; }

    public int BrandId { get; set; }
}