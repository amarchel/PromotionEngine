using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Contracts
{
    public interface IShoppingCartService
    {
        IShoppingCart CreateShoppingCart();
        void AddItem(IShoppingCart shoppingCart, IProduct product, int count);

        IShoppingCart CalculateBill(IShoppingCart shoppingCart);
        
    }
}
