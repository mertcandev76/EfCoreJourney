2. Nullable ve Nullable Olmayan Değerlerle Filtreleme ile Hesaplama Yaparken (Average, Sum, vb.)
AverageAsync, SumAsync, MinAsync, MaxAsync gibi metodlar, nullable değerlerle doğal olarak çalışır ve null olan değerleri göz ardı ederler. Bu, genellikle HasValue veya GetValueOrDefault() kullanmanıza gerek olmadığı anlamına gelir. ORM, null değerlerin etkisini otomatik olarak hesaplamada göz ardı eder.


Not!!

public async Task<decimal?> GetAverageAgeAsync()
{
    return (decimal?)await ....
}

(return await) değil


Örneğin:
// `Age` nullable ve null olan değerler otomatik olarak dikkate alınmaz

asıl yazımı şudur ama kod gereksizliğine sebep oluyor.
public async Task<decimal?> GetAverageAgeAsync()
{
    return (decimal?)await _appDbContext.Customers
        .Where(c => c.Age != null)
        .AverageAsync(c => c.Age); 
}
onun için kod fazlağını önlemek için aşağıdaki yöntem kullanılır!!!


public async Task<decimal?> GetAverageAgeAsync()
{
    return (decimal?)await _appDbContext.Customers
        .AverageAsync(c => c.Age);  // nullable olduğu için null'lar göz ardı edilir.
}
Yukarıdaki örnekte, Age nullable olduğu için AverageAsync otomatik olarak null değerleri göz ardı eder ve ortalama hesaplamasını yapar.


!!!Unutma null olan değerlerin ortalması hesaplanmaz çünkü
eğer yapmış olsakdık aşağıdaki şekilde olucaktı ama kod çalışmaz 

public async Task<decimal?> GetAverageAgeAsync()
{
    return await _appDbContext.Customers
        .Where(c => c.Age == null)
        .AverageAsync(c => c.Age); 
}
yani neden  bilinmeyenin ortalamasını hesaplayamayız.
