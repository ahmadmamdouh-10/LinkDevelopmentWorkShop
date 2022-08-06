using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class Address : BaseModel
    {
        [MaxLength(500)]
        public string Street { get; set; }

        public bool IsDefault { get; set; } = false;

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
