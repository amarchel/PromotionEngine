using Microsoft.Extensions.DependencyInjection;
using System;

namespace PromotionEngine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            

            var services = new ServiceCollection();

            Startup.Configure(services);
            

            var serviceProvider = services.BuildServiceProvider();

            


        }
    }
}
