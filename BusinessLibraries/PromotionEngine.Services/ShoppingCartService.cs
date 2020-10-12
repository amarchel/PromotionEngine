using PromotionEngine.Contracts;
using PromotionEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class ShoppingCartService : IShoppingCartService
    {



        public ShoppingCartService()
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



        public decimal CalculateBillAmount(List<KeyValuePair<IProduct, int>> cartItems, IList<IPromotion> promotions)
        {
            List<KeyValuePair<IProduct, int>> orderClone = new List<KeyValuePair<IProduct, int>>();
            List<KeyValuePair<IPromotion, decimal>> promotionsApplied = new List<KeyValuePair<IPromotion, decimal>>();

            foreach (var prooduct in cartItems)
            {
                orderClone.Add(prooduct);
            }

            decimal promotionalPrice = 0M;
            foreach (IPromotion promotion in promotions)
            {
                var PromoCount = 0;
                foreach (var product in promotion.ProductInfo)
                {
                    int prodCount = orderClone.ToList().Find(p => p.Key.Sku == product.Key.Sku).Value;
                    if (prodCount >= product.Value)
                    {
                         
                            PromoCount = (prodCount / product.Value); ;
                        
                    }
                    else
                    {
                        PromoCount = 0;
                    }
                }
                if (PromoCount > 0)
                {
                    promotionalPrice += PromoCount * promotion.PromoPrice;
                    promotionsApplied.Add(new KeyValuePair<IPromotion, decimal>(promotion, PromoCount * promotion.PromoPrice));
                    foreach (var product in promotion.ProductInfo)
                    {
                        var itemToRemove = orderClone.Single(kvp => kvp.Key.Sku == product.Key.Sku);
                        orderClone.Add(new KeyValuePair<IProduct, int>(product.Key, orderClone.First(kvp => kvp.Key.Sku == product.Key.Sku).Value - (PromoCount * product.Value)));
                        orderClone.Remove(itemToRemove);
                    }
                }
            }
            promotionalPrice += orderClone.Select(ci => ci.Key.Cost * ci.Value).Sum();

            decimal TotalBillAmount = cartItems.Select(ci => ci.Key.Cost * ci.Value).Sum();
            decimal TotalDiscountAmount = TotalBillAmount - promotionalPrice;

            return promotionalPrice;
        }



    }
}

