using CI_Platform.DataAccess.Repository;
using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Services.Service;
using CI_Platform.Services.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfService, UnitOfService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Volunteer/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
//app.Use(async (context, next) =>
//{
//    var session = context.Session;

//    var lastActivity = session.GetInt32("lastActivity");
//    var sessionExpired = lastActivity.HasValue && DateTimeOffset.FromUnixTimeSeconds(lastActivity.Value).AddSeconds(10) < DateTimeOffset.UtcNow;

//    if (sessionExpired)
//    {
//        context.Session.Clear();
//        context.Response.Headers["X-Session-Expired"] = "true";
//        return;
//    }

//    session.SetInt32("lastActivity", (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
//    await session.CommitAsync();

//    await next();
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Volunteer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
