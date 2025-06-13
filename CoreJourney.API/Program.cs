using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//Dependency Injection(Manuel) 
builder.Services.AddScoped<ILogStaticRepository,LogStaticRepository>();

//AppDbContext'i servise ekle(Manuel)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Connection String'i oku(Manuel)
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
