using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public class DiscountCombination
    {
        public IEnumerable<KeyValuePair<IProduct, int>> ItemsCombinations { get; set; }
        public decimal DiscountAmount { get; set; }

        public DiscountCombination(IEnumerable<KeyValuePair<IProduct, int>> itemsCombinations,
            decimal discountAmount)
        {

            ItemsCombinations = itemsCombinations;
            DiscountAmount = discountAmount;

        }
    }
}
