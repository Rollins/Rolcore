using System;


namespace Rolcore.Net.Sockets
{
    public sealed class TcpIpUtils
    {
        private TcpIpUtils(){}

        /// <summary>
        /// Converts an IP address to a <see cref="double"/> representation.
        /// </summary>
        /// <param name="ipAddress">Specifies the IP address (in 0.0.0.0 format) to convert.</param>
        /// <returns>A <see cref="double"/> representation of the specified IP address.</returns>
        public static double IpStringToDouble(string ipAddress)
        {
            if (String.IsNullOrEmpty(ipAddress))
                throw new ArgumentException("ipAddress is null or empty.", "ipAddress");

            double result = 0;
            string[] ipAddressParts = ipAddress.Split('.');
            if (ipAddressParts.Length > 1)
                for (int i = ipAddressParts.Length - 1; i >= 0; i--)
                    result += ((int.Parse(ipAddressParts[i]) % 256) * System.Math.Pow(256, (3 - i)));

            return result;
        }
    }
}
