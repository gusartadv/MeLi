using System.ComponentModel.DataAnnotations;

namespace Service.Contracts.Requests
{
    /// <summary>
    /// The deb request model
    /// </summary>
    public class DebtRequest
    {
        /// <summary>
        /// The due date of the debt
        /// </summary>
        [Required]
        public DateTime DateTo { get; set; }

        /// <summary>
        /// The loan identifier
        /// </summary>
        public int LoanId { get; set; }

        /// <summary>
        /// The value of the target
        /// </summary>
        public string ?Target { get; set; }   
    }
}
