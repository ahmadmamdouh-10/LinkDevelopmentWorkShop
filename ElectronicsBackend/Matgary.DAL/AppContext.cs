using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Matgary.DAL
{
    public class AppContext : DbContext
    {
        public AppContext()
        {
            this.Configuration.LazyLoadingEnabled = true;

        }
        static AppContext()
        {
            Database.SetInitializer<AppContext>(new CreateDatabaseIfNotExists<AppContext>());

        }


        public DbSet<Store> Stores { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<UserStore> UserStores { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ProductOffer> ProductOffers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<ContactEmail> ContactEmails { get; set; }
        public DbSet<BackgroundsAndVideos> BackgroundsAndVideos { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }



        public GeneralSetting GetGeneralSettings(long storeId)
        {
            GeneralSetting generalSetting = GeneralSettings.FirstOrDefault(h => h.StoreId == storeId);

            return generalSetting;
        }

        public GeneralSetting SetGeneralSettings(GeneralSetting generalSetting)
        {
            Entry(generalSetting).State = EntityState.Modified;
            SaveChanges();

            return generalSetting;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {                
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync();
        }

        private void UpdateTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.DateTime = DateTime.Now;
                }

                //if (entity.GetType() == typeof(Order))
                //{
                //    if (entry.State == EntityState.Added)
                //    {
                //        entity.DateTime = DateTime.Now;
                //    }
                //}
                //else
                //{
                //    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                //    {
                //        entity.DateTime = DateTime.Now;
                //    }
                //}
            }
        }

    }
}
