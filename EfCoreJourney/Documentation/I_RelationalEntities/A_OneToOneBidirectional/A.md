✅ 1. One-to-One (1-1) — Çift Yönlü İlişki Nedir?

Tanım:

Bir varlığa (Entity) ait tam olarak bir başka varlık varsa ve bu ilişki her iki tarafça da erişilebiliyorsa, bu ilişkiye çift yönlü birebir (1-1) ilişki denir.

🔁 Örnek: Store ⟷ StoreSetting

İki class var:

Store: Mağaza bilgilerini tutar.
StoreSetting: Bu mağazanın ayarlarını tutar (dil, para birimi vb.).

public class Store
{
    [Key]
    public int StoreID { get; set; }

    public string? Name { get; set; }

    public string? OwnerName { get; set; }

    public string? Email { get; set; }

    public StoreSetting Setting { get; set; } = new StoreSetting(); // Navigation Property (1-1)
}

public class StoreSetting
{
    [Key]
    public int StoreSettingID { get; set; }

    public string? Currency { get; set; }

    public string? Language { get; set; }

    public bool EnableNotifications { get; set; }

    public int StoreID { get; set; }  // Foreign Key

    public Store Store { get; set; } = new Store(); // Navigation Property (Çift yönlü erişim)
}

🎯 Bu yapı ile şunları sağlarsın:
Store üzerinden StoreSetting'e gidebilirsin:
store.Setting.Currency gibi.

StoreSetting üzerinden Store bilgilerine gidebilirsin:
setting.Store.Name gibi.

⚙️ Fluent API ile (opsiyonel ama önerilir)
Entity Framework Core'da One-to-One ilişkiyi Fluent API ile net tanımlayabilirsin:

modelBuilder.Entity<Store>()
    .HasOne(s => s.Setting)
    .WithOne(ss => ss.Store)
    .HasForeignKey<StoreSetting>(ss => ss.StoreID);
Bu, EF Core'a "Bu iki entity arasında 1-1 ilişki var, StoreSetting.StoreID dış anahtar olacak" demek.

📘 Semantic Naming (Anlamsal İsimlendirme) Nedir?

Tanım: Kodun anlaşılabilirliğini artırmak için isimlerin amacına göre verilmesine Semantic Naming (Anlamsal İsimlendirme) denir.

Örnek:

Setting yerine StoreSetting: Ne ayarı olduğunu açıkça belirtir.
Name yerine StoreName ya da ProductName: Büyük projelerde daha anlamlıdır.
UserID yerine CreatedByUserId, UpdatedByUserId gibi anlamlı isimler.

✅ Senin Son Halin: Semantic Naming Açısından Başarılı

public class Store
{
    [Key]
    public int StoreID { get; set; }

    public string? Name { get; set; }           // Mağaza Adı
    public string? OwnerName { get; set; }      // Sahip Adı
    public string? Email { get; set; }          // İletişim
    public StoreSetting Setting { get; set; } = new StoreSetting(); // 1-1 ilişki
}

public class StoreSetting
{
    [Key]
    public int StoreSettingID { get; set; }

    public string? Currency { get; set; }       // TL, USD vs.
    public string? Language { get; set; }       // Türkçe, İngilizce vs.
    public bool EnableNotifications { get; set; }

    public int StoreID { get; set; }            // Foreign Key
    public Store Store { get; set; } = new Store(); // Navigation Property
}

 Semantic Naming Kullanımı:

StoreSetting ⇒ Anlamlı ✔️
EnableNotifications ⇒ Anlamlı ✔️
StoreID, OwnerName gibi alanlar açık ve net ✔️

