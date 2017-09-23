using CarRentalDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CarRentalDemo.Web.DataContexts
{
    public class CarRentalDemoDb : DbContext
    {
        public CarRentalDemoDb() : base("DefaultConnection")
        {
            Database.SetInitializer<CarRentalDemoDb>(null); // Remove default initializer
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<CarRentalUser> CarRentalUsers { get; set; }
        public DbSet<DrivingLicen> DrivingLicens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // IMPORTANT: we are mapping the entity User to the same table as the entity ApplicationUser
            modelBuilder.Entity<CarRentalUser>().ToTable("AspNetUsers");
        }

        public DbQuery<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }
    }
}