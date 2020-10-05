using NUnit.Framework;
using PromotionEngine.Contracts;
using PromotionEngine.Models;
using PromotionEngine.Services;
using System.Collections.Generic;

namespace PromotionEngine.Tests
{
    [TestFixture]
    public class ShoppingCartServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateBill_CaseA_ShouldReturn100()
        {
            IDiscountService discountService = new DiscountService();
            IShoppingCartService scs = new ShoppingCartService(discountService);
            
            var shoppingCart = scs.CreateShoppingCart(0, 1);


            var ps = new ProductService();
            var pA = ps.CreateProduct("A", "product A", 50);
            var pB = ps.CreateProduct("B", "product B", 30);
            var pC = ps.CreateProduct("C", "product C", 20);

            var discountA = discountService.CreateDiscount("Discount on A", "Discount if Purchase 3 As");
            var pAs = new KeyValuePair<Product, int>(pA, 3);
            var discountCombinationAs = new List<KeyValuePair<Product, int>>();
            discountCombinationAs.Add(pAs);
            var dci = new DiscountCombination(discountCombinationAs,
                20);

            discountService.CreateDiscountCombination(discountA.Id, dci);

            scs.AddItem(shoppingCart, pA, 1);
            scs.AddItem(shoppingCart, pB, 1);
            scs.AddItem(shoppingCart, pC, 1);

            shoppingCart = scs.CalculateBill(shoppingCart);

            Assert.AreEqual(100, shoppingCart.TotalBillAmount - shoppingCart.TotalDiscountAmount); 
        }
        [Test]
        public void CalculateBill_CaseB_ShouldReturn100()
        {
        }
        }
    }