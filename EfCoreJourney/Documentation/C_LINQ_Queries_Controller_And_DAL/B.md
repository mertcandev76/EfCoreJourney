1. Nullable ve Nullable Olmayan Değerlerle Filtreleme Yaparken 
ORM sorgularında HasValue veya GetValueOrDefault() gibi fonksiyonlar, aslında genellikle veritabanındaki null değerleri kontrol ederken kullanılabilir, ancak bu metodları kullanmak çoğu zaman gereksizdir. ORM'ler bu kontrolleri derleyici seviyesinde optimize eder ve SQL sorguları veritabanı üzerinde çalışırken nullable değerler için doğru sorguyu oluşturur.

Öğretici Örnekler:

Örnek 1:

-->Bu kullanım doğrudur
public int? Age { get; set; }


var customers = _appDbContext.Customers
    .Where(c => c.Age != null) // null olmayan yaşları filtreleyebilirsiniz
    .ToList();
Burada, Age nullable olduğu için, ORM otomatik olarak SQL'de IS NOT NULL sorgusunu oluşturur.(c.Age != null: Age değeri null olmayan müşterileri filtreler.)
Örnek 2:

-->Bu kullanım doğrudur
public int? Age { get; set; }

var customers = _appDbContext.Customers
    .Where(c => c.Age == null) // null olan yaşları filtreleyebilirsiniz
    .ToList();
(c.Age == null: Age değeri null olan müşterileri filtreler.)

Örnek 3:

-->Bu kullanım yanlıştır çünkü

public int Age { get; set; }
var customers = _appDbContext.Customers
    .Where(c => c.Age == null) // Bu yanlış olur çünkü Age null olamaz.
    .ToList();

Burada, Age nullable olmayan bir int türünde tanımlanmış. Yani, Age değeri her zaman geçerli bir tam sayı olmalıdır ve null olamaz. Bu durumda, Age == null ifadesi hata verir çünkü Age nullable olmadığı için null değerini kabul etmez.


Örnek 4:

-->Bu kullanım yanlıştır çünkü

public int Age { get; set; }
var customers = _appDbContext.Customers
    .Where(c => c.Age != null) // Bu yanlış olur çünkü Age null olamaz.
    .ToList();

Age bir int türü olduğu için null olamaz. Bu nedenle, Age != null koşulunu kullanmak anlamlı değildir.
Age her zaman bir sayısal değere (tam sayıya) sahip olmalıdır. Yani, null kontrolü yapmak mümkün değildir.


şimdi yukarıdaki öğretici örnekleri anladığımıza göre iki tane örnek yapalım:


Pekiştirmeli Örnekler

public int? Age { get; set; }

Örnek 1:

Yaşı 20'den küçük olanları ve null olanları almak
return await _appDbContext.Customers
    .Where(c => c.Age < 20 || c.Age == null) // Yaşı 20'den küçük ve null olanları filtreler
    .ToListAsync();


Örnek 2:
Yaşı 20'den küçük olanları ve null olmayan olanları almak

1.adım     
.Where(c => c.Age < 20 || c.Age != null) // Yaşı 20'den küçük ve null olmayanları filtreler
    .ToListAsync();

2.adım
.Where(c => c.Age < 20) // Yaşı 20'den küçük ve null olmayanları filtreler
    .ToListAsync();

1.adım gereksiz kod yazımı olmuştur çünkü  Zaten Age > 20 demek, Age != null anlamına da gelir (null bir değerle > karşılaştırması yapılamaz).













