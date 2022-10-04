using Service.Contracts.Requests;
using Service.Contracts.Responses;


namespace Service.Contracts.Interfaces
{
    /// <summary>
    /// Payment contracts
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Register the payment
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        Task<PaymentResponse> RegisterPayment(PaymentRequest paymentRequest);
    }
}
