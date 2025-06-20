-->2.Durum

🎯 Senaryomuz: Sipariş ve Sipariş Detayları
Bir e-ticaret sisteminde:

1 sipariş (Order) içerisinde birden fazla ürün (OrderDetail) olabilir.

Her bir OrderDetail, sadece tek bir Order’a aittir.

🧱 Entity Tanımları

✅ Order Sınıfı (1 Tarafı)

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    // 1 siparişin 1'den fazla detay kaydı olabilir
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

OrderId: Anahtar (Primary Key)
OrderDetails: Bir siparişin birden fazla detayını tutar (navigation property)

✅ OrderDetail Sınıfı (N Tarafı)

public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }

    public int Quantity { get; set; }

    // Foreign Key
    public int OrderId { get; set; }

    // Navigation Property
    public Order Order { get; set; }
}

OrderDetailId: Anahtar
OrderId: Hangi siparişe ait olduğunu belirten yabancı anahtar (foreign key)
Order: Sipariş nesnesine erişmek için navigation property

🔁 Çift Yönlü Yapı Ne Sağlar?

Order → OrderDetails ile bir siparişteki tüm ürünleri listeleyebilirsin.
OrderDetail → Order ile bir detay kaydının hangi siparişe ait olduğunu görebilirsin.

🧠 Entity Framework Ne Yapar?

Entity Framework bu tanımları görünce otomatik olarak:
OrderDetail.OrderId’yi foreign key olarak kabul eder.
Order ile OrderDetail arasında 1-N ilişki oluşturur.
Gerekirse migration sırasında ilişkisel tablo yapısını (FK constraint) kurar.

🗂️ Veritabanında Ne Olur?
OrderDetail tablosunda OrderId sütunu olur.
Bu sütun Order.OrderId'ye foreign key olarak bağlı olur.