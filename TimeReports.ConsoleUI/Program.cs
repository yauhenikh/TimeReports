using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeReports.DataAccess.Data;
using TimeReports.Services;

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

            var averageHoursService = _serviceProvider.GetRequiredService<AverageHoursService>();

            var averageHoursOutput = averageHoursService.GetAverageHoursOutput();

            Console.WriteLine(averageHoursOutput);
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
            services.AddScoped<AverageHoursService>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
