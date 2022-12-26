using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SG.Models;

namespace SG.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<RelatedArea> RelatedAreas { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<ResourceCategory> ResourceCategorys { get; set; }

        public DbSet<Risk> Risks { get; set; }
        public DbSet<RiskType> RiskTypes { get; set; }
        public DbSet<ResidualRisk> ResidualRisks { get; set; }

        public DbSet<Control> Controls { get; set; }
    }
}