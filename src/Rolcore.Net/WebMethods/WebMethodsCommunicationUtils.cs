using System;
using System.Configuration;
using Rolcore.Net;

namespace Rolcore.Net.WebMethods
{
    /// <summary>
    /// A static utility class to simplify communication with WebMethods.
    /// </summary>
    public sealed class WebMethodsCommunicationUtils
    {
        private WebMethodsCommunicationUtils() { }

        private static string GetHtmlServiceBaseUrl(WebMethodsHtmlService service)
        {
            string configKey = string.Concat(service.GetType(), 
                ".", service); // Example: Rolcore.Integration.Web.WebMethods.getBranchInfoByZipService

            string result = ConfigurationManager.AppSettings[configKey]; // Return configured URL to web service
            if(!string.IsNullOrEmpty(result))
                return result;

            switch (service) // Return (production!) URL to web service by default.
            {
                //case WebMethodsHtmlService.getBranchByZip:
                //    return "http://webmp21f:9777/invoke/Rol.System.RCCC.out.Zipcode/getBranchByZip?SERVICETYPE={0}&ZIPCODE={1}"; // "http://webmt23:8888/invoke/Rol.System.RCCC.out.Zipcode/getBranchByZip?SERVICETYPE={0}&ZIPCODE={1}";
                case WebMethodsHtmlService.getBranchInfoByZipService:
                    return "http://webmp21f:9777/invoke/Rol.System.RCCC.out.Zipcode.wrapper/getBranchInfoByZipService?serviceType={0}&zipCode={1}"; // "http://webmt23:8888/invoke/Rol.System.RCCC.out.Zipcode.wrapper/getBranchInfoByZipService?serviceType={0}&zipCode={1}";
                default:
                    throw new ArgumentException("Unknown value for service.", "service");
            }
        }

        private static string GetHtmlServiceUrl(WebMethodsHtmlService service, params string[] args)
        {
            return string.Format(GetHtmlServiceBaseUrl(service), args);
        }

        /// <summary>
        /// Calls a WebMethods HTML based webservice.
        /// </summary>
        /// <param name="service">The webservice to call.</param>
        /// <param name="args">Arguments to pass to the webservice.</param>
        /// <returns>A <see cref="WebMethodsHtmlResponseObject"/> representing the response from WebMethods.</returns>
        public static WebMethodsHtmlResponseObject CallHtmlService(WebMethodsHtmlService service, params string[] args)
        {
            Uri htmlServiceUrl = new Uri(GetHtmlServiceUrl(service, args));
            string htmlResponse = htmlServiceUrl.HttpGetText();
            WebMethodsHtmlResponseObject result = new WebMethodsHtmlResponseObject(htmlResponse);
            return result;
        }
    }
}
