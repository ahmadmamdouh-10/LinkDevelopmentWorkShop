namespace Matgary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Contact = c.String(),
                        FacebookLink = c.String(),
                        InstagramLink = c.String(),
                        PintrestLink = c.String(),
                        TwitterLink = c.String(),
                        YoutubeLink = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        Location = c.String(),
                        Phone = c.String(),
                        WhatsApp = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Street = c.String(maxLength: 500),
                        IsDefault = c.Boolean(nullable: false),
                        UserId = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                        TotalDiscount = c.Double(nullable: false),
                        TotalAfterDiscount = c.Double(nullable: false),
                        DeliveryAddress = c.String(),
                        Contact = c.String(),
                        Notes = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        OrderId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Rate = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        ImageUrl = c.String(),
                        Quantity = c.Int(),
                        InStock = c.Boolean(nullable: false),
                        CategoryId = c.Long(),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Number = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        ProductId = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductOffers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        OfferId = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offers", t => t.OfferId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OfferId);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Discount = c.Double(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        OfferType = c.Int(nullable: false),
                        ExpirationDateTime = c.DateTime(nullable: false),
                        DurationSeconds = c.Double(nullable: false),
                        ProductId = c.Long(),
                        CategoryId = c.Long(),
                        RequestedCount = c.Int(),
                        Number = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.UserStores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        StoreId = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        FacebookId = c.String(),
                        GmailId = c.String(),
                        Phone1 = c.String(nullable: false),
                        Phone2 = c.String(),
                        GoogleAccessToken = c.String(),
                        FacebookAccessToken = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Currency = c.String(nullable: false),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsSuperAdmin = c.Boolean(),
                        StoreId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.BackgroundsAndVideos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WelcomePageBackgroundImageUrl = c.String(),
                        LogInPageBackgroundImageUrl = c.String(),
                        RegistrationPageBackgroundImageUrl = c.String(),
                        HomePageBackgroundImageUrl = c.String(),
                        HomePageBackgroundVideoUrl = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactEmails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        Body = c.String(),
                        Title = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Token = c.String(nullable: false),
                        UserId = c.Long(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.GeneralSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FreeDeliveryFeesOrderTotal = c.Double(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        StoreId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Admins", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserStores", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserStores", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductOffers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductOffers", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Offers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Offers", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Images", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProductCategories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropIndex("dbo.Devices", new[] { "StoreId" });
            DropIndex("dbo.Admins", new[] { "StoreId" });
            DropIndex("dbo.UserStores", new[] { "StoreId" });
            DropIndex("dbo.UserStores", new[] { "UserId" });
            DropIndex("dbo.Offers", new[] { "CategoryId" });
            DropIndex("dbo.Offers", new[] { "ProductId" });
            DropIndex("dbo.ProductOffers", new[] { "OfferId" });
            DropIndex("dbo.ProductOffers", new[] { "ProductId" });
            DropIndex("dbo.Images", new[] { "ProductId" });
            DropIndex("dbo.ProductCategories", new[] { "CategoryId" });
            DropIndex("dbo.ProductCategories", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.GeneralSettings");
            DropTable("dbo.Devices");
            DropTable("dbo.ContactEmails");
            DropTable("dbo.BackgroundsAndVideos");
            DropTable("dbo.Admins");
            DropTable("dbo.Stores");
            DropTable("dbo.UserStores");
            DropTable("dbo.Offers");
            DropTable("dbo.ProductOffers");
            DropTable("dbo.Images");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Orders");
            DropTable("dbo.Users");
            DropTable("dbo.Addresses");
            DropTable("dbo.Abouts");
        }
    }
}
