using Matgary.DAL;
using System.Collections.Generic;
using Matgary.BLL.Infra.Stores;

namespace Matgary.BLL.Infra.User
{
    public class UserResponseViewModel
    {
        public UserViewModel User { get; set; }
        public bool IsExist { get; set; }

        public List<StoreDto> Stores { get; set; }
        public long UserId { get; set; }
    }
}
