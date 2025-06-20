✅ 3. Many-to-Many (n-n) — Çift Yönlü İlişki Nedir?

🔗 Many-to-Many (N-N) — Çift Yönlü İlişki Nedir?

Birçok müşterinin (Customer) birçok kuponu (Coupon) olabilir ve bir kupon birden fazla müşteri tarafından kullanılabilir.

Bu durum Many-to-Many ilişkidir.

❗️Örnek:

Ahmet ve Ayşe adında 2 müşteri var.
"KUPON50" ve "KUPON100" adında 2 kupon var.
Ahmet her iki kuponu da kullandı, Ayşe sadece "KUPON50"yi kullandı.

Bu ilişkiyi doğrudan Customer ile Coupon sınıflarına yazamayız çünkü iki tabloyu birbirine bağlayacak bir ara tabloya (join entity) ihtiyaç var.

💡 Kod Üzerinden Açıklama
1️- Customer Sınıfı
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public string FullName { get; set; }

    public ICollection<CustomerCoupon> CustomerCoupons { get; set; } = new List<CustomerCoupon>();
}

CustomerId: Birincil anahtar.
FullName: Müşteri adı.
CustomerCoupons: Bu müşteri hangi kuponları kullanmış, ara tablo üzerinden erişiyoruz.

2️- Coupon Sınıfı

public class Coupon
{
    [Key]
    public int CouponId { get; set; }

    public string Code { get; set; }

    public ICollection<CustomerCoupon> CustomerCoupons { get; set; } = new List<CustomerCoupon>();
}

CouponId: Kuponun birincil anahtarı.
Code: Kupon kodu (örneğin: KUPON50).
CustomerCoupons: Bu kuponu hangi müşteriler kullanmış, yine ara tablo üzerinden erişiyoruz.

3-Ara Tablo: CustomerCoupon (Join Entity)
public class CustomerCoupon
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int CouponId { get; set; }
    public Coupon Coupon { get; set; }
}

Bu sınıf hem Customer hem de Coupon ile birebir (1-1) ilişkiler kurar ama aslında toplamda Many-to-Many ilişkide köprü görevi görür:
CustomerId, CouponId: Birlikte birleşik birincil anahtar gibi çalışır.
Customer ve Coupon: Navigation property’lerdir.