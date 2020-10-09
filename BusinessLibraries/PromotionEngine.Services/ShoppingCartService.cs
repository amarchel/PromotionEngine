using PromotionEngine.Contracts;
using PromotionEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class ShoppingCartService : IShoppingCartService
    {

        private readonly IDiscountService _discountService;
  
        public ShoppingCartService(IDiscountService discountService)
        {
            _discountService = discountService;
           
        }

        public IShoppingCart CreateShoppingCart()
        {
            return new ShoppingCart();
        }
        public void AddItem(IShoppingCart shoppingCart, IProduct product, int count)
        {
            var item = new KeyValuePair<IProduct, int>();
            if (shoppingCart.CartItems != null)
                item = shoppingCart.CartItems.Find(ci => ci.Key.Sku == product.Sku);

            if (item.Key == null)
            {
                shoppingCart.CartItems.Remove(item);
            }
            shoppingCart.CartItems.Add(new KeyValuePair<IProduct, int>(product, count));
        }



        public IShoppingCart CalculateBill(IShoppingCart shoppingCart)
        {
            // Get all applicable discount for the order
            var discounts = _discountService.GetAll()
                .ToList();
            var applicableDiscounts = new List<Discount>();

            var products = shoppingCart.CartItems.Select(ci => ci.Key);

            foreach (var product in products)
            {
                var discountsHavingProduct = discounts
                    .Where(d => d.DiscountCombination
                        .ItemsCombinations
                        .Any(ic => ic.Key == product))
                    .Select(disc => disc);

                applicableDiscounts.AddRange(discountsHavingProduct);
            }

            var shoppingCartClone = shoppingCart.Clone();

            foreach (var applicableDiscount in applicableDiscounts)
                ApplyDiscount(shoppingCartClone, applicableDiscount);

            shoppingCart.TotalBillAmount = shoppingCart.CartItems.Select(ci => ci.Key.Cost * ci.Value).Sum();
            shoppingCart.TotalDiscountAmount = shoppingCartClone.TotalDiscountAmount;
            shoppingCart.DiscountsApplied = shoppingCartClone.DiscountsApplied;

            return shoppingCart;
        }



        private void ApplyDiscount(IShoppingCart shoppingCartClone,
            Discount discount)
        {
            var discountCombination = discount.DiscountCombination;
            decimal discountAmount = 0;

            var itemsMarkedForDiscount = new List<KeyValuePair<IProduct, int>>();

            var eligibleForDiscount = false;
            foreach (var itemsCountsCombination in discountCombination.ItemsCombinations)
            {
                var shoppedItem = shoppingCartClone.CartItems
                    .Where(ci => ci.Key == itemsCountsCombination.Key)
                    .Select(ci => ci)
                    .SingleOrDefault();

                if (shoppedItem.Key == null)
                {
                    eligibleForDiscount = false;
                    break;
                }

                if (shoppedItem.Value >= itemsCountsCombination.Value)
                {
                    eligibleForDiscount = true;
                    itemsMarkedForDiscount.Add(shoppedItem);
                    var prod = shoppedItem.Key;
                    shoppingCartClone.CartItems.Remove(shoppedItem);
                    var remainingCount = shoppedItem.Value - itemsCountsCombination.Value;
                    shoppingCartClone.CartItems.Add(new KeyValuePair<IProduct, int>(prod, remainingCount));
                }
            }

            if (eligibleForDiscount)
            {
                discountAmount = discountCombination.DiscountAmount;
                var ds = shoppingCartClone.DiscountsApplied.Find(s => s.Key == discount);

                if (ds.Key == null)
                {
                    shoppingCartClone.DiscountsApplied.Add(
                        new KeyValuePair<Discount, decimal>(discount, discountAmount));
                }
                else
                {
                    discountAmount = ds.Value;
                    shoppingCartClone.DiscountsApplied.Remove(ds);
                    shoppingCartClone.DiscountsApplied.Add(
                        new KeyValuePair<Discount, decimal>(discount, discountAmount));
                }
            }
            else
            {
                itemsMarkedForDiscount.ForEach(it =>
                {
                    shoppingCartClone.CartItems.Remove(shoppingCartClone.CartItems.Find(p => p.Key == it.Key));
                    shoppingCartClone.CartItems.Add(it);
                });
            }

            if (discountAmount == 0) return;

            shoppingCartClone.TotalDiscountAmount += discountAmount;
            ApplyDiscount(shoppingCartClone, discount);
        }
    }
}

