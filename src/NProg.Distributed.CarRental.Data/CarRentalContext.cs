using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using NProg.Distributed.CarRental.Domain;
using NProg.Distributed.Core.Data;

namespace NProg.Distributed.CarRental.Data
{
    public class CarRentalContext : DbContext
    {
        static CarRentalContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<CarRentalContext>());
        }

        public CarRentalContext()
            : base("name=CarRental")
        {
        }

        public DbSet<Account> AccountSet { get; set; }

        public DbSet<Car> CarSet { get; set; }

        public DbSet<Rental> RentalSet { get; set; }

        public DbSet<Reservation> ReservationSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<IIdentifiableEntity<int>>();

            modelBuilder.Entity<Account>().HasKey<int>(e => e.AccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Car>().HasKey<int>(e => e.CarId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Rental>().HasKey<int>(e => e.RentalId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Reservation>().HasKey<int>(e => e.ReservationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Car>().Ignore(e => e.CurrentlyRented);
        }
    }
}