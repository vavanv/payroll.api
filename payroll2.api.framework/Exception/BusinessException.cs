using System;
using System.Runtime.Serialization;

namespace Payroll2.Api.Framework.Exception
{
    [Serializable]
    public class BusinessException : System.Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}