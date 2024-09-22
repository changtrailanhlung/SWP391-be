using Bos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PawFundContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shelter> Shelters { get; set; }
        public virtual DbSet<ShelterStaff> ShelterStaffs { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        public PawFundContext(DbContextOptions<PawFundContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString(), options =>
                options.MigrationsAssembly("RepositoryLayer"));
        }

        private string? GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DefaultConnection"];
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdoptionRegistrationForm>()
    .Property(a => a.IncomeAmount)
    .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Donation>()
    .Property(a => a.Amount)
    .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<User>()
    .Property(a => a.TotalDonation)
    .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Shelter>()
    .Property(a => a.DonationAmount)
    .HasColumnType("decimal(18, 4)");
        }
    }
}
