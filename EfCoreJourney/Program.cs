
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
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


// Generic yapı bağımlılıklarını tanımla

builder.Services.AddScoped<IOrderDetailService, OrderDetailManager>();
builder.Services.AddScoped<IOrderDetailRepository, EfOrderDetailRepository>();

builder.Services.AddScoped<IOrderPaymentService, OrderPaymentManager>();
builder.Services.AddScoped<IOrderPaymentRepository, EfOrderPaymentRepository>();

builder.Services.AddScoped<IOrderShipmentService, OrderShipmentManager>();
builder.Services.AddScoped<IOrderShipmentRepository, EfOrderShipmentRepository>();

builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();

builder.Services.AddScoped<ICustomerCouponService, CustomerCouponManager>();
builder.Services.AddScoped<ICustomerCouponRepository, EfCustomerCouponRepository>();

builder.Services.AddScoped<IVendorProductService, VendorProductManager>();
builder.Services.AddScoped<IVendorProductRepository, EfVendorProductRepository>();

builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<IProductRepository,EfProductRepository>();

builder.Services.AddScoped<IStoreSettingService, StoreSettingManager>();
builder.Services.AddScoped<IStoreSettingRepository, EfStoreSettingRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));






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
