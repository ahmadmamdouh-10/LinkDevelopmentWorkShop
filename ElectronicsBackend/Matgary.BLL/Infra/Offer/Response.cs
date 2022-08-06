using System;

namespace Matgary.BLL
{
    public class GetResponse
    {
        //public long? CategoryId { get; set; }
        //public long? ProductId { get; set; }
        //public double PackPrice { get; set; }
        //public string PackUnitOfMeasure { get; set; }
        //public double PackSize { get; set; }
        public OfferInformation OfferInformation { set; get; }
        public ProductOffers ProductDetails { get; set; }
        //public OfferType OfferType { get; set; }
    }

    public class ProductOffers
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
    }

    public class OfferInformation
    {
        public string Store { get; set; }
        public string StoreCurrency { get; set; }
        public long Id { get; set; }
        public double PackPriceAfterDiscount { get; set; }
        public double Discount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public double DurationSeconds { get; set; }
    }

    public class CheckPromoViewModel
    {
        public int UserId { get; set; }
        public long StoreId { get; set; }
        public string PromoCode { get; set; }
    }
}
