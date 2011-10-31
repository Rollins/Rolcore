using System;

namespace Rolcore.Net.WebMethods
{
    /// <summary>
    /// Exception for unexpected communications with WebMethods.
    /// </summary>
    [global::System.Serializable]
    public class WebMethodsCommunicationException : Exception
    {
        public WebMethodsCommunicationException() { }
        public WebMethodsCommunicationException(string message) : base(message) { }
        public WebMethodsCommunicationException(string message, Exception inner) : base(message, inner) { }
        protected WebMethodsCommunicationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
