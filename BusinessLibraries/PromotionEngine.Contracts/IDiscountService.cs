using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Contracts
{
    public interface IDiscountService
    {
        IEnumerable<Discount> GetAll();
        Discount Get(int id);
        Discount CreateDiscount(string name, string description);
        DiscountCombination CreateDiscountCombination(int discountId, DiscountCombination discountCombination);
        Discount UpdateDiscountCombination(int discountId, DiscountCombination discountCombination); 
    }
}
