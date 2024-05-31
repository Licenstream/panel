using Domain;
using Domain.Interfaces;
using Infrastructure;
using InfrastructureEF;

namespace LicenStream.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<LicenseService, LicenseService>();
            builder.Services.AddScoped<CustomerService, CustomerService>();
            builder.Services.AddScoped<UserService, UserService>();

            var connectionString = builder.Configuration["ConnectionString"];
            builder.Services.AddScoped<IDataHandler<License>>(x => new LicenseEFDataHandler(connectionString));
            builder.Services.AddScoped<IDataBulkHandler<License>>(x => new LicenseEFDataHandler(connectionString));
            builder.Services.AddScoped<IDataHandler<User>>(x => new UserEFDataHandler(connectionString));
            builder.Services.AddScoped<IDataHandler<Customer>>(x => new CustomerEFDataHandler(connectionString));


            builder.Services.AddMemoryCache();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}