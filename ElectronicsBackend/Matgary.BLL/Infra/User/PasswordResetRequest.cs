﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Matgary.DAL;

namespace Matgary.BLL
{
    public class UserPasswordResetRequest : IValidatableObject
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string NewPassword { get; set; }

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
