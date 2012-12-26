//-----------------------------------------------------------------------
// <copyright file="FractionException.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Math
{
    using System;

    /// <summary>
    /// Exception class for Fraction, derived from System.Exception
    /// </summary>
    [Serializable]
    public class FractionException : Exception
    {
        public FractionException()
            : base()
        { }

        public FractionException(string Message)
            : base(Message)
        { }

        public FractionException(string Message, Exception InnerException)
            : base(Message, InnerException)
        { }
        protected FractionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
           
    }
}
