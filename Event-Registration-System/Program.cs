using Event_Registration_System.Data;
using Event_Registration_System.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Registration_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            // Retrieve the connection string from configuration
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

            // Configure Entity Framework with SQL Server
            builder.Services.AddDbContext<EventRegistrationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Configure Mailjet client using API keys from configuration
            builder.Services.AddScoped(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                string apiKey = configuration["Mailjet:ApiKey"];
                string secretKey = configuration["Mailjet:SecretKey"];
                return new Mailjet.Client.MailjetClient(apiKey, secretKey);
            });

            // Register the EmailService for dependency injection
            builder.Services.AddScoped<EmailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // Use HSTS for production
                app.UseHsts();
            }

            // Middleware to handle HTTPS redirection and serve static files
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Set up routing
            app.UseRouting();

            // Enable authorization
            app.UseAuthorization();

            // Define the default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the application
            app.Run();
        }
    }
}
