using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matgary.DAL
{
    public class Order : BaseModel
    {
        [Required]
        public long UserId { get; set; }
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public Status Status { get; set; }

        public double Total { get; set; }

        public double TotalDiscount { get; set; }

        public double TotalAfterDiscount { get; set; }

        public string DeliveryAddress { get; set; }

        public string Contact { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }


    public enum Status
    {
        Submitted = 0,
        Confirmed = 1,
        Delivered = 2,
        Cancelled = 3,
        UnderShipment = 4
    }


    public enum SubmitStatus
    {
        Confirmed = 1,
        Cancelled = 3,
    }

    public enum ConfirmStatus
    {
        UnderShipment = 4,
        Cancelled = 3,
    }
    public enum ReadyForShipmentStatus
    {
        Delivered = 2,
        Cancelled = 3,
    }
}
