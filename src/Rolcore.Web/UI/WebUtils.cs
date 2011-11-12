using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;

namespace Rolcore.Web.UI
{
    /// <summary>
    /// Web-related utilities.
    /// </summary>
    public static class WebUtils //TODO: Add unit tests for this class
    {
        /// <summary>
        /// Determines if a virtual path exists physically.
        /// </summary>
        /// <param name="virtualPath">A virtual path. For example "~/hello/world/foo.aspx".</param>
        /// <param name="request">The <see cref="HttpRequest"/> used to verify the virtual path's 
        /// existance.</param>
        /// <returns>True if the virtual path has a corresponding physical path that exists.</returns>
        public static bool VirtualPathExistsPhysically(string virtualPath, HttpRequest request)
        {
            string physicalPath = request.MapPath(virtualPath);
            return File.Exists(physicalPath);
        }

        /// <summary>
        /// Given a post-back name, returns the original name of the control.
        /// </summary>
        /// <param name="formPostbackName">The post-back name of a form field.</param>
        /// <returns>A string representing the original name of the form field.</returns>
        public static string GetFormControlNameFromPostbackName(string formPostbackName)
        {
            if (string.IsNullOrEmpty(formPostbackName))
                throw new ArgumentException("formPostbackName is null or empty.", "formPostbackName");

            if(!formPostbackName.Contains("$"))
                return formPostbackName;

            string[] splitKey = formPostbackName.Split('$');
            return splitKey[splitKey.Length - 1];
        }

        /// <summary>
        /// Get's a control's value based on the specified ID.
        /// </summary>
        /// <param name="formControlId">Specifies the control ID.</param>
        /// <param name="form">Specifies the submitted form values.</param>
        /// <returns>The value of the specified control.</returns>
        public static string GetFormControlValueByFormControlId(string formControlId, NameValueCollection form)
        {
            if (String.IsNullOrEmpty(formControlId))
                throw new ArgumentException("formControlId is null or empty.", "formControlId");
            if (form == null)
                throw new ArgumentNullException("form", "form is null.");

            for (int i = 0; i < form.Count; i++)
            {
                string formKey = form.Keys[i];
                if (!string.IsNullOrWhiteSpace(formKey))
                    if (WebUtils.GetFormControlNameFromPostbackName(formKey) == formControlId)
                        return form[i];
            }
            return null;
        }
    }
}
