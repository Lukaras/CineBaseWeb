using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DbTest()
        {
            bool expected = true;
            using (StreamReader reader = new StreamReader(@"..\..\..\..\CineBase\connectionString.txt"))
            {
                string connectionString = reader.ReadLine();
                bool result = CineBase.Database.SetDb(connectionString);
                Assert.AreEqual(expected, result);
            }
        }
    }
}