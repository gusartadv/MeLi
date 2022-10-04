using System.Collections;

namespace MeliTest.Service.Exceptions
{
    /// <summary>
    /// Exception for users not found
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        private const string ERROR_CODE = "404";

        public override IDictionary Data { get; }

        public NotFoundException(string message) : base(message)
        {
            Data = new Dictionary<string, string>()
            {
                {"ErrorCode", ERROR_CODE },
                {"ErrorMessage", message },
            };
        }
    }
}
