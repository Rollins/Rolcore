//-----------------------------------------------------------------------
// <copyright file="TcpIpUtilsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Net.Sockets;
    
    /// <summary>
    ///This is a test class for TcpIpUtilsTest and is intended
    ///to contain all TcpIpUtilsTest Unit Tests
    ///</summary>
    [TestClass]
    public class TcpIpUtilsTest
    {
        /// <summary>
        ///A test for IpStringToDouble
        ///</summary>
        [TestMethod]
        public void IpStringToDoubleTest()
        {
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("87.252.223.255"), 1476190207);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("24.72.128.0"), 407404544);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("190.5.240.255"), 3188060415);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("132.248.0.0"), 2230845440);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("200.1.125.0"), 3355540736);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("190.7.160.0"), 3188170752);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("190.87.255.255"), 3193438207);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("165.98.0.0"), 2774663168);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("58.147.128.0"), 982745088);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.209.0.0"), 1053884416);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.60.128.0"), 1044152320);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.201.192.0"), 1053409280);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("80.70.128.0"), 1346797568);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("77.241.64.0"), 1307656192);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.69.128.0"), 1044742144);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("91.192.64.0"), 1539325952);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.84.64.0"), 1045708800);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.231.192.0"), 1055375360);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("58.27.128.0"), 974880768);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("78.100.0.0"), 1315176448);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.149.64.0"), 1049968640);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("82.137.192.0"), 1384759296);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("79.170.184.0"), 1336588288);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("62.248.0.0"), 1056440320);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("217.174.224.1"), 3652116481);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("85.236.128.0"), 1441562624);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("80.80.208.0"), 1347473408);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("195.94.0.0"), 3277717504);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("58.146.192.0"), 982695936);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("0.0.0.0"), 0);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("127.0.0.1"), 2130706433);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("127.0.0.10"), 2130706442);
            Assert.AreEqual(TcpIpUtils.IpStringToDouble("127.0.0.100"), 2130706532);
        }
    }
}































