using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tpa3.Loggers;
using System.Reflection;
using System.IO;

namespace UnitTestProject1
{
    [TestClass()]
    public class LoggerTests
    {
        private string Filename;
        Logger loger = new Logger();
        [TestMethod()]
        public void LogerTest()
        {
            loger.Info("Something");
            Filename ="Tpa3" + ".log";
           
            {
                try
                {
                    using (StreamReader Writer = new StreamReader(Filename))
                    {
                            Writer.ReadLine();
                            string d = Writer.ReadLine();
                        Assert.AreEqual(d, "Something");
                    }
                }
                catch
                {
                    throw;
                }
            }

        }
    }
}