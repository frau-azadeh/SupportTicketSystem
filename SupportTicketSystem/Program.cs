using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data; 
namespace SupportTicketSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connect to db
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Razor Pages
            builder.Services.AddRazorPages();

            // ADD Session

            builder.Services.AddSession();

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
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();

            app.Run();
        }
    }
}
