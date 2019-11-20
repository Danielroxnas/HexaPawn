using Microsoft.Extensions.DependencyInjection;

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

            services.AddTransient<Game>();
            return services;
        }
        static void Main(string[] args)
        {

            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Game>().GenerateBoard();
        }
    }
}
