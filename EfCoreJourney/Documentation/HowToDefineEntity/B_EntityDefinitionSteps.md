1-Entity Tanımlama Adımları

Entity tanımlaması, bir uygulamanın veri modelini oluşturmak için temel adımlardan biridir. ORM (Object-Relational Mapping) kullanarak, veritabanındaki tablolara karşılık gelen sınıfları tanımlamak, uygulamanın verilerle nasıl etkileşime gireceğini belirler. İşte entity tanımlamanın adımları:

1. Entity Sınıfını Oluşturma

Entity sınıfını oluşturmak, ORM (Object-Relational Mapping) yaklaşımında veritabanındaki tablolara karşılık gelen C# sınıflarını oluşturmak anlamına gelir. Bu sınıflar, veritabanındaki veriyle doğrudan etkileşime girecek ve nesneler üzerinden verilerin işlenmesini sağlayacaktır.

1.1.Entity Sınıfını Oluşturma Adımları:

1.1.1. Entity Sınıfının Tanımlanması
Entity sınıfı, veritabanındaki bir tabloyu temsil edecek bir C# sınıfıdır. Her bir property (özellik), veritabanındaki bir sütuna karşılık gelir. Bu sınıfın genellikle public olması gerekir, çünkü dışarıdan erişilebilir olmalıdır.

Örneğin, bir Müşteri sınıfı:

public class Customer
{
    public int Id { get; set; }        // Tablo sütunu  Id, müşteri tablosunun primary key'idir.
    public string Name { get; set; }   // Tablo sütunu Müşteri ismi
    public string Email { get; set; }  // Tablo sütunu Müşteri e-posta adresi
    public string Phone { get; set; }  // Tablo sütunuMüşteri telefon numarası
}

Bu örnekte, Customer sınıfı, veritabanındaki bir "Customers" tablosuna karşılık gelir.


1.1.2. Veri Tiplerinin Seçimi
Entity sınıfındaki her property, veritabanındaki karşılık gelen sütunun(kolonun) veri tipiyle uyumlu olmalıdır. Örneğin, veritabanında string olarak tanımlanan bir sütun, C# tarafında string veri tipiyle temsil edilir.

Bazı yaygın veri tipi eşleşmeleri:

int → int

string → nvarchar, varchar

DateTime → datetime, date

decimal → decimal

bool → bit

1.1.3.Veritabanı Kolonlarına Karşılık Gelen Özellikler

Entity sınıfındaki her özellik, veritabanındaki bir kolona karşılık gelir. Her özellik için uygun bir veri türü seçmek önemlidir. Örneğin, int türü sayısal değerleri, string türü metin verilerini, DateTime türü ise tarih-saat bilgilerini saklar.


1.1.4. Primary Key(Birincil Anahtar) Tanımlaması
Her entity sınıfında, veritabanındaki tablonun birincil anahtarını (primary key) temsil etmek önemlidir. Genellikle, Id adında bir property kullanılır. Bu property, veritabanındaki ilgili tablonun primary key'ine karşılık gelir.

Eğer Id dışında bir alan primary key olarak kullanılacaksa, bunu Key özelliği ile belirtebilirsiniz.
public class Customer
{
    [Key] // Primary Key olarak kullanılacak property
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

Not!!

Şuanlık bu üç adım temel seviye için yeterlidir
ilerleyen konulara geçtiğmizde:

1.1.5.Foreign Key (Yabancı Anahtar) Tanımlaması
1.1.6.Navigation Properties (Gezinme Özellikleri)
1.1.7.Veritabanı İlişkilerini Belirleme (Relationships)
1.1.8.Veritabanı Kuralları ve Veri Doğrulama
1.1.9.Data Annotations veya Fluent API Kullanımı(Opsiyonel)
1.1.10.Veritabanı İndeksleri ve Kısıtlamaları
1.1.11.Entity'nin Diğer Özelliklerini Tanımlama

geriye kalan bu konularıda ele alıcağız.