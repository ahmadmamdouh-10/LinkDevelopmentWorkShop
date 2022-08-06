namespace Matgary.DAL
{
    public class ContactEmail : BaseModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
