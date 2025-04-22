using DataAccessLayer.Concrete;
using Microsoft.Extensions.Configuration;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")  // appsettings.json dosyas�n� ekliyoruz.
    .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");  // ConnectionStrings i�inden DefaultConnection'� al�yoruz.

using (var context = new AppDbContext(connectionString))  // DbContext'e ba�lant� bilgisini veriyoruz.
{
    var customers = context.Customers.ToList();  // Customers tablosundaki t�m verileri �ekiyoruz.
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
