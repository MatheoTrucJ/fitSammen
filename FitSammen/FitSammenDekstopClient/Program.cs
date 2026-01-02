using FitSammenDekstopClient.BusinessLogicLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Runtime.Intrinsics.X86;
namespace FitSammenDekstopClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = configBuilder.Build();

            ClassLogic classLogic = new ClassLogic(configuration);
            LocationLogic locationLogic = new LocationLogic(configuration);

            // Her connecter vi til vores MapHub<ClassHub> i vores ClassHub
            string serviceUrl = configuration["ServiceUrlToUse"] ?? string.Empty;
            string hubBase = "https://localhost:7229";
            string hubUrl = $"{hubBase}/hubs/classes";

            Application.Run(new FitSammen(classLogic, locationLogic, hubUrl));
        }
    }
}