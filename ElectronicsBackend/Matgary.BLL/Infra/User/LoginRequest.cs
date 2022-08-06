using Matgary.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Matgary.BLL
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string DeviceToken { get; set; }

        public async Task<List<UserStore>> Validate()
        {
            using (var db = new AppContext())
            {
                var users = await db.UserStores
                    .Include(us => us.User)
                    .Include(us => us.Store)
                    .Where(
                    u => u.User.Email == Email && u.User.Password == Password)
                    .ToListAsync();
                return users;
            }
        }
    }
}
