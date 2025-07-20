using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SupportTicketSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // اتصال به دیتابیس
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Razor Pages
            builder.Services.AddRazorPages();

            // Session
            builder.Services.AddSession();

            // احراز هویت با کوکی
            builder.Services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Login";
                });

            var app = builder.Build();

            // Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers(); 

            app.Run();
        }
    }
}
