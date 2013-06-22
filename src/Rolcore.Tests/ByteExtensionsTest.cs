

namespace Rolcore.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore;

    /// <summary>
    /// Tests for <see cref="ByteExtensions"/>.
    /// </summary>
    [TestClass]
    public class ByteExtensionsTest
    {
        [TestMethod]
        public void ToHexString_ConvertsByteToHex()
        {
            const string expected = "0a";
            var actual = ((byte)10).ToHexString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToHexString_ConvertsByteArrayToHex()
        {
            const string expected = "000102030405060708090a0b0c0d0e0f101112204080";
            var target = new byte[] { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,32,64,128 };
            var actual = target.ToHexString();

            Assert.AreEqual(expected, actual);
        }
    }
}
