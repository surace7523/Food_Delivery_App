using FoodDeliveryApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //Register in memory caching service for caching data in RAM 
        builder.Services.AddMemoryCache();

        //clear the default logging providers
        builder.Logging.ClearProviders();

        //configure the host ti use serilog as the logging provider

        builder.Host.UseSerilog((context, services, configuration) => {
        configuration.ReadFrom.Configuration(context.Configuration);
       });

        //configure SQL server in Dbcontext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        //add cookie authentication
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // The string "cookie" is the name of the authentication scheme
        .AddCookie(options =>
        {
            //wherever you wnat to redirect after unathenticated and unathorized

            options.Cookie.Name = "MyAuthCookie"; // Name of the cookie stored in the browser
            options.LoginPath = "/Account/Login"; // Path to the login page
            options.AccessDeniedPath = "/Account/AccessDenied"; // Path for denied access
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.SlidingExpiration = true;
        });

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
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
