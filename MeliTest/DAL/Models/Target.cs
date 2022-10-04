using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Target
    {
        [Key]
        public int TargetId { get; set; }
        public string? Name { get; set; }
        public double Rate { get; set; }
        public int LowerQuantityLimit { get; set; }
        public int UpperQuantityLimit { get; set; }
        public double LowerAmountLimit { get; set; }
        public double UpperAmountLimit { get; set; }
        public double Max { get; set; }

    }
}
