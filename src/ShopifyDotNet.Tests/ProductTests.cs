using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShopifyDotNet.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Product_All()
        {
            // Arrange
            var client = new Client("", "");

            // Act
            var products = client.Products.All();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreNotEqual(0, products.Count());
        }

    }
}
