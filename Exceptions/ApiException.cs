using System;

namespace EmsalEWayBillSystem.Exceptions
{
    public class ApiException : Exception
    {
        public string ErrorCode { get; }
        public string ApiMessage { get; }
        
        public ApiException(string message) : base(message)
        {
        }
        
        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
        public ApiException(string message, string errorCode, string apiMessage) : base(message)
        {
            ErrorCode = errorCode;
            ApiMessage = apiMessage;
        }
    }
}