using System.ComponentModel.DataAnnotations;

namespace Service.Contracts.Requests
{
    /// <summary>
    /// The payment model request
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// The total amount to pay
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// The loan identifier
        /// </summary>
        [Required]
        public int LoanId { get; set; }
    }
}
