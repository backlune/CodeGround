using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGround.UnitTesting
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      public void TestMethod1()
      {
      }

      
      [DataTestMethod]
      [DataRow("a", "b")]
      [DataRow(" ", "a")]
      public void TestMathos1(string value1, string value2)
      {
         Assert.AreEqual(value1 + value2, string.Concat(value1, value2));
      }
   }
}
