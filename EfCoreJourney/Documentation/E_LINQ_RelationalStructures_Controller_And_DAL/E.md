🧠One-to-Many (1 - N) İlişki Nedir?
Bir entity (sınıf), birden fazla başka entity ile ilişkilidir, ama o diğer entity’ler yalnızca bir entity ile ilişkilidir.

🎯 Gerçek Hayattan Örnek:

Müşteri → Siparişler (Customer - Orders)
Her müşteri birçok sipariş verebilir.
Her sipariş yalnızca bir müşteriye aittir.


[Customer]───(1)────(∞)───[Order]
1. Yöntem: Default Convention (Varsayılan Kurallar)
EF Core, isimlendirme kurallarına göre ilişkiyi otomatik tanır.

Customer.cs
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }

    // 1 Müşterinin birden fazla siparişi olabilir
    public ICollection<Order> Orders { get; set; }
}
Order.cs
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    // Foreign key
    public int CustomerId { get; set; }

    // Sipariş sadece bir müşteriye ait
    public Customer Customer { get; set; }
}
EF Core burada CustomerId'den Customer'a giden ilişkiyi otomatik algılar.

-->Profosyonel Son Hali

public class OrderCustomer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }

    // 1 Müşterinin birden fazla siparişi olabilir
    public ICollection<Order> Orders { get; set; }
}
Order.cs
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    // Foreign key
    public int CustomerId { get; set; }

    // Sipariş sadece bir müşteriye ait
    public OrderCustomer OrderCustomer { get; set; }
}