using System.ComponentModel.DataAnnotations;

namespace Service.Contracts.Requests
{
    /// <summary>
    /// The parameters to get the list of loans
    /// </summary>
    public class LoanListRequest
    {
        /// <summary>
        /// The filter start date
        /// </summary>
        [Required]
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// The filter end date
        /// </summary>
        [Required]
        public DateTime DateTo { get; set; }
    }
}
