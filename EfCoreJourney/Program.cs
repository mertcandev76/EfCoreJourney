
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection(Manuel) 
builder.Services.AddScoped<ILogStaticRepository,LogStaticRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

//AppDbContext'i servise ekle(Manuel)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Connection String'i oku(Manuel)
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




// 🔥 Burada ICustomerDal bağlanıyor



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}






app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
