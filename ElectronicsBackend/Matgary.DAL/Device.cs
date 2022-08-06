using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class Device
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Required]
        public string Token { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(Store))]
        public long StoreId { get; set; }
        public Store Store { get; set; }
    }
}