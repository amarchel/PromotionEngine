using NUnit.Framework;
using PromotionEngine;
using PromotionEngine.Contracts;
using PromotionEngine.Models;
using PromotionEngine.Services;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace PromotionEngine.Tests
{
    [TestFixture]
    public class ShoppingCartServiceTest
    {
        private Mock<IDiscountService> _discountService;
        private IShoppingCartService _shoppingCartService;

        private IShoppingCart _shoppingCart;
        private IProduct pA;
        private IProduct pB;
        private IProduct pC;
        private IProduct pD;
        List<KeyValuePair<IProduct, int>> _cartItems;

        [SetUp]
        public void Setup()
        {
            List<Discount> _discounts = new List<Discount>();
            _cartItems = new List<KeyValuePair<IProduct, int>>();

            //Create Products
            pA = new Product("A", "product A", 50);
            pB = new Product("B", "product B", 30);
            pC = new Product("C", "product C", 20);
            pD = new Product("D", "product D", 15);


            var discountCombinationAs = new List<KeyValuePair<IProduct, int>>();
            discountCombinationAs.Add(new KeyValuePair<IProduct, int>(pA, 3));
            var dci = new DiscountCombination(discountCombinationAs,
                20);
            Discount discountA = new Discount(1, "Discount on A", dci);
            _discounts.Add(discountA);


            var discountCombinationBs = new List<KeyValuePair<IProduct, int>>();
            discountCombinationBs.Add(new KeyValuePair<IProduct, int>(pB, 2));
            var dc2 = new DiscountCombination(discountCombinationBs,
                15);
            Discount discountB = new Discount(2, "Discount on B", dc2);
            _discounts.Add(discountB);



            var discountCombinationCDs = new List<KeyValuePair<IProduct, int>>();
            var pCs = new KeyValuePair<IProduct, int>(pC, 1);
            var pDs = new KeyValuePair<IProduct, int>(pD, 1);
            discountCombinationCDs.Add(pCs);
            discountCombinationCDs.Add(pDs);
            var dc3 = new DiscountCombination(discountCombinationCDs,
                5);
            Discount discountCD = new Discount(3, "Discount on C&D", dc3);
            _discounts.Add(discountCD);

            _discountService = new Mock<IDiscountService>();
            _discountService.Setup(ds => ds.GetAll()).Returns(_discounts);

            _shoppingCartService = new ShoppingCartService(_discountService.Object);

            // Create Empty Cart
            _shoppingCart = new ShoppingCart();
        }

        [Test]
        public void CalculateBill_ScenarioA_ShouldReturn100()
        {
            _cartItems.Add(new KeyValuePair<IProduct, int>(pA, 1));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pB, 1));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pC, 1));
            _shoppingCart.CartItems = _cartItems;
            _shoppingCart = _shoppingCartService.CalculateBill(_shoppingCart);

            Assert.AreEqual(100, _shoppingCart.TotalBillAmount - _shoppingCart.TotalDiscountAmount);
        }

        [Test]
        public void CalculateBill_ScenarioB_ShouldReturn370()
        {

            _cartItems.Add(new KeyValuePair<IProduct, int>(pA, 5));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pB, 5));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pC, 1));
            _shoppingCart.CartItems = _cartItems;
            _shoppingCart = _shoppingCartService.CalculateBill(_shoppingCart);

            Assert.AreEqual(370, _shoppingCart.TotalBillAmount - _shoppingCart.TotalDiscountAmount);
        }

        [Test]
        public void CalculateBill_ScenarioC_ShouldReturn280()
        {
            _cartItems.Add(new KeyValuePair<IProduct, int>(pA, 3));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pB, 5));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pC, 1));
            _cartItems.Add(new KeyValuePair<IProduct, int>(pD, 1));
            _shoppingCart.CartItems = _cartItems;
            _shoppingCart = _shoppingCartService.CalculateBill(_shoppingCart);

            Assert.AreEqual(280, _shoppingCart.TotalBillAmount - _shoppingCart.TotalDiscountAmount);
        }
    }
}