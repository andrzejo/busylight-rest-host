using System;
using System.Runtime.Serialization;

namespace BusylightRestHost.Action
{
    public class ActionException : Exception
    {
        protected ActionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ActionException(string message) : base(message)
        {
        }

        public ActionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}