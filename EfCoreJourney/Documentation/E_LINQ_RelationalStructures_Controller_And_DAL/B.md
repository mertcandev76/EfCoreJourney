İlişkiler

 Entity Framework Core'da ilişki kurmanın 3 farklı yöntemi vardır (Default Convention, Data Annotations, Fluent API)
🧠 One-to-One (Birebir) İlişki Nedir?
Bir varlığın (entity) sadece bir başka varlıkla birebir ilişkili olduğu durumdur.
Yani, her iki taraf da sadece birbirine bağlıdır.

🎓 Teknik Tanım (Entity Framework):
Bir entity’nin bir örneği, diğer entity’nin sadece bir örneğiyle eşleşir.
Genellikle bir entity, başka bir entity'ye ait ayrıntılı bilgileri tutmak için kullanılır.

🏠 Gerçek Hayattan Örnek:
👤 Kişi – 🪪 Kimlik Kartı
Her kişinin yalnızca bir kimlik kartı vardır.
Her kimlik kartı yalnızca bir kişiye aittir.
Bu birebir (1-1) ilişkidir.

| Kişi Adı | Kimlik Kartı No |
| -------- | --------------- |
| Ali      | 1234567890      |
| Ayşe     | 9876543210      |


🏷️ 1.Yöntem Default Convention (Varsayılan Kurallar) ile One-to-One İlişki
Mantık:
Eğer sınıflar arasında navigation property’ler düzgün tanımlanırsa ve foreign key ismi EF kurallarına uygunsa, EF Core otomatik olarak ilişkiyi algılar.

💻 Kod Üzerinden Örnek (Entity Framework Core)

🟡 EF Core burada isimlerden ilişkiyi tahmin eder:
IdentityCard.PersonId → Person.PersonId ile bağlıdır.

1️⃣ Person Entity’si:
public class Person
{
    [Key]
    public int PersonID { get; set; }
    public string FullName { get; set; }

    // Navigation Property
    public IdentityCard IdentityCard { get; set; }
}
2️⃣ IdentityCard Entity’si:
public class IdentityCard
{
    [Key]
    public int IdentityCardID { get; set; }
    public string CardNumber { get; set; }

    // Foreign Key
    public int PersonID { get; set; }

    // Navigation Property
    public Person Person { get; set; }
}


