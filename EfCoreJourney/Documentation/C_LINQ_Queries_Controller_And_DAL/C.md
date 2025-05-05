1-🔹 First() Nedir?
First() metodu, bir koleksiyon (liste, sorgu vb.) içinden şartı sağlayan ilk öğeyi getirir. Eğer hiçbir öğe yoksa
hata fırlatır.

Şartsız Kullanım:
Tüm listeyi getirip ilkini seçer:
 return await _appDbContext.Customers.FirstAsync();

 Şartlı Arama:
 Customers tablosunda isim bilgisi "Hasan" olan ilk müşteri getirilir
  return await _appDbContext.Customers.FirstAsync(e => e.Name == "Hasan");

  Eğer hiç "Hasan" isimli müşteri yoksa:
System.InvalidOperationException: Sequence contains no elements

🧠 First() Ne Zaman Kullanılır?
Sonuçların mutlaka geleceğine emin olduğunuzda kullanılır.
Hata fırlatmasını istediğiniz (örneğin: "bulunmazsa sistem patlasın") senaryolarda tercih edilir.
Veri yoksa sistem durmalı diyorsanız First() mantıklıdır.

peki OrderBy ile birlikte:

ID göre sıralayıp ilk kullanıcıyı getir.
 return await _appDbContext.Customers
                .OrderBy(x=>x.CustomerID)
                .FirstAsync();