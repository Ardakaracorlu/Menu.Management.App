using CurrieTechnologies.Razor.SweetAlert2;
using Menu.Management.App.Configuration;
using Menu.Management.App.Infrastructure;
using Menu.Management.App.Services.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var baseAddress = builder.Configuration["ApiSettings:Url"];

// Add services to the container.
#region Services

builder.Services.AddSingleton<ConfigManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSweetAlert2();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<HttpClientHelper>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<MenuDataService>();
builder.Services.AddScoped<NavScrollService>();
builder.Services.AddWMBOS();

// HTTP Client with BaseAddress
builder.Services.AddHttpClient<HttpClientHelper>(client =>
{
    client.BaseAddress = new Uri(baseAddress);
});

// Session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Authentication & Authorization
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

#endregion

#region Blazor Services

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Middleware for session management
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
