using ItsRewardsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
namespace ItsRewardsApp.Server
{
    public partial class LoyaltyUserDBContext : DbContext
    {
        public LoyaltyUserDBContext()
        {
        }

        public LoyaltyUserDBContext(DbContextOptions<LoyaltyUserDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<LoyaltyUserProfile> LoyaltyUserProfile { get; set; }
        public virtual DbSet<tblstore_online> Tblstore_Online { get; set; } 
        public virtual DbSet<UserLoyaltyStoreMappings> UserLoyaltyStoreMappings { get; set; }
        public virtual DbSet<ImageBrand> ImageBrand { get; set; }
        public virtual DbSet<ImageBrandLink> ImageBrandLink { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //  modelBuilder.Entity<LoyaltyUserProfile>()
        //         .HasMany(loyal => loyal.Purchases)
        //         .WithOptional()
        //         .HasForeignKey(pi => pi.UserProfileId);

        //  modelBuilder.Entity<LoyaltyUserProfile>()
        //          .HasMany(loyal => loyal.Mappings)
        //          .WithOptional()
        //          .HasForeignKey(pi => pi.UserProfileID);

        //  base.OnModelCreating(modelBuilder);
        //}
    }
}
