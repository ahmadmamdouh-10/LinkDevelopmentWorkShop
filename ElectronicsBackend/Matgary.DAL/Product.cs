using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Matgary.DAL
{
    public class Product : BaseModel
    {

        [Required]
        [DisplayName("Name")]
        public string Title { get; set; }

        [DisplayName("Description")]
        [AllowHtml]
        public string Description { get; set; }

        public int Rate { get; set; }


        [Display(Name = "Price")]
        public double Price { get; set; }

        public double Discount { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        [DisplayName("Quantity")]
        public int? Quantity { get; set; }

        [NotMapped]
        public bool? InCart { get; set; }

        [DisplayName("In Stock?")]
        public bool InStock { get; set; }

        public long? CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }

        public virtual List<Image> Images { get; set; }
        public virtual List<ProductCategory> ProductCategories { get; set; }

        public virtual List<ProductOffer> Offers { get; set; }

        [NotMapped]
        public List<long> CategoryIds { get; set; }

    }
}
