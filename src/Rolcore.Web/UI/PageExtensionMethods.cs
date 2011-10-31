using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rolcore.Web.UI
{
    public static class PageExtensionMethods
    {
        public static string ReadFormSubmissionFieldValue(this Page page, string controlIdOrFieldName)
        {
            // Pre-conditions
            if (page == null) throw new ArgumentNullException("page");
            if (string.IsNullOrEmpty(controlIdOrFieldName)) throw new ArgumentException("controlIdOrFieldName cannot be null or empty", "controlIdOrFieldName");

            Control fieldControl = page.FindControlRecursive(controlIdOrFieldName);
            string result = null;
            if (fieldControl != null)
            {
                TextBox fieldControlTextBox = fieldControl as TextBox;
                if (fieldControlTextBox != null)
                    result = fieldControlTextBox.Text;
                else
                {
                    DropDownList fieldControlDropDownList = fieldControl as DropDownList;
                    if (fieldControlDropDownList != null)
                        result = fieldControlDropDownList.Text;
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                result = page.Request.Params[controlIdOrFieldName];
                if (string.IsNullOrEmpty(result))
                {
                    for (int i = 0; i < page.Request.Params.Count; i++)
                    {
                        if (page.Request.Params.Keys[i].EndsWith(controlIdOrFieldName))
                        {
                            result = page.Request.Params[i];
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
