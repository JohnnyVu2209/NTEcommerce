using NTEcommerce.CustomerSite.Models;
using NTEcommerce.CustomerSite.Services;
using NTEcommerce.CustomerSite.Services.API;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddRefitClient<IProductApi>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://localhost:7012");
}).SetHandlerLifetime(TimeSpan.FromMinutes(2));

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
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
