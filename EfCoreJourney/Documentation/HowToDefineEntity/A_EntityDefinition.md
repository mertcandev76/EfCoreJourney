💡 Entity Tanımlanması nasıl yapılır?

Entity, genellikle bir uygulamada veri modelini temsil eden ve veritabanı ile etkileşimde bulunan bir sınıfı
ifade eder. ASP.NET Core'da Entity Framework (EF) kullanarak entity sınıfları tanımlanır ve veritabanı ile 
çalışmak için bu sınıflar üzerinden işlemler yapılır. Entity sınıfı, veritabanındaki tablolara karşılık gelir.

Entity Tanımlaması Adımları:
1-Entity Sınıfı Oluşturma Entity,
basit bir sınıf olarak tanımlanır. Bu sınıfın içinde, veritabanındaki  tablonu sütunlarıyla eşleşen özellikler (property) yer alır.

2-DbContext Sınıfı DbContext,
EF ile veritabanı bağlantısını sağlayan sınıftır. Entity sınıfları, DbContext içinde tanımlanır. DbContext,
veritabanı işlemleri (CRUD) yapmak için kullanılır.
