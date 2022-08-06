using Matgary.DAL;

namespace Matgary.BLL
{
    public class UserResponse 
    {
        public User User { get; set; }
        //an indicator if this is first time for user to use the app or not
        public bool IsExist { get; set; } = false;
    }
}
