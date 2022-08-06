using System.ComponentModel;

namespace MatgaryAdmin.Models
{
    public class ImageVideoUploadViewModel
    {
        [DisplayName("Image")]
        public string ImageUrl { get; set; } = "";
        public int Id { get; set; }
    }
}
