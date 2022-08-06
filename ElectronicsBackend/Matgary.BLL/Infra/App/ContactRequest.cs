namespace Matgary.BLL
{
    public class ContactRequest
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public long  StoreId { get; set; }

    }
}
