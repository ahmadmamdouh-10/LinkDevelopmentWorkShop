using Matgary.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Matgary.BLL
{
    public class ConfirmCodeRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new AppContext())
            {
                var user = db.Users.FirstOrDefault(uc => uc.Code == Code && uc.Email == Email);
                if (user == null)
                    yield return new ValidationResult(
                        "Code Expire",
                        new[] { "Code Expire" }
                    );
            }
        }
    }
}