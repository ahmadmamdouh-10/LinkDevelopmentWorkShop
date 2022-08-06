using System.ComponentModel.DataAnnotations;

namespace Matgary.BLL
{
    public class GmailFacebookLogin
    {
        public string GoogleAccessToken { get; set; }
        public string FacebookAccessToken { get;  set; }
        public string DeviceToken { get; set; }
    }
}
