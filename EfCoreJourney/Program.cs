using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Connection String'i oku
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// AppDbContext'i servise ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 🔥 Burada ICustomerDal bağlanıyor
builder.Services.AddScoped<ICustomerDal, EfCustomerRepository>();

builder.Services.AddScoped<ICustomerConversion_and_Filtering_Dal, EfCustomerConversion_and_Filtering_Repository>();

builder.Services.AddScoped<ICustomerProjection_Dal, EfCustomerProjection_Repository>();


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
