using Microsoft.AspNetCore.Mvc;

namespace MeliTest.Errors
{
    public class ErrorActionResult : ObjectResult
    {
        public ErrorActionResult(ErrorResultModel value) : base(value)
        {

        }       

    }
}
