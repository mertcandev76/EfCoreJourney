🧠 Principal Entity Nedir?

Genellikle birincil anahtarına (Primary Key) sahip olan,Diğer entity’lerle ilişkilerde asıl taraf olan,Silinirse,ona bağlı 
(dependent) veriler isteğe göre silinebilir (Cascade Delete), Fluent API veya Data Annotation ile ilişki 
tanımlandığında "HasOne", "WithMany" gibi tanımlarla belirtilen entity'dir.

👨‍💼 Örnek: Customer ve Order İlişkisi

Eğer her müşteri birden fazla sipariş verebiliyorsa (1 Customer -> N Orders), o zaman:
Customer ➤ Principal Entity
Order ➤ Dependent Entity

🧠 Dependent Entity Nedir? 
Başka bir sınıfa (entity'ye) bağlı olan, yani onunla ilişkili olan ve bu ilişkiyi sağlamak için bir Foreign Key (FK) içeren entity’dir.

public class Customer//Principal Entity
{
    [Key]
    public int CustomerID { get; set; }//Principal Key
    public string? Name { get; set; }
}

public class Order //Dependent Entity
{
    [Key]
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }

    // Foreign Key - Dependent Entity özelliği
    public int CustomerID { get; set; }

}
🔗 Foreign Key (Yabancı Anahtar) Nedir?
Foreign Key, bir veritabanı tablosunda başka bir tablodaki Primary Key (birincil anahtar) değerine referans veren alandır.
Yani, bir tablo diğer tabloyla ilişkilendirilecekse, bu ilişki Foreign Key üzerinden kurulur.

Navigation Property Nedir? (C# / Entity Framework Core)
Navigation Property, Entity Framework'te iki entity (sınıf) arasındaki ilişkiyi temsil eden ve ilgili nesneye veya nesneler koleksiyonuna erişimi sağlayan özelliktir.

🧠 Basit Tanım:
İlişkili diğer tabloya kolayca ulaşmanı sağlayan köprüdür.

📌 Navigation Property'nin Amacı:
Entity’ler (sınıflar) arasındaki ilişkiyi yansıtmak
Include() ile ilgili veriyi çekmek için kullanılır
Foreign Key alanına karşılık gelir
Koddan veritabanı ilişkilerini yönetmeni sağlar.

📦 Örnek: Customer ve Order
Customer sınıfı (1 müşteri, çok sipariş)
public class Customer
{
    [Key]
    public int CustomerID { get; set; }
    public string Name { get; set; }

    // 🔄 Navigation Property (1'e çok ilişki)
    public ICollection<Order> Orders { get; set; }
}
Order sınıfı (her sipariş 1 müşteriye ait)
public class Order
{
    [Key]
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }

    // Foreign Key
    public int CustomerID { get; set; }

    // 🔄 Navigation Property (çoktan 1 ilişki)
    public Customer Customer { get; set; }
}

--->Yukarıdaki Yapıyı anladığımıza Göre İsimlendirme Şeklini aşağıdaki tablolara göre profosyonelleştirildi

public class OrderCustomer
{
    [Key]
    public int CustomerID { get; set; }
    public string Name { get; set; }

    // 🔄 Navigation Property (1'e çok ilişki)
    public ICollection<Order> Orders { get; set; }
}
Order sınıfı (her sipariş 1 müşteriye ait)
public class Order
{
    [Key]
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }

    // Foreign Key
    public int CustomerID { get; set; }

    // 🔄 Navigation Property (çoktan 1 ilişki)
    public OrderCustomer OrderCustomer { get; set; }
}

-->Bu isimlendime Şekli en yaygın kullanılır