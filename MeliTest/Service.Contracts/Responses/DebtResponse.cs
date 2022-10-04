using System.Text.Json.Serialization;

namespace Service.Contracts.Responses
{
    /// <summary>
    /// The debt response model
    /// </summary>
    public class DebtResponse
    {
        /// <summary>
        /// The total paid
        /// </summary>
        [JsonPropertyName("balance")]
        public double Paid { get; set; }

        /// <summary>
        /// The loan identifier
        /// </summary>
        public int LoanId { get; set; }
        
        /// <summary>
        /// The target of the user
        /// </summary>
        public string Target { get; set; }
    }
}
