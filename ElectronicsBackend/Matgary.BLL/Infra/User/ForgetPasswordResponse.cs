using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Matgary.DAL;

namespace Matgary.BLL
{
    public class ForgetPasswordResponse
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public long UserId { get; set; }

    }
}
