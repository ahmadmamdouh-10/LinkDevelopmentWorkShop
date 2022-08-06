using System.ComponentModel;
using System.Web.Mvc;

namespace Matgary.DAL
{
    public class About : BaseModel
    {
        public string Contact { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string PintrestLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string WhatsApp { get; set; }

    }
}
