using System;
using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public interface IPromotion
    {
        Dictionary<IProduct, int> ProductInfo { get; set; }
        decimal PromoPrice { get; set; }
        int PromotionID { get; set; }
    }

    public class Promotion : IPromotion
    {
        public int PromotionID { get; set; }
        public Dictionary<IProduct, int> ProductInfo { get; set; }
        public decimal PromoPrice { get; set; }

        public Promotion(int promotionID, Dictionary<IProduct, int> productInfo, decimal productPrice)
        {
            this.PromotionID = promotionID;
            this.ProductInfo = productInfo;
            this.PromoPrice = productPrice;
        }
    }
}
