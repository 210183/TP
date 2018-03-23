using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logging
{
    class MockLogger : ILogger
    {
        public void Log(string message, LogLevel logLevel = LogLevel.Normal)
        {
            //do nothing
        }
    }
}
