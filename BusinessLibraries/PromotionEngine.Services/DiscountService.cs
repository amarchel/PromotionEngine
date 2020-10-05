using PromotionEngine.Contracts;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PromotionEngine.Services
{
  public  class DiscountService: IDiscountService
    {
        private readonly List<Discount> _discounts = new List<Discount>();

        public IEnumerable<Discount> GetAll()
        {
            return _discounts;
        }

        public Discount Get(int id)
        {
            return _discounts[id];
        }

        public Discount CreateDiscount(string name, string description)
        {
            var maxId = _discounts.Select(c => c.Id)
                .DefaultIfEmpty(0).Max();

            _discounts.Add(new Discount(++maxId, name, description));
            return _discounts[--maxId];
        }

        public DiscountCombination CreateDiscountCombination(int discountId,
            DiscountCombination dc)
        {
            Discount discount = _discounts.Find(d => d.Id == discountId);

            if (discount == null) throw new InvalidDataException("discount does not exist");

            discount.UpdateDiscountCombination(dc);
            return dc;
        }

        public Discount UpdateDiscountCombination(int discountId,
            DiscountCombination dc)
        {
            Discount discount = _discounts.Find(d => d.Id == discountId);

            if (discount == null) throw new InvalidDataException("discount does not exist");

            discount.UpdateDiscountCombination(dc);
            return _discounts[discountId];
        }

    }
}
