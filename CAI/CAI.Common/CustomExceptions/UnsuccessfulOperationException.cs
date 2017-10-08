namespace CAI.Common.CustomExceptions
{
    using Base;

    public class UnsuccessfulOperationException : CustomException
    {
        public UnsuccessfulOperationException(string operationName) 
            : base($"{operationName} operation did not complete successfully!")
        {
        }

        public UnsuccessfulOperationException(string operationName, string addidtionalInfo)
            : base($"{operationName} operation did not complete successfully! {addidtionalInfo}")
        {
        }
    }
}