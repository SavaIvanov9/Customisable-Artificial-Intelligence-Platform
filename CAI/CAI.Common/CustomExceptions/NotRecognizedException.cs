namespace CAI.Common.CustomExceptions
{
    using Base;

    public class NotRecognizedException : CustomException
    {
        public NotRecognizedException(string objectName) 
            : base($"{objectName} not recognized!")
        {
        }

        public NotRecognizedException(string objectName, string addidtionalInfo)
            : base($"{objectName} not recognized!! {addidtionalInfo}")
        {
        }
    }
}
