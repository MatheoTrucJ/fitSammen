using FitSammenWebClient.BusinessLogicLayer;
using FitSammenWebClient.ServiceLayer;
using Microsoft.AspNetCore.Authentication.Cookies; 
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ILoginLogic, LoginLogic>();
builder.Services.AddScoped<IWaitingListLogic, WaitingListLogic>();
builder.Services.AddScoped<IClassLogic, ClassLogic>();
builder.Services.AddHttpClient<ILoginService, LoginService>();
builder.Services.AddHttpClient<IClassAccess, ClassService>();
builder.Services.AddHttpClient<IWaitingListAccess, WaitingListService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index"; 
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); //COOKIE TID MATCHER EXPPIRES PÅ TOKEN
        options.Cookie.HttpOnly = true; //TILFØJET FOR SIKKERHED, UNDGÅ XSS angreb, ingen js
    });

builder.Services.AddAuthorization(); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

app.Run();
