﻿using System;
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
        public void Set_Test()
        {
            try
            {
                Set_BadValue_ShouldThrow_Test((decimal)1.1);
                Set_BadValue_ShouldThrow_Test((decimal)-0.1);
            }
            catch (PercentageException e) { }
        }

        //[TestMethod]
        [ExpectedException(typeof(PercentageException))]
        public void Set_BadValue_ShouldThrow_Test(decimal value)
        {
            Percentage percent = new Percentage(value);
        }
    }
}
