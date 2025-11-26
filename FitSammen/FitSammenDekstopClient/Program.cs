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

            Application.Run(new FitSammen(classLogic, locationLogic));
        }
    }
}