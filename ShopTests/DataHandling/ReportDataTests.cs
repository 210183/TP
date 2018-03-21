using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Tests
{
    [TestClass()]
    public class ReportDataTests
    {
        ReportData reportData;

        [TestInitialize()]
        public void Initialize()
        {
            DateTime lastChangeTime = DateTime.Now;
            DateTime lastCombinedReportDate = DateTime.Now;

            reportData = new ReportData
            {
                LastChangeTime = lastChangeTime,
                LastCombinedReportDate = lastCombinedReportDate
            };

        }

        [TestMethod()]
        public void ReportDataTest()
        {
            Assert.IsFalse(reportData.IsReportOutdated());

            reportData.LastChangeTime = DateTime.Now;
            Assert.IsTrue(reportData.IsReportOutdated());

            reportData.UpdateReportDate();
            Assert.IsFalse(reportData.IsReportOutdated());

        }

    }
}