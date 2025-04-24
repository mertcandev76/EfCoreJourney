 ID’ye göre kayıt çekme işlemi

 1. Adım: ICustomerDal Arayüzüne GetById Metodu Eklendi

public interface ICustomerDal
{
    Customer GetById(int id);
}
✍️ Açıklama:

ICustomerDal artık sadece veri ekleme değil, belirli bir müşteriyi ID’si ile bulma işlevini de destekliyor.
Bu metodun amacı, Customer tablosundaki belirli bir CustomerID'ye sahip müşteriyi veritabanından getirmektir.
Interface olduğundan burada sadece imzası yazılır. Metodun nasıl çalışacağı EfCustomerRepository içinde tanımlanır.

2. Adım: EfCustomerRepository İçinde GetById Metodu Tanımlandı

public Customer GetById(int id)
{
    return _appDbContext.Customers.FirstOrDefault(c => c.CustomerID == id);
}
✍️ Açıklama:
Bu metod, ICustomerDal arayüzünde belirtilen GetById metodunun uygulamasıdır.
AppDbContext.Customers: EF Core üzerinden Customer tablosuna erişir.
FirstOrDefault(...): Verilen şarta uyan ilk müşteriyi getirir. Eğer yoksa null döner.

🔍 Kullanılan LINQ Açıklaması:

c => c.CustomerID == id
Bu, her bir Customer nesnesini temsil eden c üzerinden, CustomerID değeri id parametresine eşit olanı bul anlamına gelir.

🧠 Neden FirstOrDefault?
Eğer eşleşen müşteri bulunamazsa null döndürmesi istenir.
First() kullanılsaydı, eşleşen kayıt yoksa hata fırlatırdı.

📌 Genel Notlar

Terim | Açıklama
Interface | Sınıflara ne yapacaklarını söyler ama nasıl yapacaklarını söylemez.
Repository | Veritabanı işlemleri burada tanımlanır (Get, Insert, Delete, vb.).
Dependency Injection | AppDbContext dışarıdan alınır, bu sayede test edilebilir ve yönetimi kolay olur.
LINQ (Language Integrated Query) | EF Core’da koleksiyonlara sorgu yazmak için kullanılır.



🔁 Akış Senaryosu
Güncelle,silme vb. işlemlerde kullanılır.












