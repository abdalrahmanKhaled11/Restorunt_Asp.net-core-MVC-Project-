using Microsoft.EntityFrameworkCore;
using Restorunt.Models;

namespace Restorunt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<ModelContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("RestaurantConnection")));



            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
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

            app.UseAuthorization();

            app.UseSession();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}