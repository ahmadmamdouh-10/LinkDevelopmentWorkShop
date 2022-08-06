using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Matgary.DAL
{
    public class User
    {
        public User()
        {
            Orders = new Collection<Order>();
            UserStores = new List<UserStore>();

        }

        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Code { get; set; }

        public ICollection<Address> Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserStore> UserStores { get; set; }
    }
    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
