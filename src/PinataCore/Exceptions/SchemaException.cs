using System;
using SocialMiner.Core.Exceptions;

namespace PinataCore.Exceptions
{
    public class SchemaException : BaseException
    {
        public SchemaException(string errorMessage) 
            : base(errorMessage)
        {

        }

        public SchemaException(string errorMessage, Exception innerException) 
            : base(errorMessage, innerException)
        {

        }
    }
}
