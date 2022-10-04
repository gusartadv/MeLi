using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Service.Contracts.Requests
{
    /// <summary>
    /// The parameters to make the loan request
    /// </summary>
    public class LoanRequest
    {
        /// <summary>
        /// The loan amount
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// The number of installments.
        /// </summary>
        [Required]
        public int Term { get; set; }

        /// <summary>
        /// The user identifier
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The installment of the loan
        /// </summary>
        [JsonIgnore]
        public double installment { get; set; }

        /// <summary>
        /// The rate of the loan
        /// </summary>
        [JsonIgnore]
        public double Rate { get; set; }

        /// <summary>
        /// The target of the loan
        /// </summary>
        [JsonIgnore]
        public string ?Target { get; set; }

        /// <summary>
        /// The date of the loan
        /// </summary>
        [JsonIgnore]
        public DateTime Date { get; set; }
    }
}
