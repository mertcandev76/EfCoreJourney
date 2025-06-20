-->3.Durum

Tanım:

Bir Product (ürün), birçok OrderDetail (sipariş detayı) ile ilişkilidir.
Bir OrderDetail, sadece bir ürüne aittir.
Bu ilişki 1 ürün – N sipariş detayı şeklindedir.
Çift yönlü olması, her iki entity’nin de birbirine erişebileceği anlamına gelir.

📦 Entity Sınıfları Açıklaması
✅ Product Sınıfı (1 Tarafı)

public class Product
{
    [Key]
    public int ProductId { get; set; }

    public string Name { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

Açıklamalar:

ProductId: Ürünün benzersiz anahtarı (Primary Key).
Name: Ürünün adı.
OrderDetails: Bu ürünle ilişkili birden fazla sipariş detayını tutan navigation property.
ICollection<OrderDetail>: Bir ürünün birçok sipariş detayı olabilir. Yani 1 → N ilişkisi buradan kurulur.

✅ OrderDetail Sınıfı (N Tarafı)

public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}

Açıklamalar:

OrderDetailId: Sipariş detayının benzersiz anahtarı.
ProductId: Yabancı anahtar (Foreign Key). Bu siparişin hangi ürüne ait olduğunu belirtir.
Product: Bu sipariş detayının hangi ürüne ait olduğunu gösteren navigation property.

🔗 İlişkinin Oluşumu

EF Core, bu yapıya bakarak otomatik olarak bir foreign key oluşturur:
OrderDetail.ProductId → Product.ProductId
Bu ilişki sayesinde:
product.OrderDetails: Ürüne ait tüm sipariş detaylarını getirir.
orderDetail.Product: Sipariş detayına ait ürünü getirir.

🧠 Neden Çift Yönlü?

Çift yönlü ilişki, hem Product üzerinden OrderDetail'lara,
hem de OrderDetail üzerinden Product'a erişmeni sağlar.

Bu, özellikle API yazarken veya Entity'den veri çekerken çok kullanışlıdır.