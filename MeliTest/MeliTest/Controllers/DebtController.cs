using MeliTest.Errors;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Service.Contracts.Requests;
using Service.Contracts.Responses;

namespace MeliTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtController : Controller
    {
        private readonly IDebtService debtService;

        public DebtController(IDebtService debtService)
        {
            this.debtService = debtService;
        }

        /// <summary>
        /// Get the balance of the loans
        /// </summary>
        /// <param name="debtRequest">Parameter that contains the debt information</param>
        /// <returns>LoanResponse</returns>
        /// <response code="200">
        /// </response>
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// <response code="404">
        /// <strong>HTTP 404 - Not found.</strong>
        /// </response>
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// <response code="422">
        /// <strong>HTTP 422 - Unprocessable Entity.</strong>
        /// </response>
        /// -------------------------------------------------------------------------------------------
        /// <response code="500">
        /// <strong>Internal Server Error - Unhandled exception in the application.</strong>
        /// </response>
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        // GET: GetBalance

        [ProducesResponseType(type: typeof(DebtResponse), statusCode: 200)]
        [ProducesResponseType(type: typeof(ErrorResultModel), statusCode: 404)]
        [ProducesResponseType(type: typeof(ErrorResultModel), statusCode: 422)]

        [HttpGet]
        public async Task<List<DebtResponse>> GetBalance([FromQuery] DebtRequest debtRequest)
        {
            var debtResponse = new List<DebtResponse>();

            debtResponse = await debtService.GetBalance(debtRequest);

            return debtResponse;
        }

    }
}