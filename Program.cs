using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdminDecisionsSearchApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Build the application and start the web host
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers(); // Add controllers (including API controllers)

            // If you need any other services, like database context, add them here
            // builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(...));

            var app = builder.Build();

            // Enable serving static files (from wwwroot folder)
            app.UseStaticFiles();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Developer error page for debugging
            }

            app.UseHttpsRedirection(); // Redirect HTTP to HTTPS (optional but recommended)

            app.UseRouting(); // Enable routing for controllers

            app.UseAuthorization(); // Enable authorization if needed

            app.MapControllers(); // Map API controllers to routes

            // Start the application
            app.Run();
        }
    }
}