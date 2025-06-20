
✅ 4. One-to-One (1-1) — Tek Yönlü İlişki Nedir?

-->1.Durum
📘 Senaryo: Order ⟶ Payment

Her Order'ın bir Payment kaydı vardır.
Ancak Payment, Order hakkında hiçbir şey bilmez.
Bu yüzden ilişki tek yönlüdür (sadece Order → Payment yönünde navigasyon vardır).

🔎 One-to-One (1-1) Tek Yönlü Nedir?

Her iki tabloda da birebir eşleşen kayıtlar bulunur.
Ancak navigasyon (yön) sadece bir taraftadır.
Yani bir nesne diğerini bilir, ama tersi geçerli değildir.

💡 Neden Tek Yönlü?

Veri erişimi açısından sadece bir taraf üzerinden erişim yeterlidir.

Örneğin:
Sipariş üzerinden ödeme bilgisine erişmek istiyoruz.
Ama ödeme üzerinden siparişi çağırmak zorunda değiliz.

📦 Entity Sınıfları ile Açıklama
🧾 Order (Ana Varlık / Principal Entity)

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public Payment Payment { get; set; } // Navigation Property (Tek yönlü)
}

Order nesnesi, ona ait bir Payment nesnesine ulaşabilir.
Navigation property burada sadece Order içinde tanımlıdır.

💳 Payment (Bağımlı Varlık / Dependent Entity)

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public int OrderId { get; set; } // Foreign Key (Zorunlu)
}

Payment, OrderId bilgisini tutar.
Ama Order nesnesine referans vermez.

🧠 Notlar:

OrderId, Payment tablosunda hem Foreign Key hem de Unique olmalıdır.
EF Core bunu otomatik olarak bir UNIQUE constraint haline getirir.
Payment tablosunda OrderId benzersiz olduğu için bir Order sadece bir Payment ile eşleşebilir.

🎯 Gerçek Hayat Örneği:

Bir alışveriş sitesinde her siparişin tek bir ödeme kaydı vardır.
Ancak ödeme tablosu sipariş detaylarına ihtiyaç duymaz. (Tek yönlü)

✅ Özet

| Özellik            | Açıklama                             |
| ------------------ | ------------------------------------ |
| İlişki Türü        | One-to-One (1-1)                     |
| Yön                | Tek Yönlü (`Order ⟶ Payment`)        |
| Navigasyon         | Sadece `Order` içinde var            |
| Foreign Key        | `Payment.OrderId`                    |
| Yapılandırma Şekli | `HasOne().WithOne().HasForeignKey()` |


Semantik Kurallandırma


public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderPayment Payment { get; set; } // Navigation Property (Tek yönlü)
}

public class OrderPayment
{
    [Key]
    public int OrderPaymentId { get; set; }

    public decimal Amount { get; set; }

    public int OrderId { get; set; } // Foreign Key (Zorunlu)
}


