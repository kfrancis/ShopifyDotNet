using System;
using System.Runtime.Serialization;

namespace ShopifyDotNet
{
    [Serializable]
    internal class ShopifyException : Exception
    {
        public ShopifyException()
        {
        }

        public ShopifyException(string message) : base(message)
        {
        }

        public ShopifyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShopifyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}