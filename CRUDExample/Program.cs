using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add services into Ioc container
builder.Services.AddSingleton<ICountriesService, CountryService>();
builder.Services.AddSingleton<IPersonsService, PersonService>();
var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
