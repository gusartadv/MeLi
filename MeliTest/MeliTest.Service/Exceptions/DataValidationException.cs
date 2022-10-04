using System.Collections;

namespace MeliTest.Service.Exceptions
{
    /// <summary>
    /// General exception for data errors
    /// </summary>
    public class DataValidationException : ApplicationException
    {
        private const string ERROR_CODE = "422";

        public override IDictionary Data { get; }

        public DataValidationException(string message) : base(message)
        {
            Data = new Dictionary<string, string>()
            {
                {"ErrorCode", ERROR_CODE },
                {"ErrorMessage", message },
            };
        }
    }
}
