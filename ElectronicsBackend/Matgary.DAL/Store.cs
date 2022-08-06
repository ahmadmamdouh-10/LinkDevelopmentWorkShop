using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class Store
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string Currency { get; set; }

        public string Logo { get; set; }

        //public bool IsDeleted { get; set; } = false;
    }
}
