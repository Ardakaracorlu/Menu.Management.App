using Microsoft.AspNetCore.Authentication.Cookies;
using CurrieTechnologies.Razor.SweetAlert2;
using Menu.Management.App.Configuration;
using Menu.Management.App.Infrastructure;
using FluentValidation;
using ApexCharts;
using Menu.Management.App.Model.Request.Account;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ConfigManager>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<StateService>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<MenuDataService>();
builder.Services.AddWMBOS();
builder.Services.AddScoped<NavScrollService>();
builder.Services.AddSession();
builder.Services.AddScoped<SessionService>();
builder.Services.AddSweetAlert2();
builder.Services.AddScoped<HttpClientHelper>();
var baseAddress = builder.Configuration["ApiSettings:Url"];
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });


// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });


builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
        options.Cookie.Name = "YourAppCookieName";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
    
app.UseAuthentication();
app.UseAuthorization();

app.MapFallbackToPage("/_Host");

app.Run();
