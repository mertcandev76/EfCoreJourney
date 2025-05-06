Nullable ve Nullable Olmayan Tipler

Nullable Nedir?
 bir değişkenin null (boş) bir değer alabilmesi durumudur. Bu terim, özellikle değer türleri (value types) ile ilgili kullanılır. C# gibi dillerde, değer türleri varsayılan olarak null değeri almazlar, ancak nullable türler, bu türlerin null almasını sağlayan bir özellik sunar.

1-Nullable Olmayan Tipler(Null değil)
Nullable olmayan tipler, değerlerinin her zaman bir şeyler olması gereken veri türleridir. Örneğin, int, bool, string gibi temel veri türleri. Bu türler için bir değişken oluşturduğunuzda, her zaman geçerli bir değer olmalıdır; aksi takdirde, hatalarla karşılaşabilirsiniz.

public class Customer
{
    [Key]
    public int CustomerID { get; set; } // Nullable olmayan bir tip (int)
    public string FirstName { get; set; } // Nullable olmayan bir tip (string)
    public string LastName { get; set; } // Nullable olmayan bir tip (string)
    public int Age { get; set; } // Nullable olmayan bir tip (int)
    public string Email { get; set; } // Nullable olmayan bir tip (string)
    public string Phone { get; set; } // Nullable olmayan bir tip (string)
    public string Address { get; set; } // Nullable olmayan bir tip (string)
    public bool IsActive { get; set; } // Nullable olmayan bir tip (bool)
}
Burada, Customer sınıfındaki çoğu özellik, nullable olmayan tipler olarak tanımlanmıştır. Örneğin, CustomerID bir int olduğu için her zaman bir sayı değeri (örneğin 0 veya başka bir sayı) almalıdır. FirstName, LastName, Email, Phone, Address gibi string türündeki özellikler ise boş bir değer (null) alamaz.

2-Nullable Tipler(Null)
Nullable tipler, bir türün değerinin null olabilmesine olanak tanır. Nullable tipler için C#’ta ? işareti kullanılır. Yani, bir değerin hem geçerli bir değer alabileceği hem de null olabileceği durumlar için nullable türler kullanılır. Örneğin, int?, bool?, DateTime? gibi tipler nullable’dır.

public class Customer
{
    [Key]
    public int CustomerID { get; set; } // Nullable olmayan bir tip
    public string FirstName { get; set; } // Nullable olmayan bir tip
    public string LastName { get; set; } // Nullable olmayan bir tip
    public int? Age { get; set; } // Nullable olan bir tip (int?)
    public string Email { get; set; } // Nullable olmayan bir tip
    public string Phone { get; set; } // Nullable olmayan bir tip
    public string Address { get; set; } // Nullable olmayan bir tip
    public bool IsActive { get; set; } // Nullable olmayan bir tip
}
Bu örnekte, Age özelliği int? (nullable int) türünde tanımlanmış. Bu, Age değerinin null olabileceği anlamına gelir. Yani, bir müşteri kaydı için Age belirtilmemişse, bu özellik null olarak kabul edilebilir.

Nullable ve Nullable Olmayan Tipler Arasındaki Farklar

Değer Atama: Nullable olmayan tipler, her zaman geçerli bir değer almalıdır. Nullable tipler ise hem geçerli bir değer alabilir hem de null olabilir.

Veri Tabanı: ORM (Object-Relational Mapping) sistemlerinde (örneğin Entity Framework) nullable olmayan tipler için veri tabanındaki alanlar "NOT NULL" olarak tanımlanırken, nullable tipler için "NULL" değeri kabul edilebilir.

Kullanım Durumu: Nullable tipler, örneğin bir veri kaydının değeri eksik olduğunda, bilinmediğinde veya geçici olarak atanmadığında kullanılır. Nullable olmayan tipler, her zaman geçerli bir değer gerektirir.

