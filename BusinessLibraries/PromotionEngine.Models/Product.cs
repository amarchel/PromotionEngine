using System;

namespace PromotionEngine.Models
{
    public interface IProduct
    {
        decimal Cost { get; }
        string Name { get; }
        string Sku { get; }
    }

    public class Product : IProduct
    {
        public Product(string sku, string name, decimal cost)
        {
            Sku = sku;
            Name = name;
            Cost = cost;
        }

        public string Sku { get; }
        public string Name { get; }

        public decimal Cost { get; }


    }
}
