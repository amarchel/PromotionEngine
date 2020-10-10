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

        private IShoppingCartService _shoppingCartService;
        private List<IPromotion> _promotions;

        private IProduct pA;
        private IProduct pB;
        private IProduct pC;
        private IProduct pD;
        List<KeyValuePair<IProduct, int>> _cartItems;

        [SetUp]
        public void Setup()
        {

            _cartItems = new List<KeyValuePair<IProduct, int>>();

            //Create Products
            pA = new Product("A", "product A", 50);
            pB = new Product("B", "product B", 30);
            pC = new Product("C", "product C", 20);
            pD = new Product("D", "product D", 15);

            //Promotions
            Dictionary<IProduct, int> d1 = new Dictionary<IProduct, int>();
            d1.Add(pA, 3);
            Dictionary<IProduct, int> d2 = new Dictionary<IProduct, int>();
            d2.Add(pB, 2);
            Dictionary<IProduct, int> d3 = new Dictionary<IProduct, int>();
            d3.Add(pC, 1);
            d3.Add(pD, 1);

            _promotions = new List<IPromotion>()
            {
                new Promotion(1, d1, 130),
                new Promotion(2, d2, 45),
                new Promotion(3, d3, 30)
            };

            _shoppingCartService = new ShoppingCartService();


        }

        [Test]
        public void CalculateBill_ScenarioA_ShouldReturn100()
        {
            _cartItems.Add(new KeyValuePair<IProduct, int>(pA, 1));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pB, 1));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pC, 1));

            decimal billAmountAfterDiscount = _shoppingCartService.CalculateBillAmount(_cartItems, _promotions);

            Assert.AreEqual(100, billAmountAfterDiscount);
        }

        [Test]
        public void CalculateBill_ScenarioB_ShouldReturn370()
        {

            _cartItems.Add(new KeyValuePair<IProduct, int>(pA, 5));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pB, 5));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pC, 1));

            decimal billAmountAfterDiscount = _shoppingCartService.CalculateBillAmount(_cartItems, _promotions);

            Assert.AreEqual(370, billAmountAfterDiscount);
        }

        [Test]
        public void CalculateBill_ScenarioC_ShouldReturn280()
        {
            _cartItems.Add(new KeyValuePair<IProduct, int>(pA, 3));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pB, 5));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pC, 1));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pD, 1));

            decimal billAmountAfterDiscount = _shoppingCartService.CalculateBillAmount(_cartItems, _promotions);

            Assert.AreEqual(280, billAmountAfterDiscount);
        }
    }
}