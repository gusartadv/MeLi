namespace Service.Contracts.Responses
{
    /// <summary>
    /// The payment model response
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// The payment identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The loan identifier
        /// </summary>
        public int LoanId { get; set; }

        /// <summary>
        /// The amount of debt
        /// </summary>
        public double Debt { get; set; }
    }
}
