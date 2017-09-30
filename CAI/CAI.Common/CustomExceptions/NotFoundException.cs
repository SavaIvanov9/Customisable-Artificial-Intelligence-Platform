namespace CAI.Common.CustomExceptions
{
    using CAI.Common.CustomExceptions.Base;

    public class NotFoundException : CustomException
    {
        public NotFoundException(string objectName) 
            : base($"{objectName} not found!")
        {
        }

        public NotFoundException(string objectName, string addidtionalInfo)
            : base($"{objectName} not found!! {addidtionalInfo}")
        {
        }
    }
}
