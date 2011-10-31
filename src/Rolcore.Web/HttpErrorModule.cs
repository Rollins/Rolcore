using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.Configuration;

namespace Rolcore.Web
{
    public class HttpErrorModule : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
            Debug.WriteLine("HttpErrorModule.Dispose()");
        }

        public void Init(HttpApplication context)
        {
            context.Error += context_Error;
        }

        void context_Error(object sender, System.EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            HttpException exception = context.Error as HttpException;
            if (exception != null)
            {
                // Get error information
                int statusCode = exception.GetHttpCode();
                // Update the response with the correct status code
                context.Response.ClearHeaders();
                context.Response.ClearContent();
                context.Response.StatusCode = statusCode;

                // Read configuration
                CustomErrorsSection customErrorsSection = (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");
                CustomError customError = customErrorsSection.Errors[context.Response.StatusCode.ToString()];
                string errorPageUrl = (customError == null) ? null : customError.Redirect;
                if (string.IsNullOrEmpty(errorPageUrl))
                    errorPageUrl = customErrorsSection.DefaultRedirect;

                // Handle the request
                if (!string.IsNullOrEmpty(errorPageUrl))
                    context.Server.Transfer(errorPageUrl);
                else
                    context.Response.Flush();
            }
        }

        #endregion
    }
}
