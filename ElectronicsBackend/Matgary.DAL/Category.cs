using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Matgary.DAL
{
    public class Category : BaseModel
    {
        [Required, DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        public int Number { get; set; }

        public virtual List<ProductCategory> ProductCategories { get; set; }
        

    }
}
