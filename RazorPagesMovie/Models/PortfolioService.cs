using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Models
{
    public class PortfolioService : IPortfolioService
    {
        private readonly PortfolioDbContext _context;

        public PortfolioService()
        {
            var options = new DbContextOptionsBuilder<PortfolioDbContext>()
                .UseInMemoryDatabase("PortfolioDatabase")
                .Options;

            _context = new PortfolioDbContext(options);
        }

        public PortfolioService(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Portfolios.Remove(new Portfolio { Id = id });
            await _context.SaveChangesAsync();
        }

        public Portfolio Find(int id)
        {
            return _context.Portfolios.FirstOrDefault(x => x.Id == id);
        }

        public Task<Portfolio> FindAsync(int id)
        {
            return _context.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Portfolio> GetAll(int? count = null, int? page = null)
        {
            var actualCount = count.GetValueOrDefault(10);

            return _context.Portfolios
                    .Skip(actualCount * page.GetValueOrDefault(0))
                    .Take(actualCount);
        }

        public Task<Portfolio[]> GetAllAsync(int? count = null, int? page = null)
        {
            return GetAll(count, page).ToArrayAsync();
        }

        public async Task SaveAsync(Portfolio recipe)
        {
            var isNew = recipe.Id == default(int);

            _context.Entry(recipe).State = isNew ? EntityState.Added : EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}

