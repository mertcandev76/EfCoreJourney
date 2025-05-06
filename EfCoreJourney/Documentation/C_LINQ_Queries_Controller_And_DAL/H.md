20-🔹  Intersect()
iki koleksiyonda ortak olan (aynı) elemanları getirir. Yani hem A kümesinde hem B kümesinde olanları seçer.

✅ Örnek Senaryo Oluşturma

Amacımız:
Customer tablosundaki verilerden:

Liste: Aktif olan müşteriler
Liste: 20 yaşından büyük müşteriler

Bu iki listenin kesişimi: Aktif ve 20 yaşından büyük olan müşteriler

Temel Intersect() Kullanımı

Yanlış Kullanım

public async Task<List<int?>> GetUnionCustomerNamesAsync()
{
  var activeCustomers = _appDbContext.Customers
    .Where(x => x.IsActive == true)
    .ToList();

var ageAbove20Customers = _appDbContext.Customers
    .Where(x => x.Age > 20)
    .ToList();

var intersected = activeCustomers.Intersect(ageAbove20Customers).ToList();

    return intersected;
}


Kullanımı :
public async Task<List<int?>> GetUnionCustomerNamesAsync()
{
    // Aktif müşteriler
    var activeCustomers = await _appDbContext.Customers
        .Where(x => x.IsActive == true)
        .ToListAsync();

    // Yaşı 20'den büyük olan müşteriler
    var ageAbove20Customers = await _appDbContext.Customers
        .Where(x => x.Age > 20)
        .ToListAsync();

    // İki listede de olan müşterilerin CustomerID'lerini almak
    var intersected = activeCustomers
        .Select(x => x.CustomerID)  // Yalnızca CustomerID'leri alıyoruz
        .Intersect(ageAbove20Customers.Select(x => x.CustomerID))  // Yaşı 20'den büyük müşterilerin CustomerID'leri ile kesişimi alıyoruz
        .ToList();  // Sonuçları liste olarak döndürüyoruz

    return intersected;
}
