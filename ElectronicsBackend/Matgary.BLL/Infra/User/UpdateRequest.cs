using Matgary.BLL.Infra.User;
using Matgary.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Matgary.BLL
{
    public class UserUpdateRequest : IValidatableObject
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Code { get; set; }
        public AddressRegisterModel Address { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new AppContext())
            {
                var user = db.UserStores.FirstOrDefault(us => us.UserId == Id && us.StoreId == StoreId);
                if (user == null)
                    yield return new ValidationResult(
                        "User Not Exist",
                        new[] { "" }//Data.Resources.UserNotExist }
                    );
            }
        }
    }
}