using System;

using System.Xml;

namespace Rolcore.Net.WebMethods
{
    /// <summary>
    /// Represents a structured WebMethods response.
    /// </summary>
    public class WebMethodsHtmlResponseObject
    {
        private readonly XmlDocument _Xml = new XmlDocument();

        protected static XmlNode FindObjectNameNode(string objectName, XmlNode start)
        {
            if (String.IsNullOrEmpty(objectName))
                throw new ArgumentException("objectName is null or empty.", "objectName");
            if (start == null)
                throw new ArgumentNullException("start", "start is null.");
            
            string objectNameToLower = objectName.ToLower();
            XmlNode result = null;
            foreach (XmlNode node in start.ChildNodes)
            {
                if (node.InnerXml.Trim().Equals(objectNameToLower, StringComparison.OrdinalIgnoreCase))
                    result = node.ParentNode;
                else
                    result = FindObjectNameNode(objectName, node);
                if (result != null)
                    break;
            }
            return result;
        }

        /// <summary>
        /// Indicates if the response represented by the instance is an exception.
        /// </summary>
        protected bool IsExceptionResponse
        {
            get
            {
                return this.Contains(WebMethodsHtmlResponseException.ErrorInfoObjectName);
            }
        }

        /// <summary>
        /// Construcor. Creates the psuedo object hierarchy based on a response from WebMethods.
        /// </summary>
        /// <param name="htmlResponse">The response from WebMethods to interpret.</param>
        /// <exception cref="WebMethodsHtmlResponseException"></exception>
        public WebMethodsHtmlResponseObject(string htmlResponse)
        {
            if (string.IsNullOrEmpty(htmlResponse))
                throw new ArgumentException("htmlResponse is null or empty.", "htmlResponse");

            string xml = HtmlUtils.QuoteAllHtmlTagAttributes(htmlResponse); // Webmethods does not produce valid HTML
            this._Xml.LoadXml(xml);
            if (this.IsExceptionResponse)
                throw WebMethodsHtmlResponseException.Create(this);
        }

        /// <summary>
        /// Interprets the insance as a <see cref="bool"/> value.
        /// </summary>
        public bool AsBool
        {
            get
            {
                string text = this.AsString;
                switch (text.ToLower())
                {
                    case "y":
                        return true;
                    case "n":
                        return false;
                    default:
                        throw new FormatException(string.Format("Current object is not a boolean value: {0}", text));
                }
            }
        }

        /// <summary>
        /// Interprets the insance as an <see cref="int"/> value.
        /// </summary>
        public int AsInt
        {
            get
            {
                string text = this.AsString;
                int result = 0;
                int.TryParse(text, out result);
                if ((result == 0) && (text != 0.ToString()))
                    throw new FormatException(string.Format("Current object is not an integer value: {0}", text));
                return result;
            }
        }

        /// <summary>
        /// Interprets the insance as a <see cref="string"/> value.
        /// </summary>
        public string AsString
        {
            get
            {
                return this._Xml.ChildNodes[0].InnerText.Trim();
            }
        }

        /// <summary>
        /// Interprets the insance as a <see cref="DateTime"/> value.
        /// </summary>
        public DateTime AsDateTime
        {
            get
            {
                string dateString = this.AsString;
                int endIndex = dateString.LastIndexOf(" ");
                dateString = dateString.Substring(0, endIndex);
                return DateTime.Parse(dateString);
            }
        }

        /// <summary>
        /// Accesses child <see cref="WebMethodsHtmlResponseObject"/> instances of the current instance.
        /// </summary>
        /// <param name="objectName">The child instance (using instance.SubObjectName syntax) to access.</param>
        /// <returns>A <see cref="WebMethodsHtmlResponseObject"/> representing the child instance.</returns>
        /// <exception cref="WebMethodsCommunicationException"></exception>
        public WebMethodsHtmlResponseObject this[string objectName]
        {
            get
            {
                if(string.IsNullOrEmpty(objectName))
                    throw new ArgumentException("objectName is null or empty", "objectName");

                XmlNode nameNode = _Xml.FirstChild;
                if (objectName.Contains(".")) // Parse dots
                {
                    string[] objectNames = objectName.Split('.');
                    for (int i = 0; i < objectNames.Length; i++)
                    {   // Traverse each node, then sub-node, for the one we're seeking
                        nameNode = FindObjectNameNode(objectNames[i], nameNode.ParentNode);
                        if (nameNode == null)
                            throw new WebMethodsCommunicationException(string.Format("Object path {0} not found. {1} does not exist.", objectName, objectName[i]));
                    }
                }
                else // Simple find
                    nameNode = FindObjectNameNode(objectName, nameNode);

                if (nameNode == null)
                    throw new WebMethodsCommunicationException(string.Format("{0} does not exist.", objectName));

                return new WebMethodsHtmlResponseObject(nameNode.NextSibling.OuterXml);
            }
        }

        /// <summary>
        /// Determines if the given object name exists within the hierarchy.
        /// </summary>
        /// <param name="objectName">The name (using instance.SubObjectName syntax) of the object to test for.</param>
        /// <returns>True if found.</returns>
        public bool Contains(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new ArgumentException("objectName is null or empty.", "objectName");

            return (FindObjectNameNode(objectName, this._Xml) != null);
        }
    }
}
