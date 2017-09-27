namespace CAI.Common.CustomExceptions
{
    using System;

    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
        }
    }
}
