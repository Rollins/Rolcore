

namespace Rolcore.Tests.IO
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.IO;

    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void ToStream_ConvertsStringToStream()
        {
            const string expected = "Hello World! שלום עולם 你好世界";
            using (var stream = expected.ToStream())
            {
                var actual = stream.ReadToEndAsString();
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
