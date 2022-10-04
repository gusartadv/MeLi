using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public double Amount { get; set; }
        public int Term { get; set; }
        public double installment { get; set; }
        public double Rate { get; set; }
        public string Target { get; set; }
        public DateTime Date { get; set; }
        public double Paid { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

    }
}
