using Matgary.BLL;
using Matgary.DAL;
using System;
using System.Collections.Generic;

namespace Matgary.Controllers
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public int Rate { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalAfterDiscount { get; set; }
        public string DeliveryAddress { get; set; }
        public string Contact { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<OrderDetailsResponse> OrderDetails { get; set; }
        public Store Store { get; set; }
    }


    public class OrderDetailsResponse
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }

    }
}