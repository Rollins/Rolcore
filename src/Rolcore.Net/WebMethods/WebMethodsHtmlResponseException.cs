using System;

namespace Rolcore.Net.WebMethods
{
    [global::System.Serializable]
    public class WebMethodsHtmlResponseException : Exception
    {
        private readonly string _ErrorDump = null;
        private readonly string _LocalizedError = null;
        private readonly string _ErrorType = null;
        private readonly string _User = null;
        private readonly string _Details = null;
        private readonly string _ErrorMessageId = null;
        private readonly DateTime _Time;

        protected WebMethodsHtmlResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public const string DetailsObjectName = "$details";
        public const string ErrorDumpObjectName = "$errorDump";
        public const string ErrorInfoObjectName = "$errorInfo";
        public const string ErrorMessageIdObjectName = "$errorMsgId";
        public const string ErrorTypeObjectName = "$errorType";
        public const string MessageObjectName = "$error";
        public const string TimeObjectName = "$time";
        public const string UserObjectName = "$user";

        public WebMethodsHtmlResponseException() { }

        //public WebMethodsHtmlResponseException(string message) : base(message) { }

        //public WebMethodsHtmlResponseException(string message, Exception inner) : base(message, inner) { }

        protected WebMethodsHtmlResponseException(string message, string errorDump, string localizedError, string errorType, string user, DateTime time, string details, string errorMessageId)
            : base(message)
        {
            this._ErrorDump = errorDump;
            this._LocalizedError = localizedError;
            this._ErrorType = errorType;
            this._User = user;
            this._Time = time;
            this._Details = details;
            this._ErrorMessageId = errorMessageId;
        }

        static internal WebMethodsHtmlResponseException Create(WebMethodsHtmlResponseObject response)
        {
            // Get to correct object in response hierarchy (some redundant data sent at top level)
            WebMethodsHtmlResponseObject exceptionData = response;
            if (exceptionData.Contains(ErrorInfoObjectName))
                exceptionData = exceptionData[ErrorInfoObjectName];


            return new WebMethodsHtmlResponseException(response[MessageObjectName].AsString,
                exceptionData[ErrorDumpObjectName].AsString,
                exceptionData[ErrorDumpObjectName].AsString,
                exceptionData[ErrorTypeObjectName].AsString,
                exceptionData[UserObjectName].AsString,
                exceptionData[TimeObjectName].AsDateTime,
                exceptionData[DetailsObjectName].AsString,
                exceptionData[ErrorMessageIdObjectName].AsString);
        }

        public string ErrorDump
        {
            get { return _ErrorDump; }
        }

        public string LocalizedError
        {
            get { return _LocalizedError; }
        }

        public string ErrorType
        {
            get { return _ErrorType; }
        }

        public string User
        {
            get { return _User; }
        }

        public DateTime Time
        {
            get { return _Time; }
        }

        public string Details
        {
            get { return _Details; }
        }

        public string ErrorMessageId
        {
            get { return _ErrorMessageId; }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("ErrorDump", ErrorDump);
            info.AddValue("LocalizedError", LocalizedError);
            info.AddValue("ErrorType", ErrorType);
            info.AddValue("User", User);
            info.AddValue("Time", Time);
            info.AddValue("Details", Details);
            info.AddValue("ErrorMessageId", ErrorMessageId);
        } //TODO: Unit test
    }
}
