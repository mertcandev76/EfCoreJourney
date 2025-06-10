🏷️ 2.Yöntem Data Annotations (Varsayılan Kurallar) ile One-to-One İlişki
Mantık:
Attribute’lerle (özellikle [ForeignKey]) EF’e birebir ilişkiyi açıkça belirtiriz.

1.Yol
🎯 Örnek:
Person.cs
public class Person
{
    [Key]
    public int PersonId { get; set; }
    public string FullName { get; set; }

    public IdentityCard IdentityCard { get; set; }
}
IdentityCard.cs
public class IdentityCard
{
    [Key]
    public int IdentityCardId { get; set; }
    public string CardNumber { get; set; }

    [ForeignKey("Person")]
    public int PersonId { get; set; }

    public Person Person { get; set; }
}

2.Yol

🎯 Örnek:
Person.cs
public class Person
{
    [Key]
    public int PersonId { get; set; }
    public string FullName { get; set; }

    public IdentityCard IdentityCard { get; set; }
}
IdentityCard.cs
public class IdentityCard
{
    [Key]
    [ForeignKey("Person")]
    public int IdentityCardId { get; set; }
    public string CardNumber { get; set; }
    public Person Person { get; set; }
}

🧠 Açıklama:
[Key] → IdentityCardId hem primary key olur.
[ForeignKey(nameof(Person))] → aynı zamanda bu alan, Person navigation property'si ile ilişkilidir.
Bu, EF Core’a “Hem anahtar hem de bu nesneye referans!” anlamına gelir.
Bu durumda IdentityCardId ile PersonId aynı değer olur. Bu tek bir ortak anahtar olarak çalışır.

🔍 Not:
Bu yapı, EF Core’da Shared Primary Key Association olarak geçer.
Yani IdentityCard entity’si, Person entity’sinin bir uzantısı gibi davranır.

-->Profosyonel Son Hali

public class IdentityCardPerson
{
    [Key]
    public int PersonId { get; set; }
    public string FullName { get; set; }

    public IdentityCard IdentityCard { get; set; }
}

public class IdentityCard
{
    [Key]
    public int IdentityCardId { get; set; }
    public string CardNumber { get; set; }

    [ForeignKey("Person")]
    public int PersonId { get; set; }

    public IdentityCardPerson IdentityCardPerson { get; set; }
}