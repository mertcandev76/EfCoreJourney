✅ 2. One-to-Many (1-n) — Çift Yönlü İlişki Nedir?

-->1.Durum

Bu iki sınıf (Customer ve Order) arasında bir müşterinin birçok siparişi olabileceği bir ilişki tanımlanmıştır. Buna Entity Framework Core'da One-to-Many (1-N) ilişki denir.

Customer Sınıfı:
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public string FullName { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

CustomerId: Bu, müşterinin benzersiz ID’si. [Key] attribute'u bunun birincil anahtar olduğunu belirtir.
FullName: Müşterinin tam adı.
Orders: ICollection<Order> türünde bir liste. Bu, bir müşterinin birden fazla siparişi olabileceğini belirtir (1-M ilişkinin "çok" tarafı).

Order Sınıfı:
public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}

OrderId: Siparişin benzersiz ID’si.
OrderDate: Siparişin tarihi.
CustomerId: Bu, ilişkisel anahtardır (foreign key). Hangi müşteriye ait olduğunu belirtir.
Customer: Navigation property. Bu siparişin hangi müşteriye ait olduğunu belirtir (N-1 ilişkinin "bir" tarafı).

🔗 İlişki (Relationship)
"1 müşteri → N sipariş"
Bu ilişkide EF Core:

Customer.Orders ile müşterinin tüm siparişlerine erişmeni sağlar.
Order.Customer ile siparişin hangi müşteriye ait olduğunu gösterir.
CustomerId, Order tablosunda bir foreign key’dir.

Semantic Naming'e Uygun mu?

-->Hayır,ama iki tablo da başka tablolar arasında ilişki yapmışsa yani;

| İlişki Türü | Tablolar              | Açıklama                |
| ----------- | -----------------------| ----------------------- |
| 1-N Çift    | Customer ⟷ Order      | Customer → Orders       |
| N-N Çift    | Customer ⟷ Coupon     | Join: CustomerCoupon    |
| 1-N Çift    | Order ⟷ OrderDetail   | Order → OrderDetails    |
| 1-1 Tek     | Order ⟶ Payment       | Sadece Order → Payment  |
| 1-1 Tek     | Order ⟶ Shipment      | Sadece Order → Shipment |

Yani gözüktüğü gibi Customer ve Order Tablosu hem aralarında ilşki olmasına rağmen diğer tablolarlada ilişkili onun için Tablo 
isimleri baştan doğrudur.

Eğer Customer ve Order sadece kendi aralarında ilişki tablosu olsaydı o zaman isim değişikliği yani semantik kurallarına uyması gerekir?

Customer Sınıfı:
public class OrderCustomer
{
    [Key]
    public int OrderCustomerId { get; set; }

    public string FullName { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}


Order Sınıfı:
public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int OrderCustomerId { get; set; }
    public OrderCustomer Customer { get; set; }
}








