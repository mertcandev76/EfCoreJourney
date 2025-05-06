19-🔹  Union()
LINQ'deki Union metodu, iki koleksiyondaki aynı türdeki verileri birleştirip tekrar edenleri kaldırarak, SQL'deki UNION ifadesi gibi yeni bir koleksiyon döndürür.

Union Kullanım Şartları:

Koleksiyonlar aynı veri tipine sahip olmalıdır.
Eğer özel tip (class) üzerinde Union() yapılacaksa, Equals() ve GetHashCode() override edilmelidir ya da Select ile sade türlere dönüştürülmelidir (örneğin FirstName, Email vs.).

 Basit Örnek: İki string listesini birleştir

 List<string> list1 = new List<string> { "Ali", "Ayşe", "Mert" };
List<string> list2 = new List<string> { "Mert", "Zeynep", "Fatma" };

var result = list1.Union(list2).ToList();

// Sonuç: Ali, Ayşe, Mert, Zeynep, Fatma

Örnek: Customer FirstName'lerini birleştirme

public async Task<List<string?>> GetUnionCustomerNamesAsync()
{
    // 1. Aktif müşterileri al
    var activeNames = await _appDbContext.Customers
        .Where(c => c.IsActive == true && c.FirstName != null)
        .Select(c => c.FirstName)
        .ToListAsync();

    // 2. Pasif müşterileri al
    var inactiveNames = await _appDbContext.Customers
        .Where(c => c.IsActive == false && c.FirstName != null)
        .Select(c => c.FirstName)
        .ToListAsync();

    //Not!!! 3. Union işlemi bellekte yapılır(Union işlemini asenkron olarak yapmak biraz farklıdır çünkü Union() metodu bellek içi (in-memory) bir LINQ işlemi olduğundan veritabanı üzerinde doğrudan UnionAsync() gibi bir metod yoktur.)
    var result = activeNames.Union(inactiveNames).ToList();

    return result;
}
📌 Neden Böyle Yapıyoruz?
EF Core veritabanında Union() desteklese bile, çoğu zaman .Union() ile yazılan sorgular doğrudan SQL'e çevrilemez.
Bu yüzden genelde iki parçayı ayrı ayrı asenkron alıp bellekte birleştirmek daha güvenlidir.

| İşlem                           | Kullanım                   |
| ------------------------------- | -------------------------- |
| Veriyi çekme                    | `ToListAsync()`            |
| Union işlemi (asenkron sonrası) | `Union()`                  |
| Asenkron tüm işlem              | `await + Union + ToList()` |
