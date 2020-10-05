using System;

namespace PromotionEngine.Models
{
    public class Product
    {
        public Product(int id, string name, string description, decimal cost)
        {
            Id = id;
            Name = name;
            Description = description;
            Cost = cost;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Cost { get; }


    }
}
