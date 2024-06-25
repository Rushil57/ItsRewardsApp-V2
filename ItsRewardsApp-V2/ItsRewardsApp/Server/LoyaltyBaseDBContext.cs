using ItsRewardsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ItsRewardsApp.Server
{
    public partial class LoyaltyBaseDBContext : DbContext
    {
        public LoyaltyBaseDBContext()
        {
        }
        public LoyaltyBaseDBContext(DbContextOptions<LoyaltyBaseDBContext> options)
           : base(options)
        {
        }

        public virtual DbSet<StoreTbl> Stores { get; set; }

        public virtual DbSet<TobaccoRebate> Rebates { get; set; }
    }
}
