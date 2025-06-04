✅ GroupBy Nedir?
Benzer verileri gruplamak demektir.
🎯 Gerçek Hayattan Örnek
Bir sınıfta öğrenciler var, hepsinin bulunduğu şehir bilgisi var:
| Öğrenci | Şehir    |
| ------- | -------- |
| Ali     | Ankara   |
| Ayşe    | İstanbul |
| Mehmet  | Ankara   |
| Zeynep  | İzmir    |
| Veli    | Ankara   |

Şimdi bu verileri şehre göre gruplayalım.

Gruplar şöyle olur:

Ankara → Ali, Mehmet, Veli (3 kişi)
İstanbul → Ayşe (1 kişi)
İzmir → Zeynep (1 kişi)
Yani: Aynı şehirde olanları bir araya topladık → işte bu GroupBy demek!

🧠 Kod Örneği (LINQ)

var grup = await _appDbContext.Customers
    .GroupBy(x => x.Address) // Adrese göre grupla
    .Select(g => new
    {
        Sehir = g.Key,        // Grup adı (örneğin Ankara)
        KisiSayisi = g.Count() // O şehirde kaç kişi var?
    })
    .ToListAsync();
Bu sorgu:
Hangi şehirde kaç müşteri var? sorusuna cevap verir.

👀 Daha da Basit Anlatım:
🎒 GroupBy = "Benzerleri aynı kutuya koy"
Örneğin: Müşterileri yaşına göre grupla
Yani yaşları aynı olanları aynı gruba al!

🔁 Sonuç:
| Yaş | Kişi Sayısı |
| --- | ----------- |
| 25  | 5 kişi      |
| 30  | 2 kişi      |
| 40  | 1 kişi      |

✅ ÖRNEK 1: Müşterileri yaşa göre grupla
Hangi yaşta kaç müşteri var?
public async Task<List<object>> GetCustomerCountByAgeAsync()
{
    var result = await _appDbContext.Customers
        .GroupBy(c => c.Age)
        .Select(g => new
        {
            Yas = g.Key,
            KisiSayisi = g.Count()
        })
        .ToListAsync();

    return result.Cast<object>().ToList();
}
🔎 Örnek çıktı:
Yaş = 25, Kişi Sayısı = 3
Yaş = 30, Kişi Sayısı = 5

✅ÖRNEK 2: Müşterileri aktif/pasif durumuna göre grupla
Kaç kişi aktif, kaç kişi pasif?
public async Task<List<object>> GetCustomerCountByIsActiveAsync()
{
    var result = await _appDbContext.Customers
        .GroupBy(c => c.IsActive)
        .Select(g => new
        {
            Durum = g.Key == true ? "Aktif" : "Pasif",
            KisiSayisi = g.Count()
        })
        .ToListAsync();

    return result.Cast<object>().ToList();
}
🔎 Örnek çıktı:
Durum = Aktif, Kişi Sayısı = 6  
Durum = Pasif, Kişi Sayısı = 2

✅ ÖRNEK 3: Müşterileri şehir (adres) bilgisine göre grupla
Hangi şehirde kaç müşteri var?
public async Task<List<object>> GetCustomerCountByCityAsync()
{
    var result = await _appDbContext.Customers
        .GroupBy(c => c.Address)
        .Select(g => new
        {
            Sehir = g.Key,
            KisiSayisi = g.Count()
        })
        .ToListAsync();

    return result.Cast<object>().ToList();
}
🔎 Örnek çıktı:
Şehir = Ankara, Kişi Sayısı = 4  
Şehir = İstanbul, Kişi Sayısı = 3  
Şehir = null, Kişi Sayısı = 1
💡 null şehir: adresi girilmemiş müşterileri gösterir.

Örneklerde verildiği gibi;
🔍 g.Key Nedir?
GroupBy(c => c.Address)
dediğimizde, biz aslında şunu diyoruz:
"Müşterileri Address değerine göre grupla."
Bu durumda:
Aynı Address (örneğin "Ankara") olan müşteriler aynı grupta toplanır.
Bu grupların adres bilgisini (yani grup başlığını) ifade etmek için g.Key kullanılır

🎯 Yani:
g.Key
= Grupladığın değer
= Biz bu örnekte Address üzerinden grupladığımız için
= g.Key, her grubun şehir (adres) adını tutar.
🔁 Örnek:
| CustomerID | Address  |
| ---------- | -------- |
| 1          | Ankara   |
| 2          | İstanbul |
| 3          | Ankara   |

Sen şu sorguyu yazarsın:
O zaman şöyle 2 grup oluşur:

Grup 1: g.Key = "Ankara" → içinde 2 müşteri
Grup 2: g.Key = "İstanbul" → içinde 1 müşteri

Dolayısıyla:

.Select(g => new
{
    Sehir = g.Key, // yani grup adı: "Ankara" veya "İstanbul"
    KisiSayisi = g.Count()
})

📌 Tam Çıktı Ne Olur?
[
  { "Sehir": "Ankara", "KisiSayisi": 2 },
  { "Sehir": "İstanbul", "KisiSayisi": 1 }
]

🔑 Kısaca:
g = Grup
g.Key = O grubun grup değeri (sen Address üzerinden gruplayınca bu şehir olur)
g.Count() = O gruptaki müşteri sayısı


