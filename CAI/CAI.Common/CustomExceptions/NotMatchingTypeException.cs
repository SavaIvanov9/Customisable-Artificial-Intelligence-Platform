namespace CAI.Common.CustomExceptions
{
    using Base;

    public class NotMatchingTypeException : CustomException
    {
        public NotMatchingTypeException(string objectName) 
            : base($"{objectName} does not match!")
        {
        }

        public NotMatchingTypeException(string objectName, string addidtionalInfo)
            : base($"{objectName} does not mach! {addidtionalInfo}")
        {
        }
    }
}