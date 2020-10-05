using PromotionEngine.Models;
using System;
using System.Collections.Generic;

namespace PromotionEngine.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        Product CreateProduct(string name, string description, decimal costPerUnit);
        
    }
}
