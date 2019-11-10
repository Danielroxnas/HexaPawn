using Microsoft.Extensions.DependencyInjection;
using System;

namespace HexaPawnConsole
{
    class Program
    {
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<IBoardState, BoardState>();
            services.AddTransient<IMoveService, MoveService>();

            // IMPORTANT! Register our application entry point
            services.AddTransient<Game>();
            return services;
        }
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<Game>().GenerateBoard();


        }
    }
}
