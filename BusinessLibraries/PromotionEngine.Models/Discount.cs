using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public class Discount
    {
        public Discount(int id, string name, DiscountCombination discountCombination)
        {
            Id = id;
            Name = name; 
            DiscountCombination = discountCombination;
        }

        public int Id { get; }
        public string Name { get; } 

        public DiscountCombination DiscountCombination { get; private set; }

        public Discount UpdateDiscountCombination(DiscountCombination discountCombination)
        {
            this.DiscountCombination = discountCombination;
            return this;
        }
    }
    
}
