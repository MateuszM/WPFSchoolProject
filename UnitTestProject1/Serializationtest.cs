using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tpa3.ViewModel;
using Tpa3.Models;
using System.Reflection;
using Tpa3.Logic;

namespace UnitTestProject1
{
    [TestClass]
    public class Serializationtest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Serialization j = new Serialization();
            TreeNode t = new TreeNode();
            t.TypeName = "Something";
            t.Value = "Cos";
            j.Serialize(Assembly.GetExecutingAssembly().GetName().Name + ".xml", t);
            TreeNode p = new TreeNode();
            p = j.Deserialization(Assembly.GetExecutingAssembly().GetName().Name + ".xml");
            Assert.AreEqual(t.Value, p.Value);


        }
    }
}
