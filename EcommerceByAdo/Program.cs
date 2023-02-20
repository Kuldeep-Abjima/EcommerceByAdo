using EcommerceByAdo.Data;
using EcommerceByAdo.Interfaces;
using EcommerceByAdo.Repositpory;
using EcommerceByAdo.Services;
using EcommerceProject.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPhotoServices, PhotoServices>();
builder.Services.AddTransient<IMensRepository, MensRepository>();
builder.Services.AddTransient<IProductATCRepository, ProductATCRepository>();
builder.Services.AddTransient<IDataBaseConnection, DataBaseConnection>();
builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySetting"));

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
