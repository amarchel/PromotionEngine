using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public class Discount
    {
        public Discount(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public DiscountCombination DiscountCombination { get; private set; }

        public Discount UpdateDiscountCombination(DiscountCombination discountCombination)
        {
            this.DiscountCombination = discountCombination;
            return this;
        }
    }
}
