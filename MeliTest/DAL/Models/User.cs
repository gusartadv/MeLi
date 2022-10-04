using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("TargetId")]
        public int TargetId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Target Target { get; set; }
    }
}