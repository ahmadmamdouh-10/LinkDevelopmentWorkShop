using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Matgary.DAL;

namespace Matgary.BLL
{
    public class RegistrationRequest : IValidatableObject
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string DeviceToken { get; set; }
        public Gender Gender { get; set; }
        public string GoogleAccessToken { get; set; }
        public string FacebookAccessToken { get;  set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new AppContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == Email);
                if (user != null)
                    yield return new ValidationResult(
                        "Email Exist",
                        new[] { ""}//Data.Resources.EmailExist }     // path of the property
                    );
            }
        }


    }
}
