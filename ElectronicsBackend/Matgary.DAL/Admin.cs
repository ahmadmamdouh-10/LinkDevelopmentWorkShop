

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class Admin
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Admin Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool? IsSuperAdmin { get; set; }

        [ForeignKey(nameof(Store))]
        public long? StoreId { get; set; }
        public Store Store { get; set; }
    }
}
