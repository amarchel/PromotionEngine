using PromotionEngine.Contracts;
using PromotionEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class ShoppingCartService : IShoppingCartService
    {

     
  
        public ShoppingCartService( )
        {
            
           
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



        public IShoppingCart CalculateBill(IShoppingCart shoppingCart, IList<IPromotion> promotions)
        {
            List<KeyValuePair<IProduct, int>> OrderClone = new List<KeyValuePair<IProduct, int>>();
            List<KeyValuePair<IPromotion, decimal>>  PromotionsApplied = new List<KeyValuePair<IPromotion, decimal>>();
            
            foreach (var prooduct in shoppingCart.CartItems)
            {
                OrderClone.Add(prooduct);
            }
          
            decimal promotionalPrice = 0M;
            foreach (IPromotion promotion in promotions)
            {
                var discountCount = 0;
                foreach (var product in promotion.ProductInfo)
                {
                    int prodCount = OrderClone.ToList().Find(p => p.Key.Sku == product.Key.Sku).Value;
                    if (prodCount >= product.Value)
                    {
                        discountCount = (prodCount / product.Value);
                    }
                    else
                    {
                        discountCount = 0;
                    }
                }
                if (discountCount > 0)
                {
                    promotionalPrice += discountCount * promotion.PromoPrice;
                    PromotionsApplied.Add(new KeyValuePair<IPromotion, decimal>(promotion, discountCount * promotion.PromoPrice));
                    foreach (var product in promotion.ProductInfo)
                    {
                        var itemToRemove = OrderClone.Single(kvp => kvp.Key.Sku == product.Key.Sku);
                        OrderClone.Add(new KeyValuePair<IProduct, int>(product.Key, OrderClone.First(kvp => kvp.Key.Sku == product.Key.Sku).Value - (discountCount * product.Value)));
                        OrderClone.Remove(itemToRemove);
                    }
                }
            }
            promotionalPrice += OrderClone.Select(ci => ci.Key.Cost * ci.Value).Sum(); 

            shoppingCart.TotalBillAmount = shoppingCart.CartItems.Select(ci => ci.Key.Cost * ci.Value).Sum();
            shoppingCart.TotalDiscountAmount = shoppingCart.TotalBillAmount- promotionalPrice;
            shoppingCart.PromotionsApplied = PromotionsApplied;
            shoppingCart.BillAmountAfterDiscount = promotionalPrice;

            return shoppingCart;
        }


         
    }
}

