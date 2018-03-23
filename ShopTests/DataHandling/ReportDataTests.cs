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
        DateTime lastCombinedReportDate;

       [TestInitialize()]
        public void Initialize()
        {
            DateTime lastChangeTime = DateTime.Now;
            lastCombinedReportDate = DateTime.Now;
            lastCombinedReportDate = lastCombinedReportDate.AddMinutes(1);

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

            reportData.LastChangeTime = reportData.LastChangeTime.AddMinutes(2);
            Assert.IsTrue(reportData.IsReportOutdated());

            reportData.LastChangeTime = reportData.LastChangeTime.AddMinutes(-2);
            reportData.UpdateReportDate();
            Assert.IsFalse(reportData.IsReportOutdated());

        }
    }
}