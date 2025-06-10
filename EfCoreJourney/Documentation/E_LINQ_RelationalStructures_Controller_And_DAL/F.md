🏷️ 2. Yöntem: Data Annotations ile One-to-Many
Attribute kullanarak ilişkiyi açıkça tanımlarız.

🔹 Customer.cs
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
(2.yol ile)
🔹 Order.cs
public class Order
{
    [Key]
    [ForeignKey("Customer")]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public Customer Customer { get; set; }
}
[ForeignKey("Customer")] ile ilişki doğrudan tanımlanmış olur.