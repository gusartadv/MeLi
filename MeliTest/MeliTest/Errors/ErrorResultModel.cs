namespace MeliTest.Errors
{
    public class ErrorResultModel
    {
        public string Message { get; set; }
        public string Code { get; set; }
    
        public ErrorResultModel(string message, string code)
        {
            Message = message;
            Code = code;
        }

    }
}
