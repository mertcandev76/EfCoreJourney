1-Sabit Değerlerle Veri Kullanımı

Sabit değerler, kod içinde önceden belirlediğimiz, değişmeyen değerlerdir. Bu değerler, genellikle test amaçlı veya geçici bir çözüm olarak kullanılır. Örneğin, kullanıcıdan herhangi bir giriş almadan, doğrudan belirli verileri veritabanına eklemek için kullanılabilir.
Sabit verileri kullanırken, veritabanına eklediğimiz, güncellediğimiz ya da sildiğimiz veriler önceden belirlenmiş ve değişmeyen verilerdir. Bu genellikle test aşamasında işe yarar, çünkü veritabanına veri eklemek veya güncellemek için hızlıca sabit değerler kullanılır.

Örnek (Sabit Veri Kullanımı):

public void Insert(Customer customer)
{
    // Sabit değerler atanıyor
    customer.Name = "Lorem Ipsum";
    customer.Email = "loremipsum@example.com";
    customer.Phone = "1234567890";

    _appDbContext.Customers.Add(customer);
    _appDbContext.SaveChanges();
}

Burada, Insert metodunda müşteri eklerken, sabit olarak belirlediğimiz değerleri kullanıyoruz. Bu, her zaman aynı verileri ekler. Örneğin, her zaman "Lorem Ipsum" adı, "loremipsum@example.com" e-postası ve "1234567890" telefonu veritabanına kaydeder.

Sabit Değerlerin Avantajları:
Testler İçin Faydalı: Sabit veriler, yazılım geliştirme aşamasında test yaparken çok yararlıdır. Veritabanını doldurmak için kolayca kullanılabilir.
Hızlı Çözüm: Veritabanına veri eklerken hemen değerler belirlenebilir, test ortamı için hızlıca veri eklemek mümkündür.

Sabit Değerlerin Dezavantajları:
Gerçek Kullanıcı Verisiyle Bağlantısı Yok: Gerçek dünyadaki veriyle çalışmıyorsun, bu da programının gerçek hayattaki kullanımını simüle etmekte zorluklar çıkarabilir.
Esneklik Yok: Sabit verilerle çalışmak, kullanıcıya özel verilerle çalışmak kadar esnek değildir.




2-Kullanıcıdan Alınan Verilerle Veri Kullanımı

Kullanıcıdan alınan veriler, genellikle web formlarından, API çağrılarından veya başka herhangi bir dış kaynaktan gelen dinamik verilerdir. Bu veriler kullanıcı tarafından sağlanır ve gerçek dünyadaki kullanım senaryolarını yansıtır.
Kullanıcıdan veri almak, kullanıcının form aracılığıyla (veya API üzerinden) gönderdiği verileri işlemek anlamına gelir. Bu veriler, dinamik olarak değişir ve her bir kullanıcı farklı veri sağlayabilir.

Örnek (Kullanıcıdan Alınan Veri Kullanımı):

public void Insert(Customer customer)
{
    // Burada kullanıcıdan gelen veri kullanılıyor
    _appDbContext.Customers.Add(customer); // Gelen 'customer' nesnesi doğrudan ekleniyor
    _appDbContext.SaveChanges();
}

Burada, Insert metodunda sabit bir değer kullanılmıyor. Kullanıcıdan alınan customer nesnesi doğrudan veritabanına ekleniyor. Kullanıcı formu gönderdiğinde, her bir formda farklı bilgiler (isim, e-posta, telefon) olabilir.

Kullanıcıdan Alınan Verilerin Avantajları:
Dinamik ve Gerçekçi: Kullanıcı verileri ile çalışmak, uygulamanın gerçek dünyadaki kullanımını daha iyi simüle eder. Her kullanıcı farklı bilgiler sunar, bu da uygulamanın gerçek kullanım senaryolarına daha uygun olmasını sağlar.
Esneklik: Kullanıcıdan alınan veriler, uygulamanın daha esnek olmasını sağlar. Sabit verilerle çalışmak zorunda kalmazsınız ve veritabanına dinamik olarak veri ekleyebilirsiniz.

Kullanıcıdan Alınan Verilerin Dezavantajları:
Veri Doğrulama Gereksinimi: Kullanıcıdan gelen veriler her zaman doğru olmayabilir. Örneğin, bir kullanıcı yanlış e-posta girebilir veya telefon numarası formatı hatalı olabilir. Bu nedenle, veri doğrulama ve temizlik işlemleri gereklidir.
Güvenlik: Kullanıcıdan alınan veriler, potansiyel olarak tehlikeli olabilir (örneğin SQL injection saldırıları). Bu yüzden veri güvenliği önemlidir.