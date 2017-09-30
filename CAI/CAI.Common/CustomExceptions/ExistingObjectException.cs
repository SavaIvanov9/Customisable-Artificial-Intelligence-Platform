namespace CAI.Common.CustomExceptions
{
    using Base;

    public class ExistingObjectException : CustomException
    {
        public ExistingObjectException(string objectName) 
            : base($"{objectName} already exists!")
        {
        }

        public ExistingObjectException(string objectName, string addidtionalInfo)
            : base($"{objectName} already exists! {addidtionalInfo}")
        {
        }
    }
}
