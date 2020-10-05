using PromotionEngine.Contracts;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class ProductService : IProductService
    {
        readonly List<Product> products = new List<Product>();
        public Product CreateProduct(string name, string description, decimal cost)
        {
            int maxId = products.Select(c => c.Id)
               .DefaultIfEmpty(0).Max();
            products.Add(new Product(++maxId, name, description, cost));
            return products[--maxId];
        }

        public Product Get(int id)
        {
            return products.First(p => p.Id == id);
            
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }
    }
}
