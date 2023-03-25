using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RLPLibrary;

namespace RLPLibraryTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.AreEqual("CLASS-1", Class1.getClassName());
    }
}