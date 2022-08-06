using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Matgary.DAL
{
    public class Offer : BaseModel
    {
        [Range(1, 100, ErrorMessage = "Min Discount Value is 1 And Max Discount Value is 100")]
        public double Discount { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Description")]
        [AllowHtml]
        public string Description { get; set; }


        [DisplayName("Offer Type")]
        public OfferType OfferType { get; set; }


        [DisplayName("Expiration")]
        public DateTime ExpirationDateTime { get; set; }


        [DisplayName("Duration In Seconds")]
        public double DurationSeconds { get; set; } = 0;

        [ForeignKey("ProductId"), JsonIgnore]
        public virtual Product Product { get; set; }
        public long? ProductId { get; set; }

        [ForeignKey("CategoryId"), JsonIgnore]
        public Category Category { get; set; }
        public long? CategoryId { get; set; }


        [DisplayName("Number Of Time Requested")]
        public int? RequestedCount { get; set; }


        public int Number { get; set; }
        
    }

    public enum OfferType
    {
        Product,
        Category
    }
}
