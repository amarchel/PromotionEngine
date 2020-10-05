using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Contracts;
using PromotionEngine.Services;

namespace PromotionEngine
{
   internal static class Startup
    {
        internal static void Configure(IServiceCollection services) {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
        }
    }
}
