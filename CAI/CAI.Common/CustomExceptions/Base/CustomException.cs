namespace CAI.Common.CustomExceptions.Base
{
    using System;

    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
        }
    }
}
