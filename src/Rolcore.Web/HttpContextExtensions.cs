using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Rolcore.Web.UI;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace Rolcore.Web
{
    /// <summary>
    /// Extension methods for <see cref="HttpContext"/>.
    /// </summary>
    public static class HttpContextExtensions
    {
        const string ClientLanguageCodeKey = "Rolcore.Web.HttpContextExtensionMethods.ClientLanguage";
        const string UnrewrittenUrlItemsKey = "Rolcore.Web.HttpContextExtensionMethods.UnrewrittenUrl";

        /// <summary>
        /// Code indicating preferred client language.
        /// </summary>
        public static string GetClientLanguageCode(this HttpContext httpContext)
        {
            Contract.Requires(httpContext != null, "httpContext is null.");
            Contract.Ensures(Contract.Result<string>() != string.Empty); // null is okay

            string result = (string)httpContext.Session[ClientLanguageCodeKey];
            if (string.IsNullOrEmpty(result))
            {
                if (httpContext.Request.UserLanguages.Length == 0)
                    return null;
                result = httpContext.Request.UserLanguages[0];
                if (!string.IsNullOrEmpty(result))
                {
                    httpContext.Session[ClientLanguageCodeKey] = result;
                    Debug.WriteLine("GetClientLanguageCode(): " + result);
                }
            }
            return result;
        }

        public static void SetClientLanguageCode(this HttpContext httpContext, string value)
        {
            httpContext.Session[ClientLanguageCodeKey] = value;
        }

        /// <summary>
        /// The base URL of the current website based on the current request. This can be 
        /// overridden in the Web.Config by adding a Site.BaseUrl configSetting.
        /// </summary>
        public static Uri GetSiteBaseUrl(this HttpContext httpContext)
        {
            Contract.Requires(httpContext != null, "httpContext is null.");
            Contract.Ensures(Contract.Result<Uri>() != null, "Invalid result.");

            const string cacheKey = "HttpContextExtensionMethods.GetSiteBaseUrl";
            Uri result = httpContext.Items[cacheKey] as Uri;
            if (result != null)
                return result;

            string appSetting = ConfigurationManager.AppSettings["Site.BaseUrl"];
            if (!string.IsNullOrEmpty(appSetting))
                result = appSetting.ToUri();
            else
                result = httpContext.Request.Url.GetBaseUri();

            httpContext.Items[cacheKey] = result;

            Debug.WriteLine("GetSiteBaseUrl() result: " + result);

            return result;
        }

        public static string GetUnrewrittenUrl(this HttpContext httpContext)
        {
            string result = (string)httpContext.Items[UnrewrittenUrlItemsKey];
            if (!string.IsNullOrEmpty(result))
                return result;
            result = httpContext.Request.ServerVariables["HTTP_X_ORIGINAL_URL"];
            if (!string.IsNullOrEmpty(result))
                return result;
            result = httpContext.Request.ServerVariables["UNENCODED_URL"];
            if (!string.IsNullOrEmpty(result))
                return result;

            //TODO: Request.RawUrl?

            result = httpContext.Request.Url.ToString();

            return result;
        }

        public static void SetUnrewrittenUrl(this HttpContext httpContext, string value)
        {
            httpContext.Items[UnrewrittenUrlItemsKey] = value;
        }

        public static void SetUnrewrittenUrl(this HttpContext httpContext, Uri value)
        {
            HttpContextExtensions.SetUnrewrittenUrl(httpContext, value.ToString());
        }
    }
}
