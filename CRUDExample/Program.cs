using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add services into Ioc container
builder.Services.AddScoped<ICountriesService, CountryService>();
builder.Services.AddScoped<IPersonsService, PersonService>();

builder.Services.AddDbContext<PersonsDbContext>(options =>
 { 
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
 });
//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonsDatabase;Integrated Security=True;
//Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False

var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
