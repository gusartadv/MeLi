namespace Service.Contracts.Responses
{
    /// <summary>
    /// The loan response model
    /// </summary>
    public class LoanResponse
    {
        /// <summary>
        /// The loan identifier
        /// </summary>
        public int LoanId { get; set; }

        /// <summary>
        /// Monthly loan installment.
        /// </summary>
        public double Installment { get; set; }
    }
}
