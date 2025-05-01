3-🔹 OrderBy Nedir?
OrderBy, LINQ (Language Integrated Query) içinde kullanılan ve bir koleksiyonun belirli bir özelliğine göre sıralanmasını sağlayan bir metottur. Veriler artan (küçükten büyüğe, A'dan Z'ye,eskiden yeniye ,bool türünde önce false sonra true) olarak sıralanır.

📌 Genel Kullanımı:
var siraliListe = liste.OrderBy(x => x.Özellik);

📌 Entity Framework ile Kullanımı:
return await _appDbContext.Customers
    .OrderBy(x => x.Name)
    .ToListAsync();
Bu örnekte, veritabanındaki Customers tablosu Name alanına göre A'dan Z'ye sıralanır.

4-🔹 OrderByDescending Nedir?
OrderByDescending, LINQ (Language Integrated Query) içinde kullanılan bir metottur ve koleksiyonu veya veritabanı sorgusunu azalan sıraya göre sıralamak için kullanılır.

Yani:

Sayılarda: büyükten küçüğe
Harflerde: Z'den A'ya
Tarihlerde: yeniden eskiye
Boole türlerinde: true önce, false sonra

📌 Temel Kullanımı
var siraliListe = liste.OrderByDescending(x => x.Özellik);

📌 Entity Framework Örneği
return await _appDbContext.Customers
    .OrderByDescending(x => x.CustomerID)
    .ToListAsync();
Müşteriler CustomerID alanına göre en yüksek ID'den en düşük ID'ye sıralanır.



5-🔹 ThenBy Nedir?
ThenBy, LINQ içinde kullanılan bir metottur ve sıralama işlemi yaparken önceki sıralamayı koruyarak, ikinci bir sıralama yapılmasını sağlar. İlk sıralama kriteri, daha önce yapılan OrderBy veya OrderByDescending işlemiyle belirlenir. ThenBy, sıralama sırasında ikinci bir düzeyde sıralama yapmanıza olanak tanır.

📌 Kullanım Senaryosu:
Diyelim ki, bir listeyi adlarına göre sıraladık ve aynı ada sahip öğeleri, yaşlarına göre sıralamak istiyorsunuz. Bu durumda önce adlarına göre sıralama yapar, ardından yaşlarına göre sıralama yapmak için ThenBy kullanırsınız.

📌 Temel Kullanım:
var liste = new List<Customer>
{
    new Customer { Name = "Ali", Age = 30 },
    new Customer { Name = "Veli", Age = 25 },
    new Customer { Name = "Ali", Age = 20 },
};

var siraliListe = liste
    .OrderBy(x => x.Name)  // Adına göre sıralama
    .ThenBy(x => x.Age)    // Yaşına göre sıralama
    .ToList();
Sonuçta, önce "Ali" ve "Veli" isimlerine göre sıralanır, ardından "Ali" ismindeki kişiler yaşa göre küçükten büyüğe sıralanır.

📌 OrderBy ve ThenBy Birlikte Kullanımı:
Örnek olarak, musteriler listesini önce aktiflik durumu sonra ise isimlerine göre sıralayalım:
return await _appDbContext.Customers
    .OrderBy(x => x.IsActive)   // Önce aktifliği sıralar
    .ThenBy(x => x.Name)        // Ardından isme göre sıralar
    .ToListAsync();
Bu örnekte, aktif olanlar önce gelir ve aktif olmayanlar sonra sıralanır. Eğer iki müşteri de aktifse, isimlerine göre sıralama yapılır.

📌 OrderByDescending ve ThenBy Birlikte Kullanımı:
return await _appDbContext.Customers
    .OrderByDescending(x => x.IsActive)   // Önce aktiflik (tersten sıralama)
    .ThenBy(x => x.Name)                  // Sonra isme göre
    .ToListAsync();
Aktif olanlar önce gelir, aktif olmayanlar sonra sıralanır. Eğer ikisi de aktifse, isimlerine göre sıralanır.

📌 ThenBy Kullanımının Önemli Noktaları:
Birden fazla sıralama yapmanıza olanak tanır.
OrderBy bir sıralama işlemi yaptıktan sonra, sonraki sıralamaları ThenBy ile ekleyebilirsiniz.
ThenByDescending ile tersten sıralama da yapabilirsiniz.
