🧠 GroupBy Çeşitleri ve Örnekleri

🔹 1. Tek Alan Üzerinden Gruplama
_groupBy(c => c.City)
➡️ Aynı şehirdeki kayıtlar birlikte gruplanır.

🔹 2. Birden Fazla Alanla Gruplama
.GroupBy(c => new { c.FirstName, c.LastName })
➡️ Ad-soyad aynı olanlar tek grupta.

🔹 3. Koşullu Gruplama (Şartlı Gruplama)
.GroupBy(x => 
    x.Age <= 25 ? "Genç" : 
    x.Age <= 50 ? "Orta" : "Yaşlı")
➡️ Yaş aralığına göre "Genç", "Orta", "Yaşlı" diye gruplar.

🔹 4. Null/Boş Durumlara Göre Gruplama
.GroupBy(x => string.IsNullOrEmpty(x.Email))
➡️ E-postası olanlar ve olmayanlar diye iki grup.

🔹 5. Boolean Alan Üzerinden Gruplama
.GroupBy(x => x.IsActive)
➡️ Aktif ve pasif kayıtlar diye gruplar.

🔹 6. Tarihe Göre Gruplama
.GroupBy(o => o.OrderDate.Year)
➡️ Siparişleri yıllara göre gruplar.

🔹 7. Tarih Parçalama (Yıl, Ay, Gün)
.GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
➡️ Yıl ve aya göre gruplar.

🔹 8. İlk Harfe Göre Gruplama
.GroupBy(x => x.FirstName[0])
➡️ Adın ilk harfine göre A-Z grupları oluşur.

🔹 9. Aralıklarla Gruplama (Örn: yaş grupları)
.GroupBy(x => 
    x.Age < 18 ? "Çocuk" :
    x.Age < 30 ? "Genç" :
    x.Age < 60 ? "Yetişkin" : "Yaşlı")
➡️ Belirli yaş aralıklarıyla kategorilendirme.

🔄 GroupBy + Select Kullanımı
Genelde GroupBy kullanınca ardından .Select(...) ile şu yapılır:
.GroupBy(x => x.City)
.Select(g => new {
    City = g.Key,
    Count = g.Count(),
    Customers = g.ToList()
})
🧱 DTO İle Kullanımı
.Select(g => new GroupByCityDto {
    City = g.Key,
    Count = g.Count()
})

📌 Önemli Notlar
GroupBy içinde new { ... } kullanırsan .Key.AlanAdı şeklinde erişirsin.
EF Core GroupBy sorgularını SQL’e çevirebilir (ama bazı şartlı gruplamalar sadece ToListAsync() sonrası bellekte çalışır).
Performans için filtrelemeyi .Where() ile GroupBy’dan önce yap.