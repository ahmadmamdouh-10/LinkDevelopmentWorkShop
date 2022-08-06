using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class OrderProduct
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }

        public int Amount { get; set; }

        public long OrderId { get; set; }

        public virtual Order Order { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
