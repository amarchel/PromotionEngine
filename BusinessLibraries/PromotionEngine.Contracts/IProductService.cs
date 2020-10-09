using PromotionEngine.Models;
using System;
using System.Collections.Generic;

namespace PromotionEngine.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product Get(string sku);
        Product CreateProduct(string sku, string name, decimal costPerUnit);
        
    }
}
