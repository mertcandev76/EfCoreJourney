Element - Tekli Veri Getiren Sorgulama Fonksiyonları

🔸 Örnek Sınıf

public class Customer {

[Key]
public int CustomerID { get; set; }
public string Name { get; set; }
public string Email { get; set; }
public string Phone { get; set; }
public bool IsActive { get; set; }

}

Task<Customer> GetAll();  // Asenkron metodun dönüş tipi tekil
Task<List<Customer>> GetAll(); //Asenkron metodun dönüş tipi çoğul

yani biz tekil halde işlem yapacağımızdan Asenkron metodun dönüş tipi tekil işlemini seçiyoruz.


örnek:

        public async Task<Customer> GetAll()
        {

            return await _appDbContext.Customers.FirstAsync();
        }
        
 Not!!!       
Oluştuduğumuz view sayfası bu durumda değişir unutma!!

 @model EntityLayer.Concrete.Customer //tekil

@{
    ViewData["Title"] = "Müşteri Listesi";
}

<h1>@ViewData["Title"]</h1>

<table class="table">
    <thead>
        <tr>
            <th>Ad</th>
            <th>Email</th>
            <th>Telefon</th>
        </tr>
    </thead>
    <tbody>
    //forach döngüsü olmaz tekil olduğu için 
        <tr>
            <td>@Model.Name</td>
            <td>@Model.Email</td>
            <td>@Model.Phone</td>
        </tr>
       
    </tbody>
</table>




