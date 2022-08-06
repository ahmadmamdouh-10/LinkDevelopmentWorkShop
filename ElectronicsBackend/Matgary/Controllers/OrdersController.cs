using Matgary.BLL;
using Matgary.BLL.Infra.Product;
using Matgary.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;

namespace Matgary.Controllers
{
    [RoutePrefix("api/order")]
    public class OrdersController : ApiController
    {
        private readonly DAL.AppContext _db = new DAL.AppContext();

        // GET: api/Orders
        [HttpGet, Route("")]
        public List<OrderResponse> GetOrders(long userId, long storeId)
        {
            var user = _db.Users.FirstOrDefault(s => s.Id == userId);

            var generalSetting = _db.GetGeneralSettings(storeId);

            var orders = _db.Orders
                .Include(o => o.OrderProducts)
                .Where(s => s.UserId == userId && s.StoreId == storeId);

            var data = orders
                .OrderByDescending(s => s.DateTime)
                .Select(o => new OrderResponse()
                {
                    Id = o.Id,
                    Contact = o.Contact,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    Total = o.Total,
                    TotalAfterDiscount = o.TotalAfterDiscount,
                    TotalDiscount = o.TotalDiscount,
                    CreatedDate = o.DateTime,
                    OrderDetails = o.OrderProducts.Select(od => new OrderDetailsResponse()
                    {
                        ProductId = od.ProductId,
                        Amount = od.Amount,
                        ProductName = od.Product.Title,
                    })
                }).ToList();

            return data;
        }
        [ResponseType(typeof(OrderProductResponse))]
        // GET: api/Orders/5
        [HttpGet, Route("order-products")]
        public IHttpActionResult GetOrderProducts(long orderId, long storeId, string lang = "ar")
        {
            var result = (from ordProd in _db.OrderProducts
                          where ordProd.OrderId == orderId
                          join prod in _db.Products on ordProd.ProductId equals prod.Id
                          select new
                          {
                              Product = prod,
                          }).ToList();

            var generalSetting = _db.GetGeneralSettings(storeId);

            var order = _db.Orders
                .FirstOrDefault(o => o.Id == orderId && o.StoreId == storeId);

            var res = new OrderProductResponse
            {
                Id = order.Id,
                Status = order.Status,
                CreatedDate = order.DateTime,
                Contact = order.Contact,
                TotalDiscount = order.TotalDiscount,
                TotalAfterDiscount = order.TotalAfterDiscount,
                Notes = order.Notes,
                Total = order.Total,
                DeliveryAddress = order.DeliveryAddress,
                Products = result.Select(obj => new ProductOrderModel
                {
                    //Category = lang == "ar" ? obj.Category.TitleA : obj.Category.TitleE,
                    Category = string.Join(" - ", obj.Product.ProductCategories.Select(c => c.Category.Title)),
                    Discount = obj.Product.Discount,
                    Title = obj.Product.Title,
                    Description = obj.Product.Description,
                    Images = obj.Product.Images.Select(i => ConfigurationManager.AppSettings["Image_Url"] + i.ImageUrl).ToList(),
                    Id = obj.Product.Id,
                    Amount = _db.OrderProducts.First(o => o.OrderId == orderId && o.ProductId == obj.Product.Id).Amount,
                    InStock = obj.Product.InStock,
                }).ToList()
            };
            return Ok(res);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        [HttpPost, Route("")]
        public IHttpActionResult PostOrder(OrderRequest request, string lang="en")
        {
            var generalSetting = _db.GetGeneralSettings(request.StoreId);

            //New Code
            var total = 0.0;
            var discount = 0.0;
            var orderProducts = new List<OrderProduct>();
            long orderId = 0;

            try
            {
                _db.Database.BeginTransaction();

                long cityId = 0;
                var address = new Address();
                if (request.AddressId != null && request.AddressId != 0)
                {
                    address = _db.Address
                        .FirstOrDefault(a => a.Id == request.AddressId);
                }
                else
                {
                    cityId = request.Address.CityId;

                    if (request.Address.IsDefault == true)
                    {
                        _db.Address
                            .Where(a => a.UserId == request.UserId && a.IsDefault == true)
                            .ToList().ForEach(a => a.IsDefault = false);
                    }

                    address = new Address()
                    {
                        Street = request.Address.Street,
                        UserId = request.UserId,
                        IsDefault = request.Address.IsDefault,
                        StoreId = request.StoreId
                    };
                    _db.Address.Add(address);
                }

                foreach (var keyValueModel in request.OrderDetails)
                {
                    var product = _db.Products
                        .FirstOrDefault(p => p.Id == keyValueModel.ProductId);

                    if (product != null)
                    {
                        //total += (product.Size) * (product.PackUnitPrice) * keyValueModel.Amount;
                        total += product.Price * keyValueModel.Amount;
                        if (product.Discount > 0)
                        {
                            //discount += (product.Size * product.PackUnitPrice) * keyValueModel.Amount * product.Discount / 100;
                            discount += (product.Price) * keyValueModel.Amount * product.Discount / 100;
                        }                
                    }
                }

                var order = new Order()
                {
                    Total = total,
                    UserId = request.UserId,
                    DateTime = DateTime.Now,
                    Status = Status.Submitted,
                    TotalDiscount = discount,
                    TotalAfterDiscount = total - discount,
                    Contact = request.Contact,
                    DeliveryAddress = address.Street, 
                    Notes = request.Notes,
                    StoreId = request.StoreId
                };

                _db.Orders.Add(order);

                foreach (var keyValueModel in request.OrderDetails)
                {
                    var orderProduct = new OrderProduct()
                    {
                        OrderId = order.Id,
                        DateTime = DateTime.Now,
                        ProductId = keyValueModel.ProductId,
                        Amount = keyValueModel.Amount,
                    };
                    orderProducts.Add(orderProduct);
                }

                if (orderProducts.Any())
                    _db.OrderProducts.AddRange(orderProducts);


                _db.SaveChanges();
                orderId = orderProducts.FirstOrDefault().OrderId;
                _db.Database.CurrentTransaction.Commit();
            }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
            {
                _db.Database.CurrentTransaction.Rollback();
            }

   
            var response = _db.Orders
                .Where(o => o.Id == orderId)
                .Select(o => new OrderResponse()
                {
                    Id = o.Id,
                    Contact = o.Contact,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    Status = o.Status.ToString(),
                    Total = o.Total,
                    CreatedDate = o.DateTime,
                    TotalAfterDiscount = o.TotalAfterDiscount,
                    TotalDiscount = o.TotalDiscount,
                    OrderDetails = o.OrderProducts.Select(od => new OrderDetailsResponse()
                    {
                        ProductId = od.ProductId,
                        Amount = od.Amount,
                        Price = (od.Product.Price),
                        ProductName = od.Product.Title,
                    })
                });
            return Ok(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(long id)
        {
            return _db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}