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
        private readonly IConfiguration _configuration;
        private readonly string _connectionstring;
        public PawFundContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetConnectionString("default");
        }
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
    => optionsBuilder.UseSqlServer(_connectionstring);

        //private string? GetConnectionString()
        //{
        //    IConfiguration configuration = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json", true, true).Build();
        //    return configuration["ConnectionStrings:default"];
        //}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.HasOne(e => e.User)
            //          .WithMany()
            //          .HasForeignKey(e => e.UserId)
            //          .OnDelete(DeleteBehavior.Restrict);
            //});
        }
    }
}
