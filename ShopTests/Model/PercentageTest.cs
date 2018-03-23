using System;
using Shop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Exceptions;
using NUnit.Framework;

namespace ShopTests
{
    [TestClass]
    public class PercentageTest
    {
        [TestMethod]
        [ExpectedException(typeof(PercentageException))]
        public void Percentage_ConstructorWithTooLowValue_Test()
        {
            Percentage percent = new Percentage(-0.1);
        }

        [TestMethod]
        [ExpectedException(typeof(PercentageException))]
        public void Percentage_ConstructorWithTooHighValue_Test()
        {
            Percentage percent = new Percentage(100.1);
        }
    }
}
