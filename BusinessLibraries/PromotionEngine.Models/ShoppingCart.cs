using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public interface IShoppingCart
    {
        List<KeyValuePair<IProduct, int>> CartItems { get; set; }
        int CustomerId { get; set; }
        List<KeyValuePair<Discount, decimal>> DiscountsApplied { get; set; }
        int Id { get; set; }
        decimal TotalBillAmount { get; set; }
        decimal TotalDiscountAmount { get; set; }

        ShoppingCart Clone();
    }

    public class ShoppingCart : IShoppingCart
    {
        public ShoppingCart()
        {

            CartItems = new List<KeyValuePair<IProduct, int>>();
            DiscountsApplied = new List<KeyValuePair<Discount, decimal>>();
            TotalDiscountAmount = 0;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<KeyValuePair<IProduct, int>> CartItems { get; set; }
        public List<KeyValuePair<Discount, decimal>> DiscountsApplied { get; set; }

        public decimal TotalBillAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }

        public ShoppingCart Clone()
        {
            var scNew = new ShoppingCart();
            this.CartItems.ForEach(ci => { scNew.CartItems.Add(new KeyValuePair<IProduct, int>(ci.Key, ci.Value)); });
            return scNew;
        }
    }

}
