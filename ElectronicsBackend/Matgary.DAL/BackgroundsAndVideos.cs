using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matgary.DAL
{
    public class BackgroundsAndVideos : BaseModel
    {
        [DisplayName("WelcomePageBackgroundImage")]
        public string WelcomePageBackgroundImageUrl { get; set; }

        [DisplayName("LogInPageBackgroundImage")]
        public string LogInPageBackgroundImageUrl { get; set; }

        [DisplayName("RegistrationPageBackgroundImage")]
        public string RegistrationPageBackgroundImageUrl { get; set; }

        [DisplayName("HomePageBackgroundImage")]
        public string HomePageBackgroundImageUrl { get; set; }

        [DisplayName("HomePageBackgroundVideo")]
        public string HomePageBackgroundVideoUrl { get; set; }
    }
}
