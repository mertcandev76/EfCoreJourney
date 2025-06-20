
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




//Dependency Injection(Manuel) 

builder.Services.AddScoped<ICustomerWithOrdersStaticRepository, CustomerWithOrdersStaticRepository>();

builder.Services.AddScoped<IStoreSettingWithStoreStaticRepository, StoreSettingWithStoreStaticRepository>();
builder.Services.AddScoped<IStoreWithStoreSettingStaticRepository, StoreWithStoreSettingStaticRepository>();
builder.Services.AddScoped<IOrderShipmentWithOrderStaticRepository, OrderShipmentWithOrderStaticRepository>();
builder.Services.AddScoped<IOrderPaymentWithOrderStaticRepository, OrderPaymentWithOrderStaticRepository>();
builder.Services.AddScoped<IOrderDetailWithOrderAndProductStaticRepository, OrderDetailWithOrderAndProductStaticRepository>();
builder.Services.AddScoped<IOrderWithCustomerStaticRepository, OrderWithCustomerStaticRepository>();
builder.Services.AddScoped<ICustomerCouponWithCustomerAndCouponStaticRepository, CustomerCouponWithCustomerAndCouponStaticRepository>();
builder.Services.AddScoped<ICustomerStaticRepository,CustomerStaticRepository>();
builder.Services.AddScoped<ICouponStaticRepository, CouponStaticRepository>();
builder.Services.AddScoped<IVendorProductWithProductAndProductVendorStaticRepository, VendorProductWithProductAndProductVendorStaticRepository>();
builder.Services.AddScoped<IProductWithProductBrandStaticRepository, ProductWithProductBrandStaticRepository>();
builder.Services.AddScoped<IProductVendorStaticRepository,ProductVendorStaticRepository>();
builder.Services.AddScoped<IProductBrandStaticRepository, ProductBrandStaticRepository>();
builder.Services.AddScoped<ILogStaticRepository, LogStaticRepository>();


//AppDbContext'i servise ekle(Manuel)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Connection String'i oku(Manuel)
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


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
