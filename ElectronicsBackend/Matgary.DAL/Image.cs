using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Matgary.DAL
{
    public class Image : BaseModel
    {
        [Required]
        public string ImageUrl { get; set; }
        [JsonIgnore]
        public long  ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
