using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public interface IShoppingCart
    {
        List<KeyValuePair<IProduct, int>> CartItems { get; set; }
        int CustomerId { get; set; }
        List<KeyValuePair<IPromotion, decimal>> PromotionsApplied { get; set; }
        int Id { get; set; }
        decimal TotalBillAmount { get; set; }
        decimal TotalDiscountAmount { get; set; }
        decimal BillAmountAfterDiscount { get; set; }
        
       
    }

    public class ShoppingCart : IShoppingCart
    {
        public ShoppingCart()
        {

            CartItems = new List<KeyValuePair<IProduct, int>>();
            PromotionsApplied = new List<KeyValuePair<IPromotion, decimal>>();
            TotalDiscountAmount = 0;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<KeyValuePair<IProduct, int>> CartItems { get; set; }
        public List<KeyValuePair<IPromotion, decimal>> PromotionsApplied { get; set; }

        public decimal TotalBillAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal BillAmountAfterDiscount { get; set; }
        
    }

}
