🏷️ 3.Yöntem Fluent API ile One-to-One İlişki
Mantık:
EF Core’un OnModelCreating içinde ilişkiyi detaylıca ve kesin biçimde tanımlarız. En sağlam ve esnek yöntemdir.

🎯 Fluent API Kod:
modelBuilder.Entity<Person>()
    .HasOne(p => p.IdentityCard)
    .WithOne(ic => ic.Person)
    .HasForeignKey<IdentityCard>(ic => ic.PersonId);
Bu kod:

Person sınıfının bir IdentityCard’ı olduğunu belirtir.
IdentityCard sadece bir Person ile ilişkilidir.
IdentityCard.PersonId foreign key olarak atanır.

👀 Görsel Hayal Et:
[Person]───(1)────(1)───[IdentityCard]
Her Person sadece 1 IdentityCard'a sahiptir.
Her IdentityCard yalnızca 1 Person'a aittir.