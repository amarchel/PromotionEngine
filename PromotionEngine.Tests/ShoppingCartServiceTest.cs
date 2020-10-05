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
        public void CalculateBill_ScenarioA_ShouldReturn100()
        {
            IDiscountService discountService = new DiscountService();
            IShoppingCartService scs = new ShoppingCartService(discountService);

            var shoppingCart = scs.CreateShoppingCart();


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
        public void CalculateBill_ScenarioB_ShouldReturn370()
        {
            IDiscountService discountService = new DiscountService();
            IShoppingCartService scs = new ShoppingCartService(discountService);

            var shoppingCart = scs.CreateShoppingCart();


            var ps = new ProductService();
            var pA = ps.CreateProduct("A", "product A", 50);
            var pB = ps.CreateProduct("B", "product B", 30);
            var pC = ps.CreateProduct("C", "product C", 20);
            var pD = ps.CreateProduct("D", "product D", 15);

            var discountA = discountService.CreateDiscount("Discount on A", "Discount if Purchase 3 As");
            var pAs = new KeyValuePair<Product, int>(pA, 3);
            var discountCombinationAs = new List<KeyValuePair<Product, int>>();
            discountCombinationAs.Add(pAs);
            var dci = new DiscountCombination(discountCombinationAs,
                20);

            discountService.CreateDiscountCombination(discountA.Id, dci);

            //Discount B
            var discountB = discountService.CreateDiscount("Discount on B", "Discount if Purchase 2 Bs");
            var pBs = new KeyValuePair<Product, int>(pB, 2);
            var discountCombinationBs = new List<KeyValuePair<Product, int>>();
            discountCombinationBs.Add(pBs);
            var dc2 = new DiscountCombination(discountCombinationBs,
                15);

            discountService.CreateDiscountCombination(discountB.Id, dc2);

            scs.AddItem(shoppingCart, pA, 5);
            scs.AddItem(shoppingCart, pB, 5);
            scs.AddItem(shoppingCart, pC, 1);

            shoppingCart = scs.CalculateBill(shoppingCart);

            Assert.AreEqual(370, shoppingCart.TotalBillAmount - shoppingCart.TotalDiscountAmount);
        }

        [Test]
        public void CalculateBill_ScenarioC_ShouldReturn280() {
            IDiscountService discountService = new DiscountService();
            IShoppingCartService scs = new ShoppingCartService(discountService);
            
            var shoppingCart = scs.CreateShoppingCart();


            var ps = new ProductService();
            var pA = ps.CreateProduct("A", "product A", 50);
            var pB = ps.CreateProduct("B", "product B", 30);
            var pC = ps.CreateProduct("C", "product C", 20);
            var pD = ps.CreateProduct("D", "product D", 15);

            //Discount A
            var discountA = discountService.CreateDiscount("Discount on A", "Discount if Purchase 3 As");
            var pAs = new KeyValuePair<Product, int>(pA, 3);
            var discountCombinationAs = new List<KeyValuePair<Product, int>>();
            discountCombinationAs.Add(pAs);
            var dci = new DiscountCombination(discountCombinationAs,
                20);

            discountService.CreateDiscountCombination(discountA.Id, dci);

            //Discount B
            var discountB = discountService.CreateDiscount("Discount on B", "Discount if Purchase 2 Bs");
            var pBs = new KeyValuePair<Product, int>(pB, 2);
            var discountCombinationBs = new List<KeyValuePair<Product, int>>();
            discountCombinationBs.Add(pBs);
            var dc2 = new DiscountCombination(discountCombinationBs,
                15);

            discountService.CreateDiscountCombination(discountB.Id, dc2);

            //Discount C + D
            var discountCD = discountService.CreateDiscount("Discount on C & D", "Discount if Purchase C & Ds");
            var pCs = new KeyValuePair<Product, int>(pC, 1);
            var pDs = new KeyValuePair<Product, int>(pD, 1);
            var discountCombinationCDs = new List<KeyValuePair<Product, int>>();
            discountCombinationCDs.Add(pCs);
            discountCombinationCDs.Add(pDs);
            var dc3 = new DiscountCombination(discountCombinationCDs,
                5);

            discountService.CreateDiscountCombination(discountCD.Id, dc3);

            scs.AddItem(shoppingCart, pA, 3);
            scs.AddItem(shoppingCart, pB, 5);
            scs.AddItem(shoppingCart, pC, 1);
            scs.AddItem(shoppingCart, pD, 1);

            shoppingCart = scs.CalculateBill(shoppingCart);

            Assert.AreEqual(280, shoppingCart.TotalBillAmount - shoppingCart.TotalDiscountAmount);
        }
    }
}