using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using souq02.Data;
using souq02.Models;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Connection Strings
// --------------------

// Identity (ApplicationDbContext)
var identityConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// News Website (NewsContext)
var newsConnectionString = builder.Configuration.GetConnectionString("NewsWebsite")
    ?? throw new InvalidOperationException("Connection string 'NewsWebsite' not found.");

// --------------------
// Services
// --------------------

// Identity DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(identityConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// News DbContext (سجل مرة واحدة فقط)
builder.Services.AddDbContext<NewsContext>(options =>
    options.UseSqlServer(newsConnectionString));

// MVC
builder.Services.AddControllersWithViews();

// --------------------
// Build App
// --------------------
var app = builder.Build();

// --------------------
// Configure Middleware
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // عرض الأخطاء بالتفصيل أثناء التطوير
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// --------------------
// Routes
// --------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
