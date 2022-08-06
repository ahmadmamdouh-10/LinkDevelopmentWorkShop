using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Matgary.DAL;

namespace Matgary.BLL
{
  public class ForgetPasswordRequest : IValidatableObject
  {
    public string Email { get; set; }
      public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
      {
          using (var db = new AppContext())
          {
              var user = db.Users.FirstOrDefault(uc => uc.Email == Email);
              if (user == null)
                  yield return new ValidationResult(
                      "User Not Exist",
                      new[] { "" }
                  );
          }
      }
    }
}
