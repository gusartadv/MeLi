using MeliTest.Errors;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Service.Contracts.Requests;
using Service.Contracts.Responses;

namespace MeliTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        private readonly ILoanService loanService;

        public LoanController(ILoanService loanService)
        {
            this.loanService = loanService; 
        }

        /// <summary>
        /// Register a loan application
        /// </summary>
        /// <param name="loanRequest">Parameter that contains the loan information</param>
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
        // POST: LoanRequest

        [ProducesResponseType(type: typeof(LoanResponse), statusCode: 200)]
        [ProducesResponseType(type: typeof(ErrorResultModel), statusCode: 422)]

        [HttpPost]
        public async Task<LoanResponse> LoanRequest(LoanRequest loanRequest)
        {
            var loanResponse = new LoanResponse();

            loanResponse = await this.loanService.LoanRequest(loanRequest);

            return loanResponse;
        }

        /// <summary>
        /// Get the list of loans
        /// </summary>
        /// <param name="loanListRequest">Parameter that contains the loan information</param>
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
        // GET: GetLoanList

        [ProducesResponseType(type: typeof(List<LoanListResponse>), statusCode: 200)]
        [ProducesResponseType(type: typeof(ErrorResultModel), statusCode: 404)]
        [ProducesResponseType(type: typeof(ErrorResultModel), statusCode: 422)]

        [HttpGet]
        public async Task<List<LoanListResponse>> GetLoanList([FromQuery] LoanListRequest loanListRequest)
        {
            var listReponse = new List<LoanListResponse>();

            listReponse = await this.loanService.GetListOfLoans(loanListRequest.DateFrom, loanListRequest.DateTo);

            return listReponse;
        }
    }
}