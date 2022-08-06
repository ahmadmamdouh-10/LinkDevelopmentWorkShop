using Matgary.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Matgary.BLL
{
    public class Request : IValidatableObject
    {
        public long UserId { get; set; }
        public long StoreId { get; set; }
        public string DeviceToken { get; set; }
        public string Lang { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new AppContext())
            {
                var user = db.Users.FirstOrDefault(uc => uc.Id == UserId);
                if (user == null)
                    yield return new ValidationResult(
                        "User Not Exist",
                        new[] { "User Not Exist" }
                    );
            }
        }
    }
}
