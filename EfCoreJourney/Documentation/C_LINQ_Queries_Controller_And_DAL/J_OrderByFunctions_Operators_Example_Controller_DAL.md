🔹 OrderBy,OrderByDescending ve ThenBy Fonksiyonu ile Operatörlerle Çalışma

 ÖRNEKLER: &&,|| (Mantıksal VE,VEYA) ile Birlikte Karşılaştırma Operatörlerini (==, !=, <, >, <=, >=)

📌 Örnek 1:
Müşterileri isme göre (A'dan Z'ye) sıralayarak listele
return await _appDbContext.Customers
    .OrderBy(x => x.Name)
    .ToListAsync();


📌 Örnek 2:
 Müşterileri isme göre (Z'den A'ya)  sıralayarak listele
return await _appDbContext.Customers
    .OrderByDescending(x => x.Name)
    .ToListAsync();


📌 Örnek 3:
Müşterileri ID'ye göre büyükten küçüğe sıralayarak listele
return await _appDbContext.Customers
    .OrderByDescending(x => x.CustomerID)
    .ToListAsync();


📌 Örnek 4:
 Müşterileri aktif olup olmamasına göre (önce aktifler) sıralayarak listele
return await _appDbContext.Customers
    .OrderByDescending(x => x.IsActive)
    .ToListAsync();



📌 Örnek 5:
Müşterileri e-posta adresinin uzunluğuna göre  (kısadan uzuna) sıralayarak listele
return await _appDbContext.Customers
    .OrderBy(x => x.Email.Length)
    .ToListAsync();



📌 Örnek 6:
Müşterileri e-posta adresi ".com" ile bitenleri önce alacak şekilde sıralayarak listele
return await _appDbContext.Customers
    .OrderByDescending(x => x.Email.EndsWith(".com"))
    .ToListAsync();



📌 Örnek 7:
Müşterileri telefon numarasına göre (artan sıra) sıralayarak listele
return await _appDbContext.Customers
    .OrderBy(x => x.Phone)
    .ToListAsync();


📌 Örnek 8:
Müşterileri adlarının uzunluğuna göre  (uzundan kısaya) sıralayarak listele
return await _appDbContext.Customers
    .OrderByDescending(x => x.Name.Length)
    .ToListAsync();


📌 Örnek 9:
Müşterileri "A" harfi ile başlayanları öne alarak sıralayarak listele
return await _appDbContext.Customers
    .OrderByDescending(x => x.Name.StartsWith("A"))
    .ThenBy(x => x.Name)
    .ToListAsync();


📌 Örnek 10:
 Müşterileri e-posta adreslerinde "gmail" geçenleri önce alacak şekilde sıralayarak listele
return await _appDbContext.Customers
    .OrderByDescending(x => x.Email.Contains("gmail"))
    .ThenBy(x => x.Email)
    .ToListAsync();
