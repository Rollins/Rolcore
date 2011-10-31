using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Rolcore.Web.UI
{
    public static class NameValueCollectionExtentionMethods
    {
        public static NameValueCollection SimplifyPostbackNames(this NameValueCollection collection)
        {   //TODO: Unit test
            NameValueCollection result = new NameValueCollection();
            foreach (string key in collection.Keys)
            {
                string newKey = WebUtils.GetFormControlNameFromPostbackName(key);
                string value = collection[key];
                result.Add(newKey, value);
            }
            return result;
        }
    }
}
