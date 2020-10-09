using PromotionEngine.Contracts;
using PromotionEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class ProductService : IProductService
    {
        readonly List<Product> products = new List<Product>();
        public ProductService()
        {
        }
        public Product CreateProduct(string sku, string name, decimal cost)
        {
            products.Add(new Product(sku, name, cost));
            return Get(sku);
        }

        public Product Get(string sku)
        {
            return products.FirstOrDefault(p => p.Sku == sku);
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }
    }
}
