using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class UserStore
    {
        public long Id { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Store))]
        public long StoreId { get; set; }
        public virtual Store Store { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string FacebookId { get; set; }
        public string GmailId { get; set; }
        [Required]
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public string GoogleAccessToken { get; set; }
        public string FacebookAccessToken { get; set; }
    }
}