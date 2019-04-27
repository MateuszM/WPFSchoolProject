using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpa3.Models;
using System.Reflection;
using Tpa3.Logic;
using Tpa3.Loggers;

namespace ConsoleApp1.Tests
{
    [TestClass()]
    public class SecondViewModelChTests
    {
        [TestMethod()]
        public void SecondViewModelChTest()
        {

        }

        [TestMethod()]
        public void Lista2Test()
        {
            ViewLogic b = new ViewLogic();
            TreeNode l = new TreeNode();
            NamespaceMeta test;
            try
            {
                int id = 0;
              
               AssemblyMetadata AMetadata = new AssemblyMetadata(Assembly.LoadFile(@"E:\MyProjectsC#\Tpa3\UnitTestProject1\bin\Debug\GalaSoft.MvvmLight.Extras.dll"));
                foreach (NamespaceMeta d in AMetadata.m_Namespaces)
                {
                    b.ReadFromAmetaData(d,l, id, "Asembly");
                    id++;
                    using (var DataContext = new DataContext())
                    {
                        test = DataContext.Namespace.First();
                        Assert.AreEqual(test.MNamespaceName, d.MNamespaceName);
                    }
                }
                b.Clear();
                using (var DataContext = new DataContext())
                {

                    Assert.IsNull(DataContext.Namespace.First());
                }
                          // Save(" ");
                //Load(" ");
            }
            catch (Exception i)
            {
             
            }
        }


    }
    }
    
