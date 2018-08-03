using System;

namespace Currency.API.Exceptions
{
    public class QuoteException : Exception
    {
        public QuoteException(int errorCode, string message): base(message) {
            this.ErrorCode = errorCode;
        }
        public int ErrorCode { get; set; }
    }
}
