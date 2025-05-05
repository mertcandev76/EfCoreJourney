LINQ fonksiyonu kullanılarak Tekil Sorgu Örnekleri-Element

📌Örnek 1:
İlk adı "Hasan" olan müşteri kimdir?
return await _appDbContext.Customers.FirstAsync(x=>x.FirstName=="Hasan");

📌Örnek 2:
İlk adı "Ayşe" olan müşteri varsa getir, yoksa null döndür.
return await _appDbContext.Customers.FirstOrDefaultAsync(x=>x.FirstName=="Ayşe");

📌Örnek 3:
Tek bir e-posta adresine sahip olan müşteri kimdir? (Eminsek yalnızca bir kişi olmasından)
return await _appDbContext.Customers.SingleAsync(x=>x.Email== "infoayseyilmaz@domain.com");

📌Örnek 4:
İlk adı "Ceyda" olan kişi varsa getir, yoksa null döndür.
 return await _appDbContext.Customers.FirstOrDefaultAsync(x => x.FirstName == "Ceyda");

📌Örnek 5:
Id'si 3 olan müşteri kimdir? (Primary Key üzerinden arama)?
return await _appDbContext.Customers.FirstAsync(x=>x.FirstName=="Hasan"); return await _appDbContext.Customers.FindAsync(3);
📌Örnek 6:
En son eklenen "Mehmet" adlı müşteri kimdir? (LastAsync)
return await _appDbContext.Customers
                .OrderBy(x => x.CustomerID)
                .LastAsync(x=>x.FirstName=="Mehmet");

📌Örnek 7:
Soyadı "Çelik" olan son müşteriyi getir (yoksa null)
 return await _appDbContext.Customers
                .OrderBy(x => x.CustomerID)
                .LastAsync(x=>x.LastName=="Çeli

📌Örnek 8:
 Adı "Ceyda" ve soyadı "Adsız" olan tek müşteriyi getir (tek olduğundan eminsek).
 return await _appDbContext.Customers
                .SingleAsync(x=>x.FirstName=="Ceyda"&&x.LastName=="Adsız");



