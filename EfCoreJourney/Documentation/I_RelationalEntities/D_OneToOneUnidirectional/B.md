-->2.Durum

Senaryo:
Her Order (Sipariş) nesnesi tek bir Shipment (Kargo) nesnesi ile ilişkilidir.
Ancak Shipment, Order'ı bilmez (yani navigation property yok).
Veri yönü: Order → Shipment

🧠 İlişki Ne Anlama Geliyor?

| Özellik     | Açıklama                                |
| ----------- | --------------------------------------- |
| İlişki Türü | One-to-One (Bire Bir)                   |
| Yön         | Tek yönlü (`Order` → `Shipment`)        |
| Navigasyon  | Sadece `Order` tarafında navigation var |
| Foreign Key | `Shipment` tablosunda (`OrderId`)       |

Varlık Sınıfları Açıklaması

✅ Order (Ana Tablodur - Principal Entity)
public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public Shipment Shipment { get; set; } // Navigation property
}

Her siparişin bir kargo (shipment) bilgisi olabilir.
Shipment nesnesine sadece Order üzerinden erişilir.
Bu yön tek yönlüdür.

✅ Shipment (Bağımlı Tablodur - Dependent Entity)

public class Shipment
{
    [Key]
    public int ShipmentId { get; set; }

    public DateTime ShippedDate { get; set; }

    public int OrderId { get; set; } // Foreign Key
}

OrderId ile Order tablosuna bağlıdır.
Ancak Order navigation'ı yoktur (tek yönlü ilişkiyi bozmamak için).

Semantik Kurallandırma


public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderShipment Shipment { get; set; } // Navigation property
}


public class OrderShipment
{
    [Key]
    public int OrderShipmentId { get; set; }

    public DateTime ShippedDate { get; set; }

    public int OrderId { get; set; } // Foreign Key
}