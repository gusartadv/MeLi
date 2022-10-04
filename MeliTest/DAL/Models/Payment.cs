using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("LoanId")]
        public int LoanId { get; set; }
        public double Amount { get; set; }
        public virtual Loan Loan { get; set; }

    }
}