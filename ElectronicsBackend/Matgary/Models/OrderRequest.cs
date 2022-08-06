using Matgary.BLL.Infra.User;
using Matgary.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Matgary.Controllers
{
    public class OrderRequest
    {
        [Required]
        public KeyValueModel[] OrderDetails { get; set; }

        //[Required]
        //public long CityId { get; set; }

        [Required]
        public long UserId { get; set; }
        public long StoreId { get; set; }
        public string PromoCode { get; set; }

        [Required]
        public string Contact { get; set; }

        public int? AddressId { get; set; }

        public CreateAddress Address { get; set; }
        //[Required]
        //public string DeliveryAddress { get; set; }

        public string Notes { get; set; }
    }

    public class KeyValueModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}