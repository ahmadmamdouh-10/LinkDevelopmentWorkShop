using Matgary.BLL.Infra.User;
using Matgary.DAL;
using System;

namespace MatgaryAdmin.Models
{
    public class UserViewModel
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Phone2 { set; get; }
        public string Location { set; get; }
        public DateTime CreatedAt { set; get; }
        public Gender Gender { set; get; }

    }
}
