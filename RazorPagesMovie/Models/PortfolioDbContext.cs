using Microsoft.EntityFrameworkCore;

namespace RazorPagesMovie.Models
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
            //this.EnsureSeedData();
        }
    }
}
