📌 OrderBy,OrderByDescending Fonksiyonları Where ile Nasıl Birlikte Kullanılır:
Örnekleme

📌 Örnek 1:
Önce aktif  müşterileri al ardından isme göre sıralayarak listele
 return await _appDbContext.Customers
                .Where(x=>x.IsActive)
                .OrderBy(x=>x.Name)
               .ToListAsync();

📌 Örnek 2:
İsmine göre tersten sıralama ardından aktif olanları  sıralayarak listele
 return await _appDbContext.Customers
               .OrderByDescending(x => x.Name)
               .Where(x=>x.IsActive)         
               .ToListAsync();


📌 Örnek 3:ID 5'den büyük olan müşterileri al ve isimlerine göre  sırayalarak listele
 return await _appDbContext.Customers
                .Where(x=>x.CustomerID>5)
                .OrderBy(x=>x.Name)
               .ToListAsync();

📌 Performans Farkı:
Genellikle Where koşulunu OrderBy öncesinde kullanmak daha verimli olabilir, çünkü veritabanı yalnızca gerekli verileri çeker ve sıralama işlemi daha küçük bir veri kümesi üzerinde yapılır. Bu şekilde daha optimize bir sorgu elde edersiniz.

📌 Özet:
Where ile veriyi filtreleyebilir, sonra OrderBy ile sıralama yapabilirsiniz.
OrderBy sonrasında da Where kullanılabilir, ancak bu genellikle performans açısından daha verimsiz olabilir.
Her iki metot birlikte sıklıkla kullanılır ve çok yaygın bir LINQ kullanım senaryosudur.