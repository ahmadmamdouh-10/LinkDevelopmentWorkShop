using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Matgary.DAL
{
    public class ProductOffer : BaseModel
    {
        public long ProductId { get; set; }
        [JsonIgnore, ForeignKey("ProductId")]
        public Product Product { get; set; }

        public long OfferId { get; set; }
        [JsonIgnore, ForeignKey("OfferId")]
        public Offer Offer { get; set; }
    }
}
