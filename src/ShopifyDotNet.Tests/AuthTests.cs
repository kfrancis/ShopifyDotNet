using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopifyDotNet.Products;
using System.Collections.Generic;

namespace ShopifyDotNet.Tests
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Auth_TokenMissing()
        {
            // Arrange
            var client = new Client("", "123");

            // Act
            var products = client.Products.All();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Auth_ShopIdMissing()
        {
            // Arrange
            var client = new Client("123", "");

            // Act
            var products = client.Products.All();
        }
    }
}
