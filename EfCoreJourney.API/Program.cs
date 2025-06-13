using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Dependency Injection(Manuel) 
builder.Services.AddScoped<ILogStaticRepository, LogStaticRepository>();


//AppDbContext'i servise ekle(Manuel)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Connection String'i oku(Manuel)
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EfCoreJourney API V1");
    c.RoutePrefix = "swagger"; // istersen "" yap, direkt anasayfa olur
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
