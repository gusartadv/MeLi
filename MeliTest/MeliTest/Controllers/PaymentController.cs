using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Responses;
using Service.Contracts.Requests;
using Service.Contracts.Interfaces;
using MeliTest.Errors;

namespace MeliTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        /// <summary>
        /// Record a payment
        /// </summary>
        /// <param name="paymentRequest">Parameter that contains the payment information</param>
        /// <returns>LoanResponse</returns>
        /// <response code="200">
        /// </response>
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// <response code="404">
        /// <strong>HTTP 404 - Not found.</strong>
        /// </response>
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// <response code="500">
        /// <strong>Internal Server Error - Unhandled exception in the application.</strong>
        /// </response>
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        // POST: CreatePayment

        [ProducesResponseType(type: typeof(PaymentResponse), statusCode: 200)]
        [ProducesResponseType(type: typeof(ErrorResultModel), statusCode: 422)]

        [HttpPost]
        public async Task<PaymentResponse> CreatePayment(PaymentRequest paymentRequest)
        {
            var paymentResponse = new PaymentResponse();

            paymentResponse = await paymentService.RegisterPayment(paymentRequest);

            return paymentResponse;
        }
    }
}
