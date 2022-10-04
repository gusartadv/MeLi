using System.Text.Json.Serialization;

namespace Service.Contracts.Responses
{
    /// <summary>
    /// The loan list model
    /// </summary>
    public class LoanListResponse
    {
        /// <summary>
        /// The loan identifier
        /// </summary>
        [JsonPropertyName("id")]
        public int LoanId { get; set; }
        
        /// <summary>
        /// The total amount of the loan
        /// </summary>
        public double Amount { get; set; }
        
        /// <summary>
        /// The term of the loan
        /// </summary>
        public int Term { get; set; }
        
        /// <summary>
        /// The rate of the loan
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// The id identifier
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// The target of the user
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The date the loan was requested
        /// </summary>
        public DateTime Date { get; set; }

    }
}
