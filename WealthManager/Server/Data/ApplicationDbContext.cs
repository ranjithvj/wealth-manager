using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WealthManager.Server.Models;

namespace WealthManager.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>//IdentityDbContext<IdentityUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor)
           : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();


            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Unknown user";

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity<int>> entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedBy = user;
                        entry.Entity.LastUpdatedOnUtc = DateTime.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public DbSet<FixedDepositType> FixedDepositTypes { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<FixedDeposit> FixedDeposits { get; set; }
        public DbSet<RecurringDeposit> RecurringDeposits { get; set; }
        public DbSet<MutualFundDetail> MutualFundDetails { get; set; }
        public DbSet<MutualFund> MutualFunds { get; set; }
        public DbSet<MutualFundTxn> MutualFundTxns { get; set; }
    }
}
