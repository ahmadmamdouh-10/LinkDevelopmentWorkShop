using Matgary.DAL;
using System.Collections.Generic;

namespace Matgary.BLL
{
    public class ProductOrderModel
    {
        public int Amount { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public int FavoriteCount { get; set; }
        public bool? InStock { get; set; }
        public List<string> Images { get; set; }
        public string Category { get; set; }
    }
    public class ProductModel
    {
        public long Id { get; set; }
        public Store Store { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public int Rate { get; set; }
        public bool? InStock { get; set; }
        public List<string> Images { get; set; }
        public string Category { get; set; }

    }
    public class ProductSampleModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool InStock { get; set; }
        public string ImageUrl { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public Store Store { get; set; }
        public int Amount { get; set; }
    }

    public class LatestProductSampleModel
    {
        public long Id { get; set; }
        public Store Store { get; set; }
        public string Title { get; set; }
        public bool InStock { get; set; }
        public string ImageUrl { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
