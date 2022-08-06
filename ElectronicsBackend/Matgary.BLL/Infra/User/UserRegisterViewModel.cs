using Matgary.DAL;
using System.ComponentModel.DataAnnotations;

namespace Matgary.BLL.Infra.User
{
    public class UserRegisterViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public long StoreId { get; set; }
        public string DeviceToken { get; set; }
        public string GoogleAccessToken { get; set; }
        public string FacebookAccessToken { get; set; }
        public AddressRegisterModel Address { get; set; }
    }

    public class AddressRegisterModel
    {
        public string Street { get; set; }
    }
}
