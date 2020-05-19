using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeReports.DataAccess.Data;

namespace TimeReports.ConsoleUI
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            AddConfiguration();
            RegisterServices();

            var context = _serviceProvider.GetRequiredService<AppDbContext>();

            Console.WriteLine(context.Employees.First().Name);

            Console.WriteLine("Hello World!");
        }

        private static void AddConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("TimeReportsDBConnection")));

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
