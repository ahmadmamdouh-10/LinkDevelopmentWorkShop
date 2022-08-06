using Matgary.DAL;
using System;
using System.Collections.Generic;

namespace Matgary.BLL.Infra.Product
{
    public class OrderProductResponse
    {

        //public Order Order { get; set; }
        public long Id { get; set; }

        public Status Status { get; set; }
        public double Total { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalAfterDiscount { get; set; }
        public string DeliveryAddress { get; set; }
        public string Contact { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<ProductOrderModel> Products { get; set; }
        public Store Store { get; set; }
        public string CityName { get; set; }

    }
}
