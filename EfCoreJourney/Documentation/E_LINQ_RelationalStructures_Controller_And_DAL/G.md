🏷️ 3. Yöntem: Fluent API ile One-to-Many
İlişkiyi OnModelCreating() içinde programatik olarak tanımlarız.

🔹 DbContext içinde:
modelBuilder.Entity<Order>()
    .HasOne(o => o.Customer)
    .WithMany(c => c.Orders)
    .HasForeignKey(o => o.CustomerId);
Açıklama:

Her Order, bir Customer ile ilişkilidir.
Her Customer, birden fazla Order ile ilişkilidir.
CustomerId foreign key’dir.

