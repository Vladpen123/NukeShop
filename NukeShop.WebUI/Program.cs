

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NukeShop.BLL.Services;
using NukeShop.WebUI.ModelBinders;
using NukeShop.WebUI.Users;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);




var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.NumberGroupSeparator = ",";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
// Add services to the container.


builder.Services.AddSession();

builder.Services.AddDbContext<UserContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("UserConnectionString"));
});

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserContext>();



builder.Services.AddControllersWithViews(opts =>
{
    opts.ModelBinderProviders.Insert(0, new ModelBinderProvider());
});


builder.Services.AddHttpClient<IProductService, ProductService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7000/api/");
});
builder.Services.AddHttpClient<ICategoryService, CategoryService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7000/api/");
});
builder.Services.AddHttpClient<IManufacturerService, ManufacturerService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7000/api/");
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


builder.Services.AddCors(policy =>
{
    policy.AddPolicy("cors", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Pagination"));
});

builder.Services.AddServerSideBlazor();

builder.Services.AddRazorPages();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    opts =>
    {
        opts.AccessDeniedPath = "/DENIED";
        opts.LoginPath = "/Account/Login";
    });
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseSession();

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


app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapBlazorHub();
app.MapRazorPages();

app.Run();
