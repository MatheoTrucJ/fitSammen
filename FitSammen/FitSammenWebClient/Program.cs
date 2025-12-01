using FitSammenWebClient.BusinessLogicLayer;
using FitSammenWebClient.ServiceLayer;
using Microsoft.AspNetCore.Authentication.Cookies; // Nødvendig for AddCookie
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- 1. REGISTRERING AF SERVICES OG LOGIK ---
// Logic (bruger IClassAccess, IWaitingListAccess, osv.) - AddScoped
builder.Services.AddScoped<ILoginLogic, LoginLogic>();
builder.Services.AddScoped<IWaitingListLogic, WaitingListLogic>();
builder.Services.AddScoped<IClassLogic, ClassLogic>();
// Services (bruger HttpClient) - AddHttpClient
builder.Services.AddHttpClient<ILoginService, LoginService>();
builder.Services.AddHttpClient<IClassAccess, ClassService>();
builder.Services.AddHttpClient<IWaitingListAccess, WaitingListService>();


// --- 2. REGISTRERING AF COOKIE AUTHENTICATION ---
// FJERNET: JWT Bearer kode.
// Tilføj Cookie Authentication for MVC WebClient
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index"; // Sæt den korrekte loginside/action
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization(); // Behold denne

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// MIDDLEWARE RÆKKEFØLGEN ER KORREKT
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
