✅ 6. Many-to-Many (N-N) — Tek Yönlü İlişki Nedir?

Bu ilişki türünde:

Bir Vendor (Tedarikçi) birden fazla Product (Ürün) sunabilir.
Aynı şekilde bir Product (Ürün) de birden fazla Vendor (Tedarikçi) tarafından sağlanabilir.
Bu karşılıklı çoktan çoğa ilişkiyi (N-N) tek yönlü hale getirdiğimizde, sadece Vendor tarafı üzerinden Product'lara erişim vardır. Product sınıfı ise bu ilişki hakkında hiçbir şey bilmez.

🧱 Entity Sınıflarının Açıklaması

✅ Vendor Sınıfı

public class Vendor
{
    [Key]
    public int VendorId { get; set; }

    public string CompanyName { get; set; }

    public ICollection<VendorProduct> VendorProducts { get; set; } = new List<VendorProduct>();
}

VendorProducts: Bu navigation property sayesinde bir tedarikçinin sahip olduğu ürünlerin listesine ulaşılır.
Bu sınıf ilişkinin sahibi tarafı gibi davranır.

✅ Product Sınıfı

public class Product
{
    [Key]
    public int ProductId { get; set; }

    public string Name { get; set; }
}

Herhangi bir navigation property içermez.
Bu yüzden tek yönlü ilişki olur: Product → Vendor bilgisine ulaşamaz.

✅ VendorProduct Ara Tablosu

public class VendorProduct
{
    public int VendorId { get; set; }
    public Vendor Vendor { get; set; }

    public int ProductId { get; set; }
    // Product navigasyonu isteğe bağlı (tek yönlü)
}

Bu tablo, Many-to-Many ilişkisini temsil eden ara tablodur (junction table).
Hem VendorId hem ProductId birlikte bir bileşik anahtar (Composite Key) olur.
Vendor navigasyon özelliği vardır.
Product navigasyonu yazılmadığı için ilişki tek yönlüdür.

 Avantajları

Tek yönlü ilişkiler, performans veya veri akış kontrolü açısından daha sade ve optimize bir yapı sağlar.
Her iki tarafa navigation property eklememek, yazılımda bazı durumlarda daha az karmaşıklık oluşturur.

📌 Özet

| Özellik                | Açıklama                                 |
| ---------------------- | ---------------------------------------- |
| İlişki Türü            | Many-to-Many (N-N)                       |
| Yön                    | **Tek Yönlü** (Vendor → Product)         |
| Ara Tablo              | VendorProduct                            |
| Navigasyonlar          | Sadece `Vendor` → `Product` ilişkisi var |
| Fluent API Gerekli mi? | Evet, özellikle Composite Key için       |


