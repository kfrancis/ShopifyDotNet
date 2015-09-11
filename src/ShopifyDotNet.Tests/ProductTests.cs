using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ShopifyDotNet.Products;

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
            var result = client.Products.All();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.products);
            Assert.AreNotEqual(0, result.products.Count);
            Assert.IsTrue(result.products.All(p => p.id > 0));
            Assert.IsTrue(result.products.All(p => !string.IsNullOrEmpty(p.handle)));
        }
    }
}
